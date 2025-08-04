namespace Inheritance
{
	// =============================================
	// 1. BASIC INHERITANCE - Single Inheritance
	// =============================================

	// Base class (Parent/Super class)
	public class Animal
	{
		// Protected members - accessible by derived classes
		protected string name;
		protected int age;
		protected string species;

		// Public members - accessible by everyone
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public int Age
		{
			get { return age; }
			set { age = value >= 0 ? value : 0; }
		}

		// Constructors
		public Animal()
		{
			Console.WriteLine("Animal default constructor called");
			name = "Unknown";
			age = 0;
			species = "Unknown";
		}

		public Animal(string name, int age, string species)
		{
			Console.WriteLine("Animal parameterized constructor called");
			this.name = name;
			this.age = age;
			this.species = species;
		}

		// Virtual method - can be overridden by derived classes
		public virtual void MakeSound()
		{
			Console.WriteLine($"{name} makes a generic animal sound");
		}

		// Virtual method with implementation
		public virtual void Eat()
		{
			Console.WriteLine($"{name} is eating");
		}

		// Regular method - can be hidden (not overridden)
		public void Sleep()
		{
			Console.WriteLine($"{name} is sleeping");
		}

		// Virtual method for display
		public virtual void DisplayInfo()
		{
			Console.WriteLine($"Animal: {name}, Age: {age}, Species: {species}");
		}
	}

	// Derived class (Child/Sub class) - Single Inheritance
	public class Dog : Animal
	{
		// Additional properties specific to Dog
		private string breed;
		private bool isVaccinated;

		// Property
		public string Breed
		{
			get { return breed; }
			set { breed = value; }
		}

		public bool IsVaccinated
		{
			get { return isVaccinated; }
			set { isVaccinated = value; }
		}

		// Constructor chaining - calls base constructor
		public Dog() : base() // Calls Animal's default constructor
		{
			Console.WriteLine("Dog default constructor called");
			breed = "Mixed";
			isVaccinated = false;
		}

		public Dog(string name, int age, string breed) : base(name, age, "Canine")
		{
			Console.WriteLine("Dog parameterized constructor called");
			this.breed = breed;
			this.isVaccinated = false;
		}

		public Dog(string name, int age, string breed, bool vaccinated) : this(name, age, breed)
		{
			Console.WriteLine("Dog full constructor called");
			this.isVaccinated = vaccinated;
		}

		// Method overriding - provides specific implementation
		public override void MakeSound()
		{
			Console.WriteLine($"{name} barks: Woof! Woof!");
		}

		// Method overriding with base call
		public override void Eat()
		{
			base.Eat(); // Call parent's implementation
			Console.WriteLine($"{name} is wagging tail while eating");
		}

		// Method hiding (using 'new' keyword)
		public new void Sleep()
		{
			Console.WriteLine($"{name} is sleeping in a dog bed");
		}

		// Override display method
		public override void DisplayInfo()
		{
			base.DisplayInfo(); // Call parent's implementation
			Console.WriteLine($"Breed: {breed}, Vaccinated: {isVaccinated}");
		}

		// Dog-specific methods
		public void Bark()
		{
			Console.WriteLine($"{name} is barking loudly!");
		}

		public void Fetch()
		{
			Console.WriteLine($"{name} is fetching the ball");
		}
	}

	// Another derived class from Animal
	public class Cat : Animal
	{
		private bool isIndoor;
		private int livesRemaining;

		public bool IsIndoor
		{
			get { return isIndoor; }
			set { isIndoor = value; }
		}

		public int LivesRemaining
		{
			get { return livesRemaining; }
			set { livesRemaining = Math.Max(0, Math.Min(9, value)); }
		}

		public Cat(string name, int age, bool indoor = true) : base(name, age, "Feline")
		{
			Console.WriteLine("Cat constructor called");
			this.isIndoor = indoor;
			this.livesRemaining = 9;
		}

		public override void MakeSound()
		{
			Console.WriteLine($"{name} meows: Meow! Meow!");
		}

		public override void DisplayInfo()
		{
			base.DisplayInfo();
			Console.WriteLine($"Indoor: {isIndoor}, Lives Remaining: {livesRemaining}");
		}

		public void Climb()
		{
			Console.WriteLine($"{name} is climbing a tree");
		}

		public void Purr()
		{
			Console.WriteLine($"{name} is purring contentedly");
		}
	}

	// =============================================
	// 2. MULTI-LEVEL INHERITANCE
	// =============================================

	// Derived class from Dog (Grandchild of Animal)
	public class GermanShepherd : Dog
	{
		private string trainingLevel;
		private bool isServiceDog;

		public string TrainingLevel
		{
			get { return trainingLevel; }
			set { trainingLevel = value; }
		}

		public bool IsServiceDog
		{
			get { return isServiceDog; }
			set { isServiceDog = value; }
		}

		public GermanShepherd(string name, int age) : base(name, age, "German Shepherd", true)
		{
			Console.WriteLine("GermanShepherd constructor called");
			trainingLevel = "Basic";
			isServiceDog = false;
		}

		public GermanShepherd(string name, int age, string training, bool service)
			: base(name, age, "German Shepherd", true)
		{
			Console.WriteLine("GermanShepherd full constructor called");
			trainingLevel = training;
			isServiceDog = service;
		}

		// Further override the MakeSound method
		public override void MakeSound()
		{
			Console.WriteLine($"{name} barks authoritatively: WOOF! (German Shepherd)");
		}

		public override void DisplayInfo()
		{
			base.DisplayInfo();
			Console.WriteLine($"Training: {trainingLevel}, Service Dog: {isServiceDog}");
		}

		// German Shepherd specific methods
		public void Guard()
		{
			Console.WriteLine($"{name} is guarding the property");
		}

		public void Track()
		{
			Console.WriteLine($"{name} is tracking a scent");
		}
	}

	// Another multi-level inheritance
	public class PersianCat : Cat
	{
		private string furColor;
		private bool needsGrooming;

		public string FurColor
		{
			get { return furColor; }
			set { furColor = value; }
		}

		public bool NeedsGrooming
		{
			get { return needsGrooming; }
			set { needsGrooming = value; }
		}

		public PersianCat(string name, int age, string color) : base(name, age, true)
		{
			Console.WriteLine("PersianCat constructor called");
			furColor = color;
			needsGrooming = true;
		}

		public override void MakeSound()
		{
			Console.WriteLine($"{name} meows softly: mew... (Persian Cat)");
		}

		public override void DisplayInfo()
		{
			base.DisplayInfo();
			Console.WriteLine($"Fur Color: {furColor}, Needs Grooming: {needsGrooming}");
		}

		public void RequireGrooming()
		{
			Console.WriteLine($"{name} needs professional grooming for their {furColor} fur");
		}
	}

	// =============================================
	// 3. ABSTRACT CLASSES AND INHERITANCE
	// =============================================

	public abstract class Bird : Animal
	{
		protected double wingSpan;
		protected bool canFly;

		public double WingSpan
		{
			get { return wingSpan; }
			protected set { wingSpan = value; }
		}

		public bool CanFly
		{
			get { return canFly; }
			protected set { canFly = value; }
		}

		protected Bird(string name, int age, double wingSpan, bool canFly)
			: base(name, age, "Avian")
		{
			Console.WriteLine("Bird constructor called");
			this.wingSpan = wingSpan;
			this.canFly = canFly;
		}

		// Abstract method - must be implemented by derived classes
		public abstract void Fly();

		// Virtual method specific to birds
		public virtual void BuildNest()
		{
			Console.WriteLine($"{name} is building a nest");
		}

		public override void DisplayInfo()
		{
			base.DisplayInfo();
			Console.WriteLine($"Wing Span: {wingSpan} inches, Can Fly: {canFly}");
		}
	}

	public class Eagle : Bird
	{
		private string huntingStyle;

		public string HuntingStyle
		{
			get { return huntingStyle; }
			set { huntingStyle = value; }
		}

		public Eagle(string name, int age) : base(name, age, 90.0, true)
		{
			Console.WriteLine("Eagle constructor called");
			huntingStyle = "Soaring";
		}

		// Must implement abstract method
		public override void Fly()
		{
			if (canFly)
			{
				Console.WriteLine($"{name} soars majestically with {wingSpan} inch wingspan");
			}
			else
			{
				Console.WriteLine($"{name} cannot fly due to injury");
			}
		}

		public override void MakeSound()
		{
			Console.WriteLine($"{name} screeches: Screeeech!");
		}

		public override void DisplayInfo()
		{
			base.DisplayInfo();
			Console.WriteLine($"Hunting Style: {huntingStyle}");
		}

		public void Hunt()
		{
			Console.WriteLine($"{name} is hunting using {huntingStyle} technique");
		}
	}

	public class Penguin : Bird
	{
		private string species;
		private bool canSwim;

		public string Species
		{
			get { return species; }
			set { species = value; }
		}

		public bool CanSwim
		{
			get { return canSwim; }
			set { canSwim = value; }
		}

		public Penguin(string name, int age, string species) : base(name, age, 12.0, false)
		{
			Console.WriteLine("Penguin constructor called");
			this.species = species;
			this.canSwim = true;
		}

		// Implement abstract method - but penguins can't fly!
		public override void Fly()
		{
			Console.WriteLine($"{name} cannot fly, but can swim excellently!");
		}

		public override void MakeSound()
		{
			Console.WriteLine($"{name} calls: Honk! Honk!");
		}

		public override void DisplayInfo()
		{
			base.DisplayInfo();
			Console.WriteLine($"Species: {species}, Can Swim: {canSwim}");
		}

		public void Swim()
		{
			Console.WriteLine($"{name} is swimming gracefully underwater");
		}

		public void Slide()
		{
			Console.WriteLine($"{name} is sliding on their belly across the ice");
		}
	}

	// =============================================
	// 4. SEALED CLASSES (Cannot be inherited)
	// =============================================

	public sealed class RobotDog : Dog
	{
		private double batteryLevel;
		private string manufacturer;

		public double BatteryLevel
		{
			get { return batteryLevel; }
			private set { batteryLevel = Math.Max(0, Math.Min(100, value)); }
		}

		public string Manufacturer
		{
			get { return manufacturer; }
			set { manufacturer = value; }
		}

		public RobotDog(string name, string manufacturer) : base(name, 0, "Robot")
		{
			Console.WriteLine("RobotDog constructor called");
			this.manufacturer = manufacturer;
			this.batteryLevel = 100.0;
		}

		public override void MakeSound()
		{
			Console.WriteLine($"{name} makes electronic barking sound: *BEEP WOOF*");
		}

		public override void Eat()
		{
			Console.WriteLine($"{name} is charging battery instead of eating");
			batteryLevel = 100.0;
		}

		public override void DisplayInfo()
		{
			base.DisplayInfo();
			Console.WriteLine($"Battery: {batteryLevel}%, Manufacturer: {manufacturer}");
		}

		public void Charge()
		{
			Console.WriteLine($"{name} is charging...");
			batteryLevel = 100.0;
		}
	}

	// This would cause compilation error because RobotDog is sealed:
	// public class AdvancedRobotDog : RobotDog { }

	// =============================================
	// 5. INTERFACE INHERITANCE
	// =============================================

	public interface IPlayful
	{
		void Play();
		void GetToy();
	}

	public interface ITrainable
	{
		void Train(string command);
		bool IsObedient { get; set; }
	}

	public interface IAdvancedPet : IPlayful, ITrainable // Interface inheritance
	{
		void PerformTrick();
	}

	// Class implementing multiple interfaces
	public class SmartDog : Dog, IAdvancedPet
	{
		private bool isObedient;
		private List<string> knownCommands;
		private string favoriteToy;

		public bool IsObedient
		{
			get { return isObedient; }
			set { isObedient = value; }
		}

		public string FavoriteToy
		{
			get { return favoriteToy; }
			set { favoriteToy = value; }
		}

		public SmartDog(string name, int age, string breed) : base(name, age, breed, true)
		{
			Console.WriteLine("SmartDog constructor called");
			isObedient = true;
			knownCommands = new List<string>();
			favoriteToy = "Ball";
		}

		// Implement IPlayful
		public void Play()
		{
			Console.WriteLine($"{name} is playing with their {favoriteToy}");
		}

		public void GetToy()
		{
			Console.WriteLine($"{name} fetches their {favoriteToy}");
		}

		// Implement ITrainable
		public void Train(string command)
		{
			if (!knownCommands.Contains(command))
			{
				knownCommands.Add(command);
				Console.WriteLine($"{name} learned the command: {command}");
			}
			else
			{
				Console.WriteLine($"{name} already knows: {command}");
			}
		}

		// Implement IAdvancedPet
		public void PerformTrick()
		{
			if (knownCommands.Count > 0)
			{
				string trick = knownCommands[new Random().Next(knownCommands.Count)];
				Console.WriteLine($"{name} performs trick: {trick}");
			}
			else
			{
				Console.WriteLine($"{name} doesn't know any tricks yet");
			}
		}

		public override void DisplayInfo()
		{
			base.DisplayInfo();
			Console.WriteLine($"Obedient: {isObedient}, Known Commands: {string.Join(", ", knownCommands)}");
			Console.WriteLine($"Favorite Toy: {favoriteToy}");
		}
	}

	// =============================================
	// 6. PROTECTED INTERNAL ACCESS MODIFIER
	// =============================================

	public class WildAnimal : Animal
	{
		protected internal string habitat; // Accessible within assembly and by derived classes
		private string conservationStatus;

		public string ConservationStatus
		{
			get { return conservationStatus; }
			set { conservationStatus = value; }
		}

		protected WildAnimal(string name, int age, string species, string habitat, string status)
			: base(name, age, species)
		{
			Console.WriteLine("WildAnimal constructor called");
			this.habitat = habitat;
			this.conservationStatus = status;
		}

		public override void DisplayInfo()
		{
			base.DisplayInfo();
			Console.WriteLine($"Habitat: {habitat}, Conservation Status: {conservationStatus}");
		}
	}

	public class Lion : WildAnimal
	{
		private string prideRole;

		public string PrideRole
		{
			get { return prideRole; }
			set { prideRole = value; }
		}

		public Lion(string name, int age, string role)
			: base(name, age, "Panthera leo", "Savanna", "Vulnerable")
		{
			Console.WriteLine("Lion constructor called");
			this.prideRole = role;
		}

		public override void MakeSound()
		{
			Console.WriteLine($"{name} roars powerfully: ROAAAAR!");
		}

		public override void DisplayInfo()
		{
			base.DisplayInfo();
			Console.WriteLine($"Pride Role: {prideRole}");
			// Can access protected internal member from base class
			Console.WriteLine($"Lives in: {habitat}");
		}

		public void Hunt()
		{
			Console.WriteLine($"{name} ({prideRole}) is hunting in the {habitat}");
		}
	}

	// =============================================
	// 7. VIRTUAL DESTRUCTORS AND FINALIZERS
	// =============================================

	public class ResourceManager : Animal
	{
		private bool disposed = false;

		public ResourceManager(string name) : base(name, 0, "Virtual")
		{
			Console.WriteLine($"ResourceManager created for {name}");
		}

		// Virtual method for cleanup
		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					// Dispose managed resources
					Console.WriteLine($"Disposing managed resources for {name}");
				}
				// Dispose unmanaged resources
				Console.WriteLine($"Disposing unmanaged resources for {name}");
				disposed = true;
			}
		}

		~ResourceManager()
		{
			Console.WriteLine($"Finalizer called for {name}");
			Dispose(false);
		}
	}

	// =============================================
	// MAIN PROGRAM - DEMONSTRATES ALL INHERITANCE CONCEPTS
	// =============================================

	public class InheritanceDemo
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("=== COMPLETE C# INHERITANCE DEMONSTRATION ===\n");

			Console.WriteLine("1. BASIC INHERITANCE:");
			Console.WriteLine("===================");

			// Basic inheritance
			Animal genericAnimal = new Animal("Generic", 5, "Unknown");
			Dog myDog = new Dog("Buddy", 3, "Golden Retriever", true);
			Cat myCat = new Cat("Whiskers", 2, true);

			// Polymorphism with inheritance
			Animal[] animals = { genericAnimal, myDog, myCat };
			foreach (Animal animal in animals)
			{
				animal.MakeSound(); // Different implementations
				animal.Eat();
				animal.DisplayInfo();
				Console.WriteLine();
			}

			Console.WriteLine("\n2. MULTI-LEVEL INHERITANCE:");
			Console.WriteLine("===========================");

			GermanShepherd police = new GermanShepherd("Rex", 4, "Advanced", true);
			PersianCat fluffy = new PersianCat("Princess", 3, "White");

			police.MakeSound();
			police.Guard();
			police.Track();
			police.DisplayInfo();

			Console.WriteLine();

			fluffy.MakeSound();
			fluffy.Purr();
			fluffy.RequireGrooming();
			fluffy.DisplayInfo();

			Console.WriteLine("\n3. ABSTRACT CLASS INHERITANCE:");
			Console.WriteLine("==============================");

			Eagle eagle = new Eagle("Freedom", 8);
			Penguin penguin = new Penguin("Pingu", 2, "Emperor");

			// Cannot instantiate abstract class
			// Bird bird = new Bird(); // Compilation error

			Bird[] birds = { eagle, penguin };
			foreach (Bird bird in birds)
			{
				bird.MakeSound();
				bird.Fly(); // Different implementations
				bird.BuildNest();
				bird.DisplayInfo();
				Console.WriteLine();
			}

			Console.WriteLine("\n4. SEALED CLASS:");
			Console.WriteLine("================");

			RobotDog robot = new RobotDog("Robo", "Boston Dynamics");
			robot.MakeSound();
			robot.Eat(); // Charging instead
			robot.Charge();
			robot.DisplayInfo();

			Console.WriteLine("\n5. INTERFACE INHERITANCE:");
			Console.WriteLine("=========================");

			SmartDog einstein = new SmartDog("Einstein", 4, "Border Collie");

			// Using as different interface types
			IPlayful playful = einstein;
			ITrainable trainable = einstein;
			IAdvancedPet advancedPet = einstein;

			trainable.Train("sit");
			trainable.Train("stay");
			trainable.Train("roll over");

			playful.Play();
			playful.GetToy();

			advancedPet.PerformTrick();
			einstein.DisplayInfo();

			Console.WriteLine("\n6. WILD ANIMALS (Protected Internal):");
			Console.WriteLine("====================================");

			Lion king = new Lion("Simba", 6, "Alpha Male");
			king.MakeSound();
			king.Hunt();
			king.DisplayInfo();

			Console.WriteLine("\n7. METHOD HIDING vs OVERRIDING:");
			Console.WriteLine("===============================");

			Dog dog = new Dog("Max", 5, "Labrador");
			Animal animalRef = dog;

			Console.WriteLine("Direct Dog reference:");
			dog.Sleep(); // Calls Dog's hidden method

			Console.WriteLine("Animal reference to Dog:");
			animalRef.Sleep(); // Calls Animal's method (method hiding)

			Console.WriteLine("Virtual method (overriding):");
			animalRef.MakeSound(); // Calls Dog's overridden method

			Console.WriteLine("\n8. CONSTRUCTOR CHAINING:");
			Console.WriteLine("========================");

			Console.WriteLine("Creating GermanShepherd with full constructor:");
			GermanShepherd advanced = new GermanShepherd("Thor", 5, "Expert", false);

			Console.WriteLine("\n9. CASTING AND TYPE CHECKING:");
			Console.WriteLine("=============================");

			Animal[] mixedAnimals = {
			new Dog("Spot", 2, "Dalmatian"),
			new Cat("Shadow", 4, false),
			new Eagle("Storm", 10),
			new Lion("Mufasa", 8, "Pride Leader")
		};

			foreach (Animal animal in mixedAnimals)
			{
				Console.WriteLine($"\nProcessing: {animal.Name}");

				// Type checking with 'is'
				if (animal is Dog specificDog)
				{
					specificDog.Bark();
					specificDog.Fetch();
				}
				else if (animal is Cat specificCat)
				{
					specificCat.Purr();
					specificCat.Climb();
				}
				else if (animal is Eagle specificEagle)
				{
					specificEagle.Fly();
					specificEagle.Hunt();
				}
				else if (animal is Lion specificLion)
				{
					specificLion.Hunt();
				}
			}

			Console.WriteLine("\n10. RESOURCE MANAGEMENT:");
			Console.WriteLine("========================");

			ResourceManager resource = new ResourceManager("TestResource");
			// Resource will be finalized when GC runs

			Console.WriteLine("\n=== INHERITANCE CONCEPTS DEMONSTRATED ===");
			Console.WriteLine("✓ Single Inheritance (Dog : Animal)");
			Console.WriteLine("✓ Multi-level Inheritance (GermanShepherd : Dog : Animal)");
			Console.WriteLine("✓ Abstract Classes (Bird)");
			Console.WriteLine("✓ Sealed Classes (RobotDog)");
			Console.WriteLine("✓ Interface Inheritance (IAdvancedPet : IPlayful, ITrainable)");
			Console.WriteLine("✓ Constructor Chaining (base(), this())");
			Console.WriteLine("✓ Method Overriding (virtual/override)");
			Console.WriteLine("✓ Method Hiding (new keyword)");
			Console.WriteLine("✓ Access Modifiers (public, private, protected, protected internal)");
			Console.WriteLine("✓ Polymorphism through Inheritance");
			Console.WriteLine("✓ Type Checking and Casting (is, as)");
			Console.WriteLine("✓ Virtual Destructors/Finalizers");
		}
	}
}
