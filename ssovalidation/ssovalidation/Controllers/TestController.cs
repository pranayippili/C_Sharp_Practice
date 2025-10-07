using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ssovalidation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestController : ControllerBase
	{
		// This endpoint requires a valid Azure Entra ID access token
		[HttpGet("whoami")]
		[Authorize]
		public IActionResult WhoAmI()
		{
			var user = User.Identity?.Name ?? "Unknown user";

			var claims = User.Claims.Select(c => new
			{
				Type = c.Type,
				Value = c.Value
			});

			return Ok(new
			{
				Message = $"Hello {user}! You are authorized.",
				Claims = claims
			});
		}

		// Optional: a public endpoint to verify that your API itself is up
		[HttpGet("ping")]
		[AllowAnonymous]
		public IActionResult Ping()
		{
			return Ok(new { Message = "API is running and does not require auth." });
		}

		[HttpGet]
		[Authorize]
		public IActionResult Get()
		{
			return Ok(new { message = "Token is valid. Hello, Pranay!" });
		}
	}
}
