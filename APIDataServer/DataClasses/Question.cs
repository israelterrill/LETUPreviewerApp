using System.IO;
using System.Linq;

namespace DataClasses
{
    public class Question
    {
        public string Query;
        public string Answer;

        public static Question FromCsv(string csvStr)
        {
            var parts = csvStr.Split(',');
            return new Question
            {
                Query = parts[0].Replace("\"", ""),
                Answer = parts[1].Replace("\"", ""),
            };
        }

        public static Question[] FromCsvMulti(string targetPath)
        {
            return (from line in File.ReadAllLines(targetPath)
                    select FromCsv(line)).ToArray();
        }

    public string ToCsv()
        {
            return string.Format("{0},{1}",
                Query.EscapeCommas(),
                Answer.EscapeCommas());
        }
    }
}
