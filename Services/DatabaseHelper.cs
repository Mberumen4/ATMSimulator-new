using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ATMProject.Models;

namespace ATMProject.Services
{
    /// <summary>
    /// Provides methods for interacting with the database, including customer authentication,
    /// loading customer accounts, and handling account transactions.
    /// </summary>
    public class DatabaseHelper
    {
        private string connectionString = "Server=DESKTOP-O3E79VF;Database=ATMDB;Integrated Security=True";

        /// <summary>
        /// Authenticates a customer based on the provided customer ID and PIN.
        /// It queries the database to find a matching customer and retrieves their information.
        /// If a match is found, the customer's details and associated accounts are returned; otherwise, null is returned.
        /// </summary>
        /// <param name="customerID">The unique identifier for the customer.</param>
        /// <param name="pin">The PIN for the customer used for authentication.</param>
        /// <returns>A <see cref="Customer"/> object representing the authenticated customer, or null if no match is found.</returns>
        public Customer AuthenticateCustomer(int customerID, int pin)
        {
            Customer customer = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();  // Opens the connection to the SQL server
                string query = "SELECT * FROM Customers WHERE CustomerID = @CustomerID AND PIN = @PIN";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CustomerID", customerID);
                cmd.Parameters.AddWithValue("@PIN", pin);

                SqlDataReader reader = cmd.ExecuteReader();  // Executes the query and retrieves the data
                if (reader.HasRows)
                {
                    reader.Read();  // Reads the first matching row from the result
                    customer = new Customer
                    {
                        CustomerID = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        PIN = reader.GetInt32(3)
                    };

                    // Loads associated customer accounts after authentication
                    customer.Accounts = LoadCustomerAccounts(customer.CustomerID);
                }
            }

            return customer;
        }

        /// <summary>
        /// Loads and returns all accounts associated with the specified customer ID from the database.
        /// This method queries the database for accounts belonging to the customer and loads the accounts 
        /// into appropriate account types (Checking or Savings).
        /// </summary>
        /// <param name="customerID">The unique identifier of the customer whose accounts are to be loaded.</param>
        /// <returns>A list of <see cref="Account"/> objects associated with the customer, which may include 
        /// checking and savings accounts.</returns>
        private List<Account> LoadCustomerAccounts(int customerID)
        {
            List<Account> accounts = new List<Account>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();  // Opens the connection to the SQL server
                string query = "SELECT a.AccountID, a.CustomerID, a.AccountTypeID, a.Balance, at.AccountTypeName " +
                               "FROM Accounts a " +
                               "JOIN AccountTypes at ON a.AccountTypeID = at.AccountTypeID " +
                               "WHERE a.CustomerID = @CustomerID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CustomerID", customerID);

                SqlDataReader reader = cmd.ExecuteReader();  // Executes the query and retrieves the data
                while (reader.Read())
                {
                    Account account = null;
                    if (reader.GetString(4) == "Checking")
                    {
                        account = new CheckingAccount
                        {
                            AccountID = reader.GetInt32(0),
                            CustomerID = reader.GetInt32(1),
                            Balance = reader.GetDecimal(3)
                        };
                    }
                    else if (reader.GetString(4) == "Savings")
                    {
                        account = new SavingsAccount
                        {
                            AccountID = reader.GetInt32(0),
                            CustomerID = reader.GetInt32(1),
                            Balance = reader.GetDecimal(3)
                        };
                    }

                    // Add the loaded account to the list if it was successfully created
                    if (account != null)
                    {
                        accounts.Add(account);
                    }
                }
            }

            return accounts;  // Return the list of accounts, even if it is empty
        }
    }
}
