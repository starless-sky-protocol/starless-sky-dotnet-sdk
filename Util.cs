using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarlessSky.Core
{
    internal static class Util
    {
        public static string Truncate(this string value, int length)
            => (value != null && value.Length > length) ? value.Substring(0, length) : value;

        public static string ShortKey(this string public_key, int length)
        {
            return Truncate(public_key, length - 7) + "..." + public_key.Substring(Math.Max(0, public_key.Length - 4));
        }
    }
}
