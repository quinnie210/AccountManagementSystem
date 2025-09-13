using AccountsGroupProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsGroupProject
{
    public class Person
    {
        public string Name { get; }
        public string Sin { get; }
        public bool IsAuthenticated { get; private set; }

        private string Password;

        public event EventHandler<LoginEventArgs> OnLogin;

        public Person(string name, string sin)
        {
            Name = name;
            Sin = sin;
            Password = sin.Substring(0, 3);
        }
        public void Login(string password)
        {
            if (password != Password)
            {
                IsAuthenticated = false;

                //Trigger the OnLogin event to notify about failed login
                OnLogin?.Invoke(this, new LoginEventArgs(Name, false, LoginEventType.Login)); //only three arguments

                //Exception indicating incorrect password
                throw new AccountException(AccountExceptionType.PASSWORD_INCORRECT);
            }
            IsAuthenticated = true;
            OnLogin?.Invoke(this, new LoginEventArgs(Name, true, LoginEventType.Login));

        }
        public void Logout()
        {
            IsAuthenticated = false;
            OnLogin?.Invoke(this, new LoginEventArgs(Name, true, LoginEventType.Logout));

        }
        public override string ToString()
        {
            return Name;
        }
    }
}
