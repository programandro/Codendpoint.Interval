using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codendpoint.Interval.DatePart
{
    public class DayInMonth : DatePart
    {
        public override string Identifier
            => "d";

        public override int GetDomain(DateTime date)
        {
            switch (date.Month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    return 31;
                case 4:
                case 6:
                case 9:
                case 11:
                    return 30;
                case 2:
                    return year % 4 == 0 ? 

            }
        }

        public override int GetValue(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
