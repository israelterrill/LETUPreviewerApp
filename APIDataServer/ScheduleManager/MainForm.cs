using System;
using System.Collections.Generic;
using ServiceStack.Text;
using System.ComponentModel;
using System.Data;
using System.IO;
using DataClasses;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace APIDataServer
{
    public partial class MainForm : Form
    {
#if DEBUG
        private const string DATA_DIR = @"..\..\..\Install\Data\";
#else
        private const string DATA_DIR = @"Data\";
#endif

        BindingList<Schedule> schedules = new BindingList<Schedule>();
        BindingList<Activity> activities;
        public MainForm()
        {
            InitializeComponent();
            this.activities = new BindingList<Activity>();
            this.Load += new EventHandler(ActivityTab_Load);
            this.Load += new EventHandler(ScheduleTab_Load);
            dgvSchedules.UserDeletingRow += DgvSchedulesUserDeletingRow;
        }

        private void ActivityTab_Load(object sender, EventArgs e)
        {
            this.dgvActivities.DataSource = getActivityEntries();
            this.dgvActivities.AutoResizeColumns();
            //dgvActivities.Columns[0].DisplayIndex = 4;
            //dgvActivities.Columns[1].DisplayIndex = 0;
            //dgvActivities.Columns[2].DisplayIndex = 1;
            //dgvActivities.Columns[3].DisplayIndex = 2;
            //dgvActivities.Columns[4].DisplayIndex = 3;
        }

        private void ScheduleTab_Load(object sender, EventArgs e)
        {
            getScheduleEntriesFromDataFolder();
            this.dgvSchedules.DataSource = schedules;
            if (schedules.Count > 0)
                this.dgvEvents.DataSource = schedules.First().Events;
            this.dgvSchedules.AutoResizeColumns();
        }

        private BindingList<Activity> getActivityEntries()
        {
            string filename = DATA_DIR + "activities.csv";
            var fullPath = Path.GetFullPath(filename);
            if (File.Exists(fullPath))
            {
                ImportActivities(fullPath);
            }
            return activities;
        }

        private void getScheduleEntriesFromDataFolder()
        {
            if (!Directory.Exists(DATA_DIR)) return;
            String[] files = Directory.GetFiles(DATA_DIR);
            List<string> fileNames = new List<string>(files);
            var rgxFileName = new Regex(DataClasses.Schedule.FILE_PATTERN);
            var filteredFileNames = fileNames.Where(fileName => rgxFileName.IsMatch(Path.GetFileName(fileName)));

            foreach (String fileName in filteredFileNames)
            {
                schedules.Add(DataClasses.Schedule.FromCsv(Path.GetFullPath(fileName)));
            }
        }


        private void btnSaveActivities_Click(object sender, EventArgs e)
        {
            SaveActivities();
        }

        private void btnSaveSchedules_Click(object sender, EventArgs e)
        {
            SaveSchedules();
        }

        private void SaveActivities()
        {
            using (FileStream fs = (FileStream)File.Create(DATA_DIR + "Data_Activities.csv"))
            {
                CsvSerializer.SerializeToStream(activities, fs);
            }
        }

        private void SaveSchedules()
        {
            foreach (var schedule in schedules)
            {
                string fullPath = Path.GetFullPath(DATA_DIR);
                schedule.ToCsv(fullPath);
            }
        }

        private void ClearSchedule()
        {
            schedules = new BindingList<Schedule>();
        }

        private void ImportSchedule()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            DialogResult result = fileDialog.ShowDialog();
            BindingList<Event> events = new BindingList<Event>();
            if (result == DialogResult.OK)
            {
                foreach (var filename in fileDialog.FileNames)
                {
                    schedules.Add(DataClasses.Schedule.FromCsv(filename));
                }

                dgvSchedules.DataSource = schedules;
                dgvSchedules.Update();
                dgvSchedules.Refresh();
                if (schedules.Count > 0)
                {
                    dgvEvents.DataSource = schedules.First().Events;
                    dgvEvents.Update();
                    dgvEvents.Refresh();
                }
            }
        }

        public static string GetSafeFilename(string filename)
        {

            return string.Join("-", filename.Split(Path.GetInvalidFileNameChars()));

        }

        private void ExportSchedules()
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            DialogResult result = folderDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                foreach (var schedule in schedules)
                {
                    schedule.ToCsv(folderDialog.SelectedPath);
                }
            }
        }


        private void btnImportActivity_Click(object sender, EventArgs e)
        {
            //Activity Import Button
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                foreach (var filename in fileDialog.FileNames)
                {
                    ImportActivities(filename);
                }
                dgvActivities.Update();
                dgvActivities.Refresh();
            }
        }

        private void btnExportSchedules_Click(object sender, EventArgs e)
        {
            ExportSchedules();
        }

        private void btnImportSchedule_Click(object sender, EventArgs e)
        {
            ImportSchedule();
        }

        private void btnExportActivity_Click(object sender, EventArgs e)
        {
            //Activity Export
            SaveFileDialog fileDialog = new SaveFileDialog();
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                using (FileStream fs = (FileStream)fileDialog.OpenFile())
                {
                    CsvSerializer.SerializeToStream(activities, fs);
                }
            }
        }

        private void btnRemoveActivity_Click(object sender, EventArgs e)
        {
            //Remove Activities
            foreach (DataGridViewRow item in this.dgvActivities.SelectedRows)
            {
                dgvActivities.Rows.RemoveAt(item.Index);
            }

            dgvActivities.Update();
            dgvActivities.Refresh();
        }

        private void btnRemoveSchedule_Click(object sender, EventArgs e)
        {

            if (schedules.Count > 0)
            {
                foreach (DataGridViewRow item in this.dgvSchedules.SelectedRows)
                {
                    Schedule schedule = schedules.ElementAt(item.Index);
                    DeleteScheduleFileInDataDirectory(schedule);
                    dgvSchedules.Rows.RemoveAt(item.Index);
                }

                dgvActivities.Update();
                dgvActivities.Refresh();
            }
        }

        private void dgvSchedules_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (schedules.Count > 0)
            {
                if (e.RowIndex >= 0)
                {
                    dgvEvents.DataSource = schedules.ElementAt(e.RowIndex).Events;
                    dgvEvents.Update();
                    dgvEvents.Refresh();
                }
            }
        }

        private void btnExportSelectedSchedules_Click(object sender, EventArgs e)
        {
            //Export Currently Selected
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            DialogResult result = folderDialog.ShowDialog();
            if (result == DialogResult.OK && schedules.Count > 0)
            {
                var selectedCells = dgvSchedules.SelectedCells;
                List<Schedule> usedElements = new List<Schedule>();
                for (int i = 0; i < selectedCells.Count; i++)
                {
                    var schedule = schedules.ElementAt(selectedCells[i].RowIndex);

                    if (!usedElements.Contains(schedule))
                    {
                        schedule.ToCsv(folderDialog.SelectedPath);
                        usedElements.Add(schedule);
                    }
                }
            }
        }

        private void DgvSchedulesUserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            var rows = dgvSchedules.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                var schedule = schedules.ElementAt(row.Index);
                DeleteScheduleFileInDataDirectory(schedule);
            }
        }

        private void DeleteScheduleFileInDataDirectory(Schedule schedule)
        {
            string filename = DATA_DIR + "tabControl" + GetSafeFilename(schedule.ScheduleTitle + "_" + schedule.ScheduleDates) + ".csv";
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }

            dgvSchedules.Update();
            dgvSchedules.Refresh();
        }

        private void ImportActivities(string filename)
        {
            BindingList<Activity> newActivities = new BindingList<Activity>(Activity.FromCsvMulti(filename));
            foreach (var activity in newActivities)
            {
                activities.Add(activity);
            }
        }
    }
}
