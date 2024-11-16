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

namespace Attractions.Views {
    public partial class FormAttractionCategoryList : Form {
        private string pipe = " np:\\\\.\\pipe\\LOCALDB#7C99B448\\tsql\\query;";
        public FormAttractionCategoryList() {
            InitializeComponent();
        }

        private void FormCategoryList_Load(object sender, EventArgs e) {
            string sql = "SELECT * FROM tAttractionCategories;";
            displayAttractionCategory(sql, false);
        }

        private void displayAttractionCategory(string sql, bool isKeyWord) {
            string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectString)) {
                SqlCommand command = new SqlCommand(sql, connection);
                
                // 防止 SQL Injection
                if (isKeyWord) {
                    command.Parameters.Add(new SqlParameter("K_KEYWORD", "%" + (object)toolStripTextBox1.Text.Trim() + "%"));
                }

                try {
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    // 創建 DataTable 並載入資料
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);

                    // 將資料綁定到 DataGridView
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex) {

                }
            }
        }

        // 新增
        private void tsbInsert_Click(object sender, EventArgs e) {
            FormAttractionCategoryEditor f = new FormAttractionCategoryEditor();
            f.ShowDialog();
            if (f.isOk == DialogResult.OK) {
                (new CAttractionManager()).createAttractionCategory(f.attractionCategory);
                displayAttractionCategory("SELECT * FROM tAttractionCategories;", false);
            }
        }

        // 刪除
        private void tsbDelete_Click(object sender, EventArgs e) {
            List<int> deleteIndexes = new List<int>();
            
            foreach (DataGridViewRow row in dataGridView1.SelectedRows) {
                try {
                    deleteIndexes.Add((int)row.Cells["fAttractionCategoryId"].Value);
                }catch (Exception ex) { 
                
                }
            }

            if (deleteIndexes.Count == 0) return;
            setRelatedAttractionsCategeryIdIsNull(deleteIndexes);

            string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";

            // 刪除的 SQL
            string sql = "DELETE FROM tAttractionCategories WHERE fAttractionCategoryId IN (";
            for (int i = 0; i < deleteIndexes.Count; i++) {
                sql += $"@id{i},";
            }
            sql = sql.TrimEnd(',') + ")";

            try {
                using (SqlConnection connection = new SqlConnection(connectString)) {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        // 動態添加參數
                        for (int i = 0; i < deleteIndexes.Count; i++) {
                            command.Parameters.AddWithValue($"@id{i}", deleteIndexes[i]);
                        }
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) {
            }

            displayAttractionCategory("SELECT * FROM tAttractionCategories;", false);
        }

        private void setRelatedAttractionsCategeryIdIsNull(List<int> indexes) {
            string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";

            string sql = "UPDATE tAttractions SET fCategoryId = NULL WHERE fCategoryId IN (";
            for (int i = 0; i < indexes.Count; i++) {
                sql += $"@id{i},";
            }
            sql = sql.TrimEnd(',') + ")";

            try {
                using (SqlConnection connection = new SqlConnection(connectString)) {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        // 動態添加參數
                        for (int i = 0; i < indexes.Count; i++) {
                            command.Parameters.AddWithValue($"@id{i}", indexes[i]);
                        }
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) {
                //MessageBox.Show("ERROR: Can't Set Related Attractions CategeryId Is Null");
            }
        }

        private void tsbEdit_Click(object sender, EventArgs e) {

        }

        private void tsbReload_Click(object sender, EventArgs e) {

        }

        private void tsbSearch_Click(object sender, EventArgs e) {

        }
    }
}
