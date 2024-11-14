using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace prjGroupB.Views
{
    public partial class LoginBox : UserControl
    {
        public LoginBox()
        {
            InitializeComponent();
        }
        public string loginAccount
        {
            get { return txtAccount.Text; }
            set { txtAccount.Text = value; }
        }
        public string loginPassword
        {
            get { return txtPassword.Text; }
            set { txtPassword.Text = value; }
        }
        public char passwordMask
        {
            get { return txtPassword.PasswordChar; }
            set { txtPassword.PasswordChar = value; }
        }
        private void txtAccount_Enter(object sender, EventArgs e)
        {
            lblAccountHint.Visible = false;
        }

        private void txtAccount_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(loginAccount))
            {
                lblAccountHint.Visible = true;
            }
            else
            {
                lblAccountHint.Visible = false;
            }
        }  
    }
}
