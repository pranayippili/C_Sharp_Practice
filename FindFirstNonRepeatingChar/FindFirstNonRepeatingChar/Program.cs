namespace FindFirstNonRepeatingChar
{
	class Program
	{
		static void Main()
		{
			// Hardcoded string
			string input = "swiss";

			// Call method to find first non-repeating character
			char result = FindFirstNonRepeatingChar(input);

			// Print result
			if (result == '-')
			{
				Console.WriteLine("-1"); // if no non-repeating character found
			}
			else
			{
				Console.WriteLine("First non-repeating character: " + result);
			}
		}

		// Method to find first non-repeating character without using Dictionary/Map
		static char FindFirstNonRepeatingChar(string str)
		{
			for (int i = 0; i < str.Length; i++)
			{
				char ch = str[i];
				bool isRepeating = false;

				// Check if character appears anywhere else
				for (int j = 0; j < str.Length; j++)
				{
					if (i != j && ch == str[j])
					{
						isRepeating = true;
						break;
					}
				}

				// If not repeating, return immediately
				if (!isRepeating)
				{
					return ch;
				}
			}

			// If no unique char found, return special symbol
			return '-';
		}
	}
}
