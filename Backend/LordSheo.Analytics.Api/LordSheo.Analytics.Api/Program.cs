using System.Text.Json;
using LordSheo.Analytics.Api.Data;
using LordSheo.Analytics.Api.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AnalyticsDbContext>(options =>
{
	options.UseSqlite("Data Source=analytics.db");
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policy =>
	{
		policy.WithOrigins("https://localhost:7286") // <-- your Blazor app URL (adjust port if needed)
			.AllowAnyHeader()
			.AllowAnyMethod();
	});
});

var app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<AnalyticsDbContext>();

	// Optional: ensure DB created & migrations applied
	db.Database.Migrate();

	// Only seed if no events exist
	if (!db.AnalyticsEvents.Any())
	{
		SeedTestEvents(db, 1000);  // generate 100 test events
	}
}

app.Run();

void SeedTestEvents(AnalyticsDbContext context, int count)
{
	var rnd = new Random();
	var eventNames = new[] { "Login", "Logout", "PageView", "Click", "Purchase" };

	var events = new List<AnalyticsEvent>();

	for (int i = 0; i < count; i++)
	{
		var e = new AnalyticsEvent
		{
			EventName = eventNames[rnd.Next(eventNames.Length)],
			UserId = Guid.NewGuid().ToString(),
			Timestamp = DateTime.UtcNow.AddMinutes(-rnd.Next(0, 60 * 24 * 7)), // last 7 days
			MetadataJson = JsonSerializer.Serialize(new Dictionary<string, string>
			{
				{ "Detail", "Test event #" + i },
				{ "RandomValue", rnd.Next(1000).ToString() }
			})
		};
		events.Add(e);
	}

	context.AnalyticsEvents.AddRange(events);
	context.SaveChanges();
}
