namespace CountMergedRoutes
{
	internal class Program
	{
		private static int CountMergedRoutes(int[,] routes)
		{
			int rows = routes.GetLength(0);
			int columns = routes.GetLength(1);

			//sorting
			for (int i = 0; i < rows - 1; i++)
			{
				for (int j = i + 1; j < rows; j++)
				{
					if (routes[i, 0] > routes[j, 0])
					{
						int temp1 = routes[i, 0];
						int temp2 = routes[i, 1];
						routes[i, 0] = routes[j, 0];
						routes[i, 1] = routes[j, 1];
						routes[j, 0] = temp1;
						routes[j, 1] = temp2;
					}
				}
			}

			int answer = 0;
			int start = routes[0, 0];
			int end = routes[0, 1];
			for (int i = 1; i < rows; i++)
			{
				if (routes[i, 0] <= end)
				{
					end = Math.Max(routes[i, 1], end);
				}
				else
				{
					answer++;
					start = routes[i, 0];
					end = routes[i, 1];
				}

			}
			answer++;
			return answer;
		}
		static void Main()
		{
			int[,] grid = {
			{1, 3},
			{2, 6},
			{8, 10},
			{15, 18}
		};
			Console.WriteLine(CountMergedRoutes(grid));

		}
	}
}
