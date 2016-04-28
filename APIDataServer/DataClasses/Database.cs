using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace DataClasses
{
    public class Database
    {
        [Flags]
        public enum ImportOptions
        {
            Activities = 1,
            MapData = 2,
            Questions = 4,
            AllSchedules = 8,
            All = 15,
            SingleSchedule = 16,
        }

        [Flags]
        public enum ExportOptions
        {
            Activities = 1,
            MapData = 2,
            Questions = 4,
            AllSchedules = 8,
            All = 15,
            SingleSchedule = 16,
        }

        public string DataDirectory { get; private set; }

        public string DataFormat { get; private set; }

        public readonly bool RefreshOnFileChange;

        private readonly FileSystemWatcher FileSystemWatcher = new FileSystemWatcher();

        private string SchedulesFile { get { return Path.Combine(DataDirectory, "schedule." + DataFormat); } }
        private string MapDataFile { get { return Path.Combine(DataDirectory, "mapdata." + DataFormat); } }
        private string QuestionsFile { get { return Path.Combine(DataDirectory, "questions." + DataFormat); } }
        private string ActivitiesFile { get { return Path.Combine(DataDirectory, "activities." + DataFormat); } }

        public BindingList<Activity> Activities = new BindingList<Activity>();
        public BindingList<Schedule> Schedules = new BindingList<Schedule>();
        public BindingList<Question> Questions = new BindingList<Question>();
        public BindingList<Map> MapData = new BindingList<Map>();

        public Database(string dataDirectory, string dataFormat = "csv", bool refeshOnFileChange = false)
        {
            DataDirectory = dataDirectory;
            DataFormat = dataFormat;
            RefreshOnFileChange = refeshOnFileChange;

            if (!Directory.Exists(DataDirectory)) throw new DirectoryNotFoundException(string.Format("Directory '{0}' not found.", dataDirectory));
            Import(ImportOptions.All);

            FileSystemWatcher.Path = DataDirectory + "\\";
            FileSystemWatcher.Filter = "*." + DataFormat;
            FileSystemWatcher.NotifyFilter = NotifyFilters.LastAccess |
                                             NotifyFilters.LastWrite |
                                             NotifyFilters.FileName |
                                             NotifyFilters.DirectoryName;
            FileSystemWatcher.Changed += OnChanged;
            FileSystemWatcher.Created += OnChanged;
            FileSystemWatcher.Deleted += OnChanged;
            FileSystemWatcher.Renamed += OnRenamed;
            FileSystemWatcher.EnableRaisingEvents = RefreshOnFileChange;
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            var oldFile = Path.GetFileName(e.OldFullPath);
            var newFile = Path.GetFileName(e.FullPath);
            var rgxSchedule = new Regex(Schedule.FILE_PATTERN);
            if (rgxSchedule.IsMatch(oldFile))
            {
                var schedule = Schedules.First(sch => oldFile.Equals(sch.FileName + "." + DataFormat));
                if (rgxSchedule.IsMatch(newFile))
                {
                    var match = rgxSchedule.Match(newFile);
                    schedule.ScheduleTitle = match.Groups["title"].Value;
                    schedule.ScheduleDates = match.Groups["dates"].Value;
                }
                else
                {
                    Schedules.Remove(schedule);
                }
            }
            else
            {
                switch (newFile)
                {
                    case "activities":
                        Activities.Clear();
                        break;
                    case "questions":
                        Questions.Clear();
                        break;
                    case "mapdata":
                        MapData.Clear();
                        break;
                }
            }
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            var file = Path.GetFileName(e.FullPath);
            if (Regex.IsMatch(file, Schedule.FILE_PATTERN))
            {
                var newSchedule = Schedule.FromCsv(e.FullPath);
                if (e.ChangeType.HasFlag(WatcherChangeTypes.Created))
                {
                    Schedules.Add(newSchedule);
                    return;
                }
                Schedule schedule = null;
                if (e.ChangeType.HasFlag(WatcherChangeTypes.Changed) ||
                    e.ChangeType.HasFlag(WatcherChangeTypes.Deleted))
                    schedule = Schedules.First(sch => file.Equals(sch.FileName + "." + DataFormat));
                if (schedule == null) return;
                if (e.ChangeType.HasFlag(WatcherChangeTypes.Changed))
                    schedule.Update(newSchedule);
                else if (e.ChangeType.HasFlag(WatcherChangeTypes.Deleted))
                    Schedules.Remove(schedule);
            }
            else
            {
                switch (file)
                {
                    case "activities":
                        if (e.ChangeType.HasFlag(WatcherChangeTypes.Created) ||
                            e.ChangeType.HasFlag(WatcherChangeTypes.Changed))
                            Import(ImportOptions.Activities);
                        else if (e.ChangeType == WatcherChangeTypes.Deleted)
                            Activities.Clear();
                        break;
                    case "questions":
                        if (e.ChangeType.HasFlag(WatcherChangeTypes.Created) ||
                            e.ChangeType.HasFlag(WatcherChangeTypes.Changed))
                            Import(ImportOptions.Questions);
                        else if (e.ChangeType == WatcherChangeTypes.Deleted)
                            Questions.Clear();
                        break;
                    case "mapdata":
                        if (e.ChangeType.HasFlag(WatcherChangeTypes.Created) ||
                            e.ChangeType.HasFlag(WatcherChangeTypes.Changed))
                            Import(ImportOptions.MapData);
                        else if (e.ChangeType == WatcherChangeTypes.Deleted)
                            MapData.Clear();
                        break;
                }
            }
        }

        public void Export(ExportOptions options, string dataDir = null, Schedule schedule = null)
        {
            if (dataDir == null) dataDir = DataDirectory;
            var dataDirOld = DataDirectory;
            DataDirectory = dataDir;
            try
            {
                switch (DataFormat)
                {
                    case "json":
                        if (options.HasFlag(ExportOptions.AllSchedules))
                        {
                            File.WriteAllText(SchedulesFile, JsonConvert.SerializeObject(Schedules));
                        }
                        if (options.HasFlag(ExportOptions.Questions))
                        {
                            File.WriteAllText(QuestionsFile, JsonConvert.SerializeObject(Questions));
                        }
                        if (options.HasFlag(ExportOptions.Activities))
                        {
                            File.WriteAllText(QuestionsFile, JsonConvert.SerializeObject(Activities));
                        }
                        if (options.HasFlag(ExportOptions.MapData))
                        {
                            File.WriteAllText(QuestionsFile, JsonConvert.SerializeObject(MapData));
                        }
                        break;
                    case "csv":
                        if (options.HasFlag(ExportOptions.AllSchedules))
                        {
                            var rgxSchedule = new Regex(Schedule.FILE_PATTERN);
                            var files = from file in Directory.GetFiles(DataDirectory)
                                        let fiName = Path.GetFileName(file)
                                        where rgxSchedule.IsMatch(fiName)
                                        select file;
                            foreach (var file in files) File.Delete(file);

                            foreach (var sch in Schedules) sch.ToCsv(DataDirectory);
                        }
                        if (options == ExportOptions.SingleSchedule) schedule.ToCsv(DataDirectory);
                        if (options.HasFlag(ExportOptions.Questions))
                        {
                            var lines = new string[] { Question.DEFAULT_CSV_HEADER }.Concat(Questions.Select(que => que.ToCsv()));
                            File.WriteAllLines(QuestionsFile, lines, Encoding.UTF8);
                        }
                        if (options.HasFlag(ExportOptions.Activities))
                        {
                            var lines = new string[] { Activity.DEFAULT_CSV_HEADER }.Concat(Activities.Select(act => act.ToCsv()));
                            File.WriteAllLines(ActivitiesFile, lines, Encoding.UTF8);
                        }
                        if (options.HasFlag(ExportOptions.MapData))
                        {
                            var lines = new string[] { Map.DEFAULT_CSV_HEADER }.Concat(MapData.Select(map => map.ToCsv()));
                            File.WriteAllLines(MapDataFile, lines, Encoding.UTF8);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                DataDirectory = dataDirOld;
            }
        }

        public void Import(ImportOptions options, string scheduleFile = null)
        {
            try
            {
                switch (DataFormat)
                {
                    case "json":
                        if (options.HasFlag(ImportOptions.AllSchedules))
                        {
                            Schedule[] newSch = null;
                            using (var stream = File.Open(SchedulesFile, FileMode.Open, FileAccess.Read))
                            using (var reader = new StreamReader(stream))
                                newSch = JsonConvert.DeserializeObject<Schedule[]>(reader.ReadToEnd());
                            if (newSch == null) return;
                            Schedules.Clear();
                            foreach (var sch in newSch)
                                Schedules.Add(sch);
                        }
                        if (options.HasFlag(ImportOptions.MapData))
                        {
                            Map[] newMaps = null;
                            using (var stream = File.Open(MapDataFile, FileMode.Open, FileAccess.Read))
                            using (var reader = new StreamReader(stream))
                                newMaps = JsonConvert.DeserializeObject<Map[]>(reader.ReadToEnd());
                            if (newMaps == null) return;
                            MapData.Clear();
                            foreach (var map in newMaps)
                                MapData.Add(map);
                        }
                        if (options.HasFlag(ImportOptions.Questions))
                        {
                            Question[] newQueries = null;
                            using (var stream = File.Open(QuestionsFile, FileMode.Open, FileAccess.Read))
                            using (var reader = new StreamReader(stream))
                                newQueries = JsonConvert.DeserializeObject<Question[]>(reader.ReadToEnd());
                            if (newQueries == null) return;
                            Questions.Clear();
                            foreach (var query in newQueries)
                                Questions.Add(query);
                        }
                        if (options.HasFlag(ImportOptions.Activities))
                        {
                            Activity[] newAct = null;
                            using (var stream = File.Open(ActivitiesFile, FileMode.Open, FileAccess.Read))
                            using (var reader = new StreamReader(stream))
                                newAct = JsonConvert.DeserializeObject<Activity[]>(reader.ReadToEnd());
                            if (newAct == null) return;
                            Activities.Clear();
                            foreach (var act in newAct)
                                Activities.Add(act);
                        }
                        break;
                    case "csv":
                        if (options.HasFlag(ImportOptions.AllSchedules))
                        {
                            var newSch = Schedule.FromCsvMulti(DataDirectory, true);
                            Schedules.Clear();
                            foreach (var sch in newSch) Schedules.Add(sch);
                        }
                        if (options == ImportOptions.SingleSchedule) Schedules.Add(Schedule.FromCsv(scheduleFile));
                        if (options.HasFlag(ImportOptions.MapData))
                        {
                            var newMaps = Map.FromCsvMulti(MapDataFile, true);
                            MapData.Clear();
                            foreach (var map in newMaps) MapData.Add(map);
                        }
                        if (options.HasFlag(ImportOptions.Questions))
                        {
                            var newQueries = Question.FromCsvMulti(QuestionsFile, true);
                            Questions.Clear();
                            foreach (var query in newQueries) Questions.Add(query);
                        }
                        if (options.HasFlag(ImportOptions.Activities))
                        {
                            var newActs = Activity.FromCsvMulti(ActivitiesFile, true);
                            Activities.Clear();
                            foreach (var act in newActs) Activities.Add(act);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
