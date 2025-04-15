using System.Text.Json.Serialization;

namespace CRUDforPostswithSyncCommand.DTOs
{
	public class PostDto
	{
		public int Id { get; set; } // This becomes ExternalId in DB
		public int UserId { get; set; }
		[JsonPropertyName("title")]
		public string Title { get; set; }
		[JsonPropertyName("body")]
		public string Body { get; set; }
		public bool? IsApproved { get; set; } = false;
	}
}
