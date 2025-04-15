using AutoMapper;
using CRUDforPostswithSyncCommand.Data;
using CRUDforPostswithSyncCommand.DTOs;
using CRUDforPostswithSyncCommand.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDforPostswithSyncCommand.Repositories
{
	public class PostRepsitory : IPostRepository
	{
		private readonly PostDbContext _dbContext;
		private readonly IMapper _mapper;
        public PostRepsitory(PostDbContext dbContext , IMapper mapper)
        {
            _dbContext = dbContext;
			_mapper = mapper;
        }
        public async Task<int> Create(PostDto postDto)
		{
			var post = _mapper.Map<Post>(postDto);
			await _dbContext.Posts.AddAsync(post);
			await _dbContext.SaveChangesAsync();
			return post.Id;
		}

		public async Task<bool> Delete(int id)
		{
			var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == id);
			if (post == null)
				return false;

			_dbContext.Posts.Remove(post);
			await _dbContext.SaveChangesAsync();
			return true;
		}

		public async Task<IEnumerable<PostDto>> GetAll()
		{
			var posts = await _dbContext.Posts.ToListAsync();

			return _mapper.Map<IEnumerable<PostDto>>(posts);
		}

		public async Task<PostDto> GetById(int id)
		{
			var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == id);
			if (post == null)
				return null;

		
			return _mapper.Map<PostDto>(post);
		}

		public async Task<bool> Update(int id,PostDto postDto)
		{
			var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == id);
			if (post == null)
				return false;

			post.Title = postDto.Title;
			post.Body = postDto.Body;
		

			_mapper.Map<Post>(postDto);
			_dbContext.Posts.Update(post);
			await _dbContext.SaveChangesAsync();
			return true;

		}


		public async Task<bool> Approve(int id)
		{
			var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == id);
			if (post == null)
				return false;

			post.IsApproved = true;

			_dbContext.Posts.Update(post);
			await _dbContext.SaveChangesAsync();
			return true;

		}

		public async Task<bool> Exists(string title)
		{
			return await _dbContext.Posts.AnyAsync(p => p.Title == title);
		}

	}
}
