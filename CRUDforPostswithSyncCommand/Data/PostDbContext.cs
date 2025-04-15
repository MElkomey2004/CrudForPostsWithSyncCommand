using Microsoft.EntityFrameworkCore;
using CRUDforPostswithSyncCommand.Models;

namespace CRUDforPostswithSyncCommand.Data
{
	public class PostDbContext : DbContext
	{
		public PostDbContext(DbContextOptions<PostDbContext> options) : base(options)
		{
		}

		public DbSet<Post> Posts { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		


		}
	}
}