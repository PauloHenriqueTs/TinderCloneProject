using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinderClone.Modules.Users.Login;
using TinderClone.Modules.Users.Models;
using TinderClone.Modules.Users.Repository;
using TinderClone.Modules.Users.Shared;
using TinderClone.Security;

namespace TinderClone.Modules.Users.Service
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        private void ValidateUserDto(User userDb, UserDto userDto)
        {
            bool validPassword = BCrypt.Net.BCrypt.Verify(userDto.Password, userDb.Password);
            if (!validPassword)
            {
                throw new Exception();
            }
        }

        public async Task<LoginResponse> Login(UserDto userDto)
        {
            try
            {
                var userDb = await _userRepository.GetUserByEmail(userDto);
                ValidateUserDto(userDb, userDto);
                var token = TokenService.GenerateToken(userDb);
                return new LoginResponse { user = userDb, token = token };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<User> Signup(UserDto userDto)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(userDto);
                UserExist(user);
                var newUser = UserFactory.Create(userDto);
                return await _userRepository.Create(newUser);
            }
            catch (Exception e)
            {
                _logger.LogError(System.Text.Json.JsonSerializer.Serialize(e));
                throw e;
            }
        }

        public static void UserIsNull(User user)
        {
            if (user == null)
            {
                throw new Exception();
            }
        }

        public static void UserExist(User user)
        {
            if (user != null)
            {
                throw new Exception();
            }
        }
    }
}