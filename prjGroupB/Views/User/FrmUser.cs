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
    public partial class FrmUser : Form
    {
        SqlDataAdapter _da;
        SqlCommandBuilder _builder;

        int _position = -1;//點到的位置


        public FrmUser()
        {
            InitializeComponent();
        }

        private void FrmUser_Load(object sender, EventArgs e)
        {
            displayUsersbySql("select * from tUser",false);
        }
        private void displayUsersbySql(string sql,bool isKeyWord)//顯示所有tUser的資料
        {
            //連到tUser資料夾
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbGroupB;Integrated Security=True;";
            con.Open();

            _da = new SqlDataAdapter(sql, con);
            if (isKeyWord)
            {
                _da.SelectCommand.Parameters.Add(new SqlParameter
                   ("K_Keyword", "%" + (object)toolStripTextBox1.Text + "%"));
            }

            _builder = new SqlCommandBuilder();
            _builder.DataAdapter = _da;

            DataSet ds = new DataSet();
            _da.Fill(ds);
            con.Close();

            dataGridView1.DataSource = ds.Tables[0];

            
        }
        private void dataGridView1_Paint(object sender, PaintEventArgs e)
        {
            resetGridStyle();
        }
        private void resetGridStyle()
        {
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 50;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 50;
            dataGridView1.Columns[4].Width = 100;
            dataGridView1.Columns[5].Width = 50;
            dataGridView1.Columns[6].Width = 100;
            dataGridView1.Columns[7].Width = 100;
            dataGridView1.Columns[8].Width = dataGridView1.Width - 50*6 -100 * 7 - dataGridView1.RowHeadersWidth;
            dataGridView1.Columns[9].Width = 100;
            dataGridView1.Columns[10].Width = 100;
            dataGridView1.Columns[11].Width = 100;
            dataGridView1.Columns[12].Width = 50;
            dataGridView1.Columns[13].Width = 50;


            bool isColorChanged = false;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                isColorChanged = !isColorChanged;

                row.DefaultCellStyle.Font = new Font("微軟正黑體", 14);//更改row中的字體
                row.DefaultCellStyle.BackColor = Color.WhiteSmoke;//背景顏色更改(使用內建color)
                if (isColorChanged)//如果是true就再次更改
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(220, 230, 230);//背景顏色更改(使用RGB)
                }
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            FrmUserEditor f = new FrmUserEditor();
            f.ShowDialog();
            if (f._isok == DialogResult.OK)
            {
                DataTable dt = dataGridView1.DataSource as DataTable;
                DataRow row = dt.NewRow();

                row["fUserId"] = f.user.fUserId;
                row["fUserRankId"] = f.user.fUserRankId;
                row["fUserName"] = f.user.fUserName;
                row["fUserNickName"] = f.user.fUserNickName;
                row["fUserSex"] = f.user.fUserSex;
                row["fUserBirthday"] = f.user.fUserBirthday;
                row["fUserPhone"] = f.user.fUserPhone;
                row["fUserEmail"] = f.user.fUserEmail;
                row["fUserAddress"] = f.user.fUserAddress;
                row["fUserComeDate"] = f.user.fUserComeDate;
                row["fUserPassword"] = f.user.fUserPassword;
                row["fUserNotify"] = f.user.fUserNotify;
                row["fUserOnLine"] = f.user.fUserOnLine;
                row["fUserImage"] = f.user.fUserImage;

                dt.Rows.Add(row);
            }
        }

        private void FrmUser_FormClosed(object sender, FormClosedEventArgs e)
        {
            _da.Update(dataGridView1.DataSource as DataTable);
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            _position = e.RowIndex;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_position < 0)
            { return; }
            DataTable dt = dataGridView1.DataSource as DataTable;
            DataRow row = dt.Rows[_position];
            row.Delete();
            _da.Update(dataGridView1.DataSource as DataTable);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (_position < 0)
            { return; }
            DataTable dt = dataGridView1.DataSource as DataTable;
            DataRow row = dt.Rows[_position];
            FrmUserEditor f = new FrmUserEditor();
            CUser x = new CUser();
            x.fUserId = Convert.ToInt32(row["fUserId"]);
            x.fUserRankId = Convert.ToInt32(row["fUserRankId"]);//下拉式選單(但沒問題
            x.fUserName = (string)row["fUserName"];
            if (row["fUserNickName"] != DBNull.Value)
            {
                x.fUserNickName = (string)row["fUserNickName"];
            }
            x.fUserSex = (string)row["fUserSex"];//下拉式選單
            x.fUserBirthday = Convert.ToDateTime(row["fUserBirthday"]);//日期選單
            x.fUserPhone = Convert.ToInt32(row["fUserPhone"]);
            x.fUserEmail = (string)row["fUserEmail"];
            x.fUserAddress = (string)row["fUserAddress"];
            x.fUserPassword = (string)row["fUserPassword"];
            x.fUserNotify = (bool)row["fUserNotify"];
            x.fUserOnLine = (bool)row["fUserOnLine"];

            if (row["fUserImage"] != DBNull.Value) { 
            x.fUserImage = (byte[])row["fUserImage"]; }

            f.user = x;
            f.ShowDialog();
            if (f._isok == DialogResult.OK)
            {
                row["fUserId"] = f.user.fUserId;
                row["fUserRankId"] = f.user.fUserRankId;
                row["fUserName"] = f.user.fUserName;
                row["fUserNickName"] = f.user.fUserNickName;
                row["fUserSex"] = f.user.fUserSex;
                row["fUserBirthday"] = f.user.fUserBirthday;
                row["fUserPhone"] = f.user.fUserPhone;
                row["fUserEmail"] = f.user.fUserEmail;
                row["fUserAddress"] = f.user.fUserAddress;
                row["fUserComeDate"] = f.user.fUserComeDate;
                row["fUserPassword"] = f.user.fUserPassword;
                row["fUserNotify"] = f.user.fUserNotify;
                row["fUserOnLine"] = f.user.fUserOnLine;
                row["fUserImage"] = f.user.fUserImage;


            }
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            string sql = "select * from tUser Where ";
            sql += "fUserName Like @K_Keyword";
            sql += " or fUserNickName Like @K_Keyword";
            sql += " or fUserRankId Like @K_Keyword";
            sql += " or fUserPhone Like @K_Keyword";
            sql += " or fUserEmail Like @K_Keyword";
            sql += " or fUserAddress Like @K_Keyword";

            displayUsersbySql(sql, true);
        }

        
    }
}
