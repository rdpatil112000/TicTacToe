using TicTacToe.Api.DTOs;
using TicTacToe.Api.Enums;
using TicTacToe.Api.Models;
using TicTacToe.Api.Repositories;

namespace TicTacToe.Api.Services;

public class GameService : IGameService
{
    private readonly int[][] WinningCombinations =
    {
        new [] {0,1,2},
        new [] {3,4,5},
        new [] {6,7,8},

        new [] {0,3,6},
        new [] {1,4,7},
        new [] {2,5,8},

        new [] {0,4,8},
        new [] {2,4,6}
    };

    public Game CreateGame(CreateGameRequest request)
    {
        var game = new Game
        {
            Id = Guid.NewGuid(),
            Mode = request.Mode,
            CurrentPlayer = "X",
            Status = GameStatus.InProgress
        };

        GameRepository.Games[game.Id] = game;

        return game;
    }

    public Game GetGame(Guid id)
    {
        if (!GameRepository.Games.ContainsKey(id))
            throw new Exception("Game not found");

        return GameRepository.Games[id];
    }

    public Game MakeMove(Guid id, MoveRequest request)
    {
        var game = GetGame(id);

        ValidateMove(game, request);

        game.Board[request.CellIndex] = request.Player;

        game.MoveHistory.Add(new Move
        {
            MoveNumber = game.MoveHistory.Count + 1,
            Player = request.Player,
            Row = request.CellIndex / 3,
            Column = request.CellIndex % 3
        });

        if (CheckWinner(game))
        {
            UpdateScoreboard(game.Winner!);
            return game;
        }

        if (CheckDraw(game))
        {
            game.Status = GameStatus.Draw;

            GameRepository.Scoreboard.Draws++;

            return game;
        }

        game.CurrentPlayer =
            game.CurrentPlayer == "X"
            ? "O"
            : "X";

        if (game.Mode == GameMode.Computer
            && game.CurrentPlayer == "O"
            && game.Status == GameStatus.InProgress)
        {
            ExecuteComputerMove(game);
        }

        return game;
    }

    public Game Undo(Guid id)
    {
        var game = GetGame(id);

        if (game.MoveHistory.Count == 0)
            return game;

        if (game.Status != GameStatus.InProgress)
            return game;

        int removeCount =
            game.Mode == GameMode.Computer
            ? 2
            : 1;

        for (int i = 0;
             i < removeCount &&
             game.MoveHistory.Count > 0;
             i++)
        {
            game.MoveHistory.RemoveAt(
                game.MoveHistory.Count - 1);
        }

        RebuildBoard(game);

        return game;
    }

    public Game Reset(Guid id)
    {
        var game = GetGame(id);

        game.Board = new string[9];

        game.MoveHistory.Clear();

        game.CurrentPlayer = "X";

        game.Status = GameStatus.InProgress;

        game.Winner = null;

        game.WinningCells.Clear();

        return game;
    }

    public Scoreboard GetScoreboard()
    {
        return GameRepository.Scoreboard;
    }

    public void ResetScoreboard()
    {
        GameRepository.Scoreboard = new Scoreboard();
    }

    private void ValidateMove(
        Game game,
        MoveRequest request)
    {
        if (game.Status != GameStatus.InProgress)
            throw new Exception("Game completed");

        if (request.CellIndex < 0 ||
            request.CellIndex > 8)
            throw new Exception("Invalid cell");

        if (game.Board[request.CellIndex] != null)
            throw new Exception("Cell occupied");

        if (request.Player != game.CurrentPlayer)
            throw new Exception("Wrong player turn");
    }
    private bool CheckWinner(Game game)
    {
        foreach (var combo in WinningCombinations)
        {
            int a = combo[0];
            int b = combo[1];
            int c = combo[2];

            if (string.IsNullOrEmpty(game.Board[a]))
                continue;

            if (game.Board[a] == game.Board[b]
                && game.Board[b] == game.Board[c])
            {
                game.Status = GameStatus.Won;

                game.Winner = game.Board[a];

                game.WinningCells =
                    combo.ToList();

                return true;
            }
        }

        return false;
    }

    private bool CheckDraw(Game game)
    {
        return game.Board.All(x => x != null);
    }

    private void UpdateScoreboard(string winner)
    {
        if (winner == "X")
        {
            GameRepository.Scoreboard.XWins++;
        }
        else
        {
            GameRepository.Scoreboard.OWins++;
        }
    }

    private void RebuildBoard(Game game)
    {
        game.Board = new string[9];

        foreach (var move in game.MoveHistory)
        {
            int index =
                move.Row * 3 + move.Column;

            game.Board[index] = move.Player;
        }

        game.Status = GameStatus.InProgress;

        game.Winner = null;

        game.WinningCells.Clear();

        game.CurrentPlayer =
            game.MoveHistory.Count % 2 == 0
            ? "X"
            : "O";
    }

    private void ExecuteComputerMove(Game game)
    {
        int move = GetComputerMove(game);

        game.Board[move] = "O";

        game.MoveHistory.Add(new Move
        {
            MoveNumber = game.MoveHistory.Count + 1,
            Player = "O",
            Row = move / 3,
            Column = move % 3
        });

        if (CheckWinner(game))
        {
            UpdateScoreboard("O");
            return;
        }

        if (CheckDraw(game))
        {
            game.Status = GameStatus.Draw;

            GameRepository.Scoreboard.Draws++;

            return;
        }

        game.CurrentPlayer = "X";
    }

    private int GetComputerMove(Game game)
    {
        int winMove = FindWinningMove(game, "O");

        if (winMove != -1)
            return winMove;

        int blockMove = FindWinningMove(game, "X");

        if (blockMove != -1)
            return blockMove;

        if (game.Board[4] == null)
            return 4;

        int[] corners = { 0, 2, 6, 8 };

        foreach (var corner in corners)
        {
            if (game.Board[corner] == null)
                return corner;
        }

        for (int i = 0; i < 9; i++)
        {
            if (game.Board[i] == null)
                return i;
        }

        return -1;
    }

    private int FindWinningMove(
        Game game,
        string player)
    {
        for (int i = 0; i < 9; i++)
        {
            if (game.Board[i] != null)
                continue;

            game.Board[i] = player;

            bool wins = WinningCombinations.Any(c =>
                game.Board[c[0]] == player &&
                game.Board[c[1]] == player &&
                game.Board[c[2]] == player);

            game.Board[i] = null;

            if (wins)
                return i;
        }

        return -1;
    }
}