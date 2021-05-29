using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOANLTHDT_1988216.Functions
{
    public static class MyUltilities
    {
        public static bool isInt(string value, ref int result)
        {
            bool flag = int.TryParse(value, out int n);
            result = n;
            return flag;
        }

        public static bool hasSpecialChar(string input)
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
            foreach (var item in specialChar)
            {
                if (input.Contains(item)) return true;
            }

            return false;
        }
    }
}