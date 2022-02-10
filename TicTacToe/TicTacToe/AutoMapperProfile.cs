using AutoMapper;
using TicTacToe.DTO;
using TicTacToe.Models;

namespace TicTacToe
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Game, GameDTO>();
        }
    }
}
