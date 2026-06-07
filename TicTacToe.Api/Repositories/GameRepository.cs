using TicTacToe.Api.Models;

namespace TicTacToe.Api.Repositories
{
    public static class GameRepository
    {
        public static Dictionary<Guid, Game> Games
            = new();

        public static Scoreboard Scoreboard
            = new();
    }
}
