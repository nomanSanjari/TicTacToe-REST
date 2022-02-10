using TicTacToe.Models;
using Microsoft.EntityFrameworkCore;

namespace TicTacToe.Data
{
    public class GameDBContext : DbContext
    {
        public GameDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
    }
}
