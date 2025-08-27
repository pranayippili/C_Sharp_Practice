using System;
using System.Collections.Generic;

namespace PhoneLetterCombinationsApp
{
	class Solution
	{
		public IList<string> LetterCombinations(string digits)
		{
			List<string> result = new List<string>();
			if (string.IsNullOrEmpty(digits)) return result; // handle empty input

			Dictionary<char, string> phoneMap = new Dictionary<char, string>
			{
				{ '2', "abc" },
				{ '3', "def" },
				{ '4', "ghi" },
				{ '5', "jkl" },
				{ '6', "mno" },
				{ '7', "pqrs" },
				{ '8', "tuv" },
				{ '9', "wxyz" }
			};

			void Backtrack(int index, string current)
			{
				if (index == digits.Length)
				{
					result.Add(current);
					return;
				}

				string letters = phoneMap[digits[index]];
				foreach (char letter in letters)
				{
					Backtrack(index + 1, current + letter);
				}
			}

			Backtrack(0, "");
			return result;
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			Console.Write("Enter digits (2-9): ");
			string digits = Console.ReadLine();

			Solution sol = new Solution();
			var combos = sol.LetterCombinations(digits);

			Console.WriteLine("Possible combinations:");
			Console.WriteLine(string.Join(", ", combos));

			Console.WriteLine("\nPress any key to exit...");
			Console.ReadKey();
		}
	}
}
