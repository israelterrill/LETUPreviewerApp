using System.IO;
using System.Linq;

namespace DataClasses
{
    public class Event
    {
        public string Title { get; set; }
        public string Date { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        public static Event FromCsv(string csvStr)
        {
            var parts = csvStr.Split(',');
            return new Event
            {
                Title = parts[0],
                Date = parts[1],
                Location = parts[2],
                Description = parts[3].Replace("\"",""),
            };
        }

        public static Event[] FromCsvMulti(string targetPath)
        {
            return (from line in File.ReadAllLines(targetPath)
                    select FromCsv(line)).ToArray();
        }

        public string ToCsv()
        {
            return string.Format("{0},{1},{2},{3}",
                                    Title,
                                    Date,
                                    Location,
                                    Description.EscapeCommas());
        }
    }
}
