using MyMooviesApi.Repositories.Models;
using MyMooviesApi.Repositories;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using MyMooviesApi.Dtos;
using AutoMapper;

namespace MyMooviesApi.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserService(
            IHttpContextAccessor contextAccessor,
            IRepository<User> userRepository,
            IMapper mapper)
        {
            _contextAccessor = contextAccessor;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public Guid GetLoggedUserId()
        {
            var httpContext = _contextAccessor.HttpContext;

            if (httpContext == null)
            {
                throw new Exception("user must be logged in");
            }

            var loggedUserId = httpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.NameId).Value;

            return Guid.Parse(loggedUserId);
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            if (createUserDto.Email == null)
            {
                throw new Exception("user not found");
            }
            var user = new User { Email = createUserDto.Email };
            await _userRepository.CreateAsync(user);

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var usersDb = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(usersDb);
        }

        public async Task RemoveUserByIdAsync(Guid id)
        {
            var searchUser = await _userRepository.GetAsync(user => user.Id == id);

            if (searchUser == null)
            {
                throw new Exception("user not found");
            }

            await _userRepository.RemoveAsync(searchUser.Id);
        }

        public async Task<UserDto> GetUserByIdAsync(Guid id)
        {
            var searchUser = await _userRepository.GetAsync(user => user.Id == id);

            if (searchUser == null)
            {
                throw new Exception("user not found");
            }

            var user = _mapper.Map<UserDto>(searchUser);

            return user;
        }
    }
}
