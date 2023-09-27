using System.Text.Json.Serialization;

namespace MyMooviesApi.HttpClients.Models
{
    public class MovieTMDB
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; } = string.Empty;
    }
}