using CSharpExpressions.Models;

namespace CSharpExpressions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== C# Expressions Complete Guide ===\n");

            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

            List<Student> students =
			[
				new("Alice", 20, 3.5m, "Computer Science"),
                new("Bob", 22, 3.8m, "Mathematics"),
                new("Charlie", 21, 3.2m, "Physics")
            ];

			// 1. LAMBDA EXPRESSIONS

			Console.WriteLine("1. LAMBDA EXPRESSIONS");
            Console.WriteLine("====================");

            Console.WriteLine("\n--- Basic Lambda Expressions ---");

            Func<int, bool> isEven = x => x % 2 == 0;
            Console.WriteLine($"Is 4 even? {isEven(4)}");

            Func<int, int, int> add = (x, y) => x + y;
            Console.WriteLine($"5 + 3 = {add(5, 3)}");

			Func<string> getMessage = () => "Hello from lambda!";
			Console.WriteLine($"Message: {getMessage()}");

			Func<string, bool> isValidEmail = email =>
            !string.IsNullOrEmpty(email) && 
            email.Contains("@") &&
            email.Contains(".");
            Console.WriteLine($"Is 'test@email.com' valid? {isValidEmail("test@email.com")}");

			// 2. EXPRESSION-BODIED MEMBERS

			Console.WriteLine("\n\n2. EXPRESSION-BODIED MEMBERS");
			Console.WriteLine("============================");

			var calc = new Calculator();
            Console.WriteLine($"Square of 5: {calc.Square(5)}");
            Console.WriteLine($"Circle area with radius 3: {calc.CircleArea(3)}");
            Console.WriteLine($"Calculator type: {calc.Type}");


		}
    }
}
