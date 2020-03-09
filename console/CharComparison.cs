using System;
using System.Collections.Generic;

namespace console
{
    public class CharComparison : IEqualityComparer<char>
    {
        private readonly Func<char, char> _comparisonConverter;

        private CharComparison(Func<char, char> comparisonConverter) => _comparisonConverter = comparisonConverter;
        
        public bool Equals(char x, char y)
        {
            return _comparisonConverter(x).Equals(_comparisonConverter(y));
        }

        public int GetHashCode(char obj)
        {
            return _comparisonConverter(obj).GetHashCode();
        }
        
        public static IEqualityComparer<char> InvariantCultureIgnoreCase => new CharComparison( char.ToLowerInvariant );
    }
}