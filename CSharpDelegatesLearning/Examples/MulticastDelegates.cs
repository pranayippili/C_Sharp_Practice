using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDelegatesLearning.Examples
{
	public class MulticastDelegates
	{
		public delegate void NotificationHandler(string message);

		public static void RunExamples()
		{
			Console.WriteLine("\n=== Multicast Delegates Example ===");

			// Single delegate
			NotificationHandler handler = EmailNotification;
			handler("Single notification");

			// Multicast delegate
			handler += SmsNotification;
			handler += PushNotification;
			handler += LogNotification;

			Console.WriteLine("\nMulticast notification:");
			handler("Important message!");

			// Remove delegate
			handler -= SmsNotification;
			Console.WriteLine("\nAfter removing SMS:");
			handler("Another message");

			// Null check
			handler = null;
			handler?.Invoke("Safe call");
		}

		public static void EmailNotification(string message)
		{
			Console.WriteLine($"📧 Email: {message}");
		}

		public static void SmsNotification(string message)
		{
			Console.WriteLine($"📱 SMS: {message}");
		}

		public static void PushNotification(string message)
		{
			Console.WriteLine($"🔔 Push: {message}");
		}

		public static void LogNotification(string message)
		{
			Console.WriteLine($"📝 Log: {message}");
		}
	}
}
