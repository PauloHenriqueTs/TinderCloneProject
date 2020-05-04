using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TinderClone.Modules.Shared;
using TinderClone.Modules.Users.Models;
using TinderClone.Modules.Users.Shared;

namespace TinderClone.Modules.Users.Repository
{
    public interface IUserRepository : CrudRepository<User>
    {
        Task<User> GetUserByEmail(UserDto model);
    }
}