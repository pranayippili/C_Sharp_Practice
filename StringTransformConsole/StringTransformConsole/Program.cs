using System;
using System.Text;

namespace StringTransformConsole
{
	public class Solution
	{
		/// <summary>
		/// Converts string to integer by replacing letters with positions,
		/// then transforms by summing digits k times.
		/// Optimized to handle large inputs without overflow.
		/// </summary>
		/// <param name="s">String of lowercase English letters</param>
		/// <param name="k">Number of digit sum transformations</param>
		/// <returns>Final integer after k transformations</returns>
		public int GetLucky(string s, int k)
		{
			// Step 1: Convert letters to positions and calculate first digit sum directly
			// This avoids overflow from parsing very large concatenated numbers
			int digitSum = 0;

			foreach (char c in s)
			{
				int position = c - 'a' + 1; // a=1, b=2, ..., z=26

				// Add each digit of the position to our sum
				while (position > 0)
				{
					digitSum += position % 10;
					position /= 10;
				}
			}


			// We've completed the first transformation, so decrement k
			k--;

			// Step 2: Apply remaining k transformations
			int current = digitSum;
			for (int i = 0; i < k; i++)
			{
				int sum = 0;
				while (current > 0)
				{
					sum += current % 10;
					current /= 10;
				}
				current = sum;
			}

			return current;
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("=== String to Integer Transformation Console ===\n");

			Solution solution = new Solution();

			// Test with provided examples
			Console.WriteLine("Testing with provided examples:");
			RunTest(solution, "iiii", 1, 36);
			RunTest(solution, "leetcode", 2, 6);
			RunTest(solution, "zbax", 2, 8);

			Console.WriteLine("\nTesting edge cases:");
			RunTest(solution, "a", 1, 1);
			RunTest(solution, "z", 1, 8); // z=26, 2+6=8
			RunTest(solution, "abcdefghijklmnopqrstuvwxyz", 1); // All letters

			// Test large input that would cause overflow with naive approach
			Console.WriteLine("\nTesting large inputs:");
			string largeInput = new string('z', 50);
			RunTest(solution, $"50 z's", largeInput, 3);

			// Interactive mode
			Console.WriteLine("\n" + new string('=', 50));
			Console.WriteLine("Interactive Mode (type 'exit' to quit):");

			while (true)
			{
				Console.Write("\nEnter string (lowercase letters): ");
				string input = Console.ReadLine();

				if (input?.ToLower() == "exit")
					break;

				if (string.IsNullOrEmpty(input) || !IsValidInput(input))
				{
					Console.WriteLine("Invalid input! Please enter lowercase letters only.");
					continue;
				}

				Console.Write("Enter k (number of transformations): ");
				if (!int.TryParse(Console.ReadLine(), out int k) || k < 1 || k > 10)
				{
					Console.WriteLine("Invalid k! Please enter a number between 1 and 10.");
					continue;
				}

				try
				{
					int result = solution.GetLucky(input, k);
					Console.WriteLine($"Result: {result}");

					// Show step-by-step for small inputs
					if (input.Length <= 10)
					{
						ShowStepByStep(input, k);
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Error: {ex.Message}");
				}
			}

			Console.WriteLine("\nGoodbye!");
		}

		static void RunTest(Solution solution, string s, int k, int? expected = null)
		{
			RunTest(solution, s, s, k, expected);
		}

		static void RunTest(Solution solution, string description, string s, int k, int? expected = null)
		{
			try
			{
				int result = solution.GetLucky(s, k);
				string status = expected.HasValue ?
					(result == expected.Value ? "✓ PASS" : "✗ FAIL") : "DONE";

				Console.WriteLine($"Input: \"{description}\", k={k} → Result: {result} {status}");

				if (expected.HasValue && result != expected.Value)
				{
					Console.WriteLine($"  Expected: {expected.Value}, Got: {result}");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Input: \"{description}\", k={k} → ERROR: {ex.Message}");
			}
		}

		static bool IsValidInput(string s)
		{
			foreach (char c in s)
			{
				if (c < 'a' || c > 'z')
					return false;
			}
			return true;
		}

		static void ShowStepByStep(string s, int k)
		{
			Console.WriteLine("\nStep-by-step breakdown:");

			// Show conversion
			StringBuilder conversion = new StringBuilder();
			Console.Write("Convert: ");
			foreach (char c in s)
			{
				int pos = c - 'a' + 1;
				Console.Write($"{c}={pos} ");
				conversion.Append(pos);
			}
			Console.WriteLine($"→ \"{conversion}\"");

			// Show transformations
			string current = conversion.ToString();
			for (int i = 1; i <= k; i++)
			{
				int sum = 0;
				StringBuilder calc = new StringBuilder();
				foreach (char digit in current)
				{
					int d = digit - '0';
					sum += d;
					if (calc.Length > 0) calc.Append("+");
					calc.Append(d);
				}

				Console.WriteLine($"Transform #{i}: {current} → {calc} = {sum}");
				current = sum.ToString();
			}
		}
	}
}

/* 
Key Optimizations:

1. **Overflow Prevention**: Instead of building a huge concatenated string and parsing it,
   we calculate the first digit sum directly during the conversion phase.

2. **Direct Digit Sum**: For each character position, we sum its digits immediately
   rather than string concatenation.

3. **Memory Efficiency**: No large string building or StringBuilder for concatenation.

4. **Performance**: O(n + k*d) time where n=string length, k=transformations, d=avg digits.
   Space complexity is O(1) - constant extra space.

5. **Robust Error Handling**: Handles all edge cases including maximum constraints.

This solution can handle the maximum constraint of 100 'z' characters without any overflow issues.
*/