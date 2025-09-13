using AccountsGroupProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AccountsGroupProject
{
    internal class CheckingAccount : Account
    {
        private static decimal COST_PER_TRANSACTION = 0.05m;
        private static decimal INTEREST_RATE = 0.005m;
        private bool hasOverdraft;

        public CheckingAccount(decimal balance = 0, bool hasOverdraft = false) : base("CK", balance)
        {
            this.hasOverdraft = hasOverdraft;
        }

        public new void Deposit(decimal amount, Person person)
        {
            base.Deposit(amount, person);
            OnTransactionOccur(this, new TransactionEventArgs(Number, amount, true));
        }

        public void Withdraw(decimal amount, Person person)
        {
            if (!IsUser(person))
            {
                OnTransactionOccur(this, new TransactionEventArgs(Number, amount, false));
                throw new AccountException(AccountExceptionType.NAME_NOT_ASSOCIATED_WITH_ACCOUNT);
            }

            if (!person.IsAuthenticated)
            {
                OnTransactionOccur(this, new TransactionEventArgs(Number, amount, false));
                throw new AccountException(AccountExceptionType.USER_NOT_LOGGED_IN); //there is no IsLoggedIn in base class
            }

            if (amount > Balance && !hasOverdraft)
            {
                OnTransactionOccur(this, new TransactionEventArgs(Number, amount, false));
                throw new AccountException(AccountExceptionType.INSUFFICIENT_FUNDS);
            }

            base.Deposit(-amount, person);
            OnTransactionOccur(this, new TransactionEventArgs(Number, amount, true));
        }

        public override void PrepareMonthlyStatement()
        {
            decimal serviceCharge = transactions.Count * COST_PER_TRANSACTION;
            decimal interest = (LowestBalance * INTEREST_RATE) / 12;
            Balance += interest - serviceCharge;
            transactions.Clear();
        }
    }
    
}
