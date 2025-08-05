using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace LinkedInProfile
{
	class Program
	{
		static readonly string clientId = "86u06ez5d9k7kd";
		static readonly string clientSecret = "WPL_AP1.vXKkNxFz5wK0ivCw.Gi7ivw==";
		static readonly string redirectUri = "http://localhost:5000/callback";
		static readonly string scope = "openid profile email";

		static async Task Main(string[] args)
		{
			string state = Guid.NewGuid().ToString();
			string authUrl = $"https://www.linkedin.com/oauth/v2/authorization?response_type=code&client_id={clientId}&redirect_uri={Uri.EscapeDataString(redirectUri)}&state={state}&scope={Uri.EscapeDataString(scope)}&prompt=login";


			Console.WriteLine("Opening browser for LinkedIn login...");
			System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
			{
				FileName = authUrl,
				UseShellExecute = true
			});

			Console.WriteLine("Waiting for LinkedIn redirect...");
			string code = await WaitForCodeAsync();
			Console.WriteLine($"Authorization code received: {code}");

			var tokenResponse = await GetTokenResponseAsync(code);
			string accessToken = tokenResponse.GetProperty("access_token").GetString();
			string idToken = tokenResponse.TryGetProperty("id_token", out var id) ? id.GetString() : "Not returned";

			Console.WriteLine($"\nAccess Token: {accessToken}");
			Console.WriteLine($"\nID Token: {idToken}");

			string userInfo = await GetUserInfoAsync(accessToken);
			Console.WriteLine("\nUser Info:");
			Console.WriteLine(userInfo);

			Console.WriteLine("Logging out of LinkedIn...");
			System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
			{
				FileName = "https://www.linkedin.com/m/logout/",
				UseShellExecute = true
			});

		}

		static async Task<string> WaitForCodeAsync()
		{
			var listener = new HttpListener();
			listener.Prefixes.Add("http://localhost:5000/callback/");
			listener.Start();

			var context = await listener.GetContextAsync();
			var request = context.Request;
			var response = context.Response;

			string code = request.QueryString["code"];
			string htmlResponse = "<html><body><h2>Login successful. You can close this window.</h2></body></html>";
			byte[] buffer = Encoding.UTF8.GetBytes(htmlResponse);

			response.ContentLength64 = buffer.Length;
			await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
			response.OutputStream.Close();
			listener.Stop();

			return code;
		}

		static async Task<JsonElement> GetTokenResponseAsync(string code)
		{
			using var client = new HttpClient();
			var values = new Dictionary<string, string>
		{
			{ "grant_type", "authorization_code" },
			{ "code", code },
			{ "redirect_uri", redirectUri },
			{ "client_id", clientId },
			{ "client_secret", clientSecret }
		};

			var content = new FormUrlEncodedContent(values);
			var response = await client.PostAsync("https://www.linkedin.com/oauth/v2/accessToken", content);
			var json = await response.Content.ReadAsStringAsync();
			var doc = JsonDocument.Parse(json);
			return doc.RootElement;
		}

		static async Task<string> GetUserInfoAsync(string accessToken)
		{
			using var client = new HttpClient();
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var response = await client.GetAsync("https://api.linkedin.com/v2/userinfo");
			return await response.Content.ReadAsStringAsync();
		}
	}

}
