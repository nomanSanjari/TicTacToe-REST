namespace TicTacToe.Models
{
    public class Game
    {
        public int ID { get; set; }
        public Player PlayerA { get; set; }
        public Player PlayerB { get; set; }
        public char[,] State = new char[3, 3];
        public Player Winner { get; set; }
    }
}
