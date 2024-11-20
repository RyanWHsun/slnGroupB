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

namespace Attractions.Views
{
    public partial class FormAttractionCommentList : Form
    {
        //private string pipe = "np:\\\\.\\pipe\\LOCALDB#B5FE6A17\\tsql\\query;";
        public FormAttractionCommentList()
        {
            InitializeComponent();
        }

        // 取得評論資料的 SQL
        private string getSqlOfAllComment()
        {
            string sql = "SELECT ";
            sql += "fCommentId, ";
            sql += "c.fAttractionId, ";
            sql += "fAttractionName, ";
            sql += "c.fUserId, ";
            sql += "fUserName, ";
            sql += "fRating, ";
            sql += "fComment, ";
            sql += "c.fCreatedDate ";
            sql += "FROM tAttractionComments as c ";
            sql += "JOIN tAttractions as a ";
            sql += "ON c.fAttractionId = a.fAttractionId ";
            sql += "JOIN tUser as u ";
            sql += "ON c.fUserId = u.fUserId";
            return sql;
        }

        private void FormAttractionCommentList_Load(object sender, EventArgs e)
        {
            displayAttractionComment(getSqlOfAllComment(), false);
        }

        private void displayAttractionComment(string sql, bool isKeyWord)
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
                catch 
                {

                }
            }
        }

        private void displayAttractionComment(string sql, int rating)
        {
            //string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            string connectString = @"Data Source = .; Initial Catalog = dbGroupB; Integrated Security = True;";

            using (SqlConnection connection = new SqlConnection(connectString))
            {
                SqlCommand command = new SqlCommand(sql, connection);

                // 防止 SQL Injection
                command.Parameters.Add(new SqlParameter("K_KEYWORD", (object)toolStripTextBox1.Text.Trim()));

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
        }

        private void tsbInsert_Click(object sender, EventArgs e)
        {
            FormAttractionCommentEditor f = new FormAttractionCommentEditor();
            f.ShowDialog();
            if (f.isOk == DialogResult.OK)
            {
                (new CAttractionManager()).createAttractionComment(f.attractionComment);
                displayAttractionComment(getSqlOfAllComment(), false);
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
                    deleteIndexes.Add((int)row.Cells["fCommentId"].Value);
                }
                catch (Exception ex)
                {

                }
            }

            if (deleteIndexes.Count == 0) return;

            //string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            string connectString = @"Data Source = .; Initial Catalog = dbGroupB; Integrated Security = True;";

            // 刪除的 SQL
            string sql = "DELETE FROM tAttractionComments WHERE fCommentId IN (";
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

            displayAttractionComment(getSqlOfAllComment(), false);
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

            string sql = "SELECT * FROM tAttractionComments WHERE fCommentId=@K_fCommentId";
            // 防止 SQL Injection
            SqlParameter fCommentId = new SqlParameter("K_fCommentId", dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["fCommentId"].Value);

            CAttractionComment x = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.Add(fCommentId);
                    command.ExecuteNonQuery();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        x = new CAttractionComment();
                        x.fCommentId = (int)reader["fCommentId"];
                        x.fAttractionId = (int)reader["fAttractionId"];
                        x.fUserId = (int)reader["fUserId"];
                        x.fRating = (int)reader["fRating"];
                        x.fComment = reader["fComment"].ToString();
                        x.fCreatedDate = (DateTime)reader["fCreatedDate"];
                    }
                }
            }
            catch (Exception ex)
            {
            }

            if (x == null) return;

            FormAttractionCommentEditor f = new FormAttractionCommentEditor();
            f.attractionComment = x;
            f.ShowDialog();

            if (f.isOk == DialogResult.OK)
            {
                (new CAttractionManager()).updateAttractionComment(f.attractionComment);
                displayAttractionComment(getSqlOfAllComment(), false);
            }
        }

        private void tsbSearch_Click(object sender, EventArgs e)
        {
            string sql = getSqlOfAllComment();
            if (toolStripComboBox1.SelectedItem == null) return;
            switch (toolStripComboBox1.SelectedItem.ToString())
            {
                case "景點名稱":
                    sql += " WHERE fAttractionName LIKE @K_KEYWORD ";
                    break;
                case "使用者名稱":
                    sql += " WHERE fUserName LIKE @K_KEYWORD ";
                    break;
                case "評分":
                    if (int.TryParse(toolStripTextBox1.Text.Trim(), out int result) && result >= 1 && result <= 5)
                    {
                        sql += " WHERE fRating = @K_KEYWORD";
                        displayAttractionComment(sql, result);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("評分只能是1~5的數字");
                    }
                    break;
                case "評論內容":
                    sql += " WHERE fComment LIKE @K_KEYWORD ";
                    break;
                default:
                    break;
            }
            displayAttractionComment(sql, true);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            showEditView();
        }
        private void resetGridStyle()
        {
            dataGridView1.Columns["fCommentId"].HeaderText = "評論ID";
            dataGridView1.Columns["fCommentId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fAttractionId"].HeaderText = "景點ID";
            dataGridView1.Columns["fAttractionId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fAttractionName"].HeaderText = "景點名稱";
            dataGridView1.Columns["fAttractionName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fUserId"].HeaderText = "使用者ID";
            dataGridView1.Columns["fUserId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fUserName"].HeaderText = "使用者名稱";
            dataGridView1.Columns["fUserName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fRating"].HeaderText = "評分";
            dataGridView1.Columns["fRating"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fComment"].HeaderText = "評論";
            dataGridView1.Columns["fComment"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.Columns["fCreatedDate"].HeaderText = "建立時間";
            dataGridView1.Columns["fCreatedDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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
        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            resetGridStyle();
        }

        private void FormAttractionCommentList_Paint(object sender, PaintEventArgs e)
        {
            resetGridStyle();
        }
    }
}
