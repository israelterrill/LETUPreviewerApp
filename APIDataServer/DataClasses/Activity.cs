using System.ComponentModel;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic.FileIO;

namespace DataClasses
{
    public class Activity : Event
    {
        public new const string DEFAULT_CSV_HEADER = Event.DEFAULT_CSV_HEADER + ",ImageLink";
        public string ImageLink { get; set; }

        /// <summary>
        /// Creates a new Activity instance from CSV text
        /// </summary>
        /// <param name="csvStr">CSV text</param>
        /// <returns>instance of Activity</returns>
        public new static Activity FromCsv(string csvStr, string hdr = DEFAULT_CSV_HEADER)
        {
            var parts = csvStr.Split(',');
            var header = hdr.Split(',');
            var result = new Activity();
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
                    case "IMAGELINK":
                        result.ImageLink = parts[i];
                        break;

                }
            }
            return result;
        }

        /// <summary>
        /// Creates an array of Activity instances from a CSV file contents
        /// </summary>
        /// <param name="targetPath">target CSV file path</param>
        /// <returns>Activity array</returns>
        public new static Activity[] FromCsvMulti(string targetPath,bool hasHeader=true)
        {
            var contents = File.ReadAllLines(targetPath);
            var header = contents.First();
            return (from line in contents
                    where !hasHeader || !line.Equals(header)
                    select hasHeader ? FromCsv(line,header): FromCsv(line)).ToArray();
        }

        /// <summary>
        /// Serializes instance to CSV text
        /// </summary>
        /// <returns>CSV text</returns>
        public new string ToCsv()
        {
            return string.Format("{0},{1}", base.ToCsv(), ImageLink);
        }

        public static BindingList<Activity> FromCsvFile(string targetPath)
        {
            BindingList<Activity> activities = new BindingList<Activity>();
            if(File.Exists(targetPath)) { 
                using (FileStream fs = File.OpenRead(targetPath))
                {
                    TextFieldParser parser = new TextFieldParser(fs);

                    parser.HasFieldsEnclosedInQuotes = true;
                    parser.SetDelimiters(",");

                    parser.ReadLine();

                    parser.Delimiters = new[] { "," };
                    parser.HasFieldsEnclosedInQuotes = true;
                    while (!parser.EndOfData)
                    {
                        string[] line = parser.ReadFields();
                        string ImageLink = (line.Length == 5) ? line[4] : "";
                        activities.Add(new Activity
                        {
                            Title = line[1],
                            Date = line[2],
                            Location = line[3],
                            Description = line[4],
                            ImageLink = line[0]
                        });
                    }
                }
            }
            foreach(var activity in activities)
            {
                System.Diagnostics.Trace.WriteLine(activity);
            }
            return activities;
        }
    }
}
