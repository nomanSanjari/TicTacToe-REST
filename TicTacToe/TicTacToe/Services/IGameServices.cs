using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.Models;

namespace TicTacToe.Services
{
    public interface IGameServices
    {
        Task<Game> CreateGame();
        Task<List<Game>> GetAllGames();
    }
}
