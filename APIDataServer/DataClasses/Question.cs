using System.IO;
using System.Linq;

namespace DataClasses
{
    public class Question
    {
        public const string DEFAULT_CSV_HEADER = "Query,Answer";

        public string Query;
        public string Answer;

        /// <summary>
        /// Creates a new Question instance from CSV text
        /// </summary>
        /// <param name="csvStr">CSV text</param>
        /// <returns>instance of Question</returns>
        public static Question FromCsv(string csvStr, string hdr = DEFAULT_CSV_HEADER)
        {
            var parts = csvStr.SplitCsv().ToArray();
            var header = hdr.Split(',');
            var result = new Question();
            for (int i = 0; i < header.Length; i++)
            {
                switch (header[i].ToUpper())
                {
                    case "QUERY":
                        result.Query = parts[i];
                        break;
                    case "ANSWER":
                        result.Answer = parts[i];
                        break;
                }
            }
            return result;
        }

        /// <summary>
        /// Creates an array of Question instances from a CSV file contents
        /// </summary>
        /// <param name="targetPath">target CSV file path</param>
        /// <returns>Question array</returns>
        public static Question[] FromCsvMulti(string targetPath, bool hasHeader = true)
        {
            var contents = File.ReadAllLines(targetPath);
            var header = contents.First();
            return (from line in contents
                    where !hasHeader || !line.Equals(header)
                    select hasHeader ? FromCsv(line, header) : FromCsv(line)).ToArray();
        }

        /// <summary>
        /// Serializes instance to CSV text
        /// </summary>
        /// <returns>CSV text</returns>
        public string ToCsv()
        {
            return string.Format("{0},{1}",
                Query.EscapeCommas(),
                Answer.EscapeCommas());
        }
    }
}
