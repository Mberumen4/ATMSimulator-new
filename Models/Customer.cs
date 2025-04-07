using System;
using System.Collections.Generic;
using System.Linq;

namespace ATMProject.Models
{
    /// <summary>
    /// Represents a customer with one or more bank accounts.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Gets or sets the unique customer ID.
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// Gets or sets the first name of the customer.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name of the customer.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the PIN for the customer.
        /// </summary>
        public int PIN { get; set; }

        /// <summary>
        /// Gets or sets the list of accounts owned by the customer.
        /// </summary>
        public List<Account> Accounts { get; set; } = new List<Account>();

        public Customer()
        {
            Accounts = new List<Account>();
        }

        /// <summary>
        /// Retrieves an account by its ID.
        /// </summary>
        /// <param name="accountID">The account ID.</param>
        /// <returns>The account with the specified ID, or throws an exception if not found.</returns>
        public Account GetAccountByID(int accountID)
        {
            return Accounts.FirstOrDefault(acc => acc.AccountID == accountID)
                ?? throw new InvalidOperationException("Account not found.");
        }
    }
}
