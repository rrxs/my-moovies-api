using Microsoft.AspNetCore.Mvc;
using MyMooviesApi.Authentication;
using MyMooviesApi.Dtos;
using MyMooviesApi.Repositories;
using MyMooviesApi.Repositories.Models;

namespace MyMooviesApi.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;
        private readonly IJwtProvider _jwtProvider;

        public LoginController(
            IRepository<User> userRepository,
            IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
        }

        [HttpPost]
        [ProducesResponseType(typeof(LoginTokenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userRepository.GetAsync(item => item.Email == loginDto.Email);

            if (user == null)
            {
                return NotFound();
            }

            var tokenDto = new LoginTokenDto
            {
               AccessToken = _jwtProvider.GenerateToken(user)
            };

            return Ok(tokenDto);
        }
    }
}
