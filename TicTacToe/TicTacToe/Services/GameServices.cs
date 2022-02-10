using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.Models;
using TicTacToe.DTO;
using AutoMapper;

namespace TicTacToe.Services
{
    public class GameServices : IGameServices
    {
        // volatile local storage for testing purposes
        private static List<GameDTO> activeGames = new List<GameDTO>();

        private readonly IMapper _mapper;

        public GameServices(IMapper mapper)
        {
            _mapper = mapper;
        }

        // ENDPOINT 1
        public async Task<GameDTO> CreateGame()
        {
            Game game = new Game();
            game.ID = 10000001;

            activeGames.Add(_mapper.Map<GameDTO>(game));
            
            return _mapper.Map<GameDTO>(game);  
        }
    }
}
