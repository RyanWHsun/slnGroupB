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
    public delegate void D(bool isVisible);
    public partial class FrmLogin : Form
    {
        public string pipe = "np:\\\\.\\pipe\\LOCALDB#B5FE6A17\\tsql\\query;";

        public event D DshowBackend;
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
            sql += " fUserEmail = @K_FEmail";
            sql += " AND fUserPassword = @K_FPASSWORD";

            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
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
                CUserSession.fRankId = Convert.ToInt32(reader["fUserRankId"]);
                _isClosed = false;
                if (CUserSession.fRankId == 99)
                    DshowBackend(true);
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
