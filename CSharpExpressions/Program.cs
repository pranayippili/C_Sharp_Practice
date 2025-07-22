using CSharpExpressions.Models;
using System.Linq.Expressions;

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

			// 3. ANONYMOUS METHODS (older syntax)

            Console.WriteLine("\n\n3. ANONYMOUS METHODS (Legacy)");
            Console.WriteLine("============================");

            Func<int, int> squareAnonymous = delegate (int x) { return x * x; };
            Console.WriteLine($"Square of 6 using anonymous method: {squareAnonymous(6)}");

            Action<string> printAnonymous = delegate (string message)
            {
                Console.WriteLine($"Anonymous message: {message}");
			};
            printAnonymous("Hello!");


			// 4.LINQ EXPRESSIONS

			Console.WriteLine("\n\n4. LINQ EXPRESSIONS");
			Console.WriteLine("===================");

            var evenNumbers = numbers.Where(x => x % 2 == 0).ToList();
            Console.WriteLine($"Even numbers: [{string.Join(", ", evenNumbers)}]");

            var squaredNumbers = numbers.Select(x => x * x).ToList();
            Console.WriteLine($"Squared numbers: [{string.Join(", ", squaredNumbers)}]");

            var goodStudents = students
                .Where(s => s.GPA > 3.5m)
                .OrderBy(s => s.Name)
                .Select(s => new {s.Name, s.GPA})
                .ToList();

            Console.WriteLine("Good students (GPA > 3.5):");
            goodStudents.ForEach(s => Console.WriteLine($" {s.Name, -15}: {s.GPA:F2}"));

            //Grouping
            var studentsByMajor = students
                .GroupBy(s => s.Major)
                .Select(g => new { Major = g.Key, Count = g.Count() })
                .ToList();

            Console.WriteLine("\nStudents by Major:");
            studentsByMajor.ForEach(g => Console.WriteLine($" {g.Major}: {g.Count} students"));

			// 5. EXPRESSION TREES (Advanced)
            Console.WriteLine("\n\n5. EXPRESSION TREES");
            Console.WriteLine("===================");

            Expression<Func<int, bool>> isPositiveExpr = x => x > 0;
            Console.WriteLine($"Expression: {isPositiveExpr}");
            Console.WriteLine($"Expression body: {isPositiveExpr.Body}");
            Console.WriteLine($"Expression parameters: {string.Join(",",isPositiveExpr.Parameters)}");

            var compiledExpr = isPositiveExpr.Compile();
            Console.WriteLine($"Is 10 positive? {compiledExpr(10)}");

            var parameter = Expression.Parameter(typeof(int), "x");
            var constant = Expression.Constant(10);
            var greaterThan = Expression.GreaterThan(parameter, constant);
            var lambda = Expression.Lambda<Func<int, bool>>(greaterThan, parameter);

            Console.WriteLine($"Generated Expression: {lambda}");
            var compiledGeneratedExpr = lambda.Compile();
            Console.WriteLine($"Is 15 > 10? {compiledGeneratedExpr(15)}");

			// 6. PRACTICAL EXAMPLES
            Console.WriteLine("\n\n6. PRACTICAL EXAMPLES");
            Console.WriteLine("===================");

            //Event handling with expressions
            var button = new Button();
            button.Click += () => Console.WriteLine("Button clicked!");
            button.Click += () => Console.WriteLine("Logging click event...");
            button.SimulateClick();

            // Functional programming patterns
            Console.WriteLine("\n--- Functinal Programming ---");

            var doubleNumbers = numbers.Select(x => x * 2).ToList();
            Console.WriteLine($"Doubled numbers: [{string.Join(", ", doubleNumbers)}]");

            var bigNumbers = numbers.Where(x => x > 5).ToList();
            Console.WriteLine($"Numbers greater than 5: [{string.Join(", ", bigNumbers)}]");

            var sum = numbers.Aggregate((acc, x) => acc + x);
            Console.WriteLine($"Sum of numbers: {sum}");




		}
	}
}
