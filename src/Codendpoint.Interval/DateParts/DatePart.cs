using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codendpoint.Interval.DateParts
{
    public abstract class DatePart
    {
        private string _identifier;

        public DatePart(string identifier)
        {
            _identifier = identifier;
        }

        public string Identifier
            => _identifier;

        public abstract string[] PossibleIdentifiers { get; }

        public abstract int? GetValue(DateTime date);

        public abstract int GetDomain(DateTime date);

        public bool Evaluate(DateTime date, int exact, int frequency)
        {
            var value = GetValue(date);
            if (!value.HasValue)
                return false;

            var _exact = exact;
            if (exact < 0)
            {
                value = GetDomain(date) - value + 1;
                _exact = -exact;
            }

            if (frequency == 0)
                return value == _exact;

            return (value - _exact) / frequency >= 0 && (value - _exact) % frequency == 0;
        }
    }
}
