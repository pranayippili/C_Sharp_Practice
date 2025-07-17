using System;

class Program
{
	static void Main(string[] args)
	{
		if (args.Length == 0)
		{
			Console.WriteLine("No command-line arguments were provided.");
			return;
		}

		double total = 0;
		Console.WriteLine("Numbers passed as command-line arguments:");
		foreach (string arg in args)
		{
			if (double.TryParse(arg, out double number))
			{
				Console.WriteLine(number);
				total += number;
			}
			else
			{
				Console.WriteLine($"Invalid input skipped: {arg}");
			}
		}

		Console.WriteLine($"\nTotal of valid numbers: {total}");
	}
}
