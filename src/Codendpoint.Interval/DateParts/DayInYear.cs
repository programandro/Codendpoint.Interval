using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codendpoint.Interval.DateParts
{
    public class DayInYear : DatePart
    {
        private static string _id = "yd";

        public DayInYear() : base(_id)
        {
        }

        public override string[] PossibleIdentifiers
            => new string[] { _id };

        public override int GetDomain(DateTime date)
            => DateTime.DaysInMonth(date.Year, 2) == 29 ? 366 : 365;

        public override int? GetValue(DateTime date)
            => date.DayOfYear;
    }
}
