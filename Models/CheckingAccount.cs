namespace ATMProject.Models
{
    /// <summary>
    /// Represents a checking account. This class implements the abstract method Withdraw for checking accounts.
    /// </summary>
    public class CheckingAccount : Account
    {
        /// <summary>
        /// Withdraws a specified amount from the checking account.
        /// If insufficient funds, will attempt to pull from savings account.
        /// </summary>
        /// <param name="amount">The amount to withdraw.</param>
        public override void Withdraw(decimal amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
            }
            else
            {
                var savingsAccount = GetSavingsAccount();
                if (savingsAccount != null && savingsAccount.Balance >= amount - Balance)
                {
                    decimal shortfall = amount - Balance;
                    savingsAccount.Withdraw(shortfall);
                    Balance = 0;
                    Console.WriteLine($"Insufficient funds in checking. {shortfall} pulled from savings.");
                }
                else
                {
                    Console.WriteLine("Insufficient funds in both checking and savings accounts.");
                }
            }
        }

        /// <summary>
        /// Retrieves the customer's savings account.
        /// </summary>
        /// <returns>The customer's savings account, or null if not found.</returns>
        private SavingsAccount GetSavingsAccount()
        {
            return (SavingsAccount)Customer.Accounts.FirstOrDefault(acc => acc is SavingsAccount);
        }
    }
}
