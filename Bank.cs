using AccountsGroupProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;


namespace AccountsGroupProject
{
    public static class Bank
    {
        public static readonly Dictionary<string, Account> ACCOUNTS = new Dictionary<string, Account>();
        public static readonly Dictionary<string, Person> USERS = new Dictionary<string, Person>();

        static Bank()
        {
            // Initialize users
            AddUser("Narendra", "1234-5678");
            AddUser("Ilia", "2345-6789");
            AddUser("Mehrdad", "3456-7890");
            AddUser("Vinay", "4567-8901");
            AddUser("Arben", "5678-9012");
            AddUser("Patrick", "6789-0123");
            AddUser("Yin", "7890-1234");
            AddUser("Hao", "8901-2345");
            AddUser("Jake", "9012-3456");
            AddUser("Mayy", "1224-5678");
            AddUser("Nicoletta", "2344-6789");

            // Initialize accounts
            AddAccount(new VisaAccount());
            AddAccount(new VisaAccount(150, -500));
            AddAccount(new SavingAccount(5000));
            AddAccount(new SavingAccount());
            AddAccount(new CheckingAccount(2000));
            AddAccount(new CheckingAccount(1500, true));
            AddAccount(new VisaAccount(50, -550));
            AddAccount(new SavingAccount(1000));

            // Associate users with accounts
            string number = "VS-100000";
            AddUserToAccount(number, "Narendra");
            AddUserToAccount(number, "Ilia");
            AddUserToAccount(number, "Mehrdad");

            number = "VS-100001";
            AddUserToAccount(number, "Vinay");
            AddUserToAccount(number, "Arben");
            AddUserToAccount(number, "Patrick");

            number = "SV-100002";
            AddUserToAccount(number, "Yin");
            AddUserToAccount(number, "Hao");
            AddUserToAccount(number, "Jake");

            number = "SV-100003";
            AddUserToAccount(number, "Mayy");
            AddUserToAccount(number, "Nicoletta");

            number = "CK-100004";
            AddUserToAccount(number, "Mehrdad");
            AddUserToAccount(number, "Arben");
            AddUserToAccount(number, "Yin");

            number = "CK-100005";
            AddUserToAccount(number, "Jake");
            AddUserToAccount(number, "Nicoletta");

            number = "VS-100006";
            AddUserToAccount(number, "Ilia");
            AddUserToAccount(number, "Vinay");

            number = "SV-100007";
            AddUserToAccount(number, "Patrick");
            AddUserToAccount(number, "Hao");
        }

        public static void PrintAccounts()
        {
            foreach (var account in ACCOUNTS.Values)
            {
                Console.WriteLine(account);
            }
        }

        public static void PrintUsers()
        {
            foreach (var user in USERS.Values)
            {
                Console.WriteLine(user);
            }
        }

        public static void SaveAccounts(string filename)
        {
            string json = JsonSerializer.Serialize(ACCOUNTS.Values);
            File.WriteAllText(filename, json);
        }

        public static void SaveUsers(string filename)
        {
            string json = JsonSerializer.Serialize(USERS.Values);
            File.WriteAllText(filename, json);
        }

        public static Person GetUser(string name)
        {
            if (USERS.ContainsKey(name))
            {
                return USERS[name];
            }
            throw new AccountException(AccountExceptionType.USER_DOES_NOT_EXIST);
        }

        public static Account GetAccount(string number)
        {
            if (ACCOUNTS.ContainsKey(number))
            {
                return ACCOUNTS[number];
            }
            throw new AccountException(AccountExceptionType.ACCOUNT_DOES_NOT_EXIST);
        }

        public static void AddUser(string name, string sin)
        {
            if (USERS.ContainsKey(name))
            {
                throw new AccountException(AccountExceptionType.USER_ALREADY_EXIST);
            }

            Person person = new Person(name, sin);
            person.OnLogin += Logger.LoginHandler;
            USERS.Add(name, person);
        }

        public static void AddAccount(Account account)
        {
            account.OnTransaction += Logger.TransactionHandler;
            ACCOUNTS.Add(account.Number, account);
        }

        public static void AddUserToAccount(string number, string name)
        {
            Account account = GetAccount(number);
            Person user = GetUser(name);
            account.AddUser(user);
        }
    }

}
