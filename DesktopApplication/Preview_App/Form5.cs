using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataClasses;

namespace Preview_App
{
    public partial class Form5 : Form
    {
        BindingList<Schedule> schedules;
        public Form5(BindingList<Schedule> schedules)
        {
            this.schedules = schedules;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            schedules.Add(
                new Schedule
                {
                    ScheduleTitle = titleBox.Text,
                    ScheduleDates = dateBox.Text,
                });
            BindingList<Event> list = new BindingList<Event>();
            Form4 addForm = new Form4(list);
            schedules.Last().Events = new List<Event>(list);
            addForm.ShowDialog();
            Close();

        }
    }
}
