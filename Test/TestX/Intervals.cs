using Codendpoint.Interval;
using Codendpoint.Interval.DateParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TestX
{
    public class Intervals
    {
        private class IntervalCase
        {
            public string Statement { get; set; }
            public DateTime Date { get; set; }
            public bool Expected { get; set; }
        }

        [Fact]
        public void EvaluateCases()
        {
            var manager = new IntervalManager(new DatePart[] { new DayInMonth(), new DayInWeek(), new DayInYear() });

            var allDays = Enumerable.Range(1, 31).ToArray();
            var allMonths = Enumerable.Range(1, 12).ToArray();

            var cases = new List<IntervalCase>();
            cases.AddRange(allDays.Select(d => new IntervalCase { Date = new DateTime(2000, 1, d), Expected = true, Statement = "d1" }));
            cases.AddRange(allDays.Where(d => d % 2 == 0).Select(d => new IntervalCase { Date = new DateTime(2000, 1, d), Expected = true, Statement = "d2" }));
            cases.AddRange(allDays.Where(d => d % 2 != 0).Select(d => new IntervalCase { Date = new DateTime(2000, 1, d), Expected = false, Statement = "d2" }));
            cases.AddRange(allDays.Where(d => d >= 6).Select(d => new IntervalCase { Date = new DateTime(2000, 1, d), Expected = true, Statement = "s6d1" }));
            cases.AddRange(allDays.Where(d => d < 6).Select(d => new IntervalCase { Date = new DateTime(2000, 1, d), Expected = false, Statement = "s6d1" }));
            cases.AddRange(allDays.Where(d => d >= 16 && (d - 16) % 3 == 0).Select(d => new IntervalCase { Date = new DateTime(2000, 1, d), Expected = true, Statement = "s16d3" }));
            cases.AddRange(allDays.Where(d => d < 16 || (d - 16) % 3 != 0).Select(d => new IntervalCase { Date = new DateTime(2000, 1, d), Expected = false, Statement = "s16d3" }));

            cases.AddRange(allDays.Select(d => new IntervalCase { Date = new DateTime(2000, 1, d), Expected = true, Statement = "d-1" }));




            foreach (var _case in cases)
                Assert.True(_case.Expected == manager.Eval(_case.Date, _case.Statement), $"{_case.Statement}({_case.Date.ToShortDateString()})");
        }
    }
}
