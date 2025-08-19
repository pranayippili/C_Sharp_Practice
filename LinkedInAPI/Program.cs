
using LinkedInAPI.Services;

namespace LinkedInAPI
{
    public class Program
    {
        public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers();
			builder.Services.AddHttpClient();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddSingleton<LinkedInAutomationService>();

			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowAll",
					builder =>
					{
						builder
							.AllowAnyOrigin()
							.AllowAnyMethod()
							.AllowAnyHeader();
					});
			});

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseCors("AllowAll");
			app.UseAuthorization();
			app.MapControllers();

			// Add direct callback route as backup
			//app.MapGet("/callback", async (HttpContext context, LinkedInAutomationService automationService) =>
			//{
			//	var code = context.Request.Query["code"].ToString();
			//	var state = context.Request.Query["state"].ToString();
			//	var error = context.Request.Query["error"].ToString();

			//	Console.WriteLine($"Direct callback - Code: {code?.Substring(0, Math.Min(10, code?.Length ?? 0))}..., State: {state}");

			//	if (!string.IsNullOrEmpty(error))
			//	{
			//		return Results.Content($"<html><body><h2>Error: {error}</h2></body></html>", "text/html");
			//	}

			//	if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(state))
			//	{
			//		return Results.Content("<html><body><h2>Missing code or state parameter</h2></body></html>", "text/html");
			//	}

			//	try
			//	{
			//		var sessionId = await automationService.HandleCallbackAsync(code, state);
			//		return Results.Content($@"
   //         <html>
   //         <body style='font-family: Arial; text-align: center; padding: 50px;'>
   //             <h2 style='color: green;'>✅ Success!</h2>
   //             <p>Session ID: <strong>{sessionId}</strong></p>
   //             <p>Processing automatically...</p>
   //         </body>
   //         </html>", "text/html");
			//	}
			//	catch (Exception ex)
			//	{
			//		return Results.Content($"<html><body><h2>Error: {ex.Message}</h2></body></html>", "text/html");
			//	}
			//});

			app.Run();
		}
    }
}
