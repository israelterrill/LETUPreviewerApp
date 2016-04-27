using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataClasses
{
    public class Event
    {
        public const string DEFAULT_CSV_HEADER = "Title,Date,Location,Description";

        public string Title { get; set; }
        public string Date { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }


        /// <summary>
        /// Creates a new Event instance from CSV text
        /// </summary>
        /// <param name="csvStr">CSV text</param>
        /// <returns>instance of Event</returns>
        public static Event FromCsv(string csvStr,string hdr=DEFAULT_CSV_HEADER)
        {
            var parts = csvStr.SplitCsv().ToArray();
            var header = hdr.Split(',');
            var result = new Event();
            for (int i = 0; i < header.Length; i++)
            {
                switch (header[i].ToUpper())
                {
                    case "TITLE":
                        result.Title = parts[i];
                        break;
                    case "DATE":
                        result.Date = parts[i];
                        break;
                    case "LOCATION":
                        result.Location = parts[i];
                        break;
                    case "DESCRIPTION":
                        result.Description = parts[i].Replace("\"", "");
                        break;
                }
            }
            return result;
        }

        /// <summary>
        /// Creates an array of Event instances from a CSV file contents
        /// </summary>
        /// <param name="targetPath">target CSV file path</param>
        /// <returns>Event array</returns>
        public static Event[] FromCsvMulti(string targetPath,bool hasHeader=true)
        {
            string[] contents;
            using (var stream = File.Open(targetPath, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(stream))
                contents = reader.ReadToEnd().Split(new[] {'\r','\n'},StringSplitOptions.RemoveEmptyEntries);
            var header = contents.First();
            return (from line in File.ReadAllLines(targetPath)
                    where !hasHeader || !line.Equals(header)
                    select hasHeader ? FromCsv(line,header) : FromCsv(line)).ToArray();
        }

        /// <summary>
        /// Serializes instance to CSV text
        /// </summary>
        /// <returns>CSV text</returns>
        public string ToCsv()
        {
            return string.Format("{0},{1},{2},{3}",
                                    Title,
                                    Date,
                                    Location,
                                    Description.EscapeCommas());
        }

        public void Update(Event updated)
        {
            Title = updated.Title;
            Date = updated.Date;
            Location = updated.Location;
            Description = updated.Description;
        }
    }
}
