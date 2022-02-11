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
        [NotMapped]
        public int[] State { get; set; } = new int[9];
        public int Moves { get; set; } = 0;
        public bool Won { get; set; } = false;
    }
    
}
