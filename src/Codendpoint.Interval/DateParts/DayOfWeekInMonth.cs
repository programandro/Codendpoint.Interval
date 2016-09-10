using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codendpoint.Interval.DateParts
{
    public class DayOfWeekInMonth : DatePart
    {
        private int _dayOfWeek;

        public DayOfWeekInMonth(string identifier) : base(identifier)
        {
            var name = Enum.GetNames(typeof(DayOfWeek)).First(d => d.StartsWith(identifier, StringComparison.OrdinalIgnoreCase));
            _dayOfWeek = (int)(DayOfWeek)Enum.Parse(typeof(DayOfWeek), name);
        }

        public override string[] PossibleIdentifiers
            => Enum.GetNames(typeof(DayOfWeek)).Select(n => n.Substring(0, 2).ToUpper()).ToArray();

        public override int GetDomain(DateTime date)
        {
            var days = DateTime.DaysInMonth(date.Year, date.Month);
            var result = days / 7;
            var remainder = days % 7;
            var lastDay = new DateTime(date.Year, date.Month, days).DayOfWeek;
            if ((int)lastDay - _dayOfWeek < remainder)
                result++;
            return result;
        }

        public override int? GetValue(DateTime date)
        {
            var dayOfWeek = date.DayOfWeek;
            if (_dayOfWeek != (int)dayOfWeek)
                return null;

            var result = date.Day / 7;
            if (date.Day % 7 > 0) result++;
            return result;
        }
    }
}
