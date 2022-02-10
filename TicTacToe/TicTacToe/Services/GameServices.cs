using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.Models;

namespace TicTacToe.Services
{
    public class GameServices : IGameServices
    {
        // volatile local storage for testing purposes
        private static List<Game> activeGames = new List<Game>();

        // ENDPOINT 1
        public async Task<List<Game>> CreateGame()
        {
            Game game = new Game();
            game.ID = 1;
            activeGames.Add(game);
            return activeGames;  
        }
    }
}
