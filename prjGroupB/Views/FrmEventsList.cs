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
                row["fEventName"] = f.Events.fEventName;
                row["fEventDescription"] = f.Events.fEventDescription;
                row["fEventStartDate"]=f.Events.fEventStartDate;
                row["fEventEndDate"]=f.Events.fEventEndDate;
                row["fEventLocation"]=f.Events.fEventLocation;
                row["fEventCreatedDate"]=f.Events.fEventCreatedDate;
                row["fEventUpdatedDate"]=f.Events.fEventUpdatedDate;
                row["fEventActivityfee"] = f.Events.fEventActivityfee;
                row["fEventURL"] = f.Events.fEventURL;
            }
        }
    }
}
