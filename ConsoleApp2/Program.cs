using System.Data.SqlTypes;
using System.Diagnostics;

namespace ConsoleApp2
{
	// ============================================
	// INTERFACES - Abstraction through contracts
	// ============================================
	public interface IMovable
	{
		void Move();
		void Stop();
	}

	public interface IFuelable
	{
		void Refuel(double amount);
		double GetFuelLevel();
	}

	public interface IMaintainable
	{
		void PerformMaintenance();
		bool NeedsMaintenance();
	}

	// ============================================
	// ABSTRACT CLASS - Base abstraction
	// ============================================

	public abstract class Vehicle : IMovable, IMaintainable
	{
		// ENCAPSULATION - Private fields with controlled access
		private string _brand;
		private string _model;
		private int _year;
		protected double _mileage; // Protected - accessible by derived classes
		private bool _isRunning;
		private static int _totalVehicles = 0; // Static field

		// ENCAPSULATION - Properties for controlled access
		public string Brand
		{
			get { return _brand; }
			protected set { _brand = value; } // Protected setter
		}

		public string Model
		{
			get { return _model; }
			protected set { _model = value; }
		}

		public int Year
		{
			get { return _year; }
			protected set
			{
				if (value > 1900 && value <= DateTime.Now.Year + 1)
					_year = value;
				else
					throw new ArgumentException("Invalid year");
			}
		}

		public double Mileage
		{
			get { return _mileage; }
			protected set
			{
				if (value >= 0)
					_mileage = value;
			}
		}

		public bool IsRunning
		{
			get { return _isRunning; }
			private set { _isRunning = value; }
		}

		// Static property
		public static int TotalVehicles
		{
			get { return _totalVehicles; }
		}

		// Constructor
		protected Vehicle(string brand, string model, int year)
		{
			Brand = brand;
			Model = model;
			Year = year;
			_mileage = 0;
			_isRunning = false;
			_totalVehicles++; // Static increment
		}

		// ABSTRACTION - Abstract methods must be implemented by derived classes
		public abstract void StartEngine();
		public abstract double CalculateFuelEfficiency();
		public abstract string GetVehicleType();

		// POLYMORPHISM - Virtual methods can be overridden
		public virtual void Move()
		{
			if (!_isRunning)
			{
				Console.WriteLine($"{Brand} {Model} cannot move. Engine is not running!");
				return;
			}
			Console.WriteLine($"{Brand} {Model} is moving...");
			_mileage += 10; // Add some mileage
		}

		public virtual void Stop()
		{
			Console.WriteLine($"{Brand} {Model} has stopped.");
			_isRunning = false;
		}

		// Interface implementation
		public virtual bool NeedsMaintenance()
		{
			return _mileage > 100; // Needs maintenance after 100 miles
		}

		public virtual void PerformMaintenance()
		{
			Console.WriteLine($"Performing maintenance on {Brand} {Model}");
			_mileage = 0; // Reset mileage after maintenance
		}

		// Method to start engine (used by derived classes)
		protected void SetEngineRunning(bool running)
		{
			_isRunning = running;
		}

		// POLYMORPHISM - Method overloading
		public void DisplayInfo()
		{
			DisplayInfo(true);
		}

		public void DisplayInfo(bool detailed)
		{
			Console.WriteLine($"\n--- {GetVehicleType()} Information ---");
			Console.WriteLine($"Brand: {Brand}");
			Console.WriteLine($"Model: {Model}");
			Console.WriteLine($"Year: {Year}");

			if (detailed)
			{
				Console.WriteLine($"Mileage: {Mileage:F1} miles");
				Console.WriteLine($"Engine Running: {IsRunning}");
				Console.WriteLine($"Fuel Efficiency: {CalculateFuelEfficiency():F1} MPG");
				Console.WriteLine($"Needs Maintenance: {NeedsMaintenance()}");
			}
		}

		// Static method
		public static void DisplayTotalVehicles()
		{
			Console.WriteLine($"Total vehicles created: {_totalVehicles}");
		}
	}

	// ============================================
	// INHERITANCE - Concrete implementations
	// ============================================

	public class Car : Vehicle, IFuelable
	{
		private double _fuelLevel;
		private double _fuelCapacity;
		private int _numberOfDoors;

		// Constructor chaining
		public Car(string brand, string model, int year, int doors = 4)
			: base(brand, model, year)
		{
			_numberOfDoors = doors;
			_fuelCapacity = 50.0; // 50 gallons
			_fuelLevel = _fuelCapacity / 2; // Start half full
		}

		public int NumberOfDoors
		{
			get { return _numberOfDoors; }
			private set { _numberOfDoors = value; }
		}

		// ABSTRACTION - Implementation of abstract methods
		public override void StartEngine()
		{
			if (_fuelLevel <= 0)
			{
				Console.WriteLine($"{Brand} {Model} won't start - no fuel!");
				return;
			}

			SetEngineRunning(true);
			Console.WriteLine($"{Brand} {Model} engine started! Vroom vroom!");
			_fuelLevel -= 1; // Use some fuel to start
		}

		public override double CalculateFuelEfficiency()
		{
			return 25.0 + (2024 - Year) * 0.5; // Newer cars are more efficient
		}

		public override string GetVehicleType()
		{
			return "Car";
		}

		// POLYMORPHISM - Override base method
		public override void Move()
		{
			if (_fuelLevel <= 0)
			{
				Console.WriteLine($"{Brand} {Model} is out of fuel!");
				return;
			}

			base.Move(); // Call base implementation
			_fuelLevel -= 2; // Use fuel while moving
			Console.WriteLine($"Driving smoothly on the road. Fuel level: {_fuelLevel:F1}");
		}

		// Interface implementation
		public void Refuel(double amount)
		{
			if (amount <= 0) return;

			double oldLevel = _fuelLevel;
			_fuelLevel = Math.Min(_fuelLevel + amount, _fuelCapacity);
			Console.WriteLine($"Refueled {Brand} {Model}. Added {_fuelLevel - oldLevel:F1} gallons.");
		}

		public double GetFuelLevel()
		{
			return _fuelLevel;
		}

		// Additional car-specific method
		public void Honk()
		{
			Console.WriteLine($"{Brand} {Model}: Beep beep!");
		}
	}

	public class Motorcycle : Vehicle, IFuelable
	{
		private double _fuelLevel;
		private double _fuelCapacity;
		private bool _hasSidecar;

		public Motorcycle(string brand, string model, int year, bool hasSidecar = false)
			: base(brand, model, year)
		{
			_hasSidecar = hasSidecar;
			_fuelCapacity = 5.0; // 5 gallons
			_fuelLevel = _fuelCapacity;
		}

		public bool HasSidecar
		{
			get { return _hasSidecar; }
			private set { _hasSidecar = value; }
		}

		public override void StartEngine()
		{
			if (_fuelLevel <= 0)
			{
				Console.WriteLine($"{Brand} {Model} won't start - no fuel!");
				return;
			}

			SetEngineRunning(true);
			Console.WriteLine($"{Brand} {Model} engine started! Vrroooom!");
			_fuelLevel -= 0.2;
		}

		public override double CalculateFuelEfficiency()
		{
			double baseEfficiency = 45.0;
			if (_hasSidecar) baseEfficiency -= 5.0; // Sidecar reduces efficiency
			return baseEfficiency;
		}

		public override string GetVehicleType()
		{
			return _hasSidecar ? "Motorcycle with Sidecar" : "Motorcycle";
		}

		public override void Move()
		{
			if (_fuelLevel <= 0)
			{
				Console.WriteLine($"{Brand} {Model} is out of fuel!");
				return;
			}

			base.Move();
			_fuelLevel -= 0.5;
			Console.WriteLine($"Cruising on two wheels! Fuel level: {_fuelLevel:F1}");
		}

		public void Refuel(double amount)
		{
			if (amount <= 0) return;

			double oldLevel = _fuelLevel;
			_fuelLevel = Math.Min(_fuelLevel + amount, _fuelCapacity);
			Console.WriteLine($"Refueled {Brand} {Model}. Added {_fuelLevel - oldLevel:F1} gallons.");
		}

		public double GetFuelLevel()
		{
			return _fuelLevel;
		}

		public void Wheelie()
		{
			if (IsRunning && !_hasSidecar)
			{
				Console.WriteLine($"{Brand} {Model} is doing a wheelie! 🏍️");
			}
			else if (_hasSidecar)
			{
				Console.WriteLine("Cannot do wheelie with a sidecar!");
			}
			else
			{
				Console.WriteLine("Start the engine first!");
			}
		}
	}

	public class Truck : Vehicle, IFuelable
	{
		private double _fuelLevel;
		private double _fuelCapacity;
		private double _maxLoadCapacity;
		private double _currentLoad;

		public Truck(string brand, string model, int year, double maxLoad = 2000)
			: base(brand, model, year)
		{
			_maxLoadCapacity = maxLoad;
			_currentLoad = 0;
			_fuelCapacity = 100.0; // 100 gallons
			_fuelLevel = _fuelCapacity * 0.8;
		}

		public double MaxLoadCapacity
		{
			get { return _maxLoadCapacity; }
			private set { _maxLoadCapacity = value; }
		}

		public double CurrentLoad
		{
			get { return _currentLoad; }
			private set { _currentLoad = value; }
		}

		public override void StartEngine()
		{
			if (_fuelLevel <= 0)
			{
				Console.WriteLine($"{Brand} {Model} won't start - no fuel!");
				return;
			}

			SetEngineRunning(true);
			Console.WriteLine($"{Brand} {Model} diesel engine started! Rumble rumble!");
			_fuelLevel -= 2;
		}

		public override double CalculateFuelEfficiency()
		{
			double baseEfficiency = 8.0; // Trucks are less fuel efficient
			double loadFactor = _currentLoad / _maxLoadCapacity;
			return baseEfficiency * (1 - loadFactor * 0.3); // Efficiency decreases with load
		}

		public override string GetVehicleType()
		{
			return "Truck";
		}

		public override void Move()
		{
			if (_fuelLevel <= 0)
			{
				Console.WriteLine($"{Brand} {Model} is out of fuel!");
				return;
			}

			base.Move();
			double fuelUsage = 4 + (_currentLoad / _maxLoadCapacity) * 2; // More fuel with load
			_fuelLevel -= fuelUsage;
			Console.WriteLine($"Hauling cargo down the highway. Load: {_currentLoad}lbs, Fuel: {_fuelLevel:F1}");
		}

		public void Refuel(double amount)
		{
			if (amount <= 0) return;

			double oldLevel = _fuelLevel;
			_fuelLevel = Math.Min(_fuelLevel + amount, _fuelCapacity);
			Console.WriteLine($"Refueled {Brand} {Model}. Added {_fuelLevel - oldLevel:F1} gallons.");
		}

		public double GetFuelLevel()
		{
			return _fuelLevel;
		}

		public void LoadCargo(double weight)
		{
			if (weight <= 0) return;

			if (_currentLoad + weight <= _maxLoadCapacity)
			{
				_currentLoad += weight;
				Console.WriteLine($"Loaded {weight}lbs. Current load: {_currentLoad}lbs");
			}
			else
			{
				Console.WriteLine($"Cannot load {weight}lbs. Would exceed capacity!");
			}
		}

		public void UnloadCargo(double weight)
		{
			if (weight <= 0) return;

			_currentLoad = Math.Max(0, _currentLoad - weight);
			Console.WriteLine($"Unloaded {weight}lbs. Current load: {_currentLoad}lbs");
		}
	}

	// ============================================
	// COMPOSITION - "Has-a" relationship
	// ============================================

	public class GPS
	{
		private string _currentLocation;
		private string _destination;

		public GPS()
		{
			_currentLocation = "Unknown";
			_destination = "Not set";
		}

		public void SetDestination(string destination)
		{
			_destination = destination;
			Console.WriteLine($"GPS: Destination set to {destination}");
		}

		public void Navigate()
		{
			if (_destination != "Not set")
			{
				Console.WriteLine($"GPS: Navigating to {_destination}...");
			}
			else
			{
				Console.WriteLine("GPS: Please set a destination first.");
			}
		}
	}

	public class LuxuryCar : Car
	{
		private GPS _gps; // Composition - LuxuryCar "has-a" GPS
		private bool _sunroofOpen;

		public LuxuryCar(string brand, string model, int year)
			: base(brand, model, year, 4)
		{
			_gps = new GPS(); // Create GPS instance
			_sunroofOpen = false;
		}

		// Delegate GPS functionality
		public void SetGPSDestination(string destination)
		{
			_gps.SetDestination(destination);
		}

		public void UseGPS()
		{
			_gps.Navigate();
		}

		public void OperateSunroof()
		{
			_sunroofOpen = !_sunroofOpen;
			Console.WriteLine($"Sunroof is now {(_sunroofOpen ? "open" : "closed")}");
		}

		public override string GetVehicleType()
		{
			return "Luxury Car";
		}
	}

	// ============================================
	// MAIN PROGRAM - Demonstrates all concepts
	// ============================================
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("=== C# OOP Concepts Demo - Vehicle Management System ===\n");

			// Create different types of vehicles (INHERITANCE & POLYMORPHISM)
			Vehicle[] vehicles = {
			new Car("Toyota", "Camry", 2020, 4),
			new Motorcycle("Harley-Davidson", "Street 750", 2019, false),
			new Truck("Ford", "F-150", 2021, 3000),
			new LuxuryCar("BMW", "7 Series", 2023),
			new Motorcycle("Honda", "Gold Wing", 2022, true) // with sidecar
        };

			// Display total vehicles (STATIC MEMBER)
			Vehicle.DisplayTotalVehicles();

			Console.WriteLine("\n=== Vehicle Information ===");
			// POLYMORPHISM - Same method call, different behavior
			foreach (Vehicle vehicle in vehicles)
			{
				vehicle.DisplayInfo();
			}

			Console.WriteLine("\n=== Starting All Engines ===");
			// POLYMORPHISM - Different implementations of StartEngine
			foreach (Vehicle vehicle in vehicles)
			{
				vehicle.StartEngine();
			}

			Console.WriteLine("\n=== Moving All Vehicles ===");
			// POLYMORPHISM - Different implementations of Move
			foreach (Vehicle vehicle in vehicles)
			{
				vehicle.Move();
			}

			Console.WriteLine("\n=== Specific Vehicle Operations ===");

			// CASTING and TYPE-SPECIFIC operations
			foreach (Vehicle vehicle in vehicles)
			{
				// POLYMORPHISM with type checking
				if (vehicle is Car car)
				{
					car.Honk();

					// INTERFACE usage
					if (car is IFuelable fuelableCar)
					{
						Console.WriteLine($"Fuel level: {fuelableCar.GetFuelLevel():F1} gallons");
						fuelableCar.Refuel(10);
					}

					// COMPOSITION demonstration
					if (car is LuxuryCar luxuryCar)
					{
						luxuryCar.SetGPSDestination("Beverly Hills");
						luxuryCar.UseGPS();
						luxuryCar.OperateSunroof();
					}
				}
				else if (vehicle is Motorcycle motorcycle)
				{
					motorcycle.Wheelie();

					if (motorcycle is IFuelable fuelableMotorcycle)
					{
						Console.WriteLine($"Fuel level: {fuelableMotorcycle.GetFuelLevel():F1} gallons");
					}
				}
				else if (vehicle is Truck truck)
				{
					truck.LoadCargo(500);
					truck.Move(); // Show how load affects movement
					truck.UnloadCargo(200);

					if (truck is IFuelable fuelableTruck)
					{
						Console.WriteLine($"Fuel level: {fuelableTruck.GetFuelLevel():F1} gallons");
					}
				}
			}

			Console.WriteLine("\n=== Maintenance Check ===");
			// INTERFACE usage for maintenance
			foreach (Vehicle vehicle in vehicles)
			{
				if (vehicle.NeedsMaintenance())
				{
					Console.WriteLine($"{vehicle.Brand} {vehicle.Model} needs maintenance!");
					vehicle.PerformMaintenance();
				}
				else
				{
					Console.WriteLine($"{vehicle.Brand} {vehicle.Model} is in good condition.");
				}
			}

			Console.WriteLine("\n=== Stopping All Vehicles ===");
			foreach (Vehicle vehicle in vehicles)
			{
				vehicle.Stop();
			}

			// Final count
			Console.WriteLine();
			Vehicle.DisplayTotalVehicles();

			Console.WriteLine("\n=== Demo Complete ===");
			Console.WriteLine("This demo showcased:");
			Console.WriteLine("✓ Encapsulation (private fields, properties, access modifiers)");
			Console.WriteLine("✓ Inheritance (Vehicle base class, Car/Motorcycle/Truck derived)");
			Console.WriteLine("✓ Polymorphism (method overriding, interface implementation)");
			Console.WriteLine("✓ Abstraction (abstract class, interfaces)");
			Console.WriteLine("✓ Composition (GPS in LuxuryCar)");
			Console.WriteLine("✓ Static members (TotalVehicles counter)");
			Console.WriteLine("✓ Method overloading (DisplayInfo methods)");
			Console.WriteLine("✓ Interface implementation (IMovable, IFuelable, IMaintainable)");
		}
	}
}
