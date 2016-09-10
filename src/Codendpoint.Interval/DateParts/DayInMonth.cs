using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codendpoint.Interval.DateParts
{
    public class DayInMonth : DatePart
    {
        private static string _id = "d";

        public DayInMonth() : base(_id)
        {
        }

        public override string[] PossibleIdentifiers
            => new string[] { _id };

        public override int GetDomain(DateTime date)
            => DateTime.DaysInMonth(date.Year, date.Month);

        public override int? GetValue(DateTime date)
            => date.Day;
    }
}
