using System;
using System.Collections.Generic;
using System.Linq;

namespace MergeIntervalsApp
{
	class Program
	{
		static void Main(string[] args)
		{
			// Example input (rectangular array)
			int[,] intervals = new int[,]
			{
				{1, 3},
				{2, 6},
				{8, 10},
				{15, 18}
			};

			int result = MergeIntervalsLength(intervals);

			Console.WriteLine("Number of merged intervals: " + result);
			// Expected output: 3  (i.e. [[1,6],[8,10],[15,18]])
		}

		static int MergeIntervalsLength(int[,] intervals)
		{
			int n = intervals.GetLength(0);

			// Convert rectangular array -> list of int[]
			var list = new List<int[]>();
			for (int i = 0; i < n; i++)
			{
				list.Add(new int[] { intervals[i, 0], intervals[i, 1] });
			}

			// Sort by start time
			list = list.OrderBy(x => x[0]).ToList();

			// Merge process
			var merged = new List<int[]>();
			foreach (var interval in list)
			{
				if (merged.Count == 0 || merged.Last()[1] < interval[0])
				{
					// no overlap
					merged.Add(interval);
				}
				else
				{
					// overlap -> merge
					merged.Last()[1] = Math.Max(merged.Last()[1], interval[1]);
				}
			}

			// Return only the length
			return merged.Count;
		}
	}
}
