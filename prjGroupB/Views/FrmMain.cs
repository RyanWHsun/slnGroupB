using Attractions.Views;
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
            isPnlVisiable(false);
            FrmUser f = new FrmUser();
            f.MdiParent = this;
            f.Show();
        }

        private void btnPosts_Click(object sender, EventArgs e)
        {
            isPnlVisiable(false);
            FrmPosts f = new FrmPosts();
            f.MdiParent = this;
            f.Show();
        }

        private void btnAttractions_Click(object sender, EventArgs e)
        {
            isPnlVisiable(false);
            FormAttractionMain f = new FormAttractionMain();
            f.MdiParent = this;
            f.Show();
        }

        private void btnEvents_Click(object sender, EventArgs e)
        {
            isPnlVisiable(false);
            FrmEvents f = new FrmEvents();
            f.MdiParent = this;
            f.Show();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            isPnlVisiable(false);
            FrmProducts f = new FrmProducts();
            f.MdiParent = this;
            f.Show();
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            isPnlVisiable(false);
            FrmOrders f = new FrmOrders();
            f.MdiParent = this;
            f.Show();
        }

        private void btnUserPosts_Click(object sender, EventArgs e)
        {
            isPnlVisiable(false);
            FrmUserPosts f = new FrmUserPosts();
            f.MdiParent = this;
            f.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            while (this.ActiveMdiChild != null)
                this.ActiveMdiChild.Close();
            isPnlVisiable(true);
            login();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
            {
                this.ActiveMdiChild.Close();
                isPnlVisiable(false);
            }
            if (this.MdiChildren.Length == 0)
                isPnlVisiable(true);
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

        private void btnRegister_Click(object sender, EventArgs e)
        {
            isPnlVisiable(false);
            FrmEventRegistrationForm registrationForm = new FrmEventRegistrationForm();
            registrationForm.ShowDialog();
        }

        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            isPnlVisiable(false);
            FrmPlaceOrder f = new FrmPlaceOrder();
            f.MdiParent = this;
            f.Show();
        }
        private void isPnlVisiable(bool isVisiable)
        {
            pnlMain.Visible = isVisiable;
        }
    }
}