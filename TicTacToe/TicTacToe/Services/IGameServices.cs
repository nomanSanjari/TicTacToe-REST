using System.Threading.Tasks;
using TicTacToe.Models;
using TicTacToe.DTO;

namespace TicTacToe.Services
{
    public interface IGameServices
    {
        Task<GameDTO> CreateGame();
    }
}
