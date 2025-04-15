using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CRUDforPostswithSyncCommand.Data;
using CRUDforPostswithSyncCommand.Models;
using CRUDforPostswithSyncCommand.DTOs;
using System.Text.Json;
using System;

namespace CRUDforPostswithSyncCommand.Services
{
	public class PostService : IPostService
	{
		private readonly PostDbContext _context;
		private readonly HttpClient _httpClient;

		public PostService(PostDbContext context, IHttpClientFactory httpClientFactory)
		{
			_context = context;
			_httpClient = httpClientFactory.CreateClient();
		}


		public async Task<SyncResultDto> SyncPostsAsync()
		{
			var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts");

			if (!response.IsSuccessStatusCode)
				throw new HttpRequestException("Failed to fetch posts from external API.");

			var json = await response.Content.ReadAsStringAsync();

			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};

			var externalPosts = JsonSerializer.Deserialize<List<PostDto>>(json, options);



			if (externalPosts == null || !externalPosts.Any())
				throw new Exception("No posts found in external API response.");

			var added = 0;
			var skipped = new List<int>();

			foreach (var extPost in externalPosts)
			{
				bool exists = await _context.Posts.AnyAsync(p => p.ExternalId == extPost.Id);
				if (exists)
				{
					skipped.Add(extPost.Id);
					continue;
				}

				var newPost = new Post
				{
					ExternalId = extPost.Id,
					UserId = extPost.UserId,
					Title = extPost.Title,
					Body = extPost.Body,
					IsApproved = false
				};

				await _context.Posts.AddAsync(newPost);
				added++;
			}

			await _context.SaveChangesAsync();

			return new SyncResultDto
			{
				AddedCount = added,
				SkippedCount = skipped.Count,
				SkippedExternalIds = skipped
			};
		}



	}

}