using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ServiceStack.Text;
using System.Data;
using Microsoft.VisualBasic.FileIO;

namespace DataClasses
{
    public class Schedule
    {
        /// <summary>
        /// Schedule data file naming standard
        /// </summary>
        public const string FILE_PATTERN = @"^Schedule(?<title>[^_]+)_(?<dates>.*)\.csv$";
		
        public string ScheduleTitle { get; set; }
        public string ScheduleDates { get; set; }
        private BindingList<Event> _events;
        public BindingList<Event> Events 
        {
            get
            {
                if(_events == null)
                    _events = new BindingList<Event>();
                return _events;
            }
            set { _events = value; }
        }

        /// <summary>
        /// Creates a new Activity instance from CSV text
        /// </summary>
        /// <param name="csvStr">CSV text</param>
        /// <returns>instance of Activity</returns>
        public static Schedule FromCsv(string targetPath,bool hasHeader=true)
        {
            var fileName = Path.GetFileName(targetPath);
            var rgxFileName = new Regex(FILE_PATTERN);
            if (!rgxFileName.IsMatch(fileName)) throw new FormatException(string.Format("Target file '{0}' does not conform to file name standards.", targetPath));
            var match = rgxFileName.Match(fileName);
            var result = new Schedule
            {
                ScheduleTitle = match.Groups["title"].Value,
                ScheduleDates = match.Groups["dates"].Value,
            };
            var contents = File.ReadAllLines(targetPath);
            var header = contents.First();
            result.Events = new BindingList<Event>((from line in contents
                select hasHeader ? Event.FromCsv(line,header) : Event.FromCsv(line)).ToList());
            return result;
        }

        /// <summary>
        /// Creates an array of Schedule instances from a CSV file contents
        /// </summary>
        /// <param name="targetPath">target CSV file path</param>
        /// <returns>Schedule array</returns>
        public static Schedule[] FromCsvMulti(string targetDir,bool hasHeader=true)
        {
            var rgxScheduleFile = new Regex(FILE_PATTERN);
            return (from file in Directory.GetFiles(targetDir)
                    where rgxScheduleFile.IsMatch(Path.GetFileName(file))
                    select FromCsv(file,hasHeader)).ToArray();
        }

        /// <summary>
        /// Serializes instance to CSV text
        /// </summary>
        /// <returns>CSV text</returns>
        public void ToCsv(string targetDir)
        {
            if (!Directory.Exists(targetDir)) throw new DirectoryNotFoundException();
            var fileName = Path.Combine(targetDir, GetSafeFileName(string.Format("Schedule{0}_{1}.csv", ScheduleTitle, ScheduleDates)));
            using (FileStream fs = (FileStream)File.Create(fileName))
            {
                CsvSerializer.SerializeToStream(Events, fs);
            }
        }

        public static Schedule FromCsvFile(string targetPath)
        {
            BindingList<Event> events = new BindingList<Event>();
            using (FileStream fs = File.OpenRead(targetPath))
            {
                TextFieldParser parser = new TextFieldParser(fs);

                parser.HasFieldsEnclosedInQuotes = true;
                parser.SetDelimiters(",");

                parser.ReadLine();

                parser.Delimiters = new[] { "," };
                parser.HasFieldsEnclosedInQuotes = true;
                System.Diagnostics.Trace.WriteLine(targetPath);
                while (!parser.EndOfData)
                {
                    string[] line = parser.ReadFields();
                    events.Add(new Event
                    {
                        Title = line[0],
                        Date = line[1],
                        Location = line[2],
                        Description = line[3]
                    });
                }
            }

            var fileParts = Path.GetFileNameWithoutExtension(targetPath).Split('_');

            return new Schedule
            {
                ScheduleTitle = fileParts[0].Substring(8),
                ScheduleDates = fileParts[1],
                Events = events
            };
        }

        public static string GetSafeFileName(string filename)
        {

            return string.Join("-", filename.Split(Path.GetInvalidFileNameChars()));

        }

    }
}
