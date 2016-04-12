﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using ServiceStack.Text;
using System.ComponentModel;
using System.Data;
using System.IO;
using DataClasses;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
using System.Text.RegularExpressions;


namespace Preview_App
{
       public partial class Form1 : Form
        {
            private string DATA_DIRECTORY = @"../../../../APIDataServer/Data/";
            public const string FILE_PATTERN = @"^Schedule(?<title>[^_]+)_(?<dates>.*)\.csv$";

        BindingList<Schedule> schedules;
            BindingList<Activity> activities; 
            public Form1()
            {
                InitializeComponent();
                this.activities = new BindingList<Activity>();
                this.Load += new System.EventHandler(ActivityTab_Load);
                this.Load += new System.EventHandler(ScheduleTab_Load);
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
                this.dataGridView2.DataSource = getScheduleEntries();
                this.dataGridView2.AutoResizeColumns();
            }

            private BindingList<Activity> getActivityEntries()
            {
                string filename = DATA_DIRECTORY + "Data_Activities.csv";
                if (File.Exists(filename))
                {
                    ImportActivities(filename);
                }
                return activities;
            }

            private BindingList<Schedule> getScheduleEntries()
            {
            //ADD  a GET for all schedules in data directory--need regex to get files in directory
            
            JArray schedulesJson = JArray.Parse(File.ReadAllText(DATA_DIRECTORY + "schedule.json"));
                schedules = schedulesJson.ToObject<BindingList<Schedule>>();
                return schedules;
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
                using (FileStream fs = (FileStream)File.Create(DATA_DIRECTORY + "Data_Activities.csv"))
                {
                    CsvSerializer.SerializeToStream(activities, fs);
                }
            }

            private void SaveSchedules()
            {
                foreach (var schedule in schedules)
                {
                    using (FileStream fs = (FileStream)File.Create(DATA_DIRECTORY + "Schedule_" + GetSafeFilename(schedule.ScheduleTitle + "_" + schedule.ScheduleDates) +".csv"))
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
                fileDialog.Multiselect = true;
                DialogResult result = fileDialog.ShowDialog();
                String title = null;
                String dates = null;
                BindingList<Event> events = new BindingList<Event>();
                if (result == DialogResult.OK)
                {
                    foreach (var filename in fileDialog.FileNames)
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

                        var fileParts = fileDialog.FileName.Split('_');
                        schedules.Add(new Schedule
                        {
                            ScheduleTitle = Path.GetFileName(fileParts[0]),
                            ScheduleDates = Path.GetFileNameWithoutExtension(fileParts[1]),
                            Events = events
                        });
                        dataGridView2.DataSource = null;
                        dataGridView2.DataSource = schedules;
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

                if (result == DialogResult.OK) {
                    foreach (var schedule in schedules) {
                        string filename = folderDialog.SelectedPath +"\\Schedule_" + GetSafeFilename(schedule.ScheduleTitle + "_" + schedule.ScheduleDates) + ".csv";
                        using (FileStream fs = (FileStream)File.Create(filename))
                        {
                            CsvSerializer.SerializeToStream(schedule.Events, fs);
                        }
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

                foreach (DataGridViewRow item in this.dataGridView2.SelectedRows)
                {
                    Schedule schedule = schedules.ElementAt(item.Index);
                    DeleteScheduleFileInDataDirectory(schedule);
                    dataGridView2.Rows.RemoveAt(item.Index);
                }

                dataGridView1.Update();
                dataGridView1.Refresh();
            }

            private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
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

            private void button3_Click(object sender, EventArgs e)
            {
                //Export Currently Selected
                FolderBrowserDialog folderDialog = new FolderBrowserDialog();
                DialogResult result = folderDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    var selectedCells = dataGridView2.SelectedCells;
                    List<Schedule> usedElements = new List<Schedule>();
                    for (int i = 0; i < selectedCells.Count; i++)
                    {
                        var schedule = schedules.ElementAt(selectedCells[i].RowIndex);

                        if (!usedElements.Contains(schedule))
                        {
                            string filename = folderDialog.SelectedPath +"\\Schedule_" + GetSafeFilename(schedule.ScheduleTitle + "_" + schedule.ScheduleDates) + ".csv";
                            using (FileStream fs = (FileStream)File.Create(filename))
                            {
                                CsvSerializer.SerializeToStream(schedule.Events, fs);
                            }
                            usedElements.Add(schedule);
                        }
                    }
                }
            }

            private void DataGridView2_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
            {
                var rows = dataGridView2.SelectedRows;
                foreach (DataGridViewRow row in rows) {
                    var schedule = schedules.ElementAt(row.Index);
                    DeleteScheduleFileInDataDirectory(schedule);
                }
            }

            private void DeleteScheduleFileInDataDirectory (Schedule schedule)
            {
                string filename = DATA_DIRECTORY + "Schedule_" + GetSafeFilename(schedule.ScheduleTitle + "_" + schedule.ScheduleDates) + ".csv";
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }

                dataGridView2.Update();
                dataGridView2.Refresh();
            }

            private void ImportActivities(String filename)
            {
                    using (FileStream fs = File.OpenRead(filename))
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
                            String ImageLink = (line.Length == 5) ? line[4] : "";
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
}
