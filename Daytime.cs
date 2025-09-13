using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsGroupProject
{
        public struct DayTime
        {
            public long minutes;

            // C# 7.3 doesn't support parameterless constructors for structs
            // so we'll use the default value instead

            public DayTime(long minutes)
            {
                this.minutes = minutes;
            }

            public static DayTime operator +(DayTime lhs, int minutes)
            {
                return new DayTime(lhs.minutes + minutes);
            }

            public override string ToString()
            {
                int year = 2023;
                int month = 1;
                int day = 1;
                int hour = 0;
                long remainingMinutes = minutes;

                // Calculate years, months, days, hours and minutes
                if (remainingMinutes > 0)
                {
                    // Handle positive minutes
                    int minutesInHour = 60;
                    int hoursInDay = 24;
                    int daysInMonth = 30; // Simplified model assuming 30 days per month
                    int monthsInYear = 12;

                    // Extract hours
                    hour = (int)(remainingMinutes / minutesInHour);
                    remainingMinutes = remainingMinutes % minutesInHour;

                    // Extract days
                    day += hour / hoursInDay;
                    hour = hour % hoursInDay;

                    // Extract months
                    month += (day - 1) / daysInMonth;
                    day = ((day - 1) % daysInMonth) + 1;

                    // Extract years
                    year += (month - 1) / monthsInYear;
                    month = ((month - 1) % monthsInYear) + 1;
                }

                return $"- {year} - {month:D2} - {day:D2} {hour:D2}:{remainingMinutes:D2}";
            }
        }
    }

