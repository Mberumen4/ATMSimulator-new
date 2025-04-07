using System;

namespace ATMProject.Models
{
    /// <summary>
    /// Abstract base class representing a bank account.
    /// This class provides common properties and methods shared by all account types.
    /// </summary>
    public abstract class Account
    {
        /// <summary>
        /// Gets or sets the unique identifier for the account.
        /// </summary>
        public int AccountID { get; set; }

        /// <summary>
        /// Gets or sets the customer ID associated with the account.
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// Gets or sets the current balance of the account.
        /// </summary>
        public decimal Balance { get; set; }  // Made Balance public

        /// <summary>
        /// Gets the current balance of the account.
        /// </summary>
        /// <returns>The current balance.</returns>
        public decimal GetBalance()
        {
            return Balance;
        }

        /// <summary>
        /// Gets or sets the customer object that owns this account.
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// Withdraws a specified amount from the account.
        /// This method is abstract and must be implemented in derived classes.
        /// </summary>
        /// <param name="amount">The amount to withdraw.</param>
        public abstract void Withdraw(decimal amount);

        /// <summary>
        /// Deposits a specified amount into the account.
        /// </summary>
        /// <param name="amount">The amount to deposit.</param>
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Deposit amount must be greater than zero.");
                return;
            }

            Balance += amount;  // Directly modify the balance
        }

        /// <summary>
        /// Transfers a specified amount to another account, if sufficient balance exists.
        /// </summary>
        /// <param name="amount">The amount to transfer.</param>
        /// <param name="targetAccount">The target account to receive the funds.</param>
        public void Transfer(decimal amount, Account targetAccount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Transfer amount must be greater than zero.");
                return;
            }

            if (Balance >= amount)
            {
                Withdraw(amount);
                targetAccount.Deposit(amount);
                Console.WriteLine($"Successfully transferred {amount:C} to account {targetAccount.AccountID}.");
            }
            else
            {
                Console.WriteLine("Insufficient funds for transfer.");
            }
        }
    }
}
