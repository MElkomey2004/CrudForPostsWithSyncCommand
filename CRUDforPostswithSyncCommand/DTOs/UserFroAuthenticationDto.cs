using System.ComponentModel.DataAnnotations;

namespace CRUDforPostswithSyncCommand.DTOs
{
	public class UserFroAuthenticationDto
	{
		[Required(ErrorMessage ="Email is Required")]
		public string? Email { get;set; }
		[Required(ErrorMessage = "Password is Required")]
		public string? Password { get;set; }
	}
}
