using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Models
{
    public class Game
    {
        [Key]
        public int ID { get; set; } = -1;
        public Player PlayerA { get; set; }
        public Player PlayerB { get; set; }
        public char[,] State = new char[3, 3];
        public int Moves { get; set; } = 0;
        public Player Winner { get; set; }
    }
}
