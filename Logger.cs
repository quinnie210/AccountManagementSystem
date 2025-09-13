using AccountsGroupProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsGroupProject
{
    static class Logger
    {
        private static List<string> loginEvents = new List<string>();
        private static List<string> transactionEvents = new List<string>();

        public static void LoginHandler(object sender, LoginEventArgs args)
        {
            string logEntry = $"{args.PersonName} {args.EventType} {(args.Success ? "successfully" : "unsuccessfully")} on {args.Time}";
            loginEvents.Add(logEntry);
        }
        public static void TransactionHandler(object sender, TransactionEventArgs args)//changing tostring method to fit output
        {
            string logEntry = $"{args.Amount:C} deposited by {args.PersonName} on {args.Time}"; 
            //there is no args.operation
            transactionEvents.Add(logEntry);

        }
        public static void DisplayLoginEvents()
        {
            for (int i = 0; i < loginEvents.Count; i++)
            {
                var loginEvent = loginEvents[i];
                Console.WriteLine($"{i + 1}. {loginEvent}");
            }
        }
        public static void DisplayTransactionEvents()
        {
            for (int i = 0; i < loginEvents.Count; i++)
            {
                var transactionEvent = transactionEvents[i];
                Console.WriteLine($"{i + 1}. {transactionEvent}");
            }

        }
    }
}
