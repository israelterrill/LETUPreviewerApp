using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Preview_App
{
       public partial class Form1 : Form
        {

        List<Schedule> schedules;
        List<Activity> activities;
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

        private List<Activity> getActivityEntries()
        {
            JArray activitiesJson = JArray.Parse(File.ReadAllText(@"../../../../APIDataServer/Data/activities.json"));
            activities = activitiesJson.ToObject<List<Activity>>();
            return activities;
        }

        private List<Schedule> getScheduleEntries()
        {
            JArray schedulesJson = JArray.Parse(File.ReadAllText(@"../../../../APIDataServer/Data/schedule.json"));
            schedules = schedulesJson.ToObject<List<Schedule>>();
            foreach (Schedule schedule in schedules)
                System.Diagnostics.Trace.WriteLine(schedule.Events[0]);
            return schedules;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form2 addForm = new Form2();
            addForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 addForm = new Form2();
            addForm.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Add modify activity code here for activities
            //Edit
        }

        private void dataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //Add view/modify code here for schedule
            //another form 
            //Shows dates
            //Shows Schedule title
            //Shows Events (list)
        }
    }
}
