﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("/create")]
        public async Task<ActionResult<Game>> StartGame()
        {
            return Ok(await _gameServices.CreateGame());
        }

        [HttpGet("/findAll")]
        public async Task<ActionResult<List<Game>>> GetAllGames()
        {
            return Ok(await _gameServices.GetAllGames());
        }
    }
}
