using LinkedInAPI.Models;
using System.Collections.Concurrent;
using System.Net.Http.Headers;
using System.Text.Json;

namespace LinkedInAPI.Services
{
	public class LinkedInAutomationService
	{
		private readonly string _clientId = "86u06ez5d9k7kd";
		private readonly string _clientSecret = "WPL_AP1.vXKkNxFz5wK0ivCw.Gi7ivw==";
		private readonly string _redirectUri = "http://localhost:5000/callback";
		private readonly string _scope = "openid profile email";
		private readonly HttpClient _httpClient;

		// Store automation sessions
		private readonly ConcurrentDictionary<string, AutomationSession> _sessions = new();

		public LinkedInAutomationService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<AutomationResult> StartAutomatedLoginAsync()
		{
			var sessionId = Guid.NewGuid().ToString();
			var state = Guid.NewGuid().ToString();

			// Create automation session
			var session = new AutomationSession
			{
				SessionId = sessionId,
				State = state,
				CreatedAt = DateTime.UtcNow,
				Status = "Waiting for user authorization",
				TaskCompletionSource = new TaskCompletionSource<string>()
			};

			_sessions[sessionId] = session;

			// Generate auth URL
			string authUrl = $"https://www.linkedin.com/oauth/v2/authorization?" +
						   $"response_type=code&" +
						   $"client_id={_clientId}&" +
						   $"redirect_uri={Uri.EscapeDataString(_redirectUri)}&" +
						   $"state={state}&" +
						   $"scope={Uri.EscapeDataString(_scope)}&" +
						   $"prompt=login";

			// Start background task to handle the automation
			_ = Task.Run(async () =>
			{
				try
				{
					// Simulate opening browser (in real implementation, you might use Selenium)
					session.Status = "Browser opened, waiting for user to authorize...";

					// Wait for the callback to be received
					var code = await session.TaskCompletionSource.Task;

					session.Status = "Authorization code received, exchanging for token...";

					// Exchange code for token
					var tokenResponse = await ExchangeCodeForTokenAsync(code);
					session.AccessToken = tokenResponse.AccessToken;
					session.IdToken = tokenResponse.IdToken;

					session.Status = "Token received, fetching user profile...";

					// Get user profile
					var userProfile = await GetUserProfileAsync(tokenResponse.AccessToken);
					session.UserProfile = userProfile;

					session.Status = "Completed successfully";
					session.IsCompleted = true;
				}
				catch (Exception ex)
				{
					session.Status = $"Error: {ex.Message}";
					session.Error = ex.Message;
					session.IsCompleted = true;
				}
			});

			return new AutomationResult
			{
				SessionId = sessionId,
				AuthorizationUrl = authUrl,
				Status = session.Status,
				Message = "Please visit the authorization URL to complete login. Check status using the session ID."
			};
		}

		public async Task<string> HandleCallbackAsync(string code, string state)
		{
			// Find the session with matching state
			var session = _sessions.Values.FirstOrDefault(s => s.State == state);
			if (session == null)
			{
				throw new UnauthorizedAccessException("Invalid state parameter");
			}

			// Complete the waiting task with the authorization code
			session.TaskCompletionSource.SetResult(code);

			return session.SessionId;
		}

		public AutomationStatus GetSessionStatus(string sessionId)
		{
			if (!_sessions.TryGetValue(sessionId, out var session))
			{
				return new AutomationStatus
				{
					SessionId = sessionId,
					Status = "Session not found",
					IsCompleted = true,
					Error = "Invalid session ID"
				};
			}

			return new AutomationStatus
			{
				SessionId = sessionId,
				Status = session.Status,
				IsCompleted = session.IsCompleted,
				Error = session.Error,
				AccessToken = session.AccessToken,
				IdToken = session.IdToken,
				UserProfile = session.UserProfile
			};
		}

		private async Task<TokenResponse> ExchangeCodeForTokenAsync(string code)
		{
			var values = new Dictionary<string, string>
			{
				{ "grant_type", "authorization_code" },
				{ "code", code },
				{ "redirect_uri", _redirectUri },
				{ "client_id", _clientId },
				{ "client_secret", _clientSecret }
			};

			var content = new FormUrlEncodedContent(values);
			var response = await _httpClient.PostAsync("https://www.linkedin.com/oauth/v2/accessToken", content);

			if (!response.IsSuccessStatusCode)
			{
				var errorContent = await response.Content.ReadAsStringAsync();
				throw new HttpRequestException($"Token exchange failed: {errorContent}");
			}

			var json = await response.Content.ReadAsStringAsync();
			var doc = JsonDocument.Parse(json);
			var root = doc.RootElement;

			return new TokenResponse
			{
				AccessToken = root.GetProperty("access_token").GetString(),
				TokenType = root.GetProperty("token_type").GetString(),
				ExpiresIn = root.GetProperty("expires_in").GetInt32(),
				IdToken = root.TryGetProperty("id_token", out var idToken) ? idToken.GetString() : null,
				Scope = root.TryGetProperty("scope", out var scope) ? scope.GetString() : null
			};
		}

		//private async Task<UserProfile> GetUserProfileAsync(string accessToken)
		//{
		//	using var client = new HttpClient();
		//	client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

		//	var response = await client.GetAsync("https://api.linkedin.com/v2/userinfo");

		//	if (!response.IsSuccessStatusCode)
		//	{
		//		var errorContent = await response.Content.ReadAsStringAsync();
		//		throw new HttpRequestException($"Failed to get user info: {errorContent}");
		//	}

		//	var json = await response.Content.ReadAsStringAsync();
		//	var doc = JsonDocument.Parse(json);
		//	var root = doc.RootElement;

		//	return new UserProfile
		//	{
		//		Sub = root.TryGetProperty("sub", out var sub) ? sub.GetString() : null,
		//		Name = root.TryGetProperty("name", out var name) ? name.GetString() : null,
		//		GivenName = root.TryGetProperty("given_name", out var givenName) ? givenName.GetString() : null,
		//		FamilyName = root.TryGetProperty("family_name", out var familyName) ? familyName.GetString() : null,
		//		Email = root.TryGetProperty("email", out var email) ? email.GetString() : null,
		//		EmailVerified = root.TryGetProperty("email_verified", out var emailVerified) && emailVerified.GetBoolean(),
		//		Picture = root.TryGetProperty("picture", out var picture) ? picture.GetString() : null,
		//		Locale = root.TryGetProperty("locale", out var locale) ? locale.GetString() : null
		//	};
		//}
		private async Task<UserProfile> GetUserProfileAsync(string accessToken)
		{
			using var client = new HttpClient();
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var response = await client.GetAsync("https://api.linkedin.com/v2/userinfo");

			if (!response.IsSuccessStatusCode)
			{
				var errorContent = await response.Content.ReadAsStringAsync();
				throw new HttpRequestException($"Failed to get user info: {errorContent}");
			}

			var json = await response.Content.ReadAsStringAsync();
			Console.WriteLine($"LinkedIn API Response: {json}"); // Debug log

			try
			{
				var doc = JsonDocument.Parse(json);
				var root = doc.RootElement;

				return new UserProfile
				{
					Sub = GetJsonStringValue(root, "sub"),
					Name = GetJsonStringValue(root, "name"),
					GivenName = GetJsonStringValue(root, "given_name"),
					FamilyName = GetJsonStringValue(root, "family_name"),
					Email = GetJsonStringValue(root, "email"),
					EmailVerified = GetJsonBoolValue(root, "email_verified"),
					Picture = GetJsonStringValue(root, "picture"),
					Locale = GetJsonStringValue(root, "locale")
				};
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error parsing user profile: {ex.Message}");
				Console.WriteLine($"Raw JSON: {json}");

				// Return a basic profile with available info from ID token if parsing fails
				return new UserProfile
				{
					Name = "Profile parsing failed - check logs",
					Email = "Check ID token for user info"
				};
			}
		}

		// Helper methods to safely extract JSON values
		private static string? GetJsonStringValue(JsonElement element, string propertyName)
		{
			if (element.TryGetProperty(propertyName, out var property))
			{
				return property.ValueKind switch
				{
					JsonValueKind.String => property.GetString(),
					JsonValueKind.True => "true",
					JsonValueKind.False => "false",
					JsonValueKind.Number => property.GetRawText(),
					JsonValueKind.Null => null,
					_ => property.GetRawText()
				};
			}
			return null;
		}

		private static bool GetJsonBoolValue(JsonElement element, string propertyName)
		{
			if (element.TryGetProperty(propertyName, out var property))
			{
				return property.ValueKind switch
				{
					JsonValueKind.True => true,
					JsonValueKind.False => false,
					JsonValueKind.String => bool.TryParse(property.GetString(), out var result) && result,
					_ => false
				};
			}
			return false;
		}
	}
}
