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
    public partial class Form1 : Form
    {
#if DEBUG
        private const string DATA_DIR = @"..\..\..\Install\Data\";
#else
        private const string DATA_DIR = @"Data\";
#endif

        BindingList<Schedule> schedules = new BindingList<Schedule>();
        BindingList<Activity> activities;
        public Form1()
        {
            InitializeComponent();
            this.activities = new BindingList<Activity>();
            this.Load += new EventHandler(ActivityTab_Load);
            this.Load += new EventHandler(ScheduleTab_Load);
            dataGridView2.UserDeletingRow += DataGridView2_UserDeletingRow;
        }

        private void ActivityTab_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = getActivityEntries();
            this.dataGridView1.AutoResizeColumns();
            dataGridView1.Columns[0].DisplayIndex = 4;
            dataGridView1.Columns[1].DisplayIndex = 0;
            dataGridView1.Columns[2].DisplayIndex = 1;
            dataGridView1.Columns[3].DisplayIndex = 2;
            dataGridView1.Columns[4].DisplayIndex = 3;
        }

        private void ScheduleTab_Load(object sender, EventArgs e)
        {
            getScheduleEntriesFromDataFolder();
            this.dataGridView2.DataSource = schedules;
            if (schedules.Count > 0)
                this.dataGridView4.DataSource = schedules.First().Events;
            this.dataGridView2.AutoResizeColumns();
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


        private void button9_Click(object sender, EventArgs e)
        {
            SaveActivities();
        }

        private void button10_Click(object sender, EventArgs e)
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

                dataGridView2.DataSource = schedules;
                dataGridView2.Update();
                dataGridView2.Refresh();
                if (schedules.Count > 0)
                {
                    dataGridView4.DataSource = schedules.First().Events;
                    dataGridView4.Update();
                    dataGridView4.Refresh();
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


        private void button4_Click(object sender, EventArgs e)
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
                dataGridView1.Update();
                dataGridView1.Refresh();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ExportSchedules();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ImportSchedule();
        }

        private void button6_Click(object sender, EventArgs e)
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

        private void button8_Click(object sender, EventArgs e)
        {
            //Remove Activities
            foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.RemoveAt(item.Index);
            }

            dataGridView1.Update();
            dataGridView1.Refresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (schedules.Count > 0)
            {
                foreach (DataGridViewRow item in this.dataGridView2.SelectedRows)
                {
                    Schedule schedule = schedules.ElementAt(item.Index);
                    DeleteScheduleFileInDataDirectory(schedule);
                    dataGridView2.Rows.RemoveAt(item.Index);
                }

                dataGridView1.Update();
                dataGridView1.Refresh();
            }
        }

        private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            schedules.ElementAtOrDefault(e.Row.Index).Events = new BindingList<Event>();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (schedules.Count > 0)
            {
                if (e.RowIndex >= 0)
                {
                    dataGridView4.DataSource = schedules.ElementAt(e.RowIndex).Events;
                    dataGridView4.Update();
                    dataGridView4.Refresh();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Export Currently Selected
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            DialogResult result = folderDialog.ShowDialog();
            if (result == DialogResult.OK && schedules.Count > 0)
            {
                var selectedCells = dataGridView2.SelectedCells;
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

        private void DataGridView2_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            var rows = dataGridView2.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                var schedule = schedules.ElementAt(row.Index);
                DeleteScheduleFileInDataDirectory(schedule);
            }
        }

        private void DeleteScheduleFileInDataDirectory(Schedule schedule)
        {
            string filename = DATA_DIR + "Schedule" + GetSafeFilename(schedule.ScheduleTitle + "_" + schedule.ScheduleDates) + ".csv";
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }

            dataGridView2.Update();
            dataGridView2.Refresh();
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
