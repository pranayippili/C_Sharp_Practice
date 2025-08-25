using System;

namespace ThreeSumClosestApp
{
	public class Solution
	{
		public int ThreeSumClosest(int[] nums, int target)
		{
			Array.Sort(nums); // sort array
			int closestSum = nums[0] + nums[1] + nums[2]; // initial guess

			for (int i = 0; i < nums.Length - 2; i++)
			{
				int left = i + 1;
				int right = nums.Length - 1;

				while (left < right)
				{
					int sum = nums[i] + nums[left] + nums[right];

					// Update closest if needed
					if (Math.Abs(sum - target) < Math.Abs(closestSum - target))
					{
						closestSum = sum;
					}

					if (sum < target)
					{
						left++; // need bigger sum
					}
					else if (sum > target)
					{
						right--; // need smaller sum
					}
					else
					{
						return sum; // exact match found
					}
				}
			}

			return closestSum;
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			Solution sol = new Solution();

			// Example input
			int[] nums = { -1, 2, 1, -4 };
			int target = 1;

			int result = sol.ThreeSumClosest(nums, target);

			Console.WriteLine("Input: nums = [{0}], target = {1}", string.Join(", ", nums), target);
			Console.WriteLine("Closest sum: " + result);

			// Wait for keypress before closing
			Console.WriteLine("\nPress any key to exit...");
			Console.ReadKey();
		}
	}
}
