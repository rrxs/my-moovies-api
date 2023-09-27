using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyMooviesApi.Dtos;
using MyMooviesApi.HttpClients;
using MyMooviesApi.Repositories;
using MyMooviesApi.Repositories.Models;
using System.IdentityModel.Tokens.Jwt;

namespace MyMooviesApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly ITMDBClient _tMBDClient;
        private readonly IMapper _mapper;
        private readonly IRepository<UserMovie> _userMovieRepository;

        public MoviesController(
            IRepository<UserMovie> userMovieRepository,
            ITMDBClient tMBDClient,
            IMapper mapper)
        {
            _userMovieRepository = userMovieRepository;
            _tMBDClient = tMBDClient;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("popular")]
        [ProducesResponseType(typeof(IEnumerable<MovieDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetPopularMovies()
        {
            var popularMovies = await _tMBDClient.GetPopularMovies();
            var moviesDtos = _mapper.Map<IEnumerable<MovieDto>>(popularMovies);
            return Ok(moviesDtos);
        }

        [HttpPost]
        [Route("mark-watched")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MarkMovieWatched(MarkMovieWatchedDto markMovieWatchedDto)
        {
            var loggedUserId = Guid.Parse(User.Claims.First(c => c.Type == JwtRegisteredClaimNames.NameId).Value);
            var movie = await _tMBDClient.GetMovieById(markMovieWatchedDto.IdMovie);

            if (movie == null)
            {
                return BadRequest();
            }

            var userMovie = new UserMovie
            {
                IdUser = loggedUserId,
                IdMovie = markMovieWatchedDto.IdMovie,
                Title = movie.Title,
                PosterUrl = movie.PosterPath
            };

            await _userMovieRepository.CreateAsync(userMovie);

            return Ok();
        }

        [HttpPost]
        [Route("mark-unwatched")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> MarkMovieUnWatched(MarkMovieUnWatchedDto markMovieUnWatchedDto)
        {
            var loggedUserId = Guid.Parse(User.Claims.First(c => c.Type == JwtRegisteredClaimNames.NameId).Value);
            var markedMovie = await _userMovieRepository
                .GetAsync(item => item.IdMovie == markMovieUnWatchedDto.IdMovie && item.IdUser == loggedUserId);
           
            if(markedMovie == null)
            {
                return NotFound();
            }

            await _userMovieRepository.RemoveAsync(markedMovie.Id);

            return Ok();
        }

        [HttpGet]
        [Route("list-watched")]
        [ProducesResponseType(typeof(IEnumerable<MovieDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListMovieWatched()
        {
            var loggedUserId = Guid.Parse(User.Claims.First(c => c.Type == JwtRegisteredClaimNames.NameId).Value);
            var moviesWatched = await _userMovieRepository.GetAllAsync(item => item.IdUser == loggedUserId);
            var moviesWatchedDto = _mapper.Map<IEnumerable<MovieDto>>(moviesWatched);
            return Ok(moviesWatchedDto);
        }

        [HttpGet]
        [ProducesResponseType(typeof(MovieDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMovieById(int idMovie)
        {
            var movie = await _tMBDClient.GetMovieById(idMovie);

            if(movie == null)
            {
                return NotFound();
            }

            var movidDto = _mapper.Map<MovieDto>(movie);
            return Ok(movidDto);
        }
    }
}