using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.Models;
using TicTacToe.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

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
        public async Task<Game> CreateGame(JObject request)
        {
            const int Undefined = -1;
            const char SymbolUndefined = 'A';

            int ID = (int)request["id"];
            string PlayerAName = (string)request["playerA"]["name"];
            char PlayerASymbol = (char)request["playerA"]["symbol"];
            string PlayerBName = (string)request["playerB"]["name"];
            char PlayerBSymbol = (char)request["playerB"]["symbol"];

            // if block to check if ID provided else autogenerate ID
            if (ID != Undefined)
            {
                ID = 0;
            }

            // if blocks to supply name to Player objects if provided, else null
            if (PlayerAName == null)
            {
               PlayerAName = "Player A";
            }
            if (PlayerBName == null)
            {
                PlayerBName = "Player B";
            }

            // if blocks to supply symbol to Player objects if provided, else A -> X, B -> O
            if (PlayerASymbol == SymbolUndefined)
            {
                PlayerASymbol = 'X';
            }
            if (PlayerBSymbol == SymbolUndefined)
            {
                PlayerBSymbol = 'O';
            }

            // generate PlayerA object
            Player PlayerA = new Player()
            {
                Name = PlayerAName,
                Symbol = PlayerASymbol,
            };

            // generate PlayerB object
            Player PlayerB = new Player()
            {
                Name = PlayerBName,
                Symbol = PlayerBSymbol,
            };

            // generate Game object with data
            Game game = new Game()
            {
                ID = ID,
                PlayerA = PlayerA,
                PlayerB = PlayerB,
            };

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
