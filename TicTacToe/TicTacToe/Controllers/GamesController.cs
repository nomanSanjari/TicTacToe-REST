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

        // ENDPOINT 1
        [HttpPost("/create")]
        public async Task<ActionResult> CreateGame([FromBody] JObject request)
        {
            return Ok(await _gameServices.CreateGame(request));
        }

        // ENDPOINT 2
        [HttpPatch("/play")]
        public async Task<ActionResult> UpdateGame([FromBody] JObject request)
        {
            var returnString = await _gameServices.UpdateGame(request);

            if(String.Equals(returnString, "Bad Request"))
            {
                return BadRequest("Input not formatted properly. Rows and columns must be < 3");
            }
            else if(String.Equals(returnString, "Bad Operation"))
            {
                return Conflict("Space occupied!");
            }
            else if(String.Equals(returnString, "Registered"))
            {
                return Ok("Move registered");
            }
            else if(String.Equals(returnString, "Tie"))
            {
                return Ok("Tie");
            }
            
            return Ok(returnString);
            
        }

        // ENDPOINT 3
        [HttpGet("/findAll")]
        public async Task<ActionResult<List<_EP3>>> GetAllRunningGames()
        {
            return Ok(await _gameServices.GetAllRunningGames());
        }
    }
}
