using System.ComponentModel.DataAnnotations;

namespace MyMooviesApi.Dtos
{
    public class LoginTokenDto
    {
        public string AccessToken { get; set; }
    }

    public record LoginDto([Required] string Email);

}