using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DataClasses
{
    public class Schedule
    {
        public const string FILE_PATTERN = "(?<title>[^_]+)_(?<dates>.*)";
        public string ScheduleTitle { get; set; }
        public string ScheduleDates { get; set; }
        public BindingList<Event> Events { get; set; }

        public static Schedule FromCsv(string targetPath)
        {
            var fileName = Path.GetFileNameWithoutExtension(targetPath);
            var rgxFileName = new Regex(FILE_PATTERN);
            if (!rgxFileName.IsMatch(fileName)) throw new FormatException(string.Format("Target file '{0}' does not conform to file name standards.", targetPath));
            var match = rgxFileName.Match(fileName);
            return new Schedule
            {
                ScheduleTitle = match.Groups["title"].Value,
                ScheduleDates = match.Groups["dates"].Value,
                Events = new BindingList<Event>((from line in File.ReadAllLines(targetPath)
                                                 select Event.FromCsv(line)).ToList())
            };
        }

        public static Schedule[] FromCsvMulti(string targetDir)
        {
            var rgxScheduleFile = new Regex(FILE_PATTERN);
            return (from file in Directory.GetFiles(targetDir)
                    where rgxScheduleFile.IsMatch(Path.GetFileNameWithoutExtension(file))
                    select FromCsv(file)).ToArray();
        }

        public void ToCsv(string targetDir)
        {
            if (!Directory.Exists(targetDir)) throw new DirectoryNotFoundException();
            var csvStr = string.Join(Environment.NewLine, Events.Select(ev => ev.ToCsv()).ToArray());
            File.WriteAllText(Path.Combine(targetDir, string.Format("{0}_{1}", ScheduleTitle, ScheduleDates)), csvStr);
        }
    }
}
