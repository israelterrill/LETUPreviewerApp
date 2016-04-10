using System.IO;
using System.Linq;

namespace DataClasses
{
    public class Question
    {
        public string Query;
        public string Answer;

        /// <summary>
        /// Creates a new Question instance from CSV text
        /// </summary>
        /// <param name="csvStr">CSV text</param>
        /// <returns>instance of Question</returns>
        public static Question FromCsv(string csvStr)
        {
            var parts = csvStr.Split(',');
            return new Question
            {
                Query = parts[0].Replace("\"", ""),
                Answer = parts[1].Replace("\"", ""),
            };
        }

        /// <summary>
        /// Creates an array of Question instances from a CSV file contents
        /// </summary>
        /// <param name="targetPath">target CSV file path</param>
        /// <returns>Question array</returns>
        public static Question[] FromCsvMulti(string targetPath)
        {
            return (from line in File.ReadAllLines(targetPath)
                    select FromCsv(line)).ToArray();
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
