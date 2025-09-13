using AccountsGroupProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsGroupProject
{
    public class TransactionEventArgs : LoginEventArgs
    {
        public decimal Amount { get; }

        public TransactionEventArgs(string personName, decimal amount, bool success)
            : base(personName, success, LoginEventType.None)
        {
            Amount = amount;
        }
    }
}
