using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDelegatesLearning.Examples
{
	public class BasicDelegates
	{
		// Declare delegate types
		public delegate int MathOperation(int x, int y);
		public delegate void LogHandler(string message);

		public static void RunExamples()
		{
			Console.WriteLine("=== Basic Delegates Example ===");

			// Method assignment
			MathOperation add = Add;
			MathOperation subtract = Subtract;

			Console.WriteLine($"5 + 3 = {add(5, 3)}");
			Console.WriteLine($"5 - 3 = {subtract(5, 3)}");

			// Anonymous method
			MathOperation multiply = delegate (int x, int y) { return x * y; };
			Console.WriteLine($"5 * 3 = {multiply(5, 3)}");

			// Lambda expression
			MathOperation divide = (x, y) => x / y;
			Console.WriteLine($"6 / 3 = {divide(6, 3)}");

			// Delegate as parameter
			ProcessOperation(10, 5, add);
			ProcessOperation(10, 5, subtract);
		}

		public static int Add(int a, int b) => a + b;
		public static int Subtract(int a, int b) => a - b;

		public static void ProcessOperation(int x, int y, MathOperation operation)
		{
			int result = operation(x, y);
			Console.WriteLine($"Operation result: {result}");
		}
	}
}
