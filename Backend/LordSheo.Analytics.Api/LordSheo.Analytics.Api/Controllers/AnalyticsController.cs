using LordSheo.Analytics.Api.Data;
using LordSheo.Analytics.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using SQLitePCL;

namespace LordSheo.Analytics.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AnalyticsController : ControllerBase
	{
		private readonly AnalyticsDbContext _context;

		public AnalyticsController(AnalyticsDbContext context)
		{
			_context = context;
		}

		[HttpPost]
		public async Task<IActionResult> PostEvent([FromBody] AnalyticsEvent e)
		{
			e.Timestamp = DateTime.UtcNow;
			_context.AnalyticsEvents.Add(e);
			await _context.SaveChangesAsync();

			return Ok(new
			{
				Message = "Event received"
			});
		}

		[HttpGet]
		public async Task<IActionResult> GetEvents()
		{
			var events = await _context.AnalyticsEvents.ToListAsync();
			
			return Ok(events);
		}
	}
}