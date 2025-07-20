using ExpressionTree.Models;
using System.Linq.Expressions;

namespace ExpressionTree
{
    public class Program
    {
        static void Main()
        {
            var filter = new FilterRule
            {
                Field = "Age",
                Operator = ">",
                Value = 25
            };

            // Sample data
            var users = new List<User>
            {
                new User{ Name = "Alice", Age = 30 },
                new User{ Name = "Bob", Age = 20 },
                new User{ Name = "Charlie", Age = 35 },
                new User{ Name = "David", Age = 22 }
            };

            var predicate = BuildPredicate<User>(filter);

            var result = users.AsQueryable().Where(predicate).ToList();

            Console.WriteLine("Filtered Users:");
            result.ForEach(u => Console.WriteLine($" {u.Name}, Age: {u.Age}"));
		}

        static Expression<Func<T, bool>> BuildPredicate<T>(FilterRule rule)
        {
            var param = Expression.Parameter(typeof(T), "user");

            var property = Expression.Property(param, rule.Field);

            var constant = Expression.Constant(Convert.ChangeType(rule.Value, property.Type));

            BinaryExpression body = rule.Operator switch
            {
                "==" => Expression.Equal(property, constant),
                "!=" => Expression.NotEqual(property, constant),
                ">" => Expression.GreaterThan(property, constant),
                "<" => Expression.LessThan(property, constant),
                ">=" => Expression.GreaterThanOrEqual(property, constant),
                "<=" => Expression.LessThanOrEqual(property, constant),
                _ => throw new NotSupportedException($"Operator '{rule.Operator}' is not supported.")
			};

            return Expression.Lambda<Func<T, bool>>(body, param);
        }
    }
}
