﻿using System.IO;

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
    }
}
