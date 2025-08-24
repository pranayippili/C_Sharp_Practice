namespace TwoDArrayDemo
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("===== 2D Array Operations in C# =====\n");

			// 1. Rectangular Array
			int[,] rectArray = {
				{1, 2, 3},
				{4, 5, 6},
				{7, 8, 9}
			};

			Console.WriteLine("Rectangular Array:");
			PrintRect(rectArray);

			// 2. Jagged Array
			int[][] jaggedArray = new int[3][];
			jaggedArray[0] = [1, 2, 3];
			jaggedArray[1] = [4, 5, 6];
			jaggedArray[2] = [7, 8, 9];

			Console.WriteLine("\nJagged Array:");
			PrintJagged(jaggedArray);

			// 3. Access & Update
			rectArray[1, 2] = 99;
			jaggedArray[0][1] = 88;
			Console.WriteLine("\nAfter Updating:");
			PrintRect(rectArray);
			PrintJagged(jaggedArray);

			// 4. Row Sum (Jagged)
			int rowSum = jaggedArray[1].Sum();
			Console.WriteLine($"\nSum of row 1 in jagged array = {rowSum}");

			// 5. Column Sum (Rectangular)
			int colSum = 0;
			for (int i = 0; i < rectArray.GetLength(0); i++)
				colSum += rectArray[i, 2];
			Console.WriteLine($"Sum of column 2 in rectangular array = {colSum}");

			// 6. Transpose
			Console.WriteLine("\nTranspose of Rectangular Array:");
			int[,] transposed = Transpose(rectArray);
			PrintRect(transposed);

			// 7. Flatten
			int[] flatRect = rectArray.Cast<int>().ToArray();
			Console.WriteLine("\nFlattened Rectangular Array: " + string.Join(", ", flatRect));

			int[] flatJagged = jaggedArray.SelectMany(r => r).ToArray();
			Console.WriteLine("Flattened Jagged Array: " + string.Join(", ", flatJagged));

			// 8. Copy
			int[,] copyRect = (int[,])rectArray.Clone();
			int[][] copyJagged = jaggedArray.Select(r => r.ToArray()).ToArray();
			Console.WriteLine("\nCloned Rectangular Array:");
			PrintRect(copyRect);
			Console.WriteLine("Cloned Jagged Array:");
			PrintJagged(copyJagged);

			Console.WriteLine("\n===== End of Demo =====");
		}

		// --- Helper Functions ---

		static void PrintRect(int[,] arr)
		{
			int m = arr.GetLength(0);
			int n = arr.GetLength(1);
			for (int i = 0; i < m; i++)
			{
				for (int j = 0; j < n; j++)
				{
					Console.Write(arr[i, j] + " ");
				}
				Console.WriteLine();
			}
		}

		static void PrintJagged(int[][] arr)
		{
			for (int i = 0; i < arr.Length; i++)
			{
				Console.WriteLine(string.Join(" ", arr[i]));
			}
		}

		static int[,] Transpose(int[,] matrix)
		{
			int m = matrix.GetLength(0);
			int n = matrix.GetLength(1);
			int[,] result = new int[n, m];
			for (int i = 0; i < m; i++)
			{
				for (int j = 0; j < n; j++)
				{
					result[j, i] = matrix[i, j];
				}
			}
			return result;
		}
	}
}
