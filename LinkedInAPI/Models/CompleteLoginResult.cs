namespace LinkedInAPI.Models
{
	public class CompleteLoginResult
	{
		public string SessionId { get; set; }
		public string AuthorizationUrl { get; set; }
		public string[] Instructions { get; set; }
		public string StatusCheckUrl { get; set; }
	}
}
