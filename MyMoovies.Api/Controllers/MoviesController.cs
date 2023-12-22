using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyMooviesApi.Dtos;
using MyMooviesApi.Services;

namespace MyMooviesApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(
            IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        [Route("popular")]
        [ProducesResponseType(typeof(IEnumerable<MovieDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetPopularMovies()
        {
            var popularMovies = await _movieService.GetPopularMoviesAsync();
            return Ok(popularMovies);
        }

        [HttpPost]
        [Route("mark-watched")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MarkMovieWatched([FromBody] MarkMovieDto markMovie)
        {
            await _movieService.MarkMovieWatchedAsync(markMovie.IdMovie);
            return Ok();
        }

        [HttpPost]
        [Route("mark-unwatched")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> MarkMovieUnWatched([FromBody] MarkMovieDto markMovie)
        {
            await _movieService.MarkMovieUnWatchedAsync(markMovie.IdMovie);
            return Ok();
        }

        [HttpGet]
        [Route("list-watched")]
        [ProducesResponseType(typeof(IEnumerable<MovieDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListMovieWatched()
        {
            var moviesWatchedDto = await _movieService.ListMovieWatchedAsync();
            return Ok(moviesWatchedDto);
        }

        [HttpGet]
        [ProducesResponseType(typeof(MovieDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMovieById(int idMovie)
        {
            var movieDto = await _movieService.GetMovieByIdAsync(idMovie);

            if(movieDto == null)
            {
                return NotFound();
            }

            return Ok(movieDto);
        }
    }
}