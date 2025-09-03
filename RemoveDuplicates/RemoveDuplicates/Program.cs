namespace RemoveDuplicates
{
	public class Solution
	{
		public int RemoveDuplicates(int[] nums)
		{
			if (nums.Length == 0) return 0;

			int k = 1; // index for placing unique elements

			for (int i = 1; i < nums.Length; i++)
			{
				if (nums[i] != nums[i - 1])
				{
					nums[k] = nums[i];
					k++;
				}
			}

			return k;
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			Solution solution = new Solution();

			// Example 1
			int[] nums1 = { 1, 1, 2 };
			int k1 = solution.RemoveDuplicates(nums1);
			Console.WriteLine("k = " + k1);
			Console.WriteLine("nums = " + string.Join(",", FormatArray(nums1, k1)));

			// Example 2
			int[] nums2 = { 0, 0, 1, 1, 1, 2, 2, 3, 3, 4 };
			int k2 = solution.RemoveDuplicates(nums2);
			Console.WriteLine("\nk = " + k2);
			Console.WriteLine("nums = " + string.Join(",", FormatArray(nums2, k2)));

			Console.ReadLine();
		}

		// Helper to replace extra elements with "_"
		static string[] FormatArray(int[] nums, int k)
		{
			string[] result = new string[nums.Length];
			for (int i = 0; i < nums.Length; i++)
			{
				if (i < k)
					result[i] = nums[i].ToString();
				else
					result[i] = "_";
			}
			return result;
		}
	}
}
