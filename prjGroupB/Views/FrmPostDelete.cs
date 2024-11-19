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
    public partial class FrmPostDelete : Form
    {
        public FrmPostDelete()
        {
            InitializeComponent();
        }
        public DialogResult isOK { get; set; }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.isOK = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
