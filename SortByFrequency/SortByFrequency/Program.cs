namespace SortByFrequency
{
    internal class Program
    {
		private static int[] SortByFrequency(int[] input)
		{
			int n = input.Length;
			int[] answer = new int[n];
			Dictionary<int, (int, int)> frequency = new Dictionary<int, (int, int)>();

			for (int i = 0; i < n; i++)
			{
				if (!frequency.ContainsKey(input[i]))
				{
					frequency[input[i]] = (1, i);
				}
				else
				{
					var (count, index) = frequency[input[i]];
					frequency[input[i]] = (count + 1, index);
				}
			}
			int x = 0;
			while (x < n)
			{
				int maxfreq = 0;
				int value = 0;
				int position = n;
				foreach (var ele in frequency)
				{
					if (ele.Value.Item1 > 0)
					{
						if (ele.Value.Item1 > maxfreq)
						{
							maxfreq = ele.Value.Item1;
							position = ele.Value.Item2;
							value = ele.Key;
						}
						else if (ele.Value.Item1 == maxfreq && ele.Value.Item2 < position)
						{
							maxfreq = ele.Value.Item1;
							position = ele.Value.Item2;
							value = ele.Key;
						}
					}

				}
				while (maxfreq > 0)
				{
					answer[x] = value;
					x++;
					maxfreq--;
				}

				frequency[value] = (0, frequency[value].Item2);

			}
			return answer;



		}
		static void Main()
		{
			int[] x = { 5, 4, 6, 5, 4, 3 };
			//int[] y = {8,6,7,6,8,6};
			//int[] z = {1,2,3,4,5};
			int[] output = SortByFrequency(x);
			Console.WriteLine(string.Join(" ,", output));
		}
	}
}
