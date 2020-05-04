using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinderClone.Modules.Profiles.Model;
using TinderClone.Modules.Shared;

namespace TinderClone.Modules.Profiles.Repository
{
    public interface IProfileRepository : CrudRepository<Profile>
    {
        Task<Profile> GetById(Guid id);
    }
}