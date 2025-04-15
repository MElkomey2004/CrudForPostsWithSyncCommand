using CRUDforPostswithSyncCommand.DTOs;
using CRUDforPostswithSyncCommand.Models;
using CRUDforPostswithSyncCommand.Repositories;
using CRUDforPostswithSyncCommand.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;


namespace CRUDforPostswithSyncCommand.Controllers
{
	[Route("api/[Controller]")]
	[ApiController]
	public class PostsController : ControllerBase
	{
        private readonly IPostRepository _postRepository;
		private readonly IPostService _postService;
		public PostsController(IPostRepository postRepository , IPostService postService)
        {
            _postRepository = postRepository;
			_postService = postService;
		
        }



		[HttpPost("sync")]
		public async Task<IActionResult> SyncPosts()
		{
			var result = await _postService.SyncPostsAsync();

			if (result.SkippedCount > 0)
			{
				return Ok(new
				{
					message = "Some posts were skipped because they already exist.",
					result
				});
			}

			return Ok(new
			{
				message = "All posts synced successfully.",
				result
			});
		}
	



		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		//[Authorize]
		public async Task<IActionResult> Create([FromBody] PostDto postDto)
		{
			if (postDto == null)
				return BadRequest("Post data is required.");

			var postId = await _postRepository.Create(postDto);

			return CreatedAtAction(nameof(GetById), new { id = postId }, postDto);
		}



		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		//[Authorize(Policy = "AdminOrReviewerPolicy")]
	
		public async Task<IActionResult> GetAll()
		{
		
			var posts = await _postRepository.GetAll();
			return Ok(posts);
		}





		[HttpDelete]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[Route("{id:int}")]
		//[Authorize(Policy = "AdminPolicy")]

		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var post = await _postRepository.Delete(id);
			if (!post)
				return NotFound();
		
			return NoContent();
		}






		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[Route("{id:int}")]

		public async Task<IActionResult> GetById([FromRoute] int id)
		{
			var post = await _postRepository.GetById(id);
			if (post == null || post.IsApproved == false)
				return NotFound();

			return Ok(post);
		}


		[HttpPut]
		[Route("{id:int}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		//[Authorize(Policy ="AdminPolicy")]
		public async Task<IActionResult> Update([FromRoute]int id , [FromBody]PostDto postDto)
		{
			if (postDto == null)
				return BadRequest("Post Data Is Required");
			var post = await _postRepository.Update(id, postDto);
			if(!post)
				return NotFound();

			return NoContent();
		}


		[HttpPost]
		[Route("{id:int}/approve")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		//[Authorize(Policy = "ReviewerPolicy")]
		
		public async Task<IActionResult> Approve([FromRoute]int id)
		{
			var post = await _postRepository.Approve(id);
			if (post == false)
				return NotFound();
			

			return Ok(post);

		}
	}
}
