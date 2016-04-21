using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DataClasses;
using ServiceStack;
using ServiceStack.Text;

namespace APIDataServer
{
    public partial class MainForm : Form
    {
#if DEBUG
        private const string DATA_DIR = @"..\..\..\Install\Data";
#else
        private const string DATA_DIR = @"Data";
#endif
        private const string ACTIVITIES_FILE = "activities.csv";
        private const string ACTIVITIES_PATH = DATA_DIR + @"\" + ACTIVITIES_FILE;

        BindingList<Schedule> schedules = new BindingList<Schedule>();
        BindingList<Activity> activities;

        private bool eventSelected = false;
        public MainForm()
        {
            InitializeComponent();
            activities = new BindingList<Activity>();
            Load += ActivityTab_Load;
            Load += ScheduleTab_Load;
            dgvSchedules.UserDeletingRow += DgvSchedulesUserDeletingRow;

            btnSaveActivities.Click += (s, e) => SaveActivities();
            btnSaveSchedules.Click += (s, e) => SaveSchedules();
            btnImportSchedule.Click += (s, e) => ImportSchedule();
            btnExportSchedules.Click += (s, e) => ExportSchedules();

            dgvSchedules.SelectionChanged += dgvSchedules_SelectionChanged;
            dgvEvents.SelectionChanged += dgvEvents_SelectionChanged;
        }

        private void ActivityTab_Load(object sender, EventArgs e)
        {
            dgvActivities.DataSource = GetActivityEntries();
            dgvActivities.AutoResizeColumns();
        }

        private void ScheduleTab_Load(object sender, EventArgs e)
        {
            GetScheduleEntriesFromDataFolder();
            dgvSchedules.DataSource = schedules;
            if (schedules.Count > 0) dgvEvents.DataSource = schedules.First().Events;
            dgvSchedules.AutoResizeColumns();
        }

        private BindingList<Activity> GetActivityEntries()
        {
            var fullPath = Path.GetFullPath(ACTIVITIES_PATH);
            if (File.Exists(fullPath)) ImportActivities(fullPath);
            return activities;
        }

        private void GetScheduleEntriesFromDataFolder()
        {
            if (!Directory.Exists(DATA_DIR)) return;
            var fileNames = Directory.GetFiles(DATA_DIR);
            var rgxFileName = new Regex(Schedule.FILE_PATTERN);
            var filteredFileNames = fileNames.Where(fileName => rgxFileName.IsMatch(Path.GetFileName(fileName)));

            foreach (var fileName in filteredFileNames) schedules.Add(Schedule.FromCsv(Path.GetFullPath(fileName)));
        }

        private void SaveActivities()
        {
            using (var fs = File.Create(ACTIVITIES_PATH)) CsvSerializer.SerializeToStream(activities, fs);
        }

        private void SaveSchedules()
        {
            foreach (var schedule in schedules)
            {
                var fullPath = Path.GetFullPath(DATA_DIR);
                schedule.ToCsv(fullPath);
            }
        }

        private void ImportSchedule()
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            if (fileDialog.ShowDialog() != DialogResult.OK) return;
            foreach (var filename in fileDialog.FileNames)
                schedules.Add(Schedule.FromCsv(filename));

            dgvSchedules.DataSource = schedules;
            dgvSchedules.Update();
            dgvSchedules.Refresh();
            if (schedules.Count <= 0) return;
            dgvEvents.DataSource = schedules.First().Events;
            dgvEvents.Update();
            dgvEvents.Refresh();
        }

        private void ExportSchedules()
        {
            var folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() != DialogResult.OK) return;
            foreach (var schedule in schedules) schedule.ToCsv(folderDialog.SelectedPath);
        }


        private void btnImportActivity_Click(object sender, EventArgs e)
        {
            //Activity Import Button
            var fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            if (fileDialog.ShowDialog() != DialogResult.OK) return;
            foreach (var filename in fileDialog.FileNames) ImportActivities(filename);
            dgvActivities.Update();
            dgvActivities.Refresh();
        }

        private void btnExportActivity_Click(object sender, EventArgs e)
        {
            //Activity Export
            var fileDialog = new SaveFileDialog();
            fileDialog.Filter = @"CSV files (*.csv)|*.csv";
            fileDialog.DefaultExt = "csv";
            fileDialog.AddExtension = false;
            fileDialog.FileName = ACTIVITIES_FILE;
            if (fileDialog.ShowDialog() != DialogResult.OK) return;
            using (var fs = (FileStream)fileDialog.OpenFile()) CsvSerializer.SerializeToStream(activities, fs);
        }

        private void btnRemoveActivity_Click(object sender, EventArgs e)
        {
            //Remove Activities
            foreach (DataGridViewRow item in dgvActivities.SelectedRows) dgvActivities.Rows.RemoveAt(item.Index);

            dgvActivities.Update();
            dgvActivities.Refresh();
        }

        private void btnRemoveSchedule_Click(object sender, EventArgs e)
        {
            if (schedules.Count <= 0) return;
            if (eventSelected)
            {
                var schedule = schedules.ElementAt(dgvSchedules.SelectedRows.Cast<DataGridViewRow>().First().Index);
                foreach (DataGridViewRow item in dgvEvents.SelectedRows)
                {
                    DeleteEventFromScheduleFile(schedule, item.Index);
                }
                dgvEvents.Update();
                dgvEvents.Refresh();
            }
            else
            {
                foreach (DataGridViewRow item in dgvSchedules.SelectedRows)
                {
                    var schedule = schedules.ElementAt(item.Index);
                    DeleteScheduleFileInDataDirectory(schedule);
                    dgvSchedules.Rows.RemoveAt(item.Index);
                }
                dgvSchedules.Update();
                dgvSchedules.Refresh();
            }
        }

        private void dgvSchedules_SelectionChanged(object sender, EventArgs e)
        {
            if (schedules.Count <= 0 || dgvSchedules.SelectedRows.Count <= 0) return;
            dgvEvents.DataSource =
                schedules.ElementAt(dgvSchedules.SelectedRows.Cast<DataGridViewRow>().First().Index).Events;
            dgvEvents.Update();
            dgvEvents.Refresh();
            eventSelected = false;
        }

        void dgvEvents_SelectionChanged(object sender, EventArgs e)
        {
            if (schedules.Count <= 0 || dgvSchedules.SelectedRows.Count <= 0) return;
            var selectedSchedule = schedules.ElementAt(dgvSchedules.SelectedRows.Cast<DataGridViewRow>().First().Index);
            if (selectedSchedule.Events.Count <= 0 || dgvEvents.SelectedRows.Count <= 0) return;
            eventSelected = true;
        }

        private void btnExportSelectedSchedules_Click(object sender, EventArgs e)
        {
            //Export Currently Selected
            var folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() != DialogResult.OK || schedules.Count <= 0) return;
            var selectedCells = dgvSchedules.SelectedCells;
            var usedElements = new List<Schedule>();
            for (var i = 0; i < selectedCells.Count; i++)
            {
                var schedule = schedules.ElementAt(selectedCells[i].RowIndex);

                if (usedElements.Contains(schedule)) continue;
                schedule.ToCsv(folderDialog.SelectedPath);
                usedElements.Add(schedule);
            }
        }

        private void DgvSchedulesUserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            var rows = dgvSchedules.SelectedRows;
            foreach (var schedule in from DataGridViewRow row in rows select schedules.ElementAt(row.Index))
                DeleteScheduleFileInDataDirectory(schedule);
        }

        private void DeleteScheduleFileInDataDirectory(Schedule schedule)
        {
            var filename = Path.Combine(DATA_DIR,"Schedule" + (schedule.ScheduleTitle + "_" + schedule.ScheduleDates).GetSafeFileName() + ".csv");
            if (File.Exists(filename)) File.Delete(filename);

            dgvSchedules.Update();
            dgvSchedules.Refresh();
        }

        private void DeleteEventFromScheduleFile(Schedule schedule, int index)
        {
            var filename = Path.Combine(DATA_DIR, "Schedule" + (schedule.ScheduleTitle + "_" + schedule.ScheduleDates).GetSafeFileName() + ".csv");
            if (!File.Exists(filename)) return;
            schedule.Events.RemoveAt(index);
            schedule.ToCsv(DATA_DIR);
            dgvEvents.Update();
            dgvEvents.Refresh();
        }

        private void ImportActivities(string filename)
        {
            var newActivities = new BindingList<Activity>(Activity.FromCsvMulti(filename));
            foreach (var activity in newActivities) activities.Add(activity);
        }
    }
}
