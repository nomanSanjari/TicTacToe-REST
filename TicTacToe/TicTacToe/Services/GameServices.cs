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
            int index = (int)request["index"];

            Game game = new Game();
            game = _context.Games.Find(GameID);

            Player player = new Player();
            player = _context.Players.Find(Player_ID);

            // assign position data to instance game board -> State[]
            int[] State = new int[9];
            State[0] = game.pos0;
            State[1] = game.pos1;
            State[2] = game.pos2;   
            State[3] = game.pos3;
            State[4] = game.pos4;
            State[5] = game.pos5;
            State[6] = game.pos6;
            State[7] = game.pos7;
            State[8] = game.pos8;

            // sanitize inputs
            if (index > 8)
            {
                return "Bad Request";
            }

            if (State[index] != 0)
            {
                return "Bad Operation";
            }
            else
            {
                game.Moves++;
                State[index] = Player_ID;
            }

            // check board for wins
            if (game.Moves >= 3)
            {
                // horizontal win condition
                for (int i = 0; i < 9; i+=3)
                {
                    int indexA = State[i];
                    int indexB = State[i+1];
                    int indexC = State[i+2];

                    if (indexA == Player_ID && indexB == Player_ID && indexC == Player_ID)
                    {
                        winFlag = true;
                    }
                }
                // vertical win condition
                for (int i = 0; i < 3; i++)
                {
                    int indexA = State[i];
                    int indexB = State[i + 3];
                    int indexC = State[i + 6];

                    if (indexA == Player_ID && indexB == Player_ID && indexC == Player_ID)
                    {
                        winFlag = true;
                    }
                }
                // diagonal win condition
                if (State[0] == Player_ID && State[4] == Player_ID && State[8] == Player_ID)
                {
                    winFlag = true;
                }
                if (State[6] == Player_ID && State[4] == Player_ID && State[2] == Player_ID)
                {
                    winFlag = true;
                }
            }

            if (game.Moves == 9)
            {
                game.Moves = 9;
                return "Tie";
            }

            // update game position data with instance game board -> State[]
            game.pos0 = State[0];
            game.pos1 = State[1];
            game.pos2 = State[2];
            game.pos3 = State[3];
            game.pos4 = State[4];
            game.pos5 = State[5];
            game.pos6 = State[6];
            game.pos7 = State[7];
            game.pos8 = State[8];

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
    }
}
