using LibraryData.Models;
using System;
using System.Collections.Generic;

namespace LibraryServices
{
    public static class DataHelpers
    {
        public static List<string> MakeBusinessHoursReadible(IEnumerable<BranchHours> branchHours)
        {
            var hours = new List<string>();

            foreach (var time in branchHours)
            {
                var day = MakeDayReadible(time.DayOfWeek);
                var openTime = MakeTimeReadible(time.OpenTime);
                var closeTime = MakeTimeReadible(time.CloseTime);

                var timeEntry = $"{day} {openTime} to {closeTime}";
                hours.Add(timeEntry);
            }

            return hours;
        }

        private static string MakeDayReadible(int num)
        {
            //our data correlates 1 to "Sunday", so subtract 1
            return Enum.GetName(typeof(DayOfWeek), num - 1);
        }

        private static string MakeTimeReadible(int time)
        {
            return TimeSpan.FromHours(time).ToString("hh':'mm");
        }
    }
}
