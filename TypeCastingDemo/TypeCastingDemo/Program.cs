using System;

namespace TypeCastingDemo
{
	// Base class
	class Animal
	{
		public virtual void Speak()
		{
			Console.WriteLine("Animal speaks...");
		}
	}

	// Derived class
	class Dog : Animal
	{
		public override void Speak()
		{
			Console.WriteLine("Dog barks: Woof Woof!");
		}

		public void Fetch()
		{
			Console.WriteLine("Dog is fetching the ball...");
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("=== VALUE TYPE CASTING ===");

			// Implicit casting (smaller to larger type)
			int num = 100;
			double d = num;  // Implicit
			Console.WriteLine($"Implicit casting int -> double: {d}");

			// Explicit casting (larger to smaller type)
			double pi = 3.14159;
			int intPi = (int)pi; // Explicit
			Console.WriteLine($"Explicit casting double -> int: {intPi}");

			// Boxing (value type to object)
			int x = 42;
			object boxed = x; // Boxing
			Console.WriteLine($"Boxed value: {boxed}");

			// Unboxing (object back to value type)
			int unboxed = (int)boxed;
			Console.WriteLine($"Unboxed value: {unboxed}");

			Console.WriteLine("\n=== REFERENCE TYPE CASTING ===");

			// Upcasting (Derived -> Base)
			Dog dog = new Dog();
			Animal animal = dog;  // Implicit upcast
			animal.Speak();  // Calls overridden method in Dog

			// Downcasting (Base -> Derived)
			Dog downcastDog = (Dog)animal;  // Explicit downcast
			downcastDog.Fetch();

			// Using "is" keyword
			if (animal is Dog)
			{
				Console.WriteLine("Animal is actually a Dog");
			}

			// Using "as" keyword
			Dog asDog = animal as Dog;
			if (asDog != null)
			{
				asDog.Fetch();
			}

			Console.WriteLine("\n=== INVALID CASTING ===");

			Animal justAnimal = new Animal();
			// Safe check before downcasting
			Dog wrongCast = justAnimal as Dog;
			if (wrongCast == null)
			{
				Console.WriteLine("Invalid cast: Animal cannot be cast to Dog");
			}
			Console.ReadLine();
		}
	}
}
