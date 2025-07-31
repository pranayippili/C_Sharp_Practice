using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDelegatesLearning.Examples
{
	public class EventHandling
	{
		// Event declaration
		public delegate void OrderEventHandler(string orderId, decimal amount);
		public static event OrderEventHandler OrderProcessed;

		public static void RunExamples()
		{
			Console.WriteLine("\n=== Event Handling Example ===");

			// Subscribe to event
			OrderProcessed += OnOrderProcessed;
			OrderProcessed += OnOrderLogged;
			OrderProcessed += OnOrderNotification;

			// Trigger event
			ProcessOrder("ORD-001", 99.99m);
			ProcessOrder("ORD-002", 149.50m);

			// Unsubscribe
			OrderProcessed -= OnOrderLogged;
			Console.WriteLine("\nAfter unsubscribing logger:");
			ProcessOrder("ORD-003", 75.25m);
		}

		public static void ProcessOrder(string orderId, decimal amount)
		{
			Console.WriteLine($"Processing order {orderId} for ${amount}");

			// Simulate order processing
			System.Threading.Thread.Sleep(100);

			// Trigger event
			OrderProcessed?.Invoke(orderId, amount);
		}

		public static void OnOrderProcessed(string orderId, decimal amount)
		{
			Console.WriteLine($"✅ Order {orderId} processed successfully");
		}

		public static void OnOrderLogged(string orderId, decimal amount)
		{
			Console.WriteLine($"📝 Logged: Order {orderId} - ${amount}");
		}

		public static void OnOrderNotification(string orderId, decimal amount)
		{
			Console.WriteLine($"📧 Notification sent for order {orderId}");
		}
	}
}
