using CRUDforPostswithSyncCommand.DTOs;

namespace CRUDforPostswithSyncCommand.Services
{
	public interface IPostService
	{
		Task<SyncResultDto> SyncPostsAsync();
	}
}
