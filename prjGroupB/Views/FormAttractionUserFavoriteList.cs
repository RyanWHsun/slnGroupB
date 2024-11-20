using Attractions.Views;
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
    public partial class FormAttractionUserFavoriteList : Form
    {
        //private string pipe = "np:\\\\.\\pipe\\LOCALDB#B5FE6A17\\tsql\\query;";
        public FormAttractionUserFavoriteList()
        {
            InitializeComponent();
        }

        private string getSqlOfAllFavorite()
        {
            string sql = "SELECT ";
            sql += "fFavoriteId, ";
            sql += "f.fUserId, ";
            sql += "fUserName, ";
            sql += "f.fAttractionId, ";
            sql += "fAttractionName, ";
            sql += "f.fCreatedDate ";
            sql += "FROM tAttractionUserFavorites as f ";
            sql += "JOIN tUser as u ";
            sql += "ON f.fUserId = u.fUserId ";
            sql += "JOIN tAttractions as a ";
            sql += "ON f.fAttractionId = a.fAttractionId";
            return sql;
        }
        private void FormAttractionUserFavoriteList_Load(object sender, EventArgs e)
        {
            displayAttractionUserFavorite(getSqlOfAllFavorite(), false);
        }

        private void displayAttractionUserFavorite(string sql, bool isKeyWord)
        {
            //string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            string connectString = @"Data Source = .; Initial Catalog = dbGroupB; Integrated Security = True;";

            using (SqlConnection connection = new SqlConnection(connectString))
            {
                SqlCommand command = new SqlCommand(sql, connection);

                // 防止 SQL Injection
                if (isKeyWord)
                {
                    command.Parameters.Add(new SqlParameter("K_KEYWORD", "%" + (object)toolStripTextBox1.Text.Trim() + "%"));
                }

                try
                {
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    // 創建 DataTable 並載入資料
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);

                    // 將資料綁定到 DataGridView
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {

                }
            }
            resetGridStyle();
        }

        private void tsbInsert_Click(object sender, EventArgs e)
        {
            FormAttractionUserFavoriteEditor f = new FormAttractionUserFavoriteEditor();
            f.ShowDialog();
            if (f.isOk == DialogResult.OK)
            {
                (new CAttractionManager()).createAttractionUserFavorite(f.attractionUserFavorite);
                displayAttractionUserFavorite(getSqlOfAllFavorite(), false);
            }
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            List<int> deleteIndexes = new List<int>();

            // 取得所有選取到的 row
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                try
                {
                    deleteIndexes.Add((int)row.Cells["fFavoriteId"].Value);
                }
                catch (Exception ex)
                {

                }
            }

            if (deleteIndexes.Count == 0) return;

            //string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            string connectString = @"Data Source = .; Initial Catalog = dbGroupB; Integrated Security = True;";

            // 刪除的 SQL
            string sql = "DELETE FROM tAttractionUserFavorites WHERE fFavoriteId IN (";
            for (int i = 0; i < deleteIndexes.Count; i++)
            {
                sql += $"@id{i},";
            }
            sql = sql.TrimEnd(',') + ")";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // 動態添加參數
                        for (int i = 0; i < deleteIndexes.Count; i++)
                        {
                            command.Parameters.AddWithValue($"@id{i}", deleteIndexes[i]);
                        }
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
            }

            displayAttractionUserFavorite(getSqlOfAllFavorite(), false);
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            showEditView();
        }

        private void showEditView()
        {
            if (dataGridView1.CurrentCell.RowIndex < 0) return;

            //string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            string connectString = @"Data Source = .; Initial Catalog = dbGroupB; Integrated Security = True;";

            string sql = "SELECT * FROM tAttractionUserFavorites WHERE fFavoriteId=@K_fFavoriteId";
            // 防止 SQL Injection
            SqlParameter fFavoriteId = new SqlParameter("K_fFavoriteId", dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["fFavoriteId"].Value);

            CAttractionUserFavorite x = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.Add(fFavoriteId);
                    command.ExecuteNonQuery();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        x = new CAttractionUserFavorite();
                        x.fFavoriteId = (int)reader["fFavoriteId"];
                        x.fUserId = (int)reader["fUserId"];
                        x.fAttractionId = (int)reader["fAttractionId"];
                        x.fCreatedDate = (DateTime)reader["fCreatedDate"];
                    }
                }
            }
            catch (Exception ex)
            {
            }

            if (x == null) return;

            FormAttractionUserFavoriteEditor f = new FormAttractionUserFavoriteEditor();
            f.attractionUserFavorite = x;
            f.ShowDialog();

            if (f.isOk == DialogResult.OK)
            {
                (new CAttractionManager()).updateAttractionUserFavorite(f.attractionUserFavorite);
                displayAttractionUserFavorite(getSqlOfAllFavorite(), false);
            }
        }

        private void tsbSearch_Click(object sender, EventArgs e)
        {
            string sql = getSqlOfAllFavorite();
            sql += " WHERE fUserName LIKE @K_KEYWORD ";
            sql += "OR fAttractionName LIKE @K_KEYWORD ";
            displayAttractionUserFavorite(sql, true);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            showEditView();
        }

        private void resetGridStyle()
        {
            dataGridView1.Columns["fFavoriteId"].HeaderText = "收藏ID";
            dataGridView1.Columns["fFavoriteId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fUserId"].HeaderText = "使用者ID";
            dataGridView1.Columns["fUserId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fUserName"].HeaderText = "使用者名稱";
            dataGridView1.Columns["fUserName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fAttractionId"].HeaderText = "景點ID";
            dataGridView1.Columns["fAttractionId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fAttractionName"].HeaderText = "景點名稱";
            dataGridView1.Columns["fAttractionName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fCreatedDate"].HeaderText = "建立時間";
            dataGridView1.Columns["fCreatedDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // 設定 ToolStrip 的背景色
            toolStrip1.BackColor = Color.FromArgb(240, 255, 240);
            // 設定 DataGridView 的背景色（整體背景）
            dataGridView1.BackgroundColor = Color.FromArgb(240, 255, 240);

            // 設定 dataGridVIew 標頭不採用預設樣式
            dataGridView1.EnableHeadersVisualStyles = false;

            // 設定 dataGridVIew 標頭樣式
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Gainsboro;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("微軟正黑體", 14, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            bool isColorChanged = false;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                isColorChanged = !isColorChanged;

                r.DefaultCellStyle.Font = new Font("微軟正黑體", 12);
                r.DefaultCellStyle.BackColor = Color.White;
                r.DefaultCellStyle.SelectionBackColor = Color.MediumSeaGreen;

                if (isColorChanged)
                {
                    r.DefaultCellStyle.BackColor = Color.MediumAquamarine;
                }
            }
        }

        private void FormAttractionUserFavoriteList_Paint(object sender, PaintEventArgs e)
        {
            resetGridStyle();
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            resetGridStyle();
        }
    }
}
