﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataClasses;
using System.Windows.Forms;

namespace Preview_App
{
    public partial class Form2 : Form
    {
        BindingList<Activity> activities;
        public Form2(BindingList<Activity> activities)
        {
            this.activities = activities;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            activities.Add(
                new Activity
                {
                    Title = titleBox.Text,
                    Date = dateBox.Text,
                    Description = descriptionBox.Text,
                    ImageLink = imageLinkBox.Text
                });
            Close();
        }

    }
}
