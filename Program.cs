using System.Runtime.CompilerServices;

namespace task_4
{
    internal class Program
    {

        public class Account
        {
            public string Name { get; set; }
            public double Balance { get; set; }

            public Account(string name = "Unnamed Account", double balance = 0.0)
            {
                this.Name = name;
                this.Balance = balance;
            }

            public virtual bool Deposit(double amount)
            {
                if (amount < 0)
                    return false;
                else
                {
                    Balance += amount;
                    return true;
                }
            }

            public virtual bool Withdraw(double amount)
            {
                if (Balance - amount >= 0)
                {
                    Balance -= amount;
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
        class SavingsAccount : Account
        {
            public double InterestRate { get; set; }
            public SavingsAccount(string name = "Unnamed Savings Account", double balance = 0.0, double interestRate = 0.0)
                : base(name, balance)
            {
                this.InterestRate = interestRate;
            }
            public void ApplyInterest()
            {
                Balance += Balance * (InterestRate / 100);
            }
        }
        class CheckingAccount : Account
        {
            private const double Fee = 1.5;
            public CheckingAccount(string name = "Unnamed Checking Account", double balance = 0.0)
                : base(name, balance)
            {
            }
            public override bool Withdraw(double amount)
            {
                double totalAmount = amount + Fee;
                return base.Withdraw(totalAmount);
            }
        }

        class TrustAccount : Account
        {
            public double InterestRate { get; set; }
            private int withdrawalCount = 0;
            private const int MaxWithdrawals = 3;

            public TrustAccount(string name = "Unnamed Trust Account", double balance = 0.0, double interestRate = 0.0)
                : base(name, balance)
            {
                this.InterestRate = interestRate;
            }
            public void ApplyInterest()
            {
                Balance += Balance * (InterestRate / 100);
            }
            public override bool Withdraw(double amount)
            {
                if (withdrawalCount >= MaxWithdrawals || amount > Balance * 0.2)
                {
                    return false;
                }
                else
                {
                    withdrawalCount++;
                    return base.Withdraw(amount);
                }
            }
            public override bool Deposit(double amount)
            {
                if (base.Deposit(amount))
                {
                    if (amount >= 5000)
                        Balance += 50;
                    return true;
                }
                return false;


            }
        }
        public static class AccountUtil
        {
            // Utility helper functions for Account class
            public static void Deposit(List<Account> accounts, double amount)
            {
                Console.WriteLine("\n=== Depositing to Accounts =================================");
                foreach (var acc in accounts)
                {
                    if (acc.Deposit(amount))
                        Console.WriteLine($"Deposited {amount} to {acc.Name} ");
                    else
                        Console.WriteLine($"Failed Deposit of {amount} to {acc.Name} ");
                }
            }

            public static void Withdraw(List<Account> accounts, double amount)
            {
                Console.WriteLine("\n=== Withdrawing from Accounts ==============================");
                foreach (var acc in accounts)
                {
                    if (acc.Withdraw(amount))
                        Console.WriteLine($"Withdrew {amount} from {acc.Name} ");
                    else
                        Console.WriteLine($"Failed Withdrawal of {amount} from {acc.Name} ");
                }
            }






        }
        static void Main(string[] args)
        {

            // Accounts
            var accounts = new List<Account>();
            accounts.Add(new Account());
            accounts.Add(new Account("Larry"));
            accounts.Add(new Account("Moe", 2000));
            accounts.Add(new Account("Curly", 5000));

            AccountUtil.Deposit(accounts, 1000);
            AccountUtil.Withdraw(accounts, 2000);

            // Savings
            var savAccounts = new List<Account>();
            savAccounts.Add(new SavingsAccount());
            savAccounts.Add(new SavingsAccount("Superman"));
            savAccounts.Add(new SavingsAccount("Batman", 2000));
            savAccounts.Add(new SavingsAccount("Wonderwoman", 5000, 5.0));

            AccountUtil.Deposit(savAccounts, 1000);
            AccountUtil.Withdraw(savAccounts, 2000);

            // Checking
            var checAccounts = new List<Account>();
            checAccounts.Add(new CheckingAccount());
            checAccounts.Add(new CheckingAccount("Larry2"));
            checAccounts.Add(new CheckingAccount("Moe2", 2000));
            checAccounts.Add(new CheckingAccount("Curly2", 5000));

            AccountUtil.Deposit(checAccounts, 1000);
            AccountUtil.Withdraw(checAccounts, 2000);
            AccountUtil.Withdraw(checAccounts, 2000);

            // Trust
            var trustAccounts = new List<Account>();
            trustAccounts.Add(new TrustAccount());
            trustAccounts.Add(new TrustAccount("Superman2"));
            trustAccounts.Add(new TrustAccount("Batman2", 2000));
            trustAccounts.Add(new TrustAccount("Wonderwoman2", 5000, 5.0));

            AccountUtil.Deposit(trustAccounts, 1000);
            AccountUtil.Deposit(trustAccounts, 6000);
            AccountUtil.Withdraw(trustAccounts, 2000);
            AccountUtil.Withdraw(trustAccounts, 3000);
            AccountUtil.Withdraw(trustAccounts, 500);

            Console.WriteLine();


        }
    }
}
