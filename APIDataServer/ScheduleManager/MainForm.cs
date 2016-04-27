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

        private readonly Database Database;

        private bool eventSelected;

        public MainForm()
        {
            InitializeComponent();

            Database = new Database(DATA_DIR);

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
            dgvActivities.DataSource = Database.Activities;
            dgvActivities.AutoResizeColumns();
        }

        private void ScheduleTab_Load(object sender, EventArgs e)
        {
            dgvSchedules.DataSource = Database.Schedules;
            if (Database.Schedules.Count > 0) dgvEvents.DataSource = Database.Schedules.First().Events;
            dgvSchedules.AutoResizeColumns();
        }

        private void SaveActivities()
        {
            using (var fs = File.Create(ACTIVITIES_PATH)) CsvSerializer.SerializeToStream(Database.Activities, fs);
        }

        private void SaveSchedules()
        {
            foreach (var schedule in Database.Schedules)
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
                Database.Schedules.Add(Schedule.FromCsv(filename));

            dgvSchedules.DataSource = Database.Schedules;
            dgvSchedules.Update();
            dgvSchedules.Refresh();
            if (Database.Schedules.Count <= 0) return;
            dgvEvents.DataSource = Database.Schedules.First().Events;
            dgvEvents.Update();
            dgvEvents.Refresh();
        }

        private void ExportSchedules()
        {
            var folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() != DialogResult.OK) return;
            foreach (var schedule in Database.Schedules) schedule.ToCsv(folderDialog.SelectedPath);
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
            using (var fs = (FileStream)fileDialog.OpenFile()) CsvSerializer.SerializeToStream(Database.Activities, fs);
        }

        private void btnRemoveActivity_Click(object sender, EventArgs e)
        {
            //Remove Activities
            foreach (var item in dgvActivities.SelectedRows.Cast<DataGridViewRow>().OrderByDescending(r => r.Index)) 
                dgvActivities.Rows.RemoveAt(item.Index);

            dgvActivities.Update();
            dgvActivities.Refresh();
        }

        private void btnRemoveSchedule_Click(object sender, EventArgs e)
        {
            if (Database.Schedules.Count <= 0) return;
            if (eventSelected)
            {
                var schedule = Database.Schedules.ElementAt(dgvSchedules.SelectedRows.Cast<DataGridViewRow>().First().Index);
                foreach (var item in dgvEvents.SelectedRows.Cast<DataGridViewRow>().OrderByDescending(r=>r.Index))
                {
                    DeleteEventFromScheduleFile(schedule, item.Index);
                }
                dgvEvents.Update();
                dgvEvents.Refresh();
            }
            else
            {
                foreach (var item in dgvSchedules.SelectedRows.Cast<DataGridViewRow>().OrderByDescending(r=>r.Index))
                {
                    var schedule = Database.Schedules.ElementAt(item.Index);
                    DeleteScheduleFileInDataDirectory(schedule);
                    dgvSchedules.Rows.RemoveAt(item.Index);
                }
                dgvSchedules.Update();
                dgvSchedules.Refresh();
            }
        }

        private void dgvSchedules_SelectionChanged(object sender, EventArgs e)
        {
            if (Database.Schedules.Count <= 0 || dgvSchedules.SelectedRows.Count <= 0) return;
            dgvEvents.DataSource =
                Database.Schedules.ElementAt(dgvSchedules.SelectedRows.Cast<DataGridViewRow>().First().Index).Events;
            dgvEvents.Update();
            dgvEvents.Refresh();
            eventSelected = false;
        }

        void dgvEvents_SelectionChanged(object sender, EventArgs e)
        {
            if (Database.Schedules.Count <= 0 || dgvSchedules.SelectedRows.Count <= 0) return;
            var selectedSchedule = Database.Schedules.ElementAt(dgvSchedules.SelectedRows.Cast<DataGridViewRow>().First().Index);
            if (selectedSchedule.Events.Count <= 0 || dgvEvents.SelectedRows.Count <= 0) return;
            eventSelected = true;
        }

        private void btnExportSelectedSchedules_Click(object sender, EventArgs e)
        {
            //Export Currently Selected
            var folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() != DialogResult.OK || Database.Schedules.Count <= 0) return;
            var selectedCells = dgvSchedules.SelectedCells;
            var usedElements = new List<Schedule>();
            for (var i = 0; i < selectedCells.Count; i++)
            {
                var schedule = Database.Schedules.ElementAt(selectedCells[i].RowIndex);

                if (usedElements.Contains(schedule)) continue;
                schedule.ToCsv(folderDialog.SelectedPath);
                usedElements.Add(schedule);
            }
        }

        private void DgvSchedulesUserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            var rows = dgvSchedules.SelectedRows;
            foreach (var schedule in from DataGridViewRow row in rows select Database.Schedules.ElementAt(row.Index))
                DeleteScheduleFileInDataDirectory(schedule);
        }

        private void DeleteScheduleFileInDataDirectory(Schedule schedule)
        {
            var filename = Path.Combine(DATA_DIR, schedule.FileName + ".csv");
            if (File.Exists(filename)) File.Delete(filename);

            dgvSchedules.Update();
            dgvSchedules.Refresh();
        }

        private void DeleteEventFromScheduleFile(Schedule schedule, int index)
        {
            var filename = Path.Combine(DATA_DIR, schedule.FileName + ".csv");
            if (!File.Exists(filename)) return;
            schedule.Events.RemoveAt(index);
            schedule.ToCsv(DATA_DIR);
            dgvEvents.Update();
            dgvEvents.Refresh();
        }

        private void ImportActivities(string filename)
        {
            var newActivities = new BindingList<Activity>(Activity.FromCsvMulti(filename));
            foreach (var activity in newActivities) Database.Activities.Add(activity);
        }

        private void btnClearActivities_Click(object sender, EventArgs e)
        {
            dgvActivities.Rows.Clear();
            dgvActivities.Update();
            dgvActivities.Refresh();
        }

        private void btnClearSchedule_Click(object sender, EventArgs e)
        {
            if (eventSelected)
            {
                var schedule = Database.Schedules.ElementAt(dgvSchedules.SelectedRows.Cast<DataGridViewRow>().First().Index);
                schedule.Events.Clear();
                schedule.ToCsv(DATA_DIR);
                dgvEvents.Update();
                dgvEvents.Refresh();
            }
            else if (
                MessageBox.Show(
                    @"Are you sure you want to remove all AllSchedules? This will delete the associated files as well.",
                    @"Confirm Clear Selections", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
            {
                dgvSchedules.Rows.Clear();
                foreach (
                    var filePath in
                        Directory.GetFiles(DATA_DIR)
                            .Where(fi => Regex.IsMatch(Path.GetFileName(fi), Schedule.FILE_PATTERN)))
                    File.Delete(filePath);
                dgvEvents.DataSource = null;
            }
        }
    }
}
