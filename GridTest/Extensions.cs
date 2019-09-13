using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace GridTest
{
    public static class Extensions
    {
        private static readonly Regex rgx = new Regex("[^a-zA-Z0-9-]");
        public static string GetSanitizedName(this string val)
        {
            return rgx.Replace(val.ToLowerInvariant().Replace(" ", "-"), "");
        }
    }
}