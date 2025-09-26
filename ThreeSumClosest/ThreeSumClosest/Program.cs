using System;

namespace ThreeSumClosest
{
	public class Solution
	{
		public int ThreeSumClosest(int[] nums, int target)
		{

			// Sort the array first
			Array.Sort(nums);

			int n = nums.Length;
			int closestSum = nums[0] + nums[1] + nums[2];
			int minDiff = Math.Abs(target - closestSum);

			Console.WriteLine($"Initial closest sum: {closestSum}, difference: {minDiff}");
			Console.WriteLine($"Sorted array: [{string.Join(", ", nums)}]");
			Console.WriteLine();

			// Fix the first element and use two pointers for the rest
			for (int i = 0; i < n - 2; i++)
			{
				int left = i + 1;
				int right = n - 1;

				Console.WriteLine($"Iteration {i + 1}: Fixed element nums[{i}] = {nums[i]}");

				while (left < right)
				{
					int currentSum = nums[i] + nums[left] + nums[right];
					int currentDiff = Math.Abs(target - currentSum);

					Console.WriteLine($"  Checking: nums[{i}] + nums[{left}] + nums[{right}] = {nums[i]} + {nums[left]} + {nums[right]} = {currentSum}");
					Console.WriteLine($"  Difference from target ({target}): {currentDiff}");

					// Update closest sum if current sum is closer to target
					if (currentDiff < minDiff)
					{
						minDiff = currentDiff;
						closestSum = currentSum;
						Console.WriteLine($"  *** NEW CLOSEST SUM: {closestSum} (difference: {minDiff}) ***");
					}

					// If we found exact match, return immediately
					if (currentSum == target)
					{
						Console.WriteLine($"  *** EXACT MATCH FOUND! Returning {currentSum} ***");
						return currentSum;
					}
					// Move pointers based on comparison with target
					else if (currentSum < target)
					{
						Console.WriteLine($"  Sum too small, moving left pointer: {left} -> {left + 1}");
						left++;  // Need larger sum
					}
					else
					{
						Console.WriteLine($"  Sum too large, moving right pointer: {right} -> {right - 1}");
						right--; // Need smaller sum
					}
					Console.WriteLine();
				}
			}

			return closestSum;
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			Solution solution = new Solution();

			// Test cases
			var testCases = new[]
			{
				new { nums = new int[] { -1, 2, 1, -4 }, target = 1 },
				new { nums = new int[] { 0, 0, 0 }, target = 1 },
				new { nums = new int[] { 1, 1, 1, 0 }, target = -100 },
				new { nums = new int[] { 4, 0, 5, -5, 3, 3, 0, -4, -5 }, target = -2 }
			};

			for (int i = 0; i < testCases.Length; i++)
			{
				var testCase = testCases[i];

				Console.WriteLine("=".PadRight(60, '='));
				Console.WriteLine($"TEST CASE {i + 1}");
				Console.WriteLine("=".PadRight(60, '='));
				Console.WriteLine($"Input array: [{string.Join(", ", testCase.nums)}]");
				Console.WriteLine($"Target: {testCase.target}");
				Console.WriteLine();

				int result = solution.ThreeSumClosest(testCase.nums, testCase.target);

				Console.WriteLine("=".PadRight(60, '='));
				Console.WriteLine($"FINAL RESULT: {result}");
				Console.WriteLine($"Distance from target: {Math.Abs(testCase.target - result)}");
				Console.WriteLine("=".PadRight(60, '='));
				Console.WriteLine();

				if (i < testCases.Length - 1)
				{
					Console.WriteLine("Press any key to continue to next test case...");
					Console.ReadKey();
					Console.Clear();
				}
			}

			Console.WriteLine("\nPress any key to exit...");
			Console.ReadKey();
		}
	}
}