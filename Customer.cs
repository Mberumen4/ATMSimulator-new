public class Customer
{
    public int CustomerID { get; set; }
    public string FirstName { get; set; } = string.Empty; 
    public string LastName { get; set; } = string.Empty;  
    public int PIN { get; set; }
    public List<Account> Accounts { get; set; } = new List<Account>();  

    public Customer()
    {
        Accounts = new List<Account>();
    }

    public Account GetAccountByID(int accountID)
    {
        return Accounts.FirstOrDefault(acc => acc.AccountID == accountID)
            ?? throw new InvalidOperationException("Account not found.");
    }
}