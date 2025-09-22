namespace MeetingCostApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// 1. Add CORS service
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowAll", policy =>
				{
					policy.AllowAnyOrigin()
						  .AllowAnyHeader()
						  .AllowAnyMethod();
				});
			});

			var app = builder.Build();

			// 2. Enable CORS middleware
			app.UseCors("AllowAll");

			// Config dictionary from appsettings.json
			var costDictionary = builder.Configuration
				.GetSection("EmailCosts")
				.Get<Dictionary<string, int>>() ?? new();

			// GET endpoint
			app.MapGet("/api/cost", (string email) =>
			{
				int cost = costDictionary.TryGetValue(email, out var foundCost) ? foundCost : 50;
				return Results.Json(new { email, cost });
			});

			app.Run();
		}
	}
}
