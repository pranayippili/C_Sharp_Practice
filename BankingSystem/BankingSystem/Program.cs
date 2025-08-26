using System;

namespace BankingSystem
{
	// 🔹 Interface (contract for transactions)
	interface ITransaction
	{
		void Deposit(decimal amount);
		void Withdraw(decimal amount);
	}

	// 🔹 Abstract Class (base account with common logic)
	abstract class Account : ITransaction
	{
		public string AccountHolder { get; set; }
		public decimal Balance { get; protected set; }

		public Account(string accountHolder, decimal balance)
		{
			AccountHolder = accountHolder;
			Balance = balance;
		}

		// Abstract method - must be implemented by derived classes
		public abstract void CalculateInterest();

		// Implementing interface methods
		public virtual void Deposit(decimal amount)
		{
			Balance += amount;
			Console.WriteLine($"{amount:C} deposited. New balance: {Balance:C}");
		}

		public virtual void Withdraw(decimal amount)
		{
			if (Balance >= amount)
			{
				Balance -= amount;
				Console.WriteLine($"{amount:C} withdrawn. New balance: {Balance:C}");
			}
			else
			{
				Console.WriteLine("Insufficient funds!");
			}
		}
	}

	// 🔹 Concrete Class: Savings Account
	class SavingsAccount : Account
	{
		private const decimal InterestRate = 0.05m; // 5%

		public SavingsAccount(string accountHolder, decimal balance)
			: base(accountHolder, balance) { }

		public override void CalculateInterest()
		{
			decimal interest = Balance * InterestRate;
			Balance += interest;
			Console.WriteLine($"Interest added: {interest:C}. New balance: {Balance:C}");
		}
	}

	// 🔹 Concrete Class: Current Account
	class CurrentAccount : Account
	{
		public CurrentAccount(string accountHolder, decimal balance)
			: base(accountHolder, balance) { }

		public override void CalculateInterest()
		{
			// Current accounts don't earn interest
			Console.WriteLine("No interest for Current Account.");
		}
	}

	// 🔹 Program Entry
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("=== Banking System ===\n");

			Account savings = new SavingsAccount("Alice", 1000);
			Account current = new CurrentAccount("Bob", 500);

			Console.WriteLine($"Account Holder: {savings.AccountHolder}, Balance: {savings.Balance:C}");
			savings.Deposit(200);
			savings.Withdraw(100);
			savings.CalculateInterest();

			Console.WriteLine("\n----------------------\n");

			Console.WriteLine($"Account Holder: {current.AccountHolder}, Balance: {current.Balance:C}");
			current.Deposit(300);
			current.Withdraw(1000);
			current.CalculateInterest();

			Console.WriteLine("\nPress any key to exit...");
			Console.ReadKey();
		}
	}
}
