using CRUDforPostswithSyncCommand.Entities;
using CRUDforPostswithSyncCommand.SeedConfiguration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CRUDforPostswithSyncCommand.Data
{
	public class AuthDbContext :IdentityDbContext<User , Role , string>
	{
        public AuthDbContext(DbContextOptions options): base(options)
        {
            
        }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfiguration(new RoleConfiguration());
			builder.ApplyConfiguration(new UserRoleConfiguration());
			builder.ApplyConfiguration(new UserConfiguration());
		}

	}
}
