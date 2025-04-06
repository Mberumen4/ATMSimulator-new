public class SavingsAccount : Account
{
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