﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjGroupB.Views
{
    public partial class FrmPostCategoryEditor : Form
    {
        private string _message;
        public FrmPostCategoryEditor()
        {
            InitializeComponent();
        }
        public DialogResult isOK { get; set; }
        public string message
        { 
            get { return txtCategory.Text; } 
            set { txtCategory.Text = value; }
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.isOK = DialogResult.OK;
            Close();
        }   
    }
}
