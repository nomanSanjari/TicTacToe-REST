using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.Models;

namespace TicTacToe.Services
{
    public interface IGameServices
    {
        Task<string> CreateGame(JObject request);
        Task<string> UpdateGame(JObject request);
        Task<List<_EP3>> GetAllRunningGames();
    }
}
