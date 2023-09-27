namespace MyMooviesApi.Repositories.Models
{
    public class User : IEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
