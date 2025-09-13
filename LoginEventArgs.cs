using AccountsGroupProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AccountsGroupProject
{
    public class LoginEventArgs
    {
        public string PersonName { get; }
        public bool Success { get; }
        public DayTime Time { get; }
        public LoginEventType EventType { get; }

        public LoginEventArgs(string username, bool success, LoginEventType loginEventType) : base()
        {
            PersonName = username;
            Success = success;
            EventType = loginEventType;
            Time = Util.Now;

        }

    }
}
