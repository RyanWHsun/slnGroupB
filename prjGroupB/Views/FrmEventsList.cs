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
    public partial class FrmEventsList : Form
    {
        public FrmEventsList()
        {
            InitializeComponent();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FrmEventsEditor f = new FrmEventsEditor();
            f.ShowDialog();
            if (f.IsOk == DialogResult.OK)
            { 
                DataTable dt=dataGridView1.DataSource as DataTable;
                DataRow row=dt.NewRow();
                row["fEventName"] = f.Event.fEventName;
                row["fEventDescription"] = f.Event.fEventDescription;
                row["fEventStartDate"]=f.Event.fEventStartDate;
                row["fEventEndDate"]=f.Event.fEventEndDate;
                row["fEventLocation"]=f.Event.fEventLocation;
                row["fEventCreatedDate"]=f.Event.fEventCreatedDate;
                row["fEventUpdatedDate"]=f.Event.fEventUpdatedDate;
                row["fEventActivityfee"] = f.Event.fEventActivityfee;
                row["fEventURL"] = f.Event.fEventURL;
            }
        }
    }
}
