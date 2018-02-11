using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Renter
{
    public static class StringExtensions
    {
        public static string Truncate(this string str, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length", "Length must be >= 0");
            }

            if (str == null)
            {
                return null;
            }

            int maxLength = Math.Min(str.Length, length);
            return str.Substring(0, maxLength);
        }
    }
}
