using AutoMapper;
using CollegeApp.Data;
using CollegeApp.Data.Repository;
using CollegeApp.Model;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace CollegeApp.Service
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly ICollegeRepository<User> _userRepository;
        public UserService(ICollegeRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<bool> CreateUserAsync(UserDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto, $"the argument {dto} is null");

            //this is to check if the username is already taken
            var existingUser = await _userRepository.GetAsync(x => x.Username.Equals(dto.Username));

            if (existingUser is not null)
            {
                throw new Exception("This username already existing");
            }
            User user = _mapper.Map<User>(dto);
            user.IsDeleted = false;
            user.CreatededDate = DateTime.UtcNow;
            user.ModifiedDate = DateTime.UtcNow;

            if (!string.IsNullOrEmpty(dto.Password))
            {
                var passwordHash = CreatePasswordWithSalt(dto.Password);
                user.Password = passwordHash.PasswordHash;
                user.PasswordSalt = passwordHash.Salt;
            }

            await _userRepository.CreateAsync(user);
            return true;
        }
        public (string PasswordHash, string Salt) CreatePasswordWithSalt(string password)
        {
            var salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                salt,
                KeyDerivationPrf.HMACSHA256,
                10000,
                32
                ));
            return (hash, Convert.ToBase64String(salt));
        }
        public async Task<List<UserReadonlyDto>> GetUsersAsync()
        {
            //var users = await _userRepository.GetAllAsync(); //this is normal

            var users = await _userRepository.GetAllByFilterAsync( u => !u.IsDeleted); //we need this because of soft delete

             return _mapper.Map<List<UserReadonlyDto>>(users);
        }
        public async Task<UserReadonlyDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetAsync(x => x.Id == id && !x.IsDeleted); //due to soft delete

            return _mapper.Map<UserReadonlyDto>(user);
        }
        public async Task<UserReadonlyDto> GetUserByUsernameAsync(string username)
        {
            var user = await _userRepository.GetAsync(x => x.Username.Equals(username) && !x.IsDeleted);
            return _mapper.Map<UserReadonlyDto>(user);
        }
        
        public async Task<bool> UpdateUserAsync(UserDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto, $"the argument {dto} is null");
            var existingUser = await _userRepository.GetAsync(x => x.Id == dto.Id && !x.IsDeleted);
            if (existingUser == null)
                throw new Exception($"User not found with the Id: {dto.Id}");

            //await _userRepository.UpdateAsync(existingUser); //MINE

            var userToUpdate = _mapper.Map<User>(dto);
            userToUpdate.ModifiedDate = DateTime.UtcNow;

            if (!string.IsNullOrEmpty(dto.Password))
            {
                var passwordHash = CreatePasswordWithSalt(dto.Password);
                userToUpdate.Password = passwordHash.PasswordHash;
                userToUpdate.PasswordSalt = passwordHash.Salt;
            }

            // 1. in this tutorial we will update only the user information
            //2. Normally we should provide separate method to update the password. Create a separate endpoint for password update
            //3. Go deeper on password update

            await _userRepository.UpdateAsync(userToUpdate);            
            return true;
              
        }
        public async Task<bool> DeleteUserAsync(int userId) //Impplemeent Soft delete
        {
            if (userId <= 0)
                ArgumentNullException.ThrowIfNull(userId, $"the argument {userId} is null");


            var existingUser = await _userRepository.GetAsync(x => x.Id == userId && !x.IsDeleted);
            if (existingUser == null)
                throw new Exception($"User not found with the Id: {userId}");

            //1. Hard delete - it will delete the whole of the record
            //2. Soft delete - means marking it in DB as Isdeleted true

            //NB: We are implementing Soft delete

            existingUser.IsDeleted = true;

            await _userRepository.UpdateAsync(existingUser);

            return true;
        }
        
    }
}
