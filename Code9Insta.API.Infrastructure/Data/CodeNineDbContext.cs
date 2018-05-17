using System;
using Code9Insta.API.Infrastructure.Entities;
using Code9Insta.API.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Code9Insta.API.Infrastructure.Data
{
    public class CodeNineDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {

        public CodeNineDbContext(DbContextOptions<CodeNineDbContext> options)
          : base(options)
      {
      }

      public DbSet<Profile> Profiles { get; set; }
      public new DbSet<ApplicationUser> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
      {
          base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<Profile>()
               .HasOne(ap => ap.User)
               .WithOne(p => p.Profile)
               .HasForeignKey<Profile>(ap => ap.UserId);

        }

    }
        
    
}
