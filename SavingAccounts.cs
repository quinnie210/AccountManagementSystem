using AccountsGroupProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsGroupProject
{
    public class SavingAccount : Account
    {
        private static decimal COST_PER_TRANSACTION = 0.5m;
        private static decimal INTEREST_RATE = 0.015m;

        public SavingAccount(decimal balance = 0) : base("SV", balance)
        {
        }

        public new void Deposit(decimal amount, Person person)
        {
            base.Deposit(amount, person);
            OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, true));
        }

        public void Withdraw(decimal amount, Person person)
        {
            if (!IsUser(person))
            {
                OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, false));
                throw new AccountException(AccountExceptionType.NAME_NOT_ASSOCIATED_WITH_ACCOUNT);
            }

            if (!person.IsAuthenticated)
            {
                OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, false));
                throw new AccountException(AccountExceptionType.USER_NOT_LOGGED_IN);
            }

            if (amount > Balance)
            {
                OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, false));
                throw new AccountException(AccountExceptionType.INSUFFICIENT_FUNDS);
            }

            OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, true));
            Deposit(-amount, person);
        }

        public override void PrepareMonthlyStatement()
        {
            decimal serviceCharge = transactions.Count * COST_PER_TRANSACTION;
            decimal interest = LowestBalance * INTEREST_RATE / 12;
            Balance += interest - serviceCharge;
            transactions.Clear();
        }
    }
}
