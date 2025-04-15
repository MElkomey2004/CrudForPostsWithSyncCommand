using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDforPostswithSyncCommand.Models
{
	public class Post
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; } 
		public int  ExternalId { get; set; } 
		public int UserId { get; set; }
		[MaxLength(255)]
		public string? Title { get; set; }
		[MaxLength (255)]
		public string? Body { get; set; } 
		public bool? IsApproved { get; set; } = false;
	}
}
