using Codendpoint.Interval.DateParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codendpoint.Interval
{
    public class IntervalManager
    {
        private IEnumerable<DatePart> _parts;

        public IntervalManager(IEnumerable<DatePart> parts)
        {
            _parts = parts;
        }

        public IEnumerable<DatePart> Parts
            => _parts;

        public bool Eval(DateTime date, string statement)
        {
            if (_parts == null)
                return false;

            int index = 0, exact, frequency;
            DatePart part = null;
            var statements = statement.Split('|');
            foreach (var item in statements)
            {
                if (Parse(statement, ref index, out exact, out frequency, out part)
                    && part.Evaluate(date, exact, frequency))
                    return true;
            }
            return false;
        }

        private bool Parse(string statement, ref int index, out int exact, out int frequency, out DatePart part)
        {
            exact = 0;
            frequency = 0;
            part = null;

            if (index >= statement.Length)
                return false;

            if (!TryStaticPart(statement, ref index, out exact))
                exact = 0;

            if (!TryFrequencyPart(statement, ref index, out frequency, out part))
                throw new ArgumentException("Bad statement");

            return true;
        }

        private bool TryFrequencyPart(string statement, ref int index, out int frequency, out DatePart part)
        {
            var _index = index;
            frequency = 0;
            if (!TryPart(statement, ref _index, out part))
                return false;

            if (!TryNumber(statement, ref _index, out frequency))
                return false;

            index = _index;
            return true;
        }

        private bool TryPart(string statement, ref int index, out DatePart part)
        {
            var _index = index;
            string word;
            part = null;
            if (!TryWord(statement, ref _index, out word))
                return false;

            part = _parts.FirstOrDefault(p => p.Identifier == word);
            index = _index;
            return part != null;
        }

        private bool TryStaticPart(string statement, ref int index, out int exact)
        {
            int _index = index;
            exact = 0;
            string word;
            if (!TryWord(statement, ref _index, out word))
                return false;

            if (word != "s")
                return false;

            if (!TryNumber(statement, ref _index, out exact))
                return false;

            index = _index;
            return true;
        }

        private bool TryNumber(string statement, ref int index, out int number)
        {
            var length = statement.Length;
            var _index = index;
            number = 0;
            if (_index >= length)
                return false;

            bool minus = statement[_index] == '-';
            if (minus) _index++;

            var __index = _index;
            while (__index < length && Char.IsDigit(statement[__index]))
                __index++;

            if (__index <= _index)
                return false;

            number = int.Parse(statement.Substring(_index, __index - _index));
            if (minus) number = -number;
            index = __index;
            return true;
        }

        private bool TryWord(string statement, ref int index, out string word)
        {
            var _index = index;
            var length = statement.Length;
            while (_index < length && Char.IsLetter(statement[_index]))
                _index++;

            word = statement.Substring(index, _index - index);
            index = _index;
            return word.Length > 0;
        }
    }
}
