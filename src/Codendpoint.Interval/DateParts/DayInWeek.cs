using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codendpoint.Interval.DateParts
{
    public class DayInWeek : DatePart
    {
        private static string _id = "wd";

        public DayInWeek() : base(_id)
        {
        }

        public override string[] PossibleIdentifiers
            => new string[] { _id };

        public override int GetDomain(DateTime date)
            => System.Globalization.DateTimeFormatInfo.CurrentInfo.DayNames.Length;

        public override int? GetValue(DateTime date)
            => (int)date.DayOfWeek;
    }
}
