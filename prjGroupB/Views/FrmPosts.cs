using prjGroupB.Models;
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
    public partial class FrmPosts : Form
    {
        SqlDataAdapter _da;
        SqlCommandBuilder _builder;
        DataSet _ds = null;
        private int _position = -1;
        private string _connectionString = @"Data Source=.;Initial Catalog=dbGroupB;Integrated Security=True";
        public FrmPosts()
        {
            InitializeComponent();
        }
        private void FrmPosts_Load(object sender, EventArgs e)
        {
            displayRoomBySql("SELECT * FROM tPosts", false);
        }
        private void displayRoomBySql(string sql, bool isKeyword)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = _connectionString;
            con.Open();
            _da = new SqlDataAdapter(sql, con);
            if (isKeyword)
            {
                _da.SelectCommand.Parameters.Add(new SqlParameter("K_KEYWORD", (object)("%" + txtKeyword.Text + "%")));
            }
            _builder = new SqlCommandBuilder();
            _builder.DataAdapter = _da;

            _ds = new DataSet();
            _da.Fill(_ds);
            con.Close();
            dataGridView1.DataSource = _ds.Tables[0];
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            _da.Update(dataGridView1.DataSource as DataTable);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (_position < 0)
                return;
            DataTable dt = dataGridView1.DataSource as DataTable;
            DataRow row = dt.Rows[_position];
            string postId = row["fpostId"].ToString();
            row.Delete();

            string sql = "SELECT * FROM tPostImages WHERE fPostId = " + postId;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = _connectionString;
            con.Open();
            SqlDataAdapter daPostImages = new SqlDataAdapter(sql, con);
            SqlCommandBuilder builderPostImages = new SqlCommandBuilder();
            builderPostImages.DataAdapter = daPostImages;
            DataSet ds = new DataSet();
            daPostImages.Fill(ds);
            con.Close();
            DataTable dtPostImages = ds.Tables[0];
            foreach(DataRow rowPostImages in dtPostImages.Rows)
            {
                rowPostImages.Delete();
            }

            sql = "SELECT * FROM tPostAndTag WHERE fPostId = " + postId;
            con = new SqlConnection();
            con.ConnectionString = _connectionString;
            con.Open();
            SqlDataAdapter daPostAndTag = new SqlDataAdapter(sql, con);
            SqlCommandBuilder builderPostAndTag = new SqlCommandBuilder();
            builderPostAndTag.DataAdapter = daPostAndTag;
            ds = new DataSet();
            daPostAndTag.Fill(ds);
            con.Close();
            DataTable dtPostAndTag = ds.Tables[0];
            List<int> tagIds = new List<int>();
            foreach(DataRow rowPostAndTag in dtPostAndTag.Rows)
            {
                tagIds.Add(Convert.ToInt32(rowPostAndTag["fTagId"]));
                rowPostAndTag.Delete();
            }

            sql = "SELECT * FROM tPostTags";
            con = new SqlConnection();
            con.ConnectionString = _connectionString;
            con.Open();
            SqlDataAdapter daPostTags = new SqlDataAdapter(sql, con);
            SqlCommandBuilder builderPostTags = new SqlCommandBuilder();
            builderPostTags.DataAdapter = daPostTags;
            ds = new DataSet();
            daPostTags.Fill(ds);
            con.Close();
            DataTable dtPostTags = ds.Tables[0];
            foreach(DataRow rowPostTags in dtPostTags.Rows)
            {
                int x = Convert.ToInt32(rowPostTags["fTagId"]);
                foreach (int tagId in tagIds)
                {
                    if (x == tagId)
                        rowPostTags.Delete();
                }
                
            }
            daPostImages.Update(dtPostImages);
            daPostAndTag.Update(dtPostAndTag);
            daPostTags.Update(dtPostTags);
            _da.Update(dataGridView1.DataSource as DataTable);
        }
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            _position = e.RowIndex;
        }
    }
}
