using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codendpoint.Interval.DatePart
{
    public abstract class DatePart
    {
        public abstract string Identifier { get; }

        public abstract int GetValue(DateTime date);

        public abstract int GetDomain(DateTime date);

        public bool Evaluate(DateTime date, int exact, int frequency, bool reverse)
        {
            var value = GetValue(date);
            if (reverse)
                value = GetDomain(date) - value + 1;

            if (frequency == 0)
                return value == exact;

            return (value - exact) / frequency > 0 && (value - exact) % frequency == 0;
        }
    }
}
