namespace ATMProject.Models
{
    /// <summary>
    /// Represents a savings account. This class implements the abstract method Withdraw for savings accounts.
    /// </summary>
    public class SavingsAccount : Account
    {
        /// <summary>
        /// Withdraws a specified amount from the savings account.
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
                Console.WriteLine("Insufficient funds in savings account.");
            }
        }
    }
}
