using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TWSE_Trade_Web_API.Utils
{
    public class TransformTypeTools
    {
        public static string TransformTypeName(string type)
        {
            switch (type)
            {
                case "定價":
                    return "F";
                case "競價":
                    return "C";
                case "議借":
                    return "N";
                default:
                    return null;
            }
        }
        public static string TransformTypeChar(char type)
        {
            switch (type)
            {
                case 'F':
                    return "定價";
                case 'C':
                    return "競價";
                case 'N':
                    return "議借";
                default:
                    return null;
            }
        }
    }
}
