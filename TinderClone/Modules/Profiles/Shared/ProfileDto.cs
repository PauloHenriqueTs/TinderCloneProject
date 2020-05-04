using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TinderClone.Modules.Users.Models;

namespace TinderClone.Modules.Profiles.Shared
{
    public class ProfileDto
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string Description { get; set; }

        [Url]
        public string PictureUrl { get; set; }

        [Required]
        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}