using AccountsGroupProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsGroupProject
{
    public class VisaAccount : Account

    {
        private decimal creditLimit;
        private static decimal INTEREST_RATE = 0.1995m;

        public VisaAccount(decimal balance = 0, decimal creditLimit = 1200)
            : base("VS", balance)
        {
            this.creditLimit = creditLimit;
        }

        public void DoPayment(decimal amount, Person person)
        {
            Deposit(amount, person);
            OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, true));
        }

        public void DoPurchase(decimal amount, Person person)
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

            if (amount > Balance + creditLimit)
            {
                OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, false));
                throw new AccountException(AccountExceptionType.CREDIT_LIMIT_HAS_BEEN_EXCEEDED);
            }

            OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, true));
            Deposit(-amount, person);
        }

        public override void PrepareMonthlyStatement()
        {
            decimal interest = LowestBalance * INTEREST_RATE / 12;
            Balance -= interest;
            transactions.Clear();
        }
    }

}
