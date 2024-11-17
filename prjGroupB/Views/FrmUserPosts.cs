using prjGroupB.Models;
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
    public partial class FrmUserPosts : Form
    {
        public FrmUserPosts()
        {
            InitializeComponent();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            FrmPostEditor f = new FrmPostEditor();
            f.ShowDialog();
            if (f.isOK == DialogResult.OK)
            {
                (new CPostManager()).insert(f.post);
            }
        }

        private void btnInsertCategory_Click(object sender, EventArgs e)
        {
            FrmPostCategoryEditor f = new FrmPostCategoryEditor();
            f.ShowDialog();
            if (f.isOK == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(f.message))
                    return;
                (new CPostManager()).insertCategory(f.message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
