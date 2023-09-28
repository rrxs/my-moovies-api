using AutoMapper;
using MyMooviesApi.Dtos;
using MyMooviesApi.HttpClients;
using MyMooviesApi.Repositories.Models;
using MyMooviesApi.Repositories;

namespace MyMooviesApi.Services
{
    public class MovieService : IMovieService
    {
        private readonly ITMDBClient _tMBDClient;
        private readonly IMapper _mapper;
        private readonly IRepository<UserMovie> _userMovieRepository;
        private readonly IUserService _userService;

        public MovieService(
            ITMDBClient tMBDClient,
            IMapper mapper,
            IRepository<UserMovie> userMovieRepository,
            IUserService userService)
        {
            _tMBDClient = tMBDClient;
            _mapper = mapper;
            _userMovieRepository = userMovieRepository;
            _userService = userService;
        }

        public async Task<IEnumerable<MovieDto>> GetPopularMoviesAsync()
        {
            var loggedUserId = _userService.GetLoggedUserId();
            var popularMovies = await _tMBDClient.GetPopularMoviesAsync();
            var popularMoviesDto = _mapper.Map<IEnumerable<MovieDto>>(popularMovies);
            
            var userLoggedWatchedMovies = await _userMovieRepository.GetAllAsync(item => item.IdUser == loggedUserId);
            var popularMoviesParsed = new List<MovieDto>();

            foreach (var movie in popularMoviesDto) // todo: extract method/extension
            {
                popularMoviesParsed.Add(new MovieDto
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    IsWatched = userLoggedWatchedMovies.FirstOrDefault(u => u.IdMovie == movie.Id) != null,
                    PosterUrl = movie.PosterUrl 
                });
            }

            return popularMoviesParsed;
        }
    }
}
