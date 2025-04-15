namespace CRUDforPostswithSyncCommand.DTOs
{
	public class AuthResponseDto
	{
		public bool IsSuccessfull { get; set; }	
		public string? ErrorMessage { get; set; }
		public string? Token { get; set; }
	}
}
