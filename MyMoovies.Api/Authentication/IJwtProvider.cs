using MyMooviesApi.Repositories.Models;

namespace MyMooviesApi.Authentication
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}
