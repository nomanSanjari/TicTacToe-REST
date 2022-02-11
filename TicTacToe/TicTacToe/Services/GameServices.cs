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
                PlayerA_ID = PlayerA.ID,
                PlayerB_ID = PlayerB.ID,
                Moves = 0
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

        // ENDPOINT 2
        public async Task<string> UpdateGame(JObject request)
        {
            bool winFlag = false;

            int GameID = (int)request["gameID"];
            int Player_ID= (int)request["playerID"];
            int row = (int)request["row"];
            int col = (int)request["col"];

            Game game = _context.Games.Find(GameID);
            Player player = _context.Players.Find(Player_ID);

            // sanitize row and col inputs
            if (row > 2 || col > 2)
            {
                return "Bad Request";
            }



            if ((game.State[row, col] == 'X' || game.State[row, col] == 'O') == false)
            {
                return "Bad Operation";
            }
            else
            {
                game.Moves++;
                game.State[row, col] = player.Symbol;
            }

            // check board for wins
            if (game.Moves >= 3)
            {
                // horizontal win condition
                for (int i = 0; i < 3; i++)
                {
                    if (game.State[i, 0] == player.Symbol && game.State[i, 1] == player.Symbol && game.State[i, 2] == player.Symbol)
                    {
                        winFlag = true;
                    }
                }
                // vertical win condition
                for (int i = 0; i < 3; i++)
                {
                    if (game.State[0, i] == player.Symbol && game.State[1, i] == player.Symbol && game.State[2, i] == player.Symbol)
                    {
                        winFlag = true;
                    }
                }
                // diagonal win condition
                if (game.State[0, 0] == player.Symbol && game.State[1, 1] == player.Symbol && game.State[2, 2] == player.Symbol)
                {
                    winFlag = true;
                }
                if (game.State[2, 0] == player.Symbol && game.State[1, 1] == player.Symbol && game.State[0, 2] == player.Symbol)
                {
                    winFlag = true;
                }
            }

            _context.Games.Update(game);

            // boolean to see if a player won
            if (winFlag)
            {
                _EP2 winner = new _EP2()
                {
                    PlayerID = Player_ID,
                    Name = player.Name,
                    Symbol = player.Symbol,
                    Won = true
                };
                game.Won = true;

                _context.Games.Update(game);
                _context.SaveChanges();
                string jsonReturn = JsonSerializer.Serialize(winner);
                return jsonReturn;
            }

            _context.SaveChanges();

            return "Registered";
        }

        // ENDPOINT 3
        public async Task<List<_EP3>> GetAllRunningGames()
        {
            List<Game> allGames = new List<Game>();
            List<_EP3> runningGames = new List<_EP3>(); 

            var games = _context.Games;
            foreach (var game in games)
            {
                if (game.Won == false)
                {
                    allGames.Add(game);
                }
            }

            foreach (var runningGame in allGames)
            {
                var PlayerA_ID = runningGame.PlayerA_ID;
                var PlayerB_ID = runningGame.PlayerB_ID;

                string PlayerA_Name = _context.Players.Find(PlayerA_ID).Name;
                string PlayerB_Name = _context.Players.Find(PlayerB_ID).Name;

                _EP3 temp = new _EP3()
                {
                    GameID = runningGame.ID,
                    PlayerA = PlayerA_Name,
                    PlayerB = PlayerB_Name,
                    Moves = runningGame.Moves
                };
                runningGames.Add(temp);
            }

            return runningGames;
        }

        public async Task<Game> diag()
        {
            Game game = _context.Games.Find(1);
            return game;
        }
    }
}
