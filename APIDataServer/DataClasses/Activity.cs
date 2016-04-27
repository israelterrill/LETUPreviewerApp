using System;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace DataClasses
{
    public class Activity : Event
    {
        public new const string DEFAULT_CSV_HEADER = "ImageLink" + Event.DEFAULT_CSV_HEADER;
        public string ImageLink { get; set; }

        /// <summary>
        /// Creates a new Activity instance from CSV text
        /// </summary>
        /// <param name="csvStr">CSV text</param>
        /// <returns>instance of Activity</returns>
        public new static Activity FromCsv(string csvStr, string hdr = DEFAULT_CSV_HEADER)
        {
            var parts = csvStr.SplitCsv().ToArray();
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
            string[] contents;
            using (var stream = File.Open(targetPath, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(stream))
                contents = reader.ReadToEnd().Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
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

        public void Update(Activity updated)
        {
            base.Update(updated);
            ImageLink = updated.ImageLink;
        }
   }
}
