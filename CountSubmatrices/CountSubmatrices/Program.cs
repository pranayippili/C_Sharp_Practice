namespace CountSubmatrices
{
	class Program
	{
		static void Main(string[] args)
		{
			// Example input
			int[,] grid = {
			{ 7, 6, 3 },
			{ 6, 6, 1 }
		};
			int k = 18;

			int result = CountSubmatrices(grid, k);
			Console.WriteLine("Output: " + result); // Expected: 4
		}

		public static int CountSubmatrices(int[,] grid, int k)
		{
			int m = grid.GetLength(0);
			int n = grid.GetLength(1);

			long[,] prefix = new long[m, n];
			int count = 0;

			for (int i = 0; i < m; i++)
			{
				for (int j = 0; j < n; j++)
				{
					long sum = grid[i, j];

					if (i > 0) sum += prefix[i - 1, j];
					if (j > 0) sum += prefix[i, j - 1];
					if (i > 0 && j > 0) sum -= prefix[i - 1, j - 1];

					prefix[i, j] = sum;

					if (sum <= k)
						count++;
				}
			}

			return count;
		}
	}
}
