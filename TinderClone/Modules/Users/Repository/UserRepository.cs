using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinderClone.Modules.Profiles.Model;
using TinderClone.Modules.Users.Models;
using TinderClone.Modules.Users.Shared;

namespace TinderClone.Modules.Users.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext context;
        private readonly ILogger _logger;

        public UserRepository(DataContext dataContext, ILogger<UserRepository> logger)
        {
            context = dataContext;
            _logger = logger;
        }

        public async Task<User> Create(User model)
        {
            await context.Users.AddAsync(model);
      
            await context.SaveChangesAsync();

            return model;
        }

        public async Task<User> Delete(User model)
        {
            context.Users.Remove(model);
            await context.SaveChangesAsync();

            return model;
        }

        public async Task<User> GetUserByEmail(UserDto model)
        {
            var query = from u in context.Set<User>()
                        join p in context.Set<Profile>()
                            on u.ProfileId equals p.Id into grouping
                        from p in grouping.DefaultIfEmpty()
                        select UserFactory.Create(u, p);

            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

            return user;
        }

        public async Task<User> Read(User model)
        {
            var query = from u in context.Set<User>()
                        join p in context.Set<Profile>()
                            on u.ProfileId equals p.Id into grouping
                        from p in grouping.DefaultIfEmpty()
                        select UserFactory.Create(u, p);

            var user = await query.FirstOrDefaultAsync(u => u.Id == model.Id);

            return user;
        }

        public async Task<User> Update(User model)
        {
            context.Users.Update(model);
            await context.SaveChangesAsync();

            return model;
        }
    }
}