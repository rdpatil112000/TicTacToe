using FluentAssertions;
using TicTacToe.Api.DTOs;
using TicTacToe.Api.Enums;
using TicTacToe.Api.Repositories;
using TicTacToe.Api.Services;
using Xunit;

namespace TicTacToe.Api.Tests;

public class GameServiceTests
{
    private readonly GameService _service;

    public GameServiceTests()
    {
        GameRepository.Games.Clear();

        GameRepository.Scoreboard.XWins = 0;
        GameRepository.Scoreboard.OWins = 0;
        GameRepository.Scoreboard.Draws = 0;

        _service = new GameService();
    }

    [Fact]
    public void CreateGame_ShouldInitializeCorrectly()
    {
        var game = _service.CreateGame(
            new CreateGameRequest
            {
                Mode = GameMode.TwoPlayer
            });

        game.Should().NotBeNull();
        game.CurrentPlayer.Should().Be("X");
        game.Board.Should().HaveCount(9);
        game.Status.Should().Be(GameStatus.InProgress);
    }

    [Fact]
    public void ValidMove_ShouldPlaceX()
    {
        var game = _service.CreateGame(
            new CreateGameRequest
            {
                Mode = GameMode.TwoPlayer
            });

        game = _service.MakeMove(
            game.Id,
            new MoveRequest
            {
                CellIndex = 0,
                Player = "X"
            });

        game.Board[0].Should().Be("X");
    }

    [Fact]
    public void Move_ShouldSwitchTurn()
    {
        var game = _service.CreateGame(
            new CreateGameRequest
            {
                Mode = GameMode.TwoPlayer
            });

        game = _service.MakeMove(
            game.Id,
            new MoveRequest
            {
                CellIndex = 0,
                Player = "X"
            });

        game.CurrentPlayer.Should().Be("O");
    }

    [Fact]
    public void MoveOnOccupiedCell_ShouldThrowException()
    {
        var game = _service.CreateGame(
            new CreateGameRequest
            {
                Mode = GameMode.TwoPlayer
            });

        _service.MakeMove(
            game.Id,
            new MoveRequest
            {
                CellIndex = 0,
                Player = "X"
            });

        Action action = () =>
            _service.MakeMove(
                game.Id,
                new MoveRequest
                {
                    CellIndex = 0,
                    Player = "O"
                });

        action.Should().Throw<Exception>();
    }

    [Fact]
    public void RowWin_ShouldDeclareWinner()
    {
        var game = _service.CreateGame(
            new CreateGameRequest
            {
                Mode = GameMode.TwoPlayer
            });

        _service.MakeMove(game.Id, new MoveRequest { CellIndex = 0, Player = "X" });
        _service.MakeMove(game.Id, new MoveRequest { CellIndex = 3, Player = "O" });
        _service.MakeMove(game.Id, new MoveRequest { CellIndex = 1, Player = "X" });
        _service.MakeMove(game.Id, new MoveRequest { CellIndex = 4, Player = "O" });

        game = _service.MakeMove(game.Id,
            new MoveRequest
            {
                CellIndex = 2,
                Player = "X"
            });

        game.Status.Should().Be(GameStatus.Won);
        game.Winner.Should().Be("X");
    }

    [Fact]
    public void ColumnWin_ShouldDeclareWinner()
    {
        var game = _service.CreateGame(
            new CreateGameRequest
            {
                Mode = GameMode.TwoPlayer
            });

        _service.MakeMove(game.Id, new MoveRequest { CellIndex = 0, Player = "X" });
        _service.MakeMove(game.Id, new MoveRequest { CellIndex = 1, Player = "O" });
        _service.MakeMove(game.Id, new MoveRequest { CellIndex = 3, Player = "X" });
        _service.MakeMove(game.Id, new MoveRequest { CellIndex = 2, Player = "O" });

        game = _service.MakeMove(game.Id,
            new MoveRequest
            {
                CellIndex = 6,
                Player = "X"
            });

        game.Status.Should().Be(GameStatus.Won);
        game.Winner.Should().Be("X");
    }

    [Fact]
    public void DiagonalWin_ShouldDeclareWinner()
    {
        var game = _service.CreateGame(
            new CreateGameRequest
            {
                Mode = GameMode.TwoPlayer
            });

        _service.MakeMove(game.Id, new MoveRequest { CellIndex = 0, Player = "X" });
        _service.MakeMove(game.Id, new MoveRequest { CellIndex = 1, Player = "O" });
        _service.MakeMove(game.Id, new MoveRequest { CellIndex = 4, Player = "X" });
        _service.MakeMove(game.Id, new MoveRequest { CellIndex = 2, Player = "O" });

        game = _service.MakeMove(game.Id,
            new MoveRequest
            {
                CellIndex = 8,
                Player = "X"
            });

        game.Status.Should().Be(GameStatus.Won);
        game.Winner.Should().Be("X");
    }

    [Fact]
    public void Draw_ShouldBeDetected()
    {
        var game = _service.CreateGame(
            new CreateGameRequest
            {
                Mode = GameMode.TwoPlayer
            });

        int[] moves =
        {
            0,1,2,
            4,3,5,
            7,6,8
        };

        string player = "X";

        foreach (var move in moves)
        {
            game = _service.MakeMove(
                game.Id,
                new MoveRequest
                {
                    CellIndex = move,
                    Player = player
                });

            player = player == "X"
                ? "O"
                : "X";
        }

        game.Status.Should().Be(GameStatus.Draw);
    }

    [Fact]
    public void Reset_ShouldClearBoard()
    {
        var game = _service.CreateGame(
            new CreateGameRequest
            {
                Mode = GameMode.TwoPlayer
            });

        _service.MakeMove(
            game.Id,
            new MoveRequest
            {
                CellIndex = 0,
                Player = "X"
            });

        game = _service.Reset(game.Id);

        game.Board.Should().OnlyContain(x => x == null);
        game.CurrentPlayer.Should().Be("X");
    }

    [Fact]
    public void Undo_ShouldRemoveLastMove()
    {
        var game = _service.CreateGame(
            new CreateGameRequest
            {
                Mode = GameMode.TwoPlayer
            });

        _service.MakeMove(game.Id,
            new MoveRequest
            {
                CellIndex = 0,
                Player = "X"
            });

        _service.MakeMove(game.Id,
            new MoveRequest
            {
                CellIndex = 1,
                Player = "O"
            });

        game = _service.Undo(game.Id);

        game.Board[1].Should().BeNull();
        game.CurrentPlayer.Should().Be("O");
    }

    [Fact]
    public void Winner_ShouldUpdateScoreboard()
    {
        var game = _service.CreateGame(
            new CreateGameRequest
            {
                Mode = GameMode.TwoPlayer
            });

        _service.MakeMove(game.Id, new MoveRequest { CellIndex = 0, Player = "X" });
        _service.MakeMove(game.Id, new MoveRequest { CellIndex = 3, Player = "O" });
        _service.MakeMove(game.Id, new MoveRequest { CellIndex = 1, Player = "X" });
        _service.MakeMove(game.Id, new MoveRequest { CellIndex = 4, Player = "O" });
        _service.MakeMove(game.Id, new MoveRequest { CellIndex = 2, Player = "X" });

        GameRepository.Scoreboard.XWins.Should().Be(1);
    }
}