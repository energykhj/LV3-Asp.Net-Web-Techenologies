/*
 * PROG2230 Assignment4
 * 
 * Purpose: To practice validation through class library
 * 
 * Revision History
 *    Nov 17, 2019 - Heuijin Ko created
 * 
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;


namespace HKClassLibrary
{
    public static class HKValidations
    {
        /// <summary>
        /// Shift the first letter of every word in the string to upper case.
        /// </summary>
        /// <param name="sOrigin"></param>
        /// <returns></returns>
        public static string HKCapitalize(string sOrigin)
        {
            // i.	If the input string is null, return an empty string.
            if (string.IsNullOrEmpty(sOrigin)) return "";

            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

            // ii.	Change the input string to lower case and 
            // remove leading & trailing spaces.
            return myTI.ToTitleCase(sOrigin.ToLower().Trim());
        }
        /// <summary>
        /// return a string containing all digits found in the input string
        /// </summary>
        /// <param name="sOrigin"></param>
        /// <returns></returns>
        public static string HKExtractDigits(string sOrigin)
        {
            string extractedDigits = "";

            foreach (char c in sOrigin)
            {
                if (char.IsDigit(c))
                    extractedDigits += c;
            }

            return extractedDigits.ToString();

        }
        /// <summary>
        /// Making regular expressions for postal code.
        /// </summary>
        /// <param name="sPC"></param>
        /// <returns></returns>
        public static bool HKPostalCodeValidation(string sPC)
        {
            if (string.IsNullOrEmpty(sPC))  return true;

            string sPattern = @"^[ABCEGHJ-NPRSTVXY]{1}[0-9]{1}[ABCEGHJ-NPRSTV-Z]" +
                "{1}[ ]?[0-9]{1}[ABCEGHJ-NPRSTV-Z]{1}[0-9]{1}$";

            return HKIsMatch(sPattern, sPC);
        }
        /// <summary>
        /// Make Postal code format put a space between Postal code
        /// </summary>
        /// <param name="PCF"></param>
        /// <returns></returns>
        public static string HKPostalCodeFormat(string PCF)
        {
            //if postcode length is 6, insert the space into the middle of them.
            if (PCF.Length == 6)
            {
                PCF = PCF.Insert(3, " ");
            }

            return PCF.ToUpper();
        }
        /// <summary>
        /// Making regular expressions for US Zip code.
        /// </summary>
        /// <param name="sZIP"></param>
        /// <returns></returns>
        public static bool HKZipCodeValidation(ref string sZIP)
        {
            // 2. e.ii
            if (string.IsNullOrEmpty(sZIP) || string.IsNullOrWhiteSpace(sZIP))
            {
                sZIP = "";
                return true;
            }

            // 2. e.iii
            string extractedZIP = HKExtractDigits(sZIP);

            // 2. e.iv
            if (extractedZIP.Length == 5)
            {
                sZIP = extractedZIP;
                return true;
            }
            else if (sZIP.Length == 9)
            {
                sZIP = extractedZIP.Insert(5, "-");
                return true;
            }

            return false;
        }
        /// <summary>
        /// Validate with a regular pattern.
        /// </summary>
        /// <param name="sPattern"></param>
        /// <param name="sValue"></param>
        /// <returns></returns>
        private static bool HKIsMatch(string sPattern, string sValue)
        {
            try
            {
                if (string.IsNullOrEmpty(sValue) ||
                    string.IsNullOrEmpty(sPattern)) return false;

                Regex reg = new Regex(sPattern,
                    RegexOptions.IgnoreCase |
                    RegexOptions.IgnorePatternWhitespace);
                return reg.IsMatch(sValue);
            }
            catch
            {
                return false;
            }
        }
    }
}
