namespace DataClasses
{
    static class Extensions
    {
        public static string EscapeCommas(this string str)
        {
            return str.Contains(",") ? string.Format("\"{0}\"", str) : str;
        }
    }
}
