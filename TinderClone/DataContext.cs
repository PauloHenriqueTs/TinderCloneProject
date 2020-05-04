using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinderClone.Modules.Profiles.Model;
using TinderClone.Modules.Users.Models;

namespace TinderClone
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
          : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                         .HasOne(u => u.Profile)
                        .WithOne(p => p.User)
                        .HasForeignKey<Profile>(p => p.UserId);

            modelBuilder.Entity<Profile>()
                        .HasOne(p => p.User)
                       .WithOne(u => u.Profile)
                       .HasForeignKey<User>(u => u.ProfileId);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
    }
}