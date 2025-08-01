using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDelegatesLearning.Examples
{
	public class CallbackPatterns
	{
		public static void RunExamples()
		{
			Console.WriteLine("\n=== Callback Patterns Example ===");

			SynchronousCallback();
			AsynchronousCallback();
		}

		public static void SynchronousCallback()
		{
			Console.WriteLine("\n--- Synchronous Callback ---");

			ProcessData("Sample data", OnSuccess, OnError);
			ProcessData("", OnSuccess, OnError); // This will trigger error
		}

		public static void AsynchronousCallback()
		{
			Console.WriteLine("\n--- Asynchronous Callback ---");

			ProcessDataAsync("Async data", OnSuccess, OnError);

			// Wait a bit for async operation
			Thread.Sleep(1500);
		}

		public static void ProcessData(string data, Action<string> onSuccess, Action<string> onError)
		{
			try
			{
				if (string.IsNullOrEmpty(data))
					throw new ArgumentException("Data cannot be empty");

				// Simulate processing
				Thread.Sleep(500);
				string result = $"Processed: {data}";
				onSuccess(result);
			}
			catch (Exception ex)
			{
				onError(ex.Message);
			}
		}

		public static async void ProcessDataAsync(string data, Action<string> onSuccess, Action<string> onError)
		{
			try
			{
				await Task.Run(() =>
				{
					Thread.Sleep(1000); // Simulate async work
					string result = $"Async processed: {data}";
					onSuccess(result);
				});
			}
			catch (Exception ex)
			{
				onError(ex.Message);
			}
		}

		public static void OnSuccess(string result)
		{
			Console.WriteLine($"✅ Success: {result}");
		}

		public static void OnError(string error)
		{
			Console.WriteLine($"❌ Error: {error}");
		}
	}
}
