
namespace MyMooviesApi.Repositories.Models
{
    public class UserMovie : IEntity
    {
        public Guid Id{ get; set; }
        public Guid IdUser { get; set; }
        public int IdMovie { get; set; }
        public string Title { get; set; }
        public string PosterUrl { get; set; }
    }
}
