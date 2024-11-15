using prjGroupB.Views.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjGroupB.Views
{
    public partial class FrmHomePage : Form
    {
        public FrmHomePage()
        {
            InitializeComponent();
        }




        private void FrmHomePage_Load(object sender, EventArgs e)
        {
            displayUsersbySql();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            FrmDataEntry f = new FrmDataEntry();
            f.ShowDialog();

            if (f._isok == DialogResult.OK)
            {

            }

            //FrmDataEntry f = new FrmDataEntry();
            //f.ShowDialog();
            //if (f._isok == DialogResult.OK)
            //{
            //    (new CUserManager()).creatUser(f.UserEntry);
            //    MessageBox.Show("新增資料成功");
            //}
            //displayUsersbySql();
        }

        private void displayUsersbySql()
        {
            //連到tUser資料夾
            //SqlConnection con = new SqlConnection();
            //con.ConnectionString = @"Data Source=.;Initial Catalog=dbGroupB;Integrated Security=True;";
            //con.Open();

            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = con;
            //cmd.CommandText = sql;
            //cmd.ExecuteNonQuery();



        }



        private void CreateAccount_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {//設定按鈕，按去設定
            FrmDataEntry f =new FrmDataEntry();
            f.ShowDialog();

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
                    }

        

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            

        }
    }
}
