using TicTacToe.Api.DTOs;
using TicTacToe.Api.Models;

namespace TicTacToe.Api.Services
{
    public interface IGameService
    {
        Game CreateGame(CreateGameRequest request);

        Game GetGame(Guid id);

        Game MakeMove(Guid id, MoveRequest request);

        Game Undo(Guid id);

        Game Reset(Guid id);

        Scoreboard GetScoreboard();

        void ResetScoreboard();
    }
}
