using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.Models;
using TicTacToe.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text.Json;

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
        public async Task<string> CreateGame(JObject request)
        {
            // hope you're ready for some spaghetti code :') 

            const int Undefined = -1;
            const char SymbolUndefined = 'A';

            // i honestly did not have an option for some reason deconstructing the object and assing to object was creating issues

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

            // generate PlayerA object -> reason for this being like this stated above
            Player PlayerA = new Player()
            {
                ID = ID,
                Name = PlayerAName,
                Symbol = PlayerASymbol
            };
            _context.Players.Add(PlayerA);

            // generate PlayerB object -> reason for this being like this stated above
            Player PlayerB = new Player()
            {
                ID = ID,
                Name = PlayerBName,
                Symbol = PlayerBSymbol
            };
            _context.Players.Add(PlayerB);

            // generate Game object with data
            Game game = new Game()
            {
                ID = ID,
                PlayerA = PlayerA,
                PlayerB = PlayerB,
            };
            _context.Games.Add(game);

            _context.SaveChanges();


            _EP1 returnObject = new _EP1
            {
                GameID = game.ID,
                PlayerA_ID = PlayerA.ID,
                PlayerB_ID = PlayerB.ID,
            };

            string jsonReturn = JsonSerializer.Serialize(returnObject);

            return jsonReturn;
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
