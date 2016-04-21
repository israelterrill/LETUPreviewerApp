using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace DataClasses
{
    public static class Extensions
    {
        /// <summary>
        /// String extension method to encapsulate string in double quotes if it contains any commas
        /// </summary>
        /// <param name="str">string to be escaped</param>
        /// <returns>if str contains commas, returns: "str", else returns str</returns>
        public static string EscapeCommas(this string str)
        {
            return str.Contains(",") ? string.Format("\"{0}\"", str) : str;
        }

        public static string GetSafeFileName(this string filename)
        {
            return string.Join("-", filename.Split(Path.GetInvalidFileNameChars()));
        }

        public static IEnumerable<string> SplitCsv(this string csvStr)
        {
            var parts = new List<string>();
            parts.Add(String.Empty);
            var inQuotes = false;
            foreach (var ch in csvStr.ToCharArray())
            {
                if (ch.Equals('"')) inQuotes = !inQuotes;
                else if (!inQuotes && ch.Equals(',')) parts.Add(string.Empty);
                else parts[parts.Count - 1] += ch;
            }
            return parts;
        }
    }
}
