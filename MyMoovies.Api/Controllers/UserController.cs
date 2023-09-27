using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyMooviesApi.Dtos;
using MyMooviesApi.Repositories;
using MyMooviesApi.Repositories.Models;

namespace MyMooviesApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserController(IRepository<User> userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignUp(CreateUserDto createUserDto)
        {
            if (createUserDto.Email == null)
            {
                return BadRequest();
            }
            var user = new User { Email = createUserDto.Email };
            await _userRepository.CreateAsync(user);

            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpGet]
        [Route("list")]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListUsers()
        {
            var usersDb = await _userRepository.GetAllAsync();
            var users = _mapper.Map<IEnumerable<UserDto>>(usersDb);
            return Ok(users);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var searchUser = await _userRepository.GetAsync(user => user.Id == id);

            if (searchUser == null)
            {
                return NotFound();
            }

            await _userRepository.RemoveAsync(searchUser.Id);

            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var searchUser = await _userRepository.GetAsync(user => user.Id == id);

            if (searchUser == null)
            {
                return NotFound();
            }

            var user = _mapper.Map<UserDto>(searchUser);

            return Ok(user);
        }
    }
}
