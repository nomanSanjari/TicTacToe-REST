using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.Models;
using TicTacToe.Data;
using Microsoft.AspNetCore.Mvc;

namespace TicTacToe.Services
{
    public class GameServices : IGameServices
    {
        private readonly GameDBContext _context;

        public GameServices(GameDBContext context)
        {
            _context = context;
        }

        // ENDPOINT 1
        public async Task<Game> CreateGame([FromBody]int ID, Player PlayerA, Player PlayerB)
        {
            Game game = new Game();
            _context.Add<Game>(game);
            _context.SaveChanges();
            return game;
        }

        public async Task<List<Game>> GetAllGames()
        {
            List<Game> allGames = new List<Game>();

            var games = _context.Games;
            foreach (var game in games)
            {
                allGames.Add(game);
            }
            return allGames;
        }
    }
}
