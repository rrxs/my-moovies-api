using AutoMapper;
using MyMooviesApi.Dtos;
using MyMooviesApi.Repositories.Models;

namespace MyMooviesApi.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}