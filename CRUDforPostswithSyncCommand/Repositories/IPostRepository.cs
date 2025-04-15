using CRUDforPostswithSyncCommand.DTOs;

namespace CRUDforPostswithSyncCommand.Repositories
{
	public interface IPostRepository
	{
		Task<int> Create(PostDto postDto);
		Task<bool> Update(int id, PostDto postDto);
		Task<bool> Delete(int id);
		Task<IEnumerable<PostDto>> GetAll();	
		Task<PostDto> GetById(int id);
		Task<bool> Approve(int id);
		Task<bool> Exists(string title);
	}
}
