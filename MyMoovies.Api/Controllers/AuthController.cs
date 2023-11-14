using Microsoft.AspNetCore.Mvc;
using MyMooviesApi.Authentication;
using MyMooviesApi.Dtos;
using MyMooviesApi.Repositories;
using MyMooviesApi.Repositories.Models;

namespace MyMooviesApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;
        private readonly IJwtProvider _jwtProvider;

        public AuthController(
            IRepository<User> userRepository,
            IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
        }

        [HttpPost]
        [Route("signin")]
        [ProducesResponseType(typeof(SigninResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Signin(SigninDto loginDto)
        {
            var user = await _userRepository.GetAsync(item => item.Email == loginDto.Email);

            if (user == null)
            {
                return NotFound();
            }

            var tokenDto = new SigninResponseDto
            {
                AccessToken = _jwtProvider.GenerateToken(user)
            };

            return Ok(tokenDto);
        }

        [HttpPost]
        [Route("signup")]
        [ProducesResponseType(typeof(SignupResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Signup(SignupDto signupDto)
        {
            var userRegistred = await _userRepository.GetAsync(item => item.Email == signupDto.Email);

            if (userRegistred != null)
            {
                return BadRequest("Email unavailable");
            }

            var user = new User { Email = signupDto.Email, Name = signupDto.Name };
            await _userRepository.CreateAsync(user);

            var signupResponseDto = new SignupResponseDto
            {
                AccessToken = _jwtProvider.GenerateToken(user),
                Name = signupDto.Name,
                Email = signupDto.Email,
            };

            return Ok(signupResponseDto);
        }
    }
}
