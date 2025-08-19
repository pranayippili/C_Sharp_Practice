namespace LinkedInAPI.Models
{
	public class AutomationSession
	{
		public string SessionId { get; set; }
		public string State { get; set; }
		public DateTime CreatedAt { get; set; }
		public string Status { get; set; }
		public bool IsCompleted { get; set; }
		public string Error { get; set; }
		public string AccessToken { get; set; }
		public string IdToken { get; set; }
		public UserProfile UserProfile { get; set; }
		public TaskCompletionSource<string> TaskCompletionSource { get; set; }
	}
}
