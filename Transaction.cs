using AccountsGroupProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsGroupProject
{
    public struct Transaction
    {
        public string AccountNumber { get; }
        public decimal Amount { get; }
        public Person Originator { get; }
        public DayTime Time { get; }
        public Transaction(string accountNumber, decimal amount, Person person)
        {
            AccountNumber = accountNumber;
            Amount = amount;
            Originator = person;
            Time = Util.Now;
        }
        public override string ToString()
        {
            return $"Time: {Time}, Account number: {AccountNumber}, Amount: {Amount}, Person {Originator}";
        }

    }
}
