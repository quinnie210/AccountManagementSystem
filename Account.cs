using AccountsGroupProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsGroupProject
{
    public abstract class Account
    {
        private static int LAST_NUMBER = 100000;
        protected readonly List<Person> users = new List<Person>();
        public readonly List<Transaction> transactions = new List<Transaction>();
        public event EventHandler<TransactionEventArgs> OnTransaction;

        public string Number { get; }
        public decimal Balance { get; protected set; }
        public decimal LowestBalance { get; protected set; }

        public Account(string type, decimal balance)
        {
            Number = type + '-' + LAST_NUMBER++; // Added '-' : Was missing '-' in account number
            Balance = balance;
            LowestBalance = balance;
        }

        protected void Deposit(decimal amount, Person person)
        {
            Balance += amount;
            if (Balance < LowestBalance)
                LowestBalance = Balance;

            transactions.Add(new Transaction(Number, amount, person));
           
        }

        public void AddUser(Person user)
        {
            users.Add(user);
        }

        public bool IsUser(Person user)
        {
            foreach (var u in users)
            {
                if (u.Name == user.Name)
                {
                    return true;
                }
            }
            return false;
        }

        public abstract void PrepareMonthlyStatement();

        public virtual void OnTransactionOccur(object sender, TransactionEventArgs e)
        {
            OnTransaction?.Invoke(sender, e);
        }

        public override string ToString() //changing tostring methods to fit testing output
        {
            string accountDetails = $"{Number}\n";
            foreach (var user in users)
            {
                accountDetails += $" {user.Name} [{user.Sin}] {(user.IsAuthenticated ? "authenticated" : "not authenticated")}\n";
            }
            accountDetails += $"{Balance:C}\n";
            foreach (var transaction in transactions)
            {
                accountDetails += $"{transaction.AccountNumber} {transaction.Amount:C} {transaction.Originator} {transaction.Time}\n"; //there is no transaction.Type
            }
            return accountDetails;
        }
    }
}

