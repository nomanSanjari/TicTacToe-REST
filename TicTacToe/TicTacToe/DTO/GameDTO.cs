using TicTacToe.Models;

namespace TicTacToe.DTO
{
    public class GameDTO
    {
        public int ID { get; set; }
        public Player PlayerA { get; set; }
        public Player PlayerB { get; set; }
        public char[,] State = new char[3, 3];
        public int Moves { get; set; }
        public Player Winner { get; set; }
    }
}
