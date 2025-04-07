using System;
using System.Linq;
using ATMProject.Models;

namespace ATMProject.Services
{
    /// <summary>
    /// Handles the operations for managing customer accounts.
    /// This class provides methods for displaying account menus and processing withdrawals, transfers, etc.
    /// </summary>
    public class AccountService
    {
        /// <summary>
        /// Displays the main menu and handles user input for various account operations.
        /// </summary>
        /// <param name="customer">The customer whose accounts are being managed.</param>
        public void DisplayMenu(Customer customer)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. View Accounts");
                Console.WriteLine("2. Withdraw from Checking Account");
                Console.WriteLine("3. Withdraw from Savings Account");
                Console.WriteLine("4. Transfer between Accounts");
                Console.WriteLine("5. Exit");

                Console.Write("Enter choice: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayAccounts(customer);
                        break;
                    case "2":
                        ProcessWithdrawal(customer, "Checking");
                        break;
                    case "3":
                        ProcessWithdrawal(customer, "Savings");
                        break;
                    case "4":
                        ProcessTransfer(customer);
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }

        /// <summary>
        /// Displays all accounts for the given customer.
        /// </summary>
        /// <param name="customer">The customer whose accounts will be displayed.</param>
        private void DisplayAccounts(Customer customer)
        {
            Console.WriteLine("\nYour Accounts:");
            foreach (var account in customer.Accounts)
            {
                Console.WriteLine($"{account.GetType().Name} Account ID: {account.AccountID}, Balance: {account.GetBalance():C}");
            }
        }

        /// <summary>
        /// Processes the withdrawal from the specified account type (Checking/Savings).
        /// </summary>
        /// <param name="customer">The customer performing the withdrawal.</param>
        /// <param name="accountType">The type of account from which to withdraw (Checking or Savings).</param>
        private void ProcessWithdrawal(Customer customer, string accountType)
        {
            Console.WriteLine($"\nEnter the amount to withdraw from {accountType} account:");
            decimal amount;
            if (decimal.TryParse(Console.ReadLine(), out amount) && amount > 0)
            {
                Account? account = customer.Accounts.FirstOrDefault(a => a.GetType().Name == accountType);
                if (account != null)
                {
                    account.Withdraw(amount);
                }
                else
                {
                    Console.WriteLine($"{accountType} account not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid amount. Please try again.");
            }
        }

        /// <summary>
        /// Processes a transfer between two accounts.
        /// </summary>
        /// <param name="customer">The customer performing the transfer.</param>
        private void ProcessTransfer(Customer customer)
        {
            Console.WriteLine("\nEnter the amount to transfer:");
            decimal amount;
            if (decimal.TryParse(Console.ReadLine(), out amount) && amount > 0)
            {
                Console.WriteLine("Enter the source account (Checking/Savings):");
                string? sourceAccountType = Console.ReadLine();

                Console.WriteLine("Enter the target account (Checking/Savings):");
                string? targetAccountType = Console.ReadLine();

                Account? sourceAccount = customer.Accounts.FirstOrDefault(a => a.GetType().Name == sourceAccountType);
                Account? targetAccount = customer.Accounts.FirstOrDefault(a => a.GetType().Name == targetAccountType);

                if (sourceAccount != null && targetAccount != null)
                {
                    sourceAccount.Transfer(amount, targetAccount);
                }
                else
                {
                    Console.WriteLine("Invalid account type(s). Please try again.");
                }
            }
            else
            {
                Console.WriteLine("Invalid amount. Please try again.");
            }
        }
    }
}
