using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Preview_App
{
    public partial class Form1 : Form
    {
        private BindingSource bindingSource1 = new BindingSource();

        public Form1()
        {
            InitializeComponent();
            //this.Load += new System.EventHandler();
        }

        private void loadCells()
        {
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
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
    }
}
