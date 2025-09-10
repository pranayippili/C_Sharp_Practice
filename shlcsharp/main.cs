/******************************************************************************

Welcome to GDB Online.
GDB online is an online compiler and debugger tool for C, C++, Python, Java, PHP, Ruby, Perl,
C#, OCaml, VB, Swift, Pascal, Fortran, Haskell, Objective-C, Assembly, HTML, CSS, JS, SQLite, Prolog.
Code, Compile, Run and Debug online from anywhere in world.

*******************************************************************************/
using System;
using System.Collections.Generic;
class HelloWorld {
    private static int ConvertBinaryToDecimal(string binaryInput)
    {
        int ans = 0;
        int baseValue = 1;
        for (int i = binaryInput.Length - 1; i >=0; i--)
        {
            if(binaryInput[i] == '1')
            {
                ans += baseValue;
            }
            baseValue *= 2;
        }
        return ans;
    }
    
    private static int[] ReorderEvenOdd(int[] inputArray)
    {
        List<int> even = new List<int>();
        List<int> odd = new List<int>();
        
        foreach (int num in inputArray)
        {
            if (num % 2 == 0)
            {
                even.Add(num);
            }
            else
            {
                odd.Add(num);
            }
        }
        
        even.AddRange(odd);
        return even.ToArray();
    }
    
    private static char ShiftChar(char ch, int key)
    {
        if(char.IsLower(ch))
        {
            return (char)(((ch - 'a' + key) % 26) + 'a');
        }
        if(char.IsUpper(ch))
        {
            return (char)(((ch - 'A' + key) % 26) + 'A');
        }
        return ch;
    }
    
    private static int CountUnchangedPositionsEfficient(string input)
    {
        int ans = 0;
        int n = input.Length;
        for(int i = 0; i < n ; i++){
            if (input[i] == input[n - 1 - i])
            {
                ans++;
            }
        }
        return ans;
    }
    
    private static int MultiplyDigits(int input)
    {
        int ans = 1;
        foreach( char c in input.ToString())
        {
            ans *= (c - '0');
        }
        return ans;
    }
    
    private static int MultiplyDigits1(int num)
    {
        int ans = 1;
        while(num > 0)
        {
            int digit = num % 10;
            ans *= digit;
            num /= 10;
        }
        return ans;
    }
    
    private static int CountUniqueCharacters(string input)
    {
        int ans = 0;
        
        Dictionary<char,int> charFrequency = new Dictionary<char,int>();
        
        foreach(char c in input)
        {
            if(charFrequency.ContainsKey(c))
            {
                charFrequency[c]++;
            }
            else
            {
                charFrequency.Add(c,1);
                //charFrequency[c] = 1;
            }
        }
        foreach(var pair in charFrequency)
        {
            if(pair.Value == 1)
            {
                ans++;
            }
        }
        return ans;
    }
    
    private static int VowelsCount(string input)
    {
        int count = 0;
        foreach (char c in input)
        {
            char ch = char.ToLower(c);
            if("aeiou".Contains(ch))
            {
                count ++;
            }
        }
        return count;
    }
    
    private static int CountConsonants(string input)
    {
        int ans = 0;
        foreach(char c in input)
        {
            char ch = char.ToLower(c);
            if(char.IsLetter(ch) && !"aeiou".Contains(ch))
            {
                ans ++;
            }
        }
        return ans;
    }
    
    private static int MostFrequentDigit(int input)
    {
        int[] arr = new int[10];
        
        while(input > 0)
        {
            int digit = input % 10;
            arr[digit]++;
            input /= 10;
        }
        int maxFreq = 0;
        int ans = 0;
        for(int i=0;i<10;i++)
        {
            if(arr[i] > maxFreq)
            {
                ans = i;
                maxFreq = arr[i];
            }
        }
        return ans;
    }
    
    private static int MaxDigit(int input)
    {
        int ans = 0;
        while(input > 0)
        {
            int digit = input % 10;
            if(digit > ans)
            {
                ans = digit;
            }
            input /= 10;
        }
        return ans;
    }
    
    private static bool IsPerfectSquare(int input)
    {
        int root = (int) Math.Sqrt(input);
        return root * root == input;
    }
    
    private static int FindMax(string input)
    {
        int max = 0;
        string[] arr = input.Trim().Split(' ');
        foreach(string str in arr)
        {
            int value = int.Parse(str);
            if(value > max)
            {
                max = value;
            }
        }
        return max;
    }
    
    private static int PrimeSum(int x, int y)
    {
        int ans = 0;
        for(int i = x+1; i < y; i++)
        {
            if(IsPrime(i))
            {
                ans += i;
            }
        }
        return ans;
        
    }
    private static bool IsPrime(int num)
    {
        if(num < 2) return false;
        for(int i = 2; i <= Math.Sqrt(num); i++)
        {
            if(num % i == 0)
            {
                return false;
            }
        }
        return true;
    }
    
     private static double Area(int x1, int y1, int r1, int x2, int y2, int r2)
     {
        double d = Math.Sqrt((x1-x2)*(x1-x2)+(y1-y2)*(y1-y2));
        
        if(d >= r1 + r2)
        {
            return 0;
        }
        if(d <= Math.Abs(r1-r2))
        {
            double r = Math.Min(r1,r2);
            return Math.PI * r * r;
        }
        double sq_r1 = r1*r1;
        double sq_r2 = r2*r2;
        double sq_d = d*d;
        
        double a1 = sq_r1 * Math.Acos((sq_d+sq_r1-sq_r2)/(2*d*r1));
        double a2 = sq_r2 * Math.Acos((sq_d+sq_r2-sq_r1)/(2*d*r2));
        double a3 = 0.5 * Math.Sqrt((-d+r1+r2)*(d+r1-r2)*(d-r1+r2)*(d+r1+r2));
        
        return Math.Round(a1+a2-a3,6);
     }
    
  static void Main() {
    Console.WriteLine(ConvertBinaryToDecimal("1011"));
    
    int[] arr = { 2, 3, 4, 5, 6 };
    
    Console.WriteLine(string.Join(" ,",ReorderEvenOdd(arr)));
    
    string str = "abcaabcd";
    
    Console.WriteLine(CountUnchangedPositionsEfficient(str));
    
    Console.WriteLine(MultiplyDigits(69));
    Console.WriteLine(MultiplyDigits1(69));
    
    Console.WriteLine(CountUniqueCharacters("abaacdefah "));
    Console.WriteLine(VowelsCount("Akhil"));
    Console.WriteLine(CountConsonants("Akhil"));
    Console.WriteLine(MostFrequentDigit(10699600));
    Console.WriteLine(MaxDigit(106485759));
    
    Console.WriteLine(ShiftChar('X', 9));
    Console.WriteLine(IsPerfectSquare(69));
    
    Console.WriteLine(FindMax("123 345 234 222"));
    
    Console.WriteLine(PrimeSum(1,10));
    
    Console.WriteLine(Area(0,0,2,3,0,2));
  }
}