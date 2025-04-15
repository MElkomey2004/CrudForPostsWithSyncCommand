using CRUDforPostswithSyncCommand.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRUDforPostswithSyncCommand.SeedConfiguration
{
	public class RoleConfiguration : IEntityTypeConfiguration<Role>
	{
		public void Configure(EntityTypeBuilder<Role> builder)
		{
			builder.HasData(
				new Role
				{
					Id = "639de03f-7876-4fff-96ec-37f8bd3bf180",
					Name = "Admin",
					NormalizedName = "ADMIN",
					Description = "The Admin role for the user"
				},

				new Role
				{
					Id = "45deb9d6-c1ae-44a6-a03b-c9a5cfc15f2f",
					Name = "Reviewer",
					NormalizedName = "REVIEWER",
					Description = "The Reviewer role for the user"
				}
				);
		}
	}
}
