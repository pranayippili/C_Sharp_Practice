namespace LongestPalindromeApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Enter a string:");
			string input = Console.ReadLine();

			string longestPalindrome = LongestPalindrome(input);
			Console.WriteLine($"Longest Palindromic Substring: {longestPalindrome}");

			Console.WriteLine("\nPress any key to exit...");
			Console.ReadKey();
		}

		public static string LongestPalindrome(string s)
		{
			if (string.IsNullOrEmpty(s) || s.Length < 2)
				return s;

			int start = 0, maxLen = 1;

			for (int i = 0; i < s.Length; i++)
			{
				// Odd length palindrome
				ExpandFromCenter(s, i, i, ref start, ref maxLen);

				// Even length palindrome
				ExpandFromCenter(s, i, i + 1, ref start, ref maxLen);
			}

			return s.Substring(start, maxLen);
		}

		private static void ExpandFromCenter(string s, int left, int right, ref int start, ref int maxLen)
		{
			while (left >= 0 && right < s.Length && s[left] == s[right])
			{
				int len = right - left + 1;
				if (len > maxLen)
				{
					maxLen = len;
					start = left;
				}

				left--;
				right++;
			}
		}
	}
}
