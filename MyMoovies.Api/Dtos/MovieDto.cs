namespace MyMooviesApi.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string PosterUrl { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public bool IsWatched { get; set; }
    }

    public record MarkMovieDto(int IdMovie);

}