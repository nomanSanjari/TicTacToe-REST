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

        // method for generating random Game ID
        private static int GenerateID()
        {
            return 0;
        }

        // ENDPOINT 1
        public async Task<Game> createGame()
        {
            Game game = new Game();
            game.ID = 10000001;
            
            return game;  
        }


}
}
