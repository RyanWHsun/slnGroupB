using prjGroupB.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjGroupB
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        private void FrmMain_Load(object sender, EventArgs e)
        {
            login();
        }

        private void login()
        {
            showBackend(false);
            FrmLogin f = new FrmLogin();
            f.TopMost = true;
            f.DshowBackend += this.showBackend;
            f.ShowDialog();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            FrmUsers f = new FrmUsers();
            f.MdiParent = this;
            f.Show();
        }

        private void btnPosts_Click(object sender, EventArgs e)
        {
            FrmPosts f = new FrmPosts();
            f.MdiParent = this;
            f.Show();
        }

        private void btnAttractions_Click(object sender, EventArgs e)
        {
            FrmAttractions f = new FrmAttractions();
            f.MdiParent = this;
            f.Show();
        }

        private void btnEvents_Click(object sender, EventArgs e)
        {
            FrmEvents f = new FrmEvents();
            f.MdiParent = this;
            f.Show();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            FrmProducts f = new FrmProducts();
            f.MdiParent = this;
            f.Show();
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            FrmOrders f = new FrmOrders();
            f.MdiParent = this;
            f.Show();
        }

        private void btnUserPosts_Click(object sender, EventArgs e)
        {
            FrmUserPosts f = new FrmUserPosts();
            f.MdiParent = this;
            f.Show();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            while(this.ActiveMdiChild != null)
                this.ActiveMdiChild.Close();
            login();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
                this.ActiveMdiChild.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void showBackend(bool isVisible)
        {
            btnUsers.Visible = isVisible;
            btnPosts.Visible = isVisible;
            btnAttractions.Visible = isVisible;
            btnEvents.Visible = isVisible;
            btnProducts.Visible = isVisible;
            btnOrders.Visible = isVisible;
        }
    }
}
