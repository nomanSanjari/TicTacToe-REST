using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using TicTacToe.Models;
using TicTacToe.Services;

namespace TicTacToe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameServices _gameServices;

        public GamesController(IGameServices gameServices)
        {
            _gameServices = gameServices;
        }

        [HttpPost("/create")]
        public async Task<ActionResult<List<int>>> StartGame([FromBody] JObject request)
        {
            return Ok(await _gameServices.CreateGame(request));
        }

        [HttpGet("/findAll")]
        public async Task<ActionResult<List<Game>>> GetAllGames()
        {
            return Ok(await _gameServices.GetAllGames());
        }
    }
}
