using CSharpDelegatesLearning.Examples;
using System;

namespace CSharpDelegatesLearning
{
    public class Program
    {
        static void Main(string[] args)
        {
			Console.WriteLine("C# Delegates Learning Examples");
			Console.WriteLine("===============================");

			try
			{
				BasicDelegates.RunExamples();
				MulticastDelegates.RunExamples();
				BuiltInDelegates.RunExamples();
				EventHandling.RunExamples();
				CallbackPatterns.RunExamples();
				//AdvancedDelegates.RunExamples();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
			}

			Console.WriteLine("\nPress any key to exit...");
			Console.ReadKey();
		}
    }
}
