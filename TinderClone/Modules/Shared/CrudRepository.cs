using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinderClone.Modules.Shared
{
    public interface CrudRepository<T>
    {
        Task<T> Create(T model);

        Task<T> Read(T model);

        Task<T> Update(T model);

        Task<T> Delete(T model);
    }
}