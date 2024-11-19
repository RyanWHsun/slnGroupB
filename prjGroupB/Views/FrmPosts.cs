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
            displayRoomBySql("SELECT fPostId, fUserId, fTitle, fContent, fCreatedAt, fUpdatedAt, fIsPublic FROM tPosts", false);
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
            MessageBox.Show("成功修改");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_position < 0)
                return;
            FrmPostDelete f = new FrmPostDelete();
            f.ShowDialog();
            if (f.isOK == DialogResult.OK)
            {
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
                foreach (DataRow rowPostImages in dtPostImages.Rows)
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
                DataTable dtPostAndTag = ds.Tables[0];
                List<int> tagIds = new List<int>();
                foreach (DataRow rowPostAndTag in dtPostAndTag.Rows)
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
                DataTable dtPostTags = ds.Tables[0];
                foreach (DataRow rowPostTags in dtPostTags.Rows)
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
        }
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            _position = e.RowIndex;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM tPosts WHERE ";
            sql += "fTitle LIKE @K_KEYWORD";
            sql += " OR fContent LIKE @K_KEYWORD";
            displayRoomBySql(sql, true);
        }
        private void resetGridStyle()
        {
            double totalWidth = dataGridView1.Width - dataGridView1.RowHeadersWidth;
            dataGridView1.Columns[0].Width = (int)(totalWidth * 0.05);
            dataGridView1.Columns[1].Width = (int)(totalWidth * 0.05);
            dataGridView1.Columns[2].Width = (int)(totalWidth * 0.2);
            dataGridView1.Columns[3].Width = (int)(totalWidth * 0.35);
            dataGridView1.Columns[4].Width = (int)(totalWidth * 0.15);
            dataGridView1.Columns[5].Width = (int)(totalWidth * 0.15);
            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView1.Columns[0].HeaderText = "文章編號";
            dataGridView1.Columns[1].HeaderText = "作者編號";
            dataGridView1.Columns[2].HeaderText = "標題";
            dataGridView1.Columns[3].HeaderText = "內文";
            dataGridView1.Columns[4].HeaderText = "創建時間";
            dataGridView1.Columns[5].HeaderText = "修改時間";
            dataGridView1.Columns[6].HeaderText = "公開";

            int rowCount = dataGridView1.RowCount;
            if (rowCount == 0)
                rowCount = 1;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = (dataGridView1.Height - dataGridView1.ColumnHeadersHeight) / rowCount;
                if (row.Height < 100)
                    row.Height = 100;
            }
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.ReadOnly = true;
            }
            dataGridView1.Columns[6].ReadOnly = false;
            resetColor();
        }

        private void FrmPosts_Paint(object sender, PaintEventArgs e)
        {
            resetGridStyle();
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
            {
                dataGridView1.Rows[e.RowIndex].HeaderCell.Style.BackColor = Color.FromArgb(255, 253, 247);
            }
            else
            {
                dataGridView1.Rows[e.RowIndex].HeaderCell.Style.BackColor = Color.FromArgb(246, 239, 230);
            }
        }
        private void resetColor()
        {
            // 設定 ToolStrip 的背景色
            toolStrip1.BackColor = Color.FromArgb(250, 243, 224);
            // 設定 DataGridView 的背景色（整體背景）
            dataGridView1.BackgroundColor = Color.FromArgb(250, 243, 224); // 暖米白
            // 設定標題行樣式
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(201, 166, 107); // 木質棕色
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(93, 70, 49); // 深咖啡
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("微軟正黑體", 14, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            // 設定行的樣式
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.FromArgb(255, 253, 247); // 白色偏暖
            dataGridView1.RowsDefaultCellStyle.ForeColor = Color.FromArgb(93, 70, 49); // 深咖啡色文字
            dataGridView1.RowsDefaultCellStyle.Font = new Font("微軟正黑體", 14, FontStyle.Regular);
            // 設定交替行樣式
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(246, 239, 230); // 奶咖色
            // 設定選中行樣式
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(242, 213, 166); // 暖黃色
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.FromArgb(61, 43, 31); // 深棕色文字
            // 設定不可自行增加Row
            dataGridView1.AllowUserToAddRows = false;
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            resetGridStyle();
        }
    }
}
