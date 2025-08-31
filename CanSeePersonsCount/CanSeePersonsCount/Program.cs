namespace CanSeePersonsCount
{
	public class Solution
	{
		public int[] CanSeePersonsCount(int[] heights)
		{
			int n = heights.Length;
			int[] answer = new int[n];
			Stack<int> stack = new Stack<int>(); // store indices of people to the right

			// Traverse from right to left
			for (int i = n - 1; i >= 0; i--)
			{
				// Pop all shorter people → I can see them
				while (stack.Count > 0 && heights[i] > heights[stack.Peek()])
				{
					answer[i]++;
					stack.Pop();
				}

				// If there’s still someone taller, I can see them but no one behind them
				if (stack.Count > 0)
				{
					answer[i]++;
				}

				// Add myself into the stack for people on the left
				stack.Push(i);
			}

			return answer;
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			var solution = new Solution();

			// Example 1
			int[] heights1 = { 10, 6, 8, 5, 11, 9 };
			int[] result1 = solution.CanSeePersonsCount(heights1);
			Console.WriteLine("Example 1:");
			Console.WriteLine("Input:  [10, 6, 8, 5, 11, 9]");
			Console.WriteLine("Output: [" + string.Join(", ", result1) + "]\n");

			// Example 2
			int[] heights2 = { 5, 1, 2, 3, 10 };
			int[] result2 = solution.CanSeePersonsCount(heights2);
			Console.WriteLine("Example 2:");
			Console.WriteLine("Input:  [5, 1, 2, 3, 10]");
			Console.WriteLine("Output: [" + string.Join(", ", result2) + "]\n");

			// Example 3 (your custom order)
			int[] heights3 = { 10, 6, 5, 8, 11, 9 };
			int[] result3 = solution.CanSeePersonsCount(heights3);
			Console.WriteLine("Example 3:");
			Console.WriteLine("Input:  [10, 6, 5, 8, 11, 9]");
			Console.WriteLine("Output: [" + string.Join(", ", result3) + "]\n");

			Console.WriteLine("Press any key to exit...");
			Console.ReadKey();
		}
	}
}
