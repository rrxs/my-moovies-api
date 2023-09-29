using MyMooviesApi.Dtos;

namespace MyMooviesApi.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDto>> GetPopularMoviesAsync();
        Task MarkMovieWatchedAsync(int idMovie);
        Task MarkMovieUnWatchedAsync(int idMovie);
        Task<MovieDto> GetMovieByIdAsync(int idMovie);
        Task<IEnumerable<MovieDto>> ListMovieWatchedAsync();
    }
}
