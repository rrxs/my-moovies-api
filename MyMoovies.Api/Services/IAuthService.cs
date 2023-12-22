using Microsoft.AspNetCore.Mvc;
using MyMooviesApi.Dtos;

namespace MyMooviesApi.Services
{
    public interface IAuthService
    {
        Task<SignupResponseDto> Signup(SignupDto signupDto);
        Task<SigninResponseDto> Signin(SigninDto loginDto);
    }
}
