using System.IO;
using System.Linq;

namespace DataClasses
{
    public class Activity : Event
    {
        public string ImageLink { get; set; }

        public static Activity FromCsv(string csvStr)
        {
            var parts = csvStr.Split(',');
            var result = Event.FromCsv(string.Join(",", parts.Take(4))) as Activity;
            result.ImageLink = parts.Last();
            return result;
        }

        public static Activity[] FromCsvMulti(string targetPath)
        {
            return (from line in File.ReadAllLines(targetPath)
                    select FromCsv(line)).ToArray();
        }

        public string ToCsv()
        {
            return string.Format("{0},{1}", base.ToCsv(), ImageLink);
        }
}
}
