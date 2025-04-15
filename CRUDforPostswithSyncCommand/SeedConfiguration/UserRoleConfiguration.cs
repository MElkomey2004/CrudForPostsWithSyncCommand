using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRUDforPostswithSyncCommand.SeedConfiguration
{
	public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
	{
		public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
		{
			builder.HasData(

				new IdentityUserRole<string>
				{
					UserId = "6285ac5d-4d28-4b1f-b12c-e46a0d77db02",
					RoleId = "639de03f-7876-4fff-96ec-37f8bd3bf180"
				},
				new IdentityUserRole<string>
				{
					UserId = "45deb9d6-c1ae-44a6-a03b-c9a5cfc15f3f",
					RoleId = "45deb9d6-c1ae-44a6-a03b-c9a5cfc15f2f"
				}
				) ;
		}
	}
}
