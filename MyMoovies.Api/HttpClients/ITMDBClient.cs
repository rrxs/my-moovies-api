using MyMooviesApi.HttpClients.Models;

namespace MyMooviesApi.HttpClients
{
    public interface ITMDBClient
    {
        Task<IEnumerable<MovieTMDB>> GetPopularMoviesAsync();
        Task<MovieTMDB> GetMovieByIdAsync(int idMovie);
    }
}