using MyMooviesApi.Dtos;

namespace MyMooviesApi.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDto>> GetPopularMoviesAsync();
    }
}
