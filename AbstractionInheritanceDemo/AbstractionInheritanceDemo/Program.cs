using System;

namespace AbstractionInheritanceDemo
{
	// Abstract class = cannot be instantiated directly
	abstract class Animal
	{
		public string Name { get; set; }

		// Abstract method = must be implemented by derived classes
		public abstract void MakeSound();

		// Normal (non-abstract) method = common implementation for all animals
		public void Eat()
		{
			Console.WriteLine($"{Name} is eating.");
		}
	}

	// Derived class Dog inherits from Animal
	class Dog : Animal
	{
		public override void MakeSound()
		{
			Console.WriteLine($"{Name} says: Woof Woof!");
		}
	}

	// Derived class Cat inherits from Animal
	class Cat : Animal
	{
		public override void MakeSound()
		{
			Console.WriteLine($"{Name} says: Meow Meow!");
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			// Using abstraction through inheritance
			Animal dog = new Dog { Name = "Buddy" };
			Animal cat = new Cat { Name = "Whiskers" };

			dog.MakeSound();  // Calls Dog’s implementation
			dog.Eat();        // Uses Animal’s common method

			cat.MakeSound();  // Calls Cat’s implementation
			cat.Eat();        // Uses Animal’s common method

			Console.WriteLine("\nPress any key to exit...");
			Console.ReadKey();
		}
	}
}
