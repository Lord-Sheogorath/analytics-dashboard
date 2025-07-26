using LordSheo.Analytics.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace LordSheo.Analytics.Api.Data
{
	public class AnalyticsDbContext : DbContext
	{
		public DbSet<AnalyticsEvent> AnalyticsEvents { get; set; }
		
		public AnalyticsDbContext(DbContextOptions<AnalyticsDbContext> options)
			: base(options)
		{
		}
	}
}