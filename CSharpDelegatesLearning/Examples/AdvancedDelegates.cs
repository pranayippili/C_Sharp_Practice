using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDelegatesLearning.Examples
{
	public class AdvancedDelegates
	{
		public static void RunExamples()
		{
			Console.WriteLine("\n=== Advanced Delegates Example ===");

			DelegateChaining();
			DelegateComposition();
			DelegateFactory();
		}

		public static void DelegateChaining()
		{
			Console.WriteLine("\n--- Delegate Chaining ---");

			Func<int, int> addOne = x => x + 1;
			Func<int, int> multiplyByTwo = x => x * 2;
			Func<int, int> square = x => x * x;

			// Chain operations
			Func<int, int> chainedOperation = x => square(multiplyByTwo(addOne(x)));

			int result = chainedOperation(3); // (3 + 1) * 2 = 8, 8^2 = 64
			Console.WriteLine($"Chained result: {result}");

			// Using composition helper
			var composed = ComposeFunc(addOne, multiplyByTwo, square);
			result = composed(3);
			Console.WriteLine($"Composed result: {result}");
		}

		public static void DelegateComposition()
		{
			Console.WriteLine("\n--- Delegate Composition ---");

			List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

			// Compose multiple predicates
			Predicate<int> isEven = x => x % 2 == 0;
			Predicate<int> isGreaterThan5 = x => x > 5;

			var evenAndGreaterThan5 = CombinePredicates(isEven, isGreaterThan5, true);
			var evenOrGreaterThan5 = CombinePredicates(isEven, isGreaterThan5, false);

			var andResults = numbers.FindAll(evenAndGreaterThan5);
			var orResults = numbers.FindAll(evenOrGreaterThan5);

			Console.WriteLine($"Even AND > 5: {string.Join(", ", andResults)}");
			Console.WriteLine($"Even OR > 5: {string.Join(", ", orResults)}");
		}

		public static void DelegateFactory()
		{
			Console.WriteLine("\n--- Delegate Factory ---");

			// Create delegates dynamically
			var greaterThan5 = CreateComparisonDelegate(5, (x, y) => x > y);
			var lessThan10 = CreateComparisonDelegate(10, (x, y) => x < y);
			var equals7 = CreateComparisonDelegate(7, (x, y) => x == y);

			int testValue = 7;
			Console.WriteLine($"{testValue} > 5: {greaterThan5(testValue)}");
			Console.WriteLine($"{testValue} < 10: {lessThan10(testValue)}");
			Console.WriteLine($"{testValue} == 7: {equals7(testValue)}");
		}

		// Helper methods
		public static Func<int, int> ComposeFunc(params Func<int, int>[] functions)
		{
			return x => functions.Aggregate(x, (current, func) => func(current));
		}

		public static Predicate<T> CombinePredicates<T>(Predicate<T> pred1, Predicate<T> pred2, bool useAnd)
		{
			return useAnd ?
				new Predicate<T>(x => pred1(x) && pred2(x)) :
				new Predicate<T>(x => pred1(x) || pred2(x));
		}

		public static Func<int, bool> CreateComparisonDelegate(int value, Func<int, int, bool> comparer)
		{
			return x => comparer(x, value);
		}
	}
}
