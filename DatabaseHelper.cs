using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class DatabaseHelper
{
    private string connectionString = @"Server=.\SQLEXPRESS;Database=ATMDB;Trusted_Connection=True;";




    public Customer AuthenticateCustomer(int customerID, int pin)
    {
        Customer customer = null;

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT * FROM Customers WHERE CustomerID = @CustomerID AND PIN = @PIN";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@CustomerID", customerID);
            cmd.Parameters.AddWithValue("@PIN", pin);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                customer = new Customer
                {
                    CustomerID = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    PIN = reader.GetInt32(3)
                };

                
                customer.Accounts = LoadCustomerAccounts(customer.CustomerID);
            }
        }

        return customer;
    }


    private List<Account> LoadCustomerAccounts(int customerID)
    {
        List<Account> accounts = new List<Account>();

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT a.AccountID, a.CustomerID, a.AccountTypeID, a.Balance, at.AccountTypeName " +
                           "FROM Accounts a " +
                           "JOIN AccountTypes at ON a.AccountTypeID = at.AccountTypeID " +
                           "WHERE a.CustomerID = @CustomerID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@CustomerID", customerID);

            SqlDataReader reader = cmd.ExecuteReader();
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

                if (account != null)
                {
                    accounts.Add(account);
                }
            }
        }

        return accounts;
    }
}