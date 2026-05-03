using CollegeApp.Model;

namespace CollegeApp.Service
{
    public interface IUserService
    {
        (string PasswordHash, string Salt) CreatePasswordWithSalt(string password);
        Task<bool> CreateUserAsync(UserDto dto);
        Task<List<UserReadonlyDto>> GetUsersAsync();
        Task<UserReadonlyDto> GetUserByIdAsync(int id);
        Task<UserReadonlyDto> GetUserByUsernameAsync(string username);
        Task<bool> UpdateUserAsync(UserDto dto);
        Task<bool> DeleteUserAsync(int userId);
    }
}
