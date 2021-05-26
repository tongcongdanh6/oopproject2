using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOANLTHDT_1988216.Functions
{
    public class MyUltilities
    {
        public bool isInt(string value, ref int result)
        {
            bool flag = int.TryParse(value, out int n);
            result = n;
            return flag;
        }
    }
}