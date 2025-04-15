using CRUDforPostswithSyncCommand.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace CRUDforPostswithSyncCommand.SeedConfiguration
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			var hasher = new PasswordHasher<IdentityUser>();
			builder.HasData(
			  new User
			  {
				  Id = "6285ac5d-4d28-4b1f-b12c-e46a0d77db02",
				  UserName = "admin",
				  NormalizedUserName = "ADMIN",
				  Email = "admin@example.com",
				  NormalizedEmail = "ADMIN@EXAMPLE.COM",
				  EmailConfirmed = true,
				  PasswordHash = hasher.HashPassword(null, "admin123")

			  },
			  new User
			  {
				  Id = "45deb9d6-c1ae-44a6-a03b-c9a5cfc15f3f",
				  UserName = "reviewer",
				  NormalizedUserName = "REVIEWER",
				  Email = "reviewer@example.com",
				  NormalizedEmail = "REVIEWER@EXAMPLE.COM",
				  EmailConfirmed = true,
				  PasswordHash = hasher.HashPassword(null, "reviewer123")

			  }
		  );
		}
	}
}
