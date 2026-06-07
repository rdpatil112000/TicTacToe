using TicTacToe.Api.Enums;

namespace TicTacToe.Api.DTOs
{
    public class CreateGameRequest
    {
        public GameMode Mode { get; set; }
    }
}
