using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpConceptsLab
{
	// ===== OOP Examples =====
	abstract class Shape
	{
		public abstract void Draw();
	}
	class Circle : Shape
	{
		public override void Draw() => Console.WriteLine("Drawing a Circle");
	}

	interface IAnimal
	{
		void Speak();
	}
	class Cat : IAnimal
	{
		public void Speak() => Console.WriteLine("Meow!");
	}

	class Student
	{
		private string name;
		public string Name
		{
			get { return name; }
			set { name = value; }
		}
	}

	class MathOps
	{
		public int Add(int a, int b) => a + b;
		public double Add(double a, double b) => a + b;
	}
	class Animal
	{
		public virtual void Speak() => Console.WriteLine("Animal sound");
	}
	class Dog : Animal
	{
		public override void Speak() => Console.WriteLine("Woof Woof!");
	}

	// Delegate
	delegate void Greet(string name);

	class Program
	{
		static void Main(string[] args)
		{
			bool exit = false;
			while (!exit)
			{
				Console.Clear();
				Console.WriteLine("====== C# Concepts Practice Lab ======");
				Console.WriteLine("1. Abstraction (Abstract Classes)");
				Console.WriteLine("2. Interfaces");
				Console.WriteLine("3. Encapsulation (Properties)");
				Console.WriteLine("4. Polymorphism (Overloading & Overriding)");
				Console.WriteLine("5. Exception Handling");
				Console.WriteLine("6. Collections (List & Dictionary)");
				Console.WriteLine("7. Delegates & Lambda Expressions");
				Console.WriteLine("8. LINQ");
				Console.WriteLine("9. Exit");
				Console.Write("Choose an option: ");

				string choice = Console.ReadLine();
				Console.WriteLine();

				switch (choice)
				{
					case "1":
						Shape s = new Circle();
						s.Draw();
						break;

					case "2":
						IAnimal animal = new Cat();
						animal.Speak();
						break;

					case "3":
						Student st = new Student();
						st.Name = "Pranay";
						Console.WriteLine("Student Name: " + st.Name);
						break;

					case "4":
						MathOps ops = new MathOps();
						Console.WriteLine("Add int: " + ops.Add(5, 10));
						Console.WriteLine("Add double: " + ops.Add(2.5, 3.5));

						Animal a = new Dog();
						a.Speak();
						break;

					case "5":
						try
						{
							int x = 10, y = 0;
							int result = x / y;
							Console.WriteLine(result);
						}
						catch (DivideByZeroException ex)
						{
							Console.WriteLine("Error: " + ex.Message);
						}
						finally
						{
							Console.WriteLine("Finally block always runs");
						}
						break;

					case "6":
						List<string> fruits = new List<string> { "Apple", "Banana", "Mango" };
						Console.WriteLine("Fruits List:");
						foreach (var f in fruits) Console.WriteLine(f);

						Dictionary<int, string> dict = new Dictionary<int, string>
						{
							{1, "One"},
							{2, "Two"},
							{3, "Three"}
						};
						Console.WriteLine("\nDictionary:");
						foreach (var kv in dict) Console.WriteLine($"{kv.Key} => {kv.Value}");
						break;

					case "7":
						Greet greet = name => Console.WriteLine($"Hello, {name}!");
						greet("Pranay");
						greet("World");
						break;

					case "8":
						int[] numbers = { 1, 2, 3, 4, 5, 6 };
						var evens = from n in numbers
									where n % 2 == 0
									select n;
						Console.WriteLine("Even numbers:");
						foreach (var num in evens) Console.WriteLine(num);
						break;

					case "9":
						exit = true;
						break;

					default:
						Console.WriteLine("Invalid choice. Try again.");
						break;
				}

				if (!exit)
				{
					Console.WriteLine("\nPress any key to return to menu...");
					Console.ReadKey();
				}
			}
		}
	}
}
