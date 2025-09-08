using System;
using System.Collections.Generic;

namespace EncapsulationDemo
{
	public class BankAccount
	{
		// Private field (data is hidden)
		private decimal balance;

		// Public property (read-only to outside)
		public decimal Balance
		{
			get { return balance; }
			private set { balance = value; }
		}

		// Public methods (control access to data)
		public void Deposit(decimal amount)
		{
			if (amount > 0)
				Balance += amount;
			else
				Console.WriteLine("Invalid deposit amount");
		}

		public void Withdraw(decimal amount)
		{
			if (amount > 0 && Balance >= amount)
				Balance -= amount;
			else
				Console.WriteLine("Invalid or insufficient funds");
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			BankAccount account = new BankAccount();

			Console.WriteLine("Initial Balance: " + account.Balance);

			account.Deposit(500);
			Console.WriteLine("After Deposit: " + account.Balance);

			account.Withdraw(200);
			Console.WriteLine("After Withdrawal: " + account.Balance);

			account.Withdraw(1000); // Should fail, encapsulation protects balance
			Console.WriteLine("Final Balance: " + account.Balance);

			Console.ReadLine();
		}
	}
}
