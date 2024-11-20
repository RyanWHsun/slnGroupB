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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Attractions.Views
{
    public partial class FormAttractionCategoryList : Form
    {
        //private string pipe = "np:\\\\.\\pipe\\LOCALDB#B5FE6A17\\tsql\\query;";
        public FormAttractionCategoryList()
        {
            InitializeComponent();
        }

        private void FormCategoryList_Load(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM tAttractionCategories;";
            displayAttractionCategory(sql, false);
        }

        private void displayAttractionCategory(string sql, bool isKeyWord)
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
        }

        // 點擊"新增"按鈕
        private void tsbInsert_Click(object sender, EventArgs e)
        {
            FormAttractionCategoryEditor f = new FormAttractionCategoryEditor();
            f.ShowDialog();
            if (f.isOk == DialogResult.OK)
            {
                (new CAttractionManager()).createAttractionCategory(f.attractionCategory);
                displayAttractionCategory("SELECT * FROM tAttractionCategories;", false);
            }
        }

        // 點擊"刪除"按鈕
        private void tsbDelete_Click(object sender, EventArgs e)
        {
            List<int> deleteIndexes = new List<int>();

            // 取得所有選取到的 row
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                try
                {
                    deleteIndexes.Add((int)row.Cells["fAttractionCategoryId"].Value);
                }
                catch (Exception ex)
                {

                }
            }

            if (deleteIndexes.Count == 0) return;

            // 刪掉 category 之前，要把相關的 tAttractions 的 fCategoryId 改成 NULL 
            setRelatedAttractionsCategeryIdIsNull(deleteIndexes);

            //string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            string connectString = @"Data Source = .; Initial Catalog = dbGroupB; Integrated Security = True;";

            // 刪除的 SQL
            string sql = "DELETE FROM tAttractionCategories WHERE fAttractionCategoryId IN (";
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

            displayAttractionCategory("SELECT * FROM tAttractionCategories;", false);
        }

        // 設定相關 attraction 的 categoryId 為 null
        private void setRelatedAttractionsCategeryIdIsNull(List<int> indexes)
        {
            //string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            string connectString = @"Data Source = .; Initial Catalog = dbGroupB; Integrated Security = True;";

            string sql = "UPDATE tAttractions SET fCategoryId = NULL WHERE fCategoryId IN (";
            for (int i = 0; i < indexes.Count; i++)
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
                        for (int i = 0; i < indexes.Count; i++)
                        {
                            command.Parameters.AddWithValue($"@id{i}", indexes[i]);
                        }
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("ERROR: Can't Set Related Attractions CategeryId Is Null");
            }
        }

        // 點擊"修改"按鈕
        private void tsbEdit_Click(object sender, EventArgs e)
        {
            showEditView();
        }

        // 顯示編輯畫面
        private void showEditView()
        {
            if (dataGridView1.CurrentCell.RowIndex < 0) return;

            //string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            string connectString = @"Data Source = .; Initial Catalog = dbGroupB; Integrated Security = True;";

            string sql = "SELECT * FROM tAttractionCategories WHERE fAttractionCategoryId=@K_fAttractionCategoryId";
            // 防止 SQL Injection
            SqlParameter fAttractionCategoryId = new SqlParameter("K_fAttractionCategoryId", dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["fAttractionCategoryId"].Value);

            CAttractionCategory x = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.Add(fAttractionCategoryId);
                    command.ExecuteNonQuery();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        x = new CAttractionCategory();
                        x.fAttractionCategoryId = (int)reader["fAttractionCategoryId"];
                        x.fAttractionCategoryName = reader["fAttractionCategoryName"].ToString();
                        x.fDescription = reader["fDescription"].ToString();
                        x.fCreateDate = (DateTime)reader["fCreateDate"];
                    }
                }
            }
            catch (Exception ex)
            {
            }

            if (x == null) return;

            FormAttractionCategoryEditor f = new FormAttractionCategoryEditor();
            f.attractionCategory = x; // f.customer 會觸發  public CCustomer customer {set;}
            f.ShowDialog();

            if (f.isOk == DialogResult.OK)
            {
                (new CAttractionManager()).updateAttractionCategory(f.attractionCategory);
                displayAttractionCategory("SELECT * FROM tAttractionCategories;", false);
            }
        }

        // 點擊"搜尋"按鈕
        private void tsbSearch_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM tAttractionCategories WHERE ";
            sql += "fAttractionCategoryName LIKE @K_KEYWORD ";
            sql += "OR fDescription LIKE @K_KEYWORD";

            displayAttractionCategory(sql, true);
        }

        // 雙擊 dataGridView1 欄位，開啟編輯頁面
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            showEditView();
        }

        private void resetGridStyle()
        {
            dataGridView1.Columns["fAttractionCategoryId"].HeaderText = "分類ID";
            dataGridView1.Columns["fAttractionCategoryId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fAttractionCategoryName"].HeaderText = "分類名稱";
            dataGridView1.Columns["fAttractionCategoryName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fDescription"].HeaderText = "描述";
            dataGridView1.Columns["fDescription"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.Columns["fCreateDate"].HeaderText = "建立時間";
            dataGridView1.Columns["fCreateDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 300;
            dataGridView1.Columns[3].Width = 200;

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

        private void FormAttractionCategoryList_Paint(object sender, PaintEventArgs e)
        {
            resetGridStyle();
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            resetGridStyle();
        }
    }
}
