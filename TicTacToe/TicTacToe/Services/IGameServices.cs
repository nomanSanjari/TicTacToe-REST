using System.Threading.Tasks;
using TicTacToe.Models;

namespace TicTacToe.Services
{
    public interface IGameServices
    {
        Task<Game> createGame();
    }
}
