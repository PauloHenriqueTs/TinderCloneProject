using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using TinderClone.Modules.Profiles.Model;
using System.ComponentModel.DataAnnotations.Schema;
using TinderClone.Modules.Users.Shared;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace TinderClone.Modules.Users.Models
{
    public class User
    {
        public User()
        {
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(32)]
        [Newtonsoft.Json.JsonIgnore]
        public string Password { get; set; }

        [AllowNull]
        public Guid ProfileId { get; set; }

        [AllowNull]
        public Profile Profile { get; set; }
    }

    public static class UserFactory
    {
        public static User Create(UserDto userDto)
        {
            var password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            return new User
            {
                Id = Guid.NewGuid(),
                Email = userDto.Email,
                Password = password,
            };
        }

        public static User Create(User user, Profile profile)
        {
            return new User
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                Profile = profile,
                ProfileId = profile.Id
            };
        }
    }
}