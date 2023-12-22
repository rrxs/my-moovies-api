using MyMooviesApi.Dtos;

namespace MyMooviesApi.Services
{
    public interface IUserService
    {
        Guid GetLoggedUserId();
        Task<UserDto> CreateUserAsync(CreateUserDto createDto);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task RemoveUserByIdAsync(Guid id);
        Task<UserDto> GetUserByIdAsync(Guid id);
    }
}
