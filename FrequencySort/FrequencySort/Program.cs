namespace FrequencySort
{
    public class Program
    {
		private static int[] SortByFrequency(int[] input)
		{
			int n = input.Length;
			int[] answer = new int[n];
			int max = 0;
			foreach (int i in input)
			{
				if (i > max)
				{
					max = i;
				}
			}
			int[] freq = new int[max + 1];
			foreach (int i in input)
			{
				freq[i]++;
			}
			// 		Dictionary<int, int> positions = new Dictionary<int, int>();
			// 		for(int i = 0; i < freq.Length; i++)
			// 		{
			// 			if(freq[i] != 0)
			// 			{
			// 				for(int j = 0; j < n; j++)
			// 				{
			// 					if(i == input[j])
			// 					{
			// 						positions.Add(i, j);
			// 						break;
			// 					}
			// 				}
			// 			}
			// 		}
			int[] positions1 = new int[max + 1];
			for (int i = 0; i < freq.Length; i++)
			{
				if (freq[i] != 0)
				{
					for (int j = 0; j < n; j++)
					{
						if (i == input[j])
						{
							positions1[i] = j;
							break;
						}
					}
				}
			}
			int x = 0;
			while (x < n)
			{
				int maxfreq = 0;
				int index = 0;
				int position = n;
				// 			for(int i = 0; i < freq.Length; i++)
				// 			{
				// 				if(freq[i] == maxfreq && freq[i] > 0)
				// 				{
				// 					if(position > positions[i])
				// 					{
				// 						position = positions[i];
				// 						maxfreq = freq[i];
				// 						index = i;
				// 					}
				// 				} else if(freq[i] > maxfreq)
				// 				{
				// 					maxfreq = freq[i];
				// 					index = i;
				// 					position = positions[i];
				// 				}
				// 			}
				for (int i = 0; i < freq.Length; i++)
				{
					if (freq[i] == maxfreq && freq[i] > 0)
					{
						if (position > positions1[i])
						{
							position = positions1[i];
							maxfreq = freq[i];
							index = i;
						}
					}
					else if (freq[i] > maxfreq)
					{
						maxfreq = freq[i];
						index = i;
						position = positions1[i];
					}
				}
				while (maxfreq > 0)
				{
					answer[x] = index;
					maxfreq--;
					x++;
				}
				freq[index] = 0;
			}

			return answer;
		}
		static void Main()
		{
			//int[] x = {4,5,6,5,4,3};
			//int[] y = {8,6,7,6,8,6};
			int[] z = { 1, 2, 3, 4, 5 };
			int[] output = SortByFrequency(z);
			Console.WriteLine(string.Join(" ,", output));
		}
	}
}
