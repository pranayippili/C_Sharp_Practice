using LinkedInAPI.Models;
using LinkedInAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkedInAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class LinkedInController : ControllerBase
	{
		private readonly LinkedInAutomationService _automationService;

		public LinkedInController(LinkedInAutomationService automationService)
		{
			_automationService = automationService;
		}

		/// <summary>
		/// One-click automated login (most similar to console app)
		/// </summary>
		[HttpPost("login")]
		public async Task<ActionResult<CompleteLoginResult>> CompleteLogin()
		{
			try
			{
				// Start the automation
				var automation = await _automationService.StartAutomatedLoginAsync();

				// Return instructions for user
				return Ok(new CompleteLoginResult
				{
					SessionId = automation.SessionId,
					AuthorizationUrl = automation.AuthorizationUrl,
					Instructions = new[]
					{
						"Step 1: Visit the authorization URL to login to LinkedIn",
						"Step 2: After authorization, the process will complete automatically",
						"Step 3: Check the status using GET /api/linkedin/status/{sessionId}",
						"Step 4: Once status shows 'Completed successfully', your profile data will be available"
					},
					StatusCheckUrl = $"/api/linkedin/status/{automation.SessionId}"
				});
			}
			catch (Exception ex)
			{
				return BadRequest(new { error = ex.Message });
			}
		}

		/// <summary>
		/// Check the status of an automation session
		/// </summary>
		[HttpGet("status/{sessionId}")]
		public ActionResult<AutomationStatus> GetStatus(string sessionId)
		{
			var status = _automationService.GetSessionStatus(sessionId);
			return Ok(status);
		}
	}
}
