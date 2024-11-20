using Attractions.Models;
using prjGroupB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;

namespace Attractions.Views
{
    public partial class FormAttractionImageList : Form
    {
        //private string pipe = "np:\\\\.\\pipe\\LOCALDB#B5FE6A17\\tsql\\query;";
        private int _imageIndex = 0;
        private class CImageData
        {
            public int fAttractionImageId { get; set; }
            public int fAttractionId { get; set; }
            public string fAttractionName { get; set; }
            public string fDescription { get; set; }
            public byte[] fImage { get; set; }
        }
        private List<CImageData> _imageDataList;
        public FormAttractionImageList()
        {
            InitializeComponent();
        }
        private string getSqlOfAllImage()
        {
            string sql = "SELECT ";
            sql += "fAttractionImageId, ";
            sql += "i.fAttractionId, ";
            sql += "fAttractionName, ";
            sql += "fDescription, ";
            sql += "fImage ";
            sql += "FROM tAttractionImages as i ";
            sql += "JOIN tAttractions as a ";
            sql += "ON i.fAttractionId = a.fAttractionId";
            return sql;
        }
        private void FormAttractionImageList_Load(object sender, EventArgs e)
        {
            displayAttractionImage(getSqlOfAllImage(), false);
        }

        private void displayAttractionImage(string sql, bool isKeyWord)
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

        private void displayAttractionImage(string sql, int id)
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
            resetGridStyle();
        }

        private void tsbInsert_Click(object sender, EventArgs e)
        {
            FormAttractionImageEditor f = new FormAttractionImageEditor();
            f.ShowDialog();
            if (f.isOk == DialogResult.OK)
            {
                (new CAttractionManager()).createAttractionImage(f.attractionImage);
                displayAttractionImage(getSqlOfAllImage(), false);
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
                    deleteIndexes.Add((int)row.Cells["fAttractionImageId"].Value);
                }
                catch (Exception ex)
                {

                }
            }

            if (deleteIndexes.Count == 0) return;

            //string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            string connectString = @"Data Source = .; Initial Catalog = dbGroupB; Integrated Security = True;";

            // 刪除的 SQL
            string sql = "DELETE FROM tAttractionImages WHERE fAttractionImageId IN (";
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

            displayAttractionImage(getSqlOfAllImage(), false);
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            showEditView();
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            showEditView();
        }
        private void showEditView()
        {
            if (dataGridView1.CurrentCell.RowIndex < 0) return;

            //string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            string connectString = @"Data Source = .; Initial Catalog = dbGroupB; Integrated Security = True;";

            string sql = "SELECT * FROM tAttractionImages WHERE fAttractionImageId=@K_fAttractionImageId";
            // 防止 SQL Injection
            SqlParameter fAttractionTicketId = new SqlParameter("K_fAttractionImageId", dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["fAttractionImageId"].Value);

            CAttractionImage x = null;
            FormAttractionImageEditor f = new FormAttractionImageEditor();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.Add(fAttractionTicketId);
                    command.ExecuteNonQuery();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        x = new CAttractionImage();
                        x.fAttractionImageId = (int)reader["fAttractionImageId"];
                        x.fAttractionId = (int)reader["fAttractionId"];
                        f.attractionImage = x;
                        try
                        {
                            f.showSavedImage((int)reader["fAttractionImageId"], 0);
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            if (x == null) return;

            f.ShowDialog();

            if (f.isOk == DialogResult.OK)
            {
                (new CAttractionManager()).updateAttractionImage(f.attractionImage);
                displayAttractionImage(getSqlOfAllImage(), false);
            }
        }

        private void btnPreviousImage_Click(object sender, EventArgs e)
        {
            this._imageIndex--;
            if (_imageDataList == null) return;
            if (this._imageIndex < 0) this._imageIndex = 0;
            if (_imageDataList.Count > 0) showLargeImage(_imageDataList, this._imageIndex);
        }

        private void btnNextImage_Click(object sender, EventArgs e)
        {
            this._imageIndex++;
            if (_imageDataList == null) return;
            if (this._imageIndex >= this._imageDataList.Count) this._imageIndex -= 1;
            if (_imageDataList.Count > 0) showLargeImage(_imageDataList, this._imageIndex);
        }

        private void tsbSearch_Click(object sender, EventArgs e)
        {
            string sql = getSqlOfAllImage();
            if (toolStripComboBox1.SelectedItem == null) return;

            switch (toolStripComboBox1.SelectedItem.ToString())
            {
                case "景點ID":
                    if (int.TryParse(toolStripTextBox1.Text.Trim(), out int result))
                    {
                        sql += " WHERE i.fAttractionId = @K_KEYWORD";
                        displayAttractionImage(sql, result);
                        return;
                    }
                    else MessageBox.Show("請輸入 >=1 的整數數字");
                    break;
                case "景點名稱":
                    sql += " WHERE fAttractionName LIKE @K_KEYWORD ";
                    break;
                case "描述":
                    sql += " WHERE fDescription LIKE @K_KEYWORD ";
                    break;
                default:
                    break;
            }
            displayAttractionImage(sql, true);
        }
        // 顯示已儲存的圖片
        public void showSavedImage(int id, int index)
        {
            CAttractionImage attractionImage = new CAttractionImage();
            if (attractionImage.fImage != null) attractionImage.fImage.Clear();
            // 連線
            //string connectionString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True;";
            string connectionString = @"Data Source = .; Initial Catalog = dbGroupB; Integrated Security = True;";

            // SQL 查詢語句
            string query = "SELECT fImage FROM tAttractionImages WHERE fAttractionId = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (attractionImage.fImage == null)
                            attractionImage.fImage = new List<byte[]>();
                        while (reader.Read())
                        {
                            // 取得圖片資料 
                            attractionImage.fImage.Add(reader["fImage"] as byte[]);
                        }
                        if (attractionImage.fImage.Count > 0)
                        {

                            // 將二進制資料轉換成圖片
                            using (MemoryStream ms = new MemoryStream(attractionImage.fImage[index]))
                            {
                                pcbImage.Image = Image.FromStream(ms);
                            }
                        }
                    }
                }
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            pcbImage.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;

            if (dataGridView1.CurrentCell.RowIndex < 0) return;

            //string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            string connectionString = @"Data Source = .; Initial Catalog = dbGroupB; Integrated Security = True;";

            string sql = getSqlOfAllImage() + " WHERE i.fAttractionId = @K_fAttractionId";
            // 防止 SQL Injection
            SqlParameter fAttractionId = new SqlParameter("K_fAttractionId", dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["fAttractionId"].Value);

            //_imageDataList = null;
            _imageDataList = new List<CImageData>();
            FormAttractionImageEditor f = new FormAttractionImageEditor();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.Add(fAttractionId);
                    command.ExecuteNonQuery();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        CImageData _imageData = new CImageData();

                        _imageData.fAttractionImageId = (int)reader["fAttractionImageId"];
                        _imageData.fAttractionId = (int)reader["fAttractionId"];
                        _imageData.fAttractionName = reader["fAttractionName"].ToString();
                        _imageData.fDescription = reader["fDescription"].ToString();
                        _imageData.fImage = reader["fImage"] as byte[];

                        _imageDataList.Add(_imageData);
                    }
                }
            }
            catch (Exception ex)
            {
            }

            if (_imageDataList.Count > 0) showLargeImage(_imageDataList, 0);
        }

        private void showLargeImage(List<CImageData> imageDataList, int index)
        {
            if (index < 0) return;
            lbAttractionId.Text = imageDataList[index].fAttractionId.ToString();
            lbAttractionName.Text = imageDataList[index].fAttractionName;
            lbAttractionDescription.Text = imageDataList[index].fDescription;
            // 將二進制資料轉換成圖片
            using (MemoryStream ms = new MemoryStream(imageDataList[index].fImage))
            {
                pcbImage.Image = Image.FromStream(ms);
            }
        }

        private void resetGridStyle()
        {
            dataGridView1.Columns["fAttractionImageId"].HeaderText = "景點圖片ID";
            dataGridView1.Columns["fAttractionImageId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fAttractionId"].HeaderText = "景點ID";
            dataGridView1.Columns["fAttractionId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fAttractionName"].HeaderText = "景點名稱";
            dataGridView1.Columns["fAttractionName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fDescription"].HeaderText = "描述";
            dataGridView1.Columns["fDescription"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fImage"].HeaderText = "圖片";
            dataGridView1.Columns["fImage"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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

            tableLayoutPanel1.BackColor = Color.FromArgb(240, 255, 240);
        }

        private void FormAttractionImageList_Paint(object sender, PaintEventArgs e)
        {
            resetGridStyle();
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            resetGridStyle();
        }
    }
}
