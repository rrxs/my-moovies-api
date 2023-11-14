using System.ComponentModel.DataAnnotations;

namespace MyMooviesApi.Dtos
{
    public class SigninResponseDto
    {
        public string AccessToken { get; set; }
    }

    public record SigninDto([Required] string Email);

    public record SignupDto([Required] string Name, [Required] string Email, [Required] string Password);

    public class SignupResponseDto
    {
        public string AccessToken { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }


}