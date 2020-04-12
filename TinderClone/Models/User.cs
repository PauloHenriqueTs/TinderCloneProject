using System;
using System.ComponentModel.DataAnnotations;

namespace TinderClone.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(32)]
        public string Password { get; set; }
    }

    public class UserDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(32)]
        public string Password { get; set; }
    }

    public static class UserFactory
    {
        public static User create(UserDto dto)
        {
            var password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            return new User { Id = Guid.NewGuid(), Email = dto.Email, Password = password };
        }
    }
}