using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinderClone.Modules.Profiles.Model;

namespace TinderClone.Modules.Profiles.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly DataContext context;

        public ProfileRepository(DataContext dataContext)
        {
            context = dataContext;
        }

        public async Task<Profile> Create(Profile model)
        {
            await context.Profiles.AddAsync(model);
            await context.SaveChangesAsync();

            return model;
        }

        public async Task<Profile> Delete(Profile model)
        {
            context.Profiles.Remove(model);
            await context.SaveChangesAsync();

            return model;
        }

        public async Task<Profile> GetById(Guid id)
        {
            var profile = await context.Profiles.Include(p => p.User).SingleAsync(u => u.Id == id);

            return profile;
        }

        public async Task<Profile> Read(Profile model)
        {
            var profile = await context.Profiles.Include(p => p.User).SingleAsync(u => u.Id == model.Id);

            return profile;
        }

        public async Task<Profile> Update(Profile model)
        {
            context.Profiles.Update(model);
            await context.SaveChangesAsync();

            return model;
        }
    }
}