using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Models
{
    public class Player
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; } = null;
        public char Symbol { get; set; } = 'A';
    }
}
