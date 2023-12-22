using MyMooviesApi.Repositories.Models;
using MyMooviesApi.Repositories;
using MyMooviesApi.Dtos;
using MyMooviesApi.Authentication;

namespace MyMooviesApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IJwtProvider _jwtProvider;

        public AuthService(
            IJwtProvider jwtProvider,
            IRepository<User> userRepository
            )
        {
            _jwtProvider = jwtProvider;
            _userRepository = userRepository;
        }

        public async Task<SignupResponseDto> Signup(SignupDto signupDto)
        {
            var userRegistred = await _userRepository.GetAsync(item => item.Email == signupDto.Email);

            if (userRegistred != null)
            {
                throw new Exception("email unavailable");
            }

            var user = new User { Email = signupDto.Email, Name = signupDto.Name };
            await _userRepository.CreateAsync(user);

            var signupResponseDto = new SignupResponseDto
            {
                AccessToken = _jwtProvider.GenerateToken(user),
                Name = signupDto.Name,
                Email = signupDto.Email,
            };

            return signupResponseDto;
        }

        public async Task<SigninResponseDto> Signin(SigninDto loginDto)
        {
            var user = await _userRepository.GetAsync(item => item.Email == loginDto.Email);

            if (user == null)
            {
                throw new Exception("user not found");
            }

            var tokenDto = new SigninResponseDto
            {
                AccessToken = _jwtProvider.GenerateToken(user)
            };

            return tokenDto;
        }
    }
}
