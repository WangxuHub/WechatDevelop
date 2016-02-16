using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace Common.Helper
{
    public class Common
    {
        public static bool IsValidEmail(string str)
        {
            string pattern = "^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(.[a-zA-Z0-9_-])+$";
            Regex regex = new Regex(pattern);

            bool isMatch = regex.IsMatch(str);

            return isMatch;
        }
    }
}
