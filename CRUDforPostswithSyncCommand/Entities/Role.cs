using Microsoft.AspNetCore.Identity;

namespace CRUDforPostswithSyncCommand.Entities
{
	public class Role : IdentityRole
	{
		public string? Description { get;set; }
	}
}
