namespace TicTacToe.Api.DTOs
{
    public class MoveRequest
    {
        public int CellIndex { get; set; }

        public string Player { get; set; } = "";
    }
}
