using System;
using System.Collections.Generic;

namespace CityClustersApp
{
	class Program
	{
		public static int CountClusters(int[,] matInput)
		{
			int n = matInput.GetLength(0);
			bool[] visited = new bool[n];
			int clusters = 0;

			for (int i = 0; i < n; i++)
			{
				if (!visited[i])
				{
					BFS(i, matInput, visited, n);
					clusters++;
				}
			}

			return clusters;
		}

		private static void BFS(int start, int[,] matInput, bool[] visited, int n)
		{
			Queue<int> queue = new Queue<int>();
			queue.Enqueue(start);
			visited[start] = true;

			while (queue.Count > 0)
			{
				int city = queue.Dequeue();

				for (int j = 0; j < n; j++)
				{
					if (matInput[city, j] == 1 && !visited[j])
					{
						visited[j] = true;
						queue.Enqueue(j);
					}
				}
			}
		}

		static void Main(string[] args)
		{
			Console.WriteLine("Enter number of cities (N) and columns (M):");
			string[] input = Console.ReadLine().Split(' ');
			int matInput_row = int.Parse(input[0]);
			int matInput_col = int.Parse(input[1]);

			int[,] matInput = new int[matInput_row, matInput_col];

			Console.WriteLine("Enter adjacency matrix:");
			for (int i = 0; i < matInput_row; i++)
			{
				input = Console.ReadLine().Split(' ');
				for (int j = 0; j < matInput_col; j++)
				{
					matInput[i, j] = int.Parse(input[j]);
				}
			}

			int result = CountClusters(matInput);
			Console.WriteLine("Total number of clusters: " + result);
		}
	}
}
