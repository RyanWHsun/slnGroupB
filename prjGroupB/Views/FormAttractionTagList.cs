﻿using prjGroupB.Models;
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
    public partial class FormAttractionTagList : Form
    {
        //private string pipe = "np:\\\\.\\pipe\\LOCALDB#B5FE6A17\\tsql\\query;";
        public FormAttractionTagList()
        {
            InitializeComponent();
        }

        // 點擊"新增"按鈕
        private void tsbInsert_Click(object sender, EventArgs e)
        {
            FormAttractionTagEditor f = new FormAttractionTagEditor();
            f.ShowDialog();
            if (f.isOk == DialogResult.OK)
            {
                (new CAttractionManager()).createAttractionTag(f.attractionTag);
                displayAttractionTag("SELECT * FROM tAttractionTags;", false);
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
                    deleteIndexes.Add((int)row.Cells["fTagId"].Value);
                }
                catch 
                {

                }
            }

            if (deleteIndexes.Count == 0) return;

            //string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            string connectString = @"Data Source = .; Initial Catalog = dbGroupB; Integrated Security = True;";

            // 刪除的 SQL
            string sql = "DELETE FROM tAttractionTags WHERE fTagId IN (";
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
            catch 
            {
            }

            displayAttractionTag("SELECT * FROM tAttractionTags;", false);
        }

        // 點擊"修改"按鈕
        private void tsbEdit_Click(object sender, EventArgs e)
        {
            showEditView();
        }

        // 點擊"搜尋"按鈕
        private void tsbSearch_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM tAttractionTags WHERE ";
            sql += "fTagName LIKE @K_KEYWORD;";

            displayAttractionTag(sql, true);
        }

        private void FormTagList_Load(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM tAttractionTags;";
            displayAttractionTag(sql, false);
        }

        private void displayAttractionTag(string sql, bool isKeyWord)
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
            resetGridStyle();
        }

        // 顯示編輯畫面
        private void showEditView()
        {
            if (dataGridView1.CurrentCell.RowIndex < 0) return;

            //string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            string connectString = @"Data Source = .; Initial Catalog = dbGroupB; Integrated Security = True;";

            string sql = "SELECT * FROM tAttractionTags WHERE fTagId=@K_fTagId";
            // 防止 SQL Injection
            SqlParameter fAttractionTagId = new SqlParameter("K_fTagId", dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["fTagId"].Value);

            CAttractionTag x = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.Add(fAttractionTagId);
                    command.ExecuteNonQuery();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        x = new CAttractionTag();
                        x.fTagId = (int)reader["fTagId"];
                        x.fTagName = reader["fTagName"].ToString();
                        x.fCreatedDate = (DateTime)reader["fCreatedDate"];
                    }
                }
            }
            catch 
            {
            }

            if (x == null) return;

            FormAttractionTagEditor f = new FormAttractionTagEditor();
            f.attractionTag = x; // f.customer 會觸發  public CCustomer customer {set;}
            f.ShowDialog();

            if (f.isOk == DialogResult.OK)
            {
                (new CAttractionManager()).updateAttractionTag(f.attractionTag);
                displayAttractionTag("SELECT * FROM tAttractionTags;", false);
            }
        }

        // 雙擊 dataGridView1 欄位，開啟編輯頁面
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            showEditView();
        }

        private void resetGridStyle()
        {
            dataGridView1.Columns["fTagId"].HeaderText = "標籤ID";
            dataGridView1.Columns["fTagId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fTagName"].HeaderText = "標籤名稱";
            dataGridView1.Columns["fTagName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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

        private void FormAttractionTagList_Paint(object sender, PaintEventArgs e)
        {
            resetGridStyle();
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            resetGridStyle();
        }
    }
}
