public class CheckingAccount : Account
{
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

   
    private SavingsAccount GetSavingsAccount()
    {
        
        return (SavingsAccount)Customer.Accounts.FirstOrDefault(acc => acc is SavingsAccount);
    }
}
