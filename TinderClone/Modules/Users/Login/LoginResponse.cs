using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinderClone.Modules.Users.Models;

namespace TinderClone.Modules.Users.Login
{
    public class LoginResponse
    {
        public string token { get; set; }
        public User user { get; set; }
    }
}