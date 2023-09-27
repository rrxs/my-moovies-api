using MyMooviesApi.HttpClients.Models;

namespace MyMooviesApi.HttpClients
{
    public interface ITMDBClient
    {
        Task<IEnumerable<MovieTMDB>> GetPopularMovies();
        Task<MovieTMDB> GetMovieById(int idMovie);
    }
}