﻿using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.Models;

namespace TicTacToe.Services
{
    public interface IGameServices
    {
        Task<string> CreateGame(JObject request);
        Task<List<Game>> GetAllGames();
    }
}
