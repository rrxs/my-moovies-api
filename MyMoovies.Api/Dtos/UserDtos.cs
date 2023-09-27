namespace MyMooviesApi.Dtos
{
    public record CreateUserDto(string Email);

    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
    }
}