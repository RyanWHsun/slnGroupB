using prjGroupB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjGroupB.Views
{
    public partial class FrmLogin : Form
    {
        private bool _isClosed = true;
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            loginBox1.passwordMask = '*';
        }
        private void FrmLogin_Shown(object sender, EventArgs e)
        {
            btnOk.Focus();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM tUSER WHERE";
            sql += " fEmail = @K_FEmail";
            sql += " AND fPassword = @K_FPASSWORD";

            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbGroupB;Integrated Security=True";
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = sql;
            cmd.Parameters.Add(new SqlParameter("K_FEmail", (object)loginBox1.loginAccount));
            cmd.Parameters.Add(new SqlParameter("K_FPASSWORD", (object)loginBox1.loginPassword));
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                CUserSession.fUserId = Convert.ToInt32(reader["fUserId"]);
                _isClosed = false;
                Close();
            }
            else
                MessageBox.Show("帳號與密碼不符");
        }

        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = _isClosed;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _isClosed = false;
            Application.Exit();
        }
    }
}
