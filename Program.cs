using System;
using static System.Console;

namespace MoneyBoxBalance
{
	internal delegate void balanceHandlerDelegate(double balance);
	internal class MoneyBox
	{
		private const double SavingsGoal = 250;

		private double balance;

		internal event balanceHandlerDelegate balanceChnaged;

		internal MoneyBox()
		{
			this.balance = 0;
		}

		internal double Balance
		{
			get => this.balance;

			set
			{
				this.balance += value;
				this.balanceChnaged(this.balance);
			}
		}


		internal double GoalAmount
		{
			get => SavingsGoal;
		}
	}


	class Program
	{
		static void Main(string[] args)
		{
			string userInput;
			MoneyBox myMoneyBox = new MoneyBox();

			// Connect event handler with the event 
			myMoneyBox.balanceChnaged += delegate (double amount)
			{
				WriteLine($"The balance amount is {amount}");
			};

			myMoneyBox.balanceChnaged += (amount) => 
			{
				if (amount >= myMoneyBox.GoalAmount)
				{
					WriteLine($"You have reached your savings goal {myMoneyBox.GoalAmount}. You have {myMoneyBox.Balance}");
				}
			};

			WriteLine("Hello! This is your moneybox.");

			do
			{
				WriteLine("\nHow much to deposit?");
				userInput = ReadLine().ToUpper();

				if (string.IsNullOrEmpty(userInput))
				{
					throw new ArgumentException("Enter a valid number. Entry cannot be empty, null or different than a number.", nameof(userInput));
				}

				if (userInput.Equals("EXIT"))
				{
					return;
				}

				myMoneyBox.Balance = double.Parse(userInput, System.Globalization.CultureInfo.InvariantCulture);
			} 
			while (true);
		}
	}
}
