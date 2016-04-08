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
    public partial class Form4 : Form
    {
        BindingList<Event> events;

        public Form4(BindingList<Event> events)
        {
            InitializeComponent();
            this.events = events;
            dataGridView2.DataSource = this.events;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in this.dataGridView2.SelectedRows)
            {
                dataGridView2.Rows.RemoveAt(item.Index);
            }
            dataGridView2.Update();

        }

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form6 createEventDialog = new Form6(events);
            createEventDialog.ShowDialog();
        }
    }
}
