using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicTacToe.Models
{
    public class Game
    {
        [Key]
        public int ID { get; set; } = -1;
        public int PlayerA_ID { get; set; }
        public int PlayerB_ID { get; set; }
        public char[,] State = new char[3, 3];
        public int Moves { get; set; } = 0;
        public bool Won { get; set; } = false;

        public Game()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    this.State[i, j] = 'A';
                }
            }
        }
    }
}
