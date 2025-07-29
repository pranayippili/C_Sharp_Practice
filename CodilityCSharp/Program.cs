namespace CodilityCSharp
{
	internal class Program
	{
		public static int Solution(int Y, string A, string B, string W)
		{
			Dictionary<string, int> monthMap = new Dictionary<string, int>()
			{
				{"January", 0}, {"February", 1}, {"March", 2}, {"April", 3},
				{"May", 4}, {"June", 5}, {"July", 6}, {"August", 7},
				{"September", 8}, {"October", 9}, {"November", 10}, {"December", 11}

			};

			Dictionary<string, int> dayMap = new Dictionary<string, int>()
			{
				{"Monday", 0}, {"Tuesday", 1}, {"Wednesday", 2}, {"Thursday", 3},
				{"Friday", 4}, {"Saturday", 5}, {"Sunday", 6}
			};

			int[] daysInMonth = {31, 28,31, 30, 31, 30, 31, 31, 30, 31, 30, 31
			};

			if (Y % 4 == 0) daysInMonth[1] = 29; // Adjust for leap year

			int startMonth = monthMap[A];
			int endMonth = monthMap[B];

			int startDayofVacation = 0;
			for (int i=0; i<startMonth; i++)
			{
				startDayofVacation += daysInMonth[i];
			}

			int jan1Weekday = dayMap[W];

			int startDayWeekday = (jan1Weekday + startDayofVacation) % 7;

			int offsetToMonday = (7 - startDayWeekday) % 7;
			int firstMonday = startDayofVacation + offsetToMonday;

			int endDayofVacation = 0;
			for (int i = 0; i <= endMonth; i++)
			{
				endDayofVacation += daysInMonth[i];
			}
			int lastDayofVacation = endDayofVacation - 1;

			int lastDayWeekday = (jan1Weekday + lastDayofVacation) % 7;

			int offsetToSunday = (lastDayWeekday + 1) % 7;

			int lastSunday = lastDayofVacation - offsetToSunday;

			if (firstMonday > lastSunday)
				return 0;

			// Count full weeks
			return (lastSunday - firstMonday) / 7 + 1;

		}
		static void Main(string[] args)
		{

			Console.WriteLine(Solution(2014, "April", "May", "Wednesday")); // Output: 7
		}
	}
}
