using MyMooviesApi.Repositories.Models;
using System.IdentityModel.Tokens.Jwt;

namespace MyMooviesApi.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Guid GetLoggedUserId()
        {
            var httpContext = _contextAccessor.HttpContext;

            if (httpContext == null)
            {
                throw new Exception("User must be logged in");
            }

            var loggedUserId = httpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.NameId).Value;

            return Guid.Parse(loggedUserId);
        }
    }
}
