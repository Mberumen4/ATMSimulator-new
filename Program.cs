#nullable enable
using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Linq;

using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        DatabaseHelper dbHelper = new DatabaseHelper();

      
        Console.WriteLine("Welcome to the ATM!");

        Console.Write("Enter Customer ID: ");
        int customerID = int.Parse(Console.ReadLine());

        Console.Write("Enter PIN: ");
        int pin = int.Parse(Console.ReadLine());

    
        Customer customer = dbHelper.AuthenticateCustomer(customerID, pin);

        if (customer != null)
        {
            Console.WriteLine($"Welcome {customer.FirstName} {customer.LastName}!");

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
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayAccounts(customer);
                        break;
                    case "2":
                        WithdrawFromAccount(customer, "Checking");
                        break;
                    case "3":
                        WithdrawFromAccount(customer, "Savings");
                        break;
                    case "4":
                        TransferBetweenAccounts(customer);
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
        else
        {
            Console.WriteLine("Invalid Customer ID or PIN. Please try again.");
        }
    }

   
    static void DisplayAccounts(Customer customer)
    {
        Console.WriteLine("\nYour Accounts:");
        foreach (var account in customer.Accounts)
        {
            Console.WriteLine($"{account.GetType().Name} Account ID: {account.AccountID}, Balance: {account.GetBalance()}");
        }
    }

  
    static void WithdrawFromAccount(Customer customer, string accountType)
    {
        Console.WriteLine($"\nEnter the amount to withdraw from {accountType} account:");
        decimal amount = decimal.Parse(Console.ReadLine());

        
        Account account = customer.Accounts.Find(a => a.GetType().Name == accountType);
        if (account != null)
        {
            if (amount <= account.GetBalance())
            {
                account.Withdraw(amount);
                Console.WriteLine($"You have successfully withdrawn {amount} from your {accountType} account.");
                Console.WriteLine($"New Balance: {account.GetBalance()}");
            }
            else
            {
                Console.WriteLine("Insufficient funds.");
            }
        }
        else
        {
            Console.WriteLine($"{accountType} account not found.");
        }
    }

 
    static void TransferBetweenAccounts(Customer customer)
    {
        Console.WriteLine("\nEnter the amount to transfer:");
        decimal amount = decimal.Parse(Console.ReadLine());

        Console.WriteLine("Enter the source account (Checking/Savings):");
        string sourceAccountType = Console.ReadLine();

        Console.WriteLine("Enter the target account (Checking/Savings):");
        string targetAccountType = Console.ReadLine();

        Account sourceAccount = customer.Accounts.Find(a => a.GetType().Name == sourceAccountType);
        Account targetAccount = customer.Accounts.Find(a => a.GetType().Name == targetAccountType);

        if (sourceAccount != null && targetAccount != null)
        {
            if (amount <= sourceAccount.GetBalance())
            {
                sourceAccount.Transfer(amount, targetAccount);
                Console.WriteLine($"Successfully transferred {amount} from {sourceAccountType} to {targetAccountType}.");
                Console.WriteLine($"New {sourceAccountType} Balance: {sourceAccount.GetBalance()}");
                Console.WriteLine($"New {targetAccountType} Balance: {targetAccount.GetBalance()}");
            }
            else
            {
                Console.WriteLine("Insufficient funds for the transfer.");
            }
        }
        else
        {
            Console.WriteLine("Invalid account type(s). Please try again.");
        }
    }
}

