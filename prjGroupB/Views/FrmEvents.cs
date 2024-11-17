using System;
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
    public partial class FrmEvents : Form
    {
        public FrmEvents()
        {
            InitializeComponent();
        }

        private void showEventsSystem()
        {
            FrmEventsList f = new FrmEventsList();
            f.ShowDialog();
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            showEventsSystem();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            FrmEventCategoriesList f = new FrmEventCategoriesList();
            f.ShowDialog();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            FrmEventRegistrationinformation f = new FrmEventRegistrationinformation();
            f.ShowDialog();
        }
    }
}