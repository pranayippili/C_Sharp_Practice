using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpExpressions
{
	public class Calculator
	{
		public int Square(int x) => x * x;

		public string Type => "Scientific Calculator";

		public double CircleArea(double radius) =>Math.PI * radius * radius;

		public Calculator() => Console.WriteLine("Calculator created.");
	}
}
