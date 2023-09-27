namespace MyMooviesApi.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string PosterUrl { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }

    public record MarkMovieWatchedDto(int IdMovie);
    public record MarkMovieUnWatchedDto(int IdMovie);
}