using AutoMapper;
using MyMooviesApi.Dtos;
using MyMooviesApi.HttpClients.Models;
using MyMooviesApi.Repositories.Models;

namespace MyMooviesApi.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<MovieTMDB, MovieDto>()
                .ForMember(destination => destination.PosterUrl,
                            map => map.MapFrom(src => src.PosterPath))
                .ForMember(destination => destination.IsWatched,
                            map => map.Ignore());

            CreateMap<UserMovie, MovieDto>()
                .ForMember(destination => destination.Id, map => map.MapFrom(src => src.IdMovie));
        }
    }
}