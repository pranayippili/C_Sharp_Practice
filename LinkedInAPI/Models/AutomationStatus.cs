namespace LinkedInAPI.Models
{
	public class AutomationStatus
	{
		public string SessionId { get; set; }
		public string Status { get; set; }
		public bool IsCompleted { get; set; }
		public string Error { get; set; }
		public string AccessToken { get; set; }
		public string IdToken { get; set; }
		public UserProfile UserProfile { get; set; }
	}
}
