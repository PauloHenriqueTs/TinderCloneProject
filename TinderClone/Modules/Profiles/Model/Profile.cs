using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TinderClone.Modules.Profiles.Shared;
using TinderClone.Modules.Users.Models;

namespace TinderClone.Modules.Profiles.Model
{
    public class Profile
    {
        private Profile()
        {
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string Description { get; set; }

        [Url]
        public string PictureUrl { get; set; }

        [Required]
       
        public User User { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public static class Factory
        {
            public static Profile Create(ProfileDto dto)
            {
                return new Profile
                {
                    Id = Guid.NewGuid(),
                    Name = dto.Name,
                    Description = dto.Description,
                    PictureUrl = dto.PictureUrl,
                    User = dto.User,
                    UserId = dto.UserId
                };
            }
        }
    }
}