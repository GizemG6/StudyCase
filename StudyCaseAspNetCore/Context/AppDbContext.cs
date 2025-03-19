using Microsoft.EntityFrameworkCore;
using StudyCaseAspNetCore.Models;

namespace StudyCaseAspNetCore.Context
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<Notification> Notifications { get; set; }
	}
}
