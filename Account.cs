public abstract class Account
{
    public int AccountID { get; set; }
    public int CustomerID { get; set; }
    public decimal Balance { get; set; }

  
    public Customer Customer { get; set; }

   
    public abstract void Withdraw(decimal amount);

   
    public void Deposit(decimal amount)
    {
        Balance += amount;
    }

   
    public void Transfer(decimal amount, Account targetAccount)
    {
        if (Balance >= amount)
        {
            Withdraw(amount);
            targetAccount.Deposit(amount);
        }
        else
        {
            Console.WriteLine("Insufficient funds for transfer.");
        }
    }

    
    public decimal GetBalance()
    {
        return Balance;
    }
}
