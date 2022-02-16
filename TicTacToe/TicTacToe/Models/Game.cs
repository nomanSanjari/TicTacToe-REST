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
        public int pos0 { get; set; }
        public int pos1 { get; set; }
        public int pos2 { get; set; }
        public int pos3 { get; set; }
        public int pos4 { get; set; }
        public int pos5 { get; set; }
        public int pos6 { get; set; }
        public int pos7 { get; set; }
        public int pos8 { get; set; }
        public int Moves { get; set; } = 0;
        public bool Won { get; set; } = false;
    }
    
}
