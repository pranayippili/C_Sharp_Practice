using LinkedInAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkedInAPI.Controllers
{
	//[ApiController]
	//[Route("[controller]")]
	//public class CallbackController : ControllerBase
	//{
	//	private readonly LinkedInAutomationService _automationService;

	//	public CallbackController(LinkedInAutomationService automationService)
	//	{
	//		_automationService = automationService;
	//	}

	//	[HttpGet]
	//	public async Task<IActionResult> Index([FromQuery] string code, [FromQuery] string state, [FromQuery] string error)
	//	{
	//		if (!string.IsNullOrEmpty(error))
	//		{
	//			return Content($"<html><body><h2>❌ Error: {error}</h2><p>You can close this window.</p></body></html>", "text/html");
	//		}

	//		try
	//		{
	//			var sessionId = await _automationService.HandleCallbackAsync(code, state);
	//			return Content($@"
	//                   <html>
	//                   <body style='font-family: Arial, sans-serif; text-align: center; padding: 50px;'>
	//                       <h2 style='color: green;'>✅ LinkedIn Login Successful!</h2>
	//                       <p><strong>Session ID:</strong> <code>{sessionId}</code></p>
	//                       <p>🔄 Processing your request automatically...</p>
	//                       <p>You can close this window and check the API status.</p>
	//                       <script>
	//                           setTimeout(function() {{
	//                               window.close();
	//                           }}, 3000);
	//                       </script>
	//                   </body>
	//                   </html>", "text/html");
	//		}
	//		catch (Exception ex)
	//		{
	//			return Content($"<html><body><h2>❌ Error: {ex.Message}</h2></body></html>", "text/html");
	//		}
	//	}
	//}
	[ApiController]
	[Route("[controller]")]
	public class CallbackController : ControllerBase
	{
		private readonly LinkedInAutomationService _automationService;

		public CallbackController(LinkedInAutomationService automationService)
		{
			_automationService = automationService;
		}

		[HttpGet]
		public async Task<IActionResult> Index([FromQuery] string? code, [FromQuery] string? state, [FromQuery] string? error)
		{
			// Log the incoming request for debugging
			Console.WriteLine($"Callback received - Code: {code?.Substring(0, Math.Min(10, code?.Length ?? 0))}..., State: {state}, Error: {error}");

			if (!string.IsNullOrEmpty(error))
			{
				return Content($@"
                    <html>
                    <body style='font-family: Arial; text-align: center; padding: 50px;'>
                        <h2 style='color: red;'>❌ LinkedIn Authorization Failed</h2>
                        <p><strong>Error:</strong> {error}</p>
                        <p>You can close this window and try again.</p>
                    </body>
                    </html>", "text/html");
			}

			if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(state))
			{
				return Content(@"
                    <html>
                    <body style='font-family: Arial; text-align: center; padding: 50px;'>
                        <h2 style='color: orange;'>⚠️ Missing Parameters</h2>
                        <p>The callback is missing required parameters (code or state).</p>
                        <p>Please start the authentication process again.</p>
                        <a href='/swagger' style='background: #0066cc; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>Go to API Documentation</a>
                    </body>
                    </html>", "text/html");
			}

			try
			{
				var sessionId = await _automationService.HandleCallbackAsync(code, state);

				return Content($@"
                    <html>
                    <head>
                        <title>LinkedIn Login Success</title>
                        <style>
                            body {{ font-family: Arial, sans-serif; text-align: center; padding: 50px; background: #f5f5f5; }}
                            .container {{ background: white; padding: 30px; border-radius: 10px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); max-width: 500px; margin: 0 auto; }}
                            .success {{ color: #28a745; }}
                            .code {{ background: #f8f9fa; padding: 5px 10px; border-radius: 3px; font-family: monospace; }}
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <h2 class='success'>✅ LinkedIn Login Successful!</h2>
                            <p><strong>Session ID:</strong></p>
                            <p class='code'>{sessionId}</p>
                            <p>🔄 Your profile is being processed automatically...</p>
                            <p><small>You can close this window and check the API status using the session ID above.</small></p>
                            <br>
                            <a href='/swagger' style='background: #0066cc; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>Go to API Documentation</a>
                        </div>
                        <script>
                            // Optional: Auto-close after 5 seconds
                            setTimeout(function() {{ 
                                if (confirm('Close this window?')) {{
                                    window.close(); 
                                }}
                            }}, 5000);
                        </script>
                    </body>
                    </html>", "text/html");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Callback error: {ex.Message}");
				return Content($@"
                    <html>
                    <body style='font-family: Arial; text-align: center; padding: 50px;'>
                        <h2 style='color: red;'>❌ Processing Error</h2>
                        <p><strong>Error:</strong> {ex.Message}</p>
                        <p>Please try the authentication process again.</p>
                        <a href='/swagger' style='background: #0066cc; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>Go to API Documentation</a>
                    </body>
                    </html>", "text/html");
			}
		}
	}
}
