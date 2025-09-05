/******************************************************************************

Welcome to GDB Online.
GDB online is an online compiler and debugger tool for C, C++, Python, Java, PHP, Ruby, Perl,
C#, OCaml, VB, Swift, Pascal, Fortran, Haskell, Objective-C, Assembly, HTML, CSS, JS, SQLite, Prolog.
Code, Compile, Run and Debug online from anywhere in world.

*******************************************************************************/
using System;
class HelloWorld {
	private static string LongestCommonSubstring(string[] input)
	{
		if(input == null || input.Length == 0) return "";

		string shortest = input[0];

		foreach(string item in input)
		{
			if(item.Length < shortest.Length)
			{
				shortest = item;
			}
		}
		for(int end = shortest.Length; end > 0; end--)
		{
			for(int start = 0; start <= shortest.Length - end; start++)
			{
				bool found = true;
				string sub = shortest.Substring(start, end);
				foreach(string str in input)
				{
					if(!str.Contains(sub))
					{
						found = false;
						break;
					}
				}
				if(found) {
					return sub;
				}
			}
		}
		return "";
	}
	static void Main() {
		string[] arr = { "I am from hyderabad", "I love hyderabad", "I like hyderabad" };
		string result = LongestCommonSubstring(arr);
		Console.WriteLine(result); // Output: hyderabad
	}
}