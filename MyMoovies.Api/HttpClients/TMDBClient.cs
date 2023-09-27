using Microsoft.Net.Http.Headers;
using MyMooviesApi.HttpClients.Models;
using MyMooviesApi.Settings;

namespace MyMooviesApi.HttpClients
{
    public class TMDBClient : ITMDBClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public TMDBClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            var tmdbConfig = _configuration.GetSection(nameof(TMDBConfig)).Get<TMDBConfig>();

            _httpClient.BaseAddress = new Uri(tmdbConfig.Host);
            _httpClient.DefaultRequestHeaders.Add(
                HeaderNames.Authorization, $"Bearer {tmdbConfig.ApiToken}");
            _httpClient.DefaultRequestHeaders.Add(
                HeaderNames.Accept, "application/json");
        }

        public async Task<IEnumerable<MovieTMDB>> GetPopularMovies()
        {
            PagedResult<MovieTMDB> result = new();
            var response = await _httpClient.GetAsync("/3/movie/popular?language=en-US&page=1");
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<PagedResult<MovieTMDB>>();
            }

            return result.Results;
        }

        public async Task<MovieTMDB> GetMovieById(int idMovie)
        {
            MovieTMDB result = null;
            var response = await _httpClient.GetAsync($"/3/movie/{idMovie}&language=en-US"); 
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<MovieTMDB>();
            }
            

            return result;
        }
    }
}