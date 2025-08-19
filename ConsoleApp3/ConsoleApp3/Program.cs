using System;
using System.Collections.Generic;
using System.Linq;

namespace InheritanceDemo
{
	// ===== BASE CLASSES AND INHERITANCE HIERARCHY =====

	// Abstract base class
	public abstract class Animal
	{
		public string Name { get; set; }
		public int Age { get; set; }
		protected string species;

		protected Animal(string name, int age, string species)
		{
			Name = name;
			Age = age;
			this.species = species;
			Console.WriteLine($"Animal constructor called for {name}");
		}

		// Virtual method - can be overridden
		public virtual void MakeSound()
		{
			Console.WriteLine($"{Name} makes a generic animal sound");
		}

		// Abstract method - must be implemented by derived classes
		public abstract void Move();

		// Regular method - inherited as-is
		public void Sleep()
		{
			Console.WriteLine($"{Name} is sleeping peacefully");
		}

		public virtual void DisplayInfo()
		{
			Console.WriteLine($"Name: {Name}, Age: {Age}, Species: {species}");
		}
	}

	// ===== DERIVED CLASSES =====

	public class Dog : Animal
	{
		public string Breed { get; set; }

		public Dog(string name, int age, string breed) : base(name, age, "Canine")
		{
			Breed = breed;
			Console.WriteLine($"Dog constructor called for {name}");
		}

		public override void MakeSound()
		{
			Console.WriteLine($"{Name} barks: Woof! Woof!");
		}

		public override void Move()
		{
			Console.WriteLine($"{Name} runs on four legs");
		}

		public void Fetch()
		{
			Console.WriteLine($"{Name} is fetching the ball!");
		}

		public override void DisplayInfo()
		{
			base.DisplayInfo();
			Console.WriteLine($"Breed: {Breed}");
		}
	}

	public class Cat : Animal
	{
		public bool IsIndoor { get; set; }

		public Cat(string name, int age, bool isIndoor) : base(name, age, "Feline")
		{
			IsIndoor = isIndoor;
			Console.WriteLine($"Cat constructor called for {name}");
		}

		public override void MakeSound()
		{
			Console.WriteLine($"{Name} meows: Meow! Meow!");
		}

		public override void Move()
		{
			Console.WriteLine($"{Name} stalks gracefully on silent paws");
		}

		public void Climb()
		{
			Console.WriteLine($"{Name} climbs up the tree with ease");
		}

		public override void DisplayInfo()
		{
			base.DisplayInfo();
			Console.WriteLine($"Indoor cat: {IsIndoor}");
		}
	}

	public class Bird : Animal
	{
		public double WingSpan { get; set; }

		public Bird(string name, int age, double wingSpan) : base(name, age, "Avian")
		{
			WingSpan = wingSpan;
			Console.WriteLine($"Bird constructor called for {name}");
		}

		public override void MakeSound()
		{
			Console.WriteLine($"{Name} chirps: Tweet! Tweet!");
		}

		public override void Move()
		{
			Console.WriteLine($"{Name} flies through the air with {WingSpan} inch wingspan");
		}

		public virtual void Fly()
		{
			Console.WriteLine($"{Name} soars high in the sky");
		}

		public override void DisplayInfo()
		{
			base.DisplayInfo();
			Console.WriteLine($"Wing Span: {WingSpan} inches");
		}
	}

	// Multilevel inheritance
	public class Eagle : Bird
	{
		public string HuntingStyle { get; set; }

		public Eagle(string name, int age, double wingSpan, string huntingStyle)
			: base(name, age, wingSpan)
		{
			HuntingStyle = huntingStyle;
			Console.WriteLine($"Eagle constructor called for {name}");
		}

		public override void MakeSound()
		{
			Console.WriteLine($"{Name} screeches: SCREECH!");
		}

		public override void Fly()
		{
			Console.WriteLine($"{Name} soars majestically, hunting with {HuntingStyle} style");
		}

		public void Hunt()
		{
			Console.WriteLine($"{Name} dives down to catch prey with precision");
		}

		public override void DisplayInfo()
		{
			base.DisplayInfo();
			Console.WriteLine($"Hunting Style: {HuntingStyle}");
		}
	}

	// ===== INTERFACES FOR MULTIPLE INHERITANCE =====

	public interface ISwimmable
	{
		void Swim();
		double SwimSpeed { get; set; }
	}

	public interface IFlyable
	{
		void Fly();
		double FlightSpeed { get; set; }
	}

	public interface IClimbable
	{
		void Climb();
		double ClimbHeight { get; set; }
	}

	// Class implementing multiple interfaces
	public class Duck : Animal, ISwimmable, IFlyable
	{
		public double SwimSpeed { get; set; }
		public double FlightSpeed { get; set; }

		public Duck(string name, int age, double swimSpeed, double flightSpeed)
			: base(name, age, "Waterfowl")
		{
			SwimSpeed = swimSpeed;
			FlightSpeed = flightSpeed;
		}

		public override void MakeSound()
		{
			Console.WriteLine($"{Name} quacks: Quack! Quack!");
		}

		public override void Move()
		{
			Console.WriteLine($"{Name} waddles on land");
		}

		public void Swim()
		{
			Console.WriteLine($"{Name} swims at {SwimSpeed} mph");
		}

		public void Fly()
		{
			Console.WriteLine($"{Name} flies at {FlightSpeed} mph");
		}

		public override void DisplayInfo()
		{
			base.DisplayInfo();
			Console.WriteLine($"Swim Speed: {SwimSpeed} mph, Flight Speed: {FlightSpeed} mph");
		}
	}

	// ===== SEALED CLASS EXAMPLE =====

	public class WildAnimal : Animal
	{
		public string Habitat { get; set; }

		public WildAnimal(string name, int age, string species, string habitat)
			: base(name, age, species)
		{
			Habitat = habitat;
		}

		public override void MakeSound()
		{
			Console.WriteLine($"{Name} makes wild sounds");
		}

		public virtual void Hunt()
		{
			Console.WriteLine($"{Name} hunts in the {Habitat}");
		}

		// Sealed method - cannot be overridden further
		public sealed override void Move()
		{
			Console.WriteLine($"{Name} moves stealthily in the {Habitat}");
		}
	}

	public sealed class Lion : WildAnimal
	{
		public bool IsMale { get; set; }

		public Lion(string name, int age, bool isMale)
			: base(name, age, "Big Cat", "Savanna")
		{
			IsMale = isMale;
		}

		public override void MakeSound()
		{
			Console.WriteLine($"{Name} roars: ROAR!");
		}

		public override void Hunt()
		{
			if (IsMale)
				Console.WriteLine($"{Name} protects the pride while females hunt");
			else
				Console.WriteLine($"{Name} hunts in coordinated groups");
		}

		// Cannot override Move() because it's sealed in WildAnimal

		public override void DisplayInfo()
		{
			base.DisplayInfo();
			Console.WriteLine($"Gender: {(IsMale ? "Male" : "Female")}");
		}
	}

	// ===== SHAPE HIERARCHY FOR POLYMORPHISM DEMO =====

	public abstract class Shape
	{
		public string Color { get; set; }

		protected Shape(string color)
		{
			Color = color;
		}

		public abstract double CalculateArea();
		public abstract double CalculatePerimeter();

		public virtual void Display()
		{
			Console.WriteLine($"{GetType().Name}: Color = {Color}, Area = {CalculateArea():F2}, Perimeter = {CalculatePerimeter():F2}");
		}
	}

	public class Circle : Shape
	{
		public double Radius { get; set; }

		public Circle(double radius, string color) : base(color)
		{
			Radius = radius;
		}

		public override double CalculateArea()
		{
			return Math.PI * Radius * Radius;
		}

		public override double CalculatePerimeter()
		{
			return 2 * Math.PI * Radius;
		}
	}

	public class Rectangle : Shape
	{
		public double Width { get; set; }
		public double Height { get; set; }

		public Rectangle(double width, double height, string color) : base(color)
		{
			Width = width;
			Height = height;
		}

		public override double CalculateArea()
		{
			return Width * Height;
		}

		public override double CalculatePerimeter()
		{
			return 2 * (Width + Height);
		}
	}

	public class Triangle : Shape
	{
		public double Base { get; set; }
		public double Height { get; set; }
		public double Side1 { get; set; }
		public double Side2 { get; set; }

		public Triangle(double baseLength, double height, double side1, double side2, string color) : base(color)
		{
			Base = baseLength;
			Height = height;
			Side1 = side1;
			Side2 = side2;
		}

		public override double CalculateArea()
		{
			return 0.5 * Base * Height;
		}

		public override double CalculatePerimeter()
		{
			return Base + Side1 + Side2;
		}
	}

	// ===== EMPLOYEE HIERARCHY FOR ADVANCED POLYMORPHISM =====

	public abstract class Employee
	{
		public string Name { get; set; }
		public int Id { get; set; }
		public DateTime HireDate { get; set; }

		protected Employee(string name, int id, DateTime hireDate)
		{
			Name = name;
			Id = id;
			HireDate = hireDate;
		}

		public abstract decimal CalculateMonthlyPay();
		public virtual void DisplayEmployeeInfo()
		{
			Console.WriteLine($"ID: {Id}, Name: {Name}, Hire Date: {HireDate:yyyy-MM-dd}");
			Console.WriteLine($"Monthly Pay: ${CalculateMonthlyPay():F2}");
		}

		public int GetYearsOfService()
		{
			return DateTime.Now.Year - HireDate.Year;
		}
	}

	public class FullTimeEmployee : Employee
	{
		public decimal AnnualSalary { get; set; }

		public FullTimeEmployee(string name, int id, DateTime hireDate, decimal annualSalary)
			: base(name, id, hireDate)
		{
			AnnualSalary = annualSalary;
		}

		public override decimal CalculateMonthlyPay()
		{
			return AnnualSalary / 12;
		}

		public override void DisplayEmployeeInfo()
		{
			Console.WriteLine("=== Full-Time Employee ===");
			base.DisplayEmployeeInfo();
			Console.WriteLine($"Annual Salary: ${AnnualSalary:F2}");
		}
	}

	public class PartTimeEmployee : Employee
	{
		public decimal HourlyRate { get; set; }
		public int MonthlyHours { get; set; }

		public PartTimeEmployee(string name, int id, DateTime hireDate, decimal hourlyRate, int monthlyHours)
			: base(name, id, hireDate)
		{
			HourlyRate = hourlyRate;
			MonthlyHours = monthlyHours;
		}

		public override decimal CalculateMonthlyPay()
		{
			return HourlyRate * MonthlyHours;
		}

		public override void DisplayEmployeeInfo()
		{
			Console.WriteLine("=== Part-Time Employee ===");
			base.DisplayEmployeeInfo();
			Console.WriteLine($"Hourly Rate: ${HourlyRate:F2}, Monthly Hours: {MonthlyHours}");
		}
	}

	public class Contractor : Employee
	{
		public decimal ProjectRate { get; set; }
		public int ProjectsCompleted { get; set; }

		public Contractor(string name, int id, DateTime hireDate, decimal projectRate)
			: base(name, id, hireDate)
		{
			ProjectRate = projectRate;
			ProjectsCompleted = 0;
		}

		public override decimal CalculateMonthlyPay()
		{
			return ProjectRate * ProjectsCompleted;
		}

		public void CompleteProject()
		{
			ProjectsCompleted++;
			Console.WriteLine($"{Name} completed a project! Total projects: {ProjectsCompleted}");
		}

		public override void DisplayEmployeeInfo()
		{
			Console.WriteLine("=== Contractor ===");
			base.DisplayEmployeeInfo();
			Console.WriteLine($"Project Rate: ${ProjectRate:F2}, Projects Completed: {ProjectsCompleted}");
		}
	}

	// ===== MAIN PROGRAM =====

	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("=".PadRight(60, '='));
			Console.WriteLine("    C# INHERITANCE DEMONSTRATION CONSOLE APP");
			Console.WriteLine("=".PadRight(60, '='));

			while (true)
			{
				DisplayMainMenu();
				string choice = Console.ReadLine();

				switch (choice)
				{
					case "1":
						DemonstrateBasicInheritance();
						break;
					case "2":
						DemonstrateConstructorChaining();
						break;
					case "3":
						DemonstrateMethodOverriding();
						break;
					case "4":
						DemonstrateMultipleInterfaces();
						break;
					case "5":
						DemonstrateSealedClasses();
						break;
					case "6":
						DemonstrateAbstractClasses();
						break;
					case "7":
						DemonstratePolymorphism();
						break;
					case "8":
						DemonstrateEmployeeHierarchy();
						break;
					case "9":
						DemonstrateBaseKeyword();
						break;
					case "0":
						Console.WriteLine("Thank you for exploring C# Inheritance!");
						return;
					default:
						Console.WriteLine("Invalid choice. Please try again.");
						break;
				}

				Console.WriteLine("\nPress any key to continue...");
				Console.ReadKey();
				Console.Clear();
			}
		}

		static void DisplayMainMenu()
		{
			Console.WriteLine("\n" + "=".PadRight(50, '='));
			Console.WriteLine("          INHERITANCE DEMO MENU");
			Console.WriteLine("=".PadRight(50, '='));
			Console.WriteLine("1. Basic Inheritance & Class Hierarchy");
			Console.WriteLine("2. Constructor Chaining");
			Console.WriteLine("3. Method Overriding vs Method Hiding");
			Console.WriteLine("4. Multiple Interface Implementation");
			Console.WriteLine("5. Sealed Classes and Methods");
			Console.WriteLine("6. Abstract Classes");
			Console.WriteLine("7. Polymorphism with Shapes");
			Console.WriteLine("8. Employee Hierarchy (Advanced Polymorphism)");
			Console.WriteLine("9. Base Keyword Usage");
			Console.WriteLine("0. Exit");
			Console.WriteLine("=".PadRight(50, '='));
			Console.Write("Enter your choice (0-9): ");
		}

		static void DemonstrateBasicInheritance()
		{
			Console.WriteLine("\n=== BASIC INHERITANCE DEMONSTRATION ===\n");

			// Create different animals
			Dog myDog = new Dog("Buddy", 3, "Golden Retriever");
			Cat myCat = new Cat("Whiskers", 2, true);
			Eagle myEagle = new Eagle("Storm", 5, 84, "Diving");

			Console.WriteLine("\n--- Animal Information ---");
			myDog.DisplayInfo();
			Console.WriteLine();
			myCat.DisplayInfo();
			Console.WriteLine();
			myEagle.DisplayInfo();

			Console.WriteLine("\n--- Calling Methods ---");
			myDog.MakeSound();
			myDog.Move();
			myDog.Fetch();
			myDog.Sleep(); // Inherited from Animal

			Console.WriteLine();
			myCat.MakeSound();
			myCat.Move();
			myCat.Climb();

			Console.WriteLine();
			myEagle.MakeSound();
			myEagle.Move();
			myEagle.Fly();
			myEagle.Hunt();
		}

		static void DemonstrateConstructorChaining()
		{
			Console.WriteLine("\n=== CONSTRUCTOR CHAINING DEMONSTRATION ===\n");
			Console.WriteLine("Watch the order of constructor calls:\n");

			Eagle eagle = new Eagle("Thunder", 4, 90, "Stealth");

			Console.WriteLine("\nNotice how constructors are called in order:");
			Console.WriteLine("1. Animal constructor (base)");
			Console.WriteLine("2. Bird constructor (intermediate)");
			Console.WriteLine("3. Eagle constructor (derived)");
		}

		static void DemonstrateMethodOverriding()
		{
			Console.WriteLine("\n=== METHOD OVERRIDING DEMONSTRATION ===\n");

			// Polymorphic array
			Animal[] animals = {
				new Dog("Rex", 4, "German Shepherd"),
				new Cat("Mittens", 1, false),
				new Eagle("Sky", 6, 96, "Precision")
			};

			Console.WriteLine("Each animal makes its own sound (method overriding):\n");

			foreach (Animal animal in animals)
			{
				Console.Write($"{animal.Name}: ");
				animal.MakeSound(); // Polymorphic call
				animal.Move();      // Polymorphic call
				Console.WriteLine();
			}

			Console.WriteLine("This demonstrates runtime polymorphism through method overriding!");
		}

		static void DemonstrateMultipleInterfaces()
		{
			Console.WriteLine("\n=== MULTIPLE INTERFACE IMPLEMENTATION ===\n");

			Duck mallard = new Duck("Daffy", 2, 8.5, 25.0);

			Console.WriteLine("Duck implementing multiple interfaces:");
			mallard.DisplayInfo();
			Console.WriteLine();

			// Using as Animal
			Console.WriteLine("As an Animal:");
			mallard.MakeSound();
			mallard.Move();

			Console.WriteLine("\nAs ISwimmable:");
			mallard.Swim();

			Console.WriteLine("\nAs IFlyable:");
			mallard.Fly();

			Console.WriteLine("\nThis shows how one class can implement multiple interfaces!");
		}

		static void DemonstrateSealedClasses()
		{
			Console.WriteLine("\n=== SEALED CLASSES AND METHODS ===\n");

			Lion lion = new Lion("Simba", 5, true);
			Lion lioness = new Lion("Nala", 4, false);

			Console.WriteLine("Lion class is sealed - cannot be inherited further:");
			lion.DisplayInfo();
			Console.WriteLine();
			lion.MakeSound();
			lion.Hunt();
			lion.Move(); // This method is sealed in WildAnimal

			Console.WriteLine();
			lioness.DisplayInfo();
			Console.WriteLine();
			lioness.Hunt();

			Console.WriteLine("\nNote: Lion.Move() cannot be overridden because it's sealed in WildAnimal.");
		}

		static void DemonstrateAbstractClasses()
		{
			Console.WriteLine("\n=== ABSTRACT CLASSES DEMONSTRATION ===\n");

			// Cannot instantiate Animal directly because it's abstract
			// Animal animal = new Animal(); // This would cause compilation error

			Console.WriteLine("Abstract Animal class forces derived classes to implement abstract methods:");

			Dog abstractDog = new Dog("Abstract Buddy", 2, "Labrador");
			Console.WriteLine($"\n{abstractDog.Name} must implement Move() method:");
			abstractDog.Move();

			Console.WriteLine($"\n{abstractDog.Name} inherits Sleep() method from abstract Animal:");
			abstractDog.Sleep();

			Console.WriteLine("\nAbstract classes can have both abstract and concrete methods!");
		}

		static void DemonstratePolymorphism()
		{
			Console.WriteLine("\n=== POLYMORPHISM WITH SHAPES ===\n");

			// Array of different shapes
			Shape[] shapes = {
				new Circle(5.0, "Red"),
				new Rectangle(4.0, 6.0, "Blue"),
				new Triangle(3.0, 4.0, 5.0, 5.0, "Green")
			};

			Console.WriteLine("Polymorphic behavior with different shapes:\n");

			double totalArea = 0;
			foreach (Shape shape in shapes)
			{
				shape.Display(); // Polymorphic method call
				totalArea += shape.CalculateArea();
				Console.WriteLine();
			}

			Console.WriteLine($"Total area of all shapes: {totalArea:F2}");
			Console.WriteLine("\nEach shape calculates area differently through method overriding!");
		}

		static void DemonstrateEmployeeHierarchy()
		{
			Console.WriteLine("\n=== EMPLOYEE HIERARCHY (ADVANCED POLYMORPHISM) ===\n");

			// Create different types of employees
			List<Employee> employees = new List<Employee>
			{
				new FullTimeEmployee("Alice Johnson", 1001, new DateTime(2020, 1, 15), 75000),
				new PartTimeEmployee("Bob Smith", 1002, new DateTime(2021, 3, 10), 25.00m, 80),
				new Contractor("Carol Williams", 1003, new DateTime(2022, 6, 1), 1500)
			};

			// Add some completed projects for the contractor
			Contractor contractor = (Contractor)employees[2];
			contractor.CompleteProject();
			contractor.CompleteProject();

			Console.WriteLine("Employee Information and Monthly Pay:\n");

			decimal totalPayroll = 0;
			foreach (Employee emp in employees)
			{
				emp.DisplayEmployeeInfo();
				Console.WriteLine($"Years of Service: {emp.GetYearsOfService()}");
				totalPayroll += emp.CalculateMonthlyPay();
				Console.WriteLine();
			}

			Console.WriteLine($"Total Monthly Payroll: ${totalPayroll:F2}");
			Console.WriteLine("\nDifferent employee types calculate pay differently!");
		}

		static void DemonstrateBaseKeyword()
		{
			Console.WriteLine("\n=== BASE KEYWORD DEMONSTRATION ===\n");

			Console.WriteLine("Creating a Dog to show base keyword usage:");
			Dog dog = new Dog("Max", 3, "Beagle");

			Console.WriteLine("\nCalling DisplayInfo() which uses base.DisplayInfo():");
			dog.DisplayInfo();

			Console.WriteLine("\nThe Dog.DisplayInfo() method calls base.DisplayInfo() first,");
			Console.WriteLine("then adds breed-specific information.");

			Console.WriteLine("\nThis shows how 'base' keyword allows calling parent methods!");
		}
	}
}