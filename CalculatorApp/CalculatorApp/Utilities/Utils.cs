using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CalculatorApp.Utilities
{
    public static class Utils
    {
        public static string FormatCulture(string numberString)
        {
            bool hasDecimal = numberString.Contains(".");

            if(hasDecimal)
            {
                string[] parts = numberString.Split('.');

                if (parts.Length > 2) return "Error";

                string beforeDecimal = parts[0];

                string afterDecimal = parts[1];

                if (long.TryParse(beforeDecimal, out long numberValue))
                {
                    string dotComma = ".";
                    switch(CultureInfo.CurrentCulture.Name)
                    {
                        case "ro-RO":
                            dotComma = ",";
                            break;
                        case "en-UK":
                            dotComma = ".";
                            break;
                    }

                    return numberValue.ToString("N0", CultureInfo.CurrentCulture) + dotComma + afterDecimal;
                }
            }
            else
            {
                if (long.TryParse(numberString, out long numberValue))
                {
                    return numberValue.ToString("N0", CultureInfo.CurrentCulture);
                }
            }

            return "0";
        }



        public static string FromDecToHex(string value)
        {
            if (value == null) return "0";
            if (value == "") return "";
            int decimalValue = int.Parse(Convert.ToInt32(double.Parse(value)).ToString());
            if (decimalValue == 0)
            {
                return "0";
            }
            string hexString = string.Empty;
            while (decimalValue > 0)
            {
                int digit = decimalValue % 16;
                if (digit > 9)
                {
                    hexString += (char)('A' + digit - 10);
                }
                else
                {
                    hexString += (char)('0' + digit);
                }
                decimalValue /= 16;
            }
            return new string(hexString.Reverse().ToArray());
        }

        public static string FromHexToDec(string hexValue)
        {
            if (hexValue == null) return "0";
            if (hexValue == "") return "";
            hexValue = hexValue.Trim();
            try
            {
                int decimalValue = Convert.ToInt32(hexValue, 16);
                return decimalValue.ToString();
            }
            catch(Exception) { return "0"; }
        }

        public static string FromDecToBase(string value, int toBase)
        {
            if (value == null) return "0";
            if (value == "") return "";
            int decimalValue = int.Parse(Convert.ToInt32(double.Parse(value)).ToString());
            string newBaseString = string.Empty;
            if (decimalValue == 0)
            {
                return "0";
            }
            while (decimalValue > 0)
            {
                int digit = decimalValue % toBase;
                newBaseString += (char)('0' + digit);

                decimalValue /= toBase;
            }
            return new string(newBaseString.Reverse().ToArray());
        }

        public static string FromBaseToDec(string value, int fromBase)
        {
            if (value == null) return "0";
            if (value == "") return "";
            if (value == "0")
            {
                return "0";
            }
            int newBaseString = 0;
            value = new string(value.Reverse().ToArray());
            int counter = 0;
            foreach (char c in value)
            {
                int digit = c - '0';
                newBaseString += digit * (int)Math.Pow(fromBase, counter++);
            }
            return newBaseString.ToString();
        }







    }
}
