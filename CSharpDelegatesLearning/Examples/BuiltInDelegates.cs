using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDelegatesLearning.Examples
{
	public class BuiltInDelegates
	{
		public static void RunExamples()
		{
			Console.WriteLine("\n=== Built-in Delegates Example ===");

			ActionExamples();
			FuncExamples();
			PredicateExamples();
		}

		public static void ActionExamples()
		{
			Console.WriteLine("\n--- Action Examples ---");

			// Action with no parameters
			Action greet = () => Console.WriteLine("Hello World!");
			greet();

			// Action with parameters
			Action<string> printMessage = message => Console.WriteLine($"Message: {message}");
			printMessage("Learning delegates!");

			// Action with multiple parameters
			Action<string, int> printInfo = (name, age) =>
				Console.WriteLine($"Name: {name}, Age: {age}");
			printInfo("John", 25);

			// Action array
			Action[] actions = {
				() => Console.WriteLine("Task 1"),
				() => Console.WriteLine("Task 2"),
				() => Console.WriteLine("Task 3")
			};

			foreach (var action in actions)
				action();
		}

		public static void FuncExamples()
		{
			Console.WriteLine("\n--- Func Examples ---");

			// Func with return value
			Func<int> getRandom = () => new Random().Next(1, 100);
			Console.WriteLine($"Random number: {getRandom()}");

			// Func with parameters
			Func<int, int, int> multiply = (x, y) => x * y;
			Console.WriteLine($"5 * 3 = {multiply(5, 3)}");

			// Func with complex logic
			Func<string, bool> isValidEmail = email =>
				!string.IsNullOrEmpty(email) && email.Contains("@");
			Console.WriteLine($"Is 'test@email.com' valid? {isValidEmail("test@email.com")}");

			// Using Func with collections
			List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
			Func<int, bool> isEven = x => x % 2 == 0;
			var evenNumbers = numbers.Where(isEven).ToList();
			Console.WriteLine($"Even numbers: {string.Join(", ", evenNumbers)}");
		}

		public static void PredicateExamples()
		{
			Console.WriteLine("\n--- Predicate Examples ---");

			List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

			// Predicate examples
			Predicate<int> isEven = x => x % 2 == 0;
			Predicate<int> isGreaterThan5 = x => x > 5;

			var evenNumbers = numbers.FindAll(isEven);
			var greaterThan5 = numbers.FindAll(isGreaterThan5);

			Console.WriteLine($"Even numbers: {string.Join(", ", evenNumbers)}");
			Console.WriteLine($"Greater than 5: {string.Join(", ", greaterThan5)}");

			// Exists and TrueForAll
			bool hasEven = numbers.Exists(isEven);
			bool allPositive = numbers.TrueForAll(x => x > 0);

			Console.WriteLine($"Has even numbers: {hasEven}");
			Console.WriteLine($"All positive: {allPositive}");
		}
	}
}
