using AccountsGroupProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsGroupProject
{
    public static class Util
    {
        static Random random = new Random();
        private static DayTime currentTime = new DayTime(1154055);


        public static DayTime Now
        {
            get { return currentTime = currentTime + random.Next(100); }
        }
    }
}
