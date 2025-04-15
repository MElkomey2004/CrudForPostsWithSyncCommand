namespace CRUDforPostswithSyncCommand.DTOs
{
	public class SyncResultDto
	{
		public int AddedCount { get; set; }
		public int SkippedCount { get; set; }
		public List<int> SkippedExternalIds { get; set; }
	}
}
