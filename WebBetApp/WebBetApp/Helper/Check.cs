using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBetApp.Helper
{
    public class Check
    {
        public static bool IsEmptyOrWhitespace(string value)
        {
            return (value != null) && value.Trim() == string.Empty;
        }
        public static bool IsNullOrEmptyOrWhitespace(string value)
        {
            return (value == null) || IsEmptyOrWhitespace(value);
        }

        public static bool IsValidMatchType(string type)
        {
           string[] matchTypes = new string[] { "1", "x", "2", "1x", "x2", "12" };

           return Array.Exists(matchTypes, value => value == type);
        }

    }
}

