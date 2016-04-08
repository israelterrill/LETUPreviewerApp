using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using ServiceStack.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;

namespace Preview_App
{
       public partial class Form1 : Form
        {

        BindingList<Schedule> schedules;
        BindingList<Activity> activities;
        public Form1()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(ActivityTab_Load);
            this.Load += new System.EventHandler(ScheduleTab_Load);
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
            this.dataGridView2.DataSource = getScheduleEntries();
            this.dataGridView2.AutoResizeColumns();
        }

        private BindingList<Activity> getActivityEntries()
        {
            JArray activitiesJson = JArray.Parse(File.ReadAllText(@"../../../../APIDataServer/Data/activities.json"));
            activities = activitiesJson.ToObject<BindingList<Activity>>();
            return activities;
        }

        private BindingList<Schedule> getScheduleEntries()
        {
            JArray schedulesJson = JArray.Parse(File.ReadAllText(@"../../../../APIDataServer/Data/schedule.json"));
            schedules = schedulesJson.ToObject<BindingList<Schedule>>();
            foreach (Schedule schedule in schedules)
                System.Diagnostics.Trace.WriteLine(schedule.Events[0]);
            return schedules;
        }


        private void button9_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            foreach (var schedule in schedules)
            {
                using (FileStream fs = (System.IO.FileStream)File.Create(@"../../../../DataFiles/" + schedule.ScheduleTitle + ".csv"))
                {
                    string myString = string.Format("{0}\n{1}\n", schedule.ScheduleTitle, schedule.ScheduleDates);
                    var byteString = myString.ToUtf8Bytes();
                    fs.Write(byteString, 0, byteString.Length);
                    CsvSerializer.SerializeToStream(schedules.ElementAt(0).Events, fs);
                }
            }
        }

        private void ClearSchedule ()
        {
            schedules = new BindingList<Schedule>();
        }

        private void ImportSchedule()
        {
            //Do not support entering commas in Excel!
            OpenFileDialog fileDialog = new OpenFileDialog();
            DialogResult result = fileDialog.ShowDialog();
            String title = null;
            String dates = null;
            BindingList<Event> events = new BindingList<Event>();
            if (result == DialogResult.OK)
            {
                using (FileStream fs = (System.IO.FileStream)fileDialog.OpenFile())
                {
                    TextFieldParser parser = new TextFieldParser(fs);

                    parser.HasFieldsEnclosedInQuotes = true;
                    parser.SetDelimiters(",");

                    title = parser.ReadLine();
                    dates = parser.ReadLine();

                    parser.ReadLine();

                    parser.Delimiters = new[] { "," };
                    parser.HasFieldsEnclosedInQuotes = true;
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

            }

            schedules.Add(new Schedule
            {
                ScheduleTitle = title,
                ScheduleDates = dates,
                Events = events
            });
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = schedules;
        }

        private void ExportSchedules()
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            DialogResult result = folderDialog.ShowDialog();
            System.Diagnostics.Trace.WriteLine(folderDialog.SelectedPath.ToString());
            if (result == DialogResult.OK) {
                foreach (var schedule in schedules) {
                    using (FileStream fs = (System.IO.FileStream)File.Create(folderDialog.SelectedPath +"\\" +  schedule.ScheduleTitle + ".csv"))
                    {
                        string myString = string.Format("{0}\n{1}\n", schedule.ScheduleTitle, schedule.ScheduleDates);
                        var byteString = myString.ToUtf8Bytes();
                        fs.Write(byteString, 0, byteString.Length);
                        CsvSerializer.SerializeToStream(schedule.Events, fs);
                    }
                }
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            //Activity Import
            OpenFileDialog fileDialog = new OpenFileDialog();
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                using (FileStream fs = (System.IO.FileStream)fileDialog.OpenFile())
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
                        String ImageLink =  (line.Length == 5)? line[4] : "";
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
            SaveFileDialog fileDialog = new SaveFileDialog();
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                using (FileStream fs = (System.IO.FileStream)fileDialog.OpenFile())
                {
                    CsvSerializer.SerializeToStream(activities, fs);
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.RemoveAt(item.Index);
            }

            dataGridView1.Update();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            foreach (DataGridViewRow item in this.dataGridView2.SelectedRows)
            {
                dataGridView2.Rows.RemoveAt(item.Index);
            }
        }

        private void dataGridView1_DefaultValuesNeeded(object sender, System.Windows.Forms.DataGridViewRowEventArgs e)
        {
            schedules.ElementAtOrDefault(e.Row.Index).Events = new BindingList<Event>();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.CommitEdit(DataGridViewDataErrorContexts.Commit);
            if (schedules.Last().Events == null)
                schedules.Last().Events = new BindingList<Event>();
            dataGridView4.CommitEdit(DataGridViewDataErrorContexts.Commit);
            if(e.RowIndex >= 0)
                dataGridView4.DataSource = schedules.ElementAt(e.RowIndex).Events;
            dataGridView4.Update();
            dataGridView4.Refresh();
        }

    }
}
