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

namespace Attractions.Views {
    public partial class FormAttractionRecommendationList : Form {
        private string pipe = "np:\\\\.\\pipe\\LOCALDB#B5FE6A17\\tsql\\query;";
        public FormAttractionRecommendationList() {
            InitializeComponent();
        }

        // 取得推薦資料的 SQL
        private string getSqlOfAllRecommendation() {
            string sql = "SELECT ";
            sql += "r.fAttractionRecommendationId, ";
            sql += "r.fAttractionId, ";
            sql += "a1.fAttractionName AS fAttractionName, ";
            sql += "r.fRecommendationId, ";
            sql += "a2.fAttractionName AS fRecommendationName, ";
            sql += "r.fReason, ";
            sql += "r.fCreatedDate ";
            sql += "FROM tAttractionRecommendations AS r ";
            sql += "JOIN tAttractions AS a1 ";
            sql += "ON r.fAttractionId = a1.fAttractionId ";
            sql += "JOIN tAttractions AS a2 ";
            sql += "ON r.fRecommendationId = a2.fAttractionId";
            return sql;
        }

        private void FormRecommendationList_Load(object sender, EventArgs e) {
            
            displayAttractionRecommendation(getSqlOfAllRecommendation(), false);
        }   

        private void displayAttractionRecommendation(string sql, bool isKeyWord) {
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

        // 點擊"新增"按鈕
        private void tsbInsert_Click(object sender, EventArgs e) {
            FormAttractionRecommendationEditor f = new FormAttractionRecommendationEditor();
            f.ShowDialog();
            if (f.isOk == DialogResult.OK) {
                (new CAttractionManager()).createAttractionRecommendation(f.attractionRecommendation);
                displayAttractionRecommendation(getSqlOfAllRecommendation(), false);
            }
        }
        
        // 點擊"刪除"按鈕
        private void tsbDelete_Click(object sender, EventArgs e) {
            List<int> deleteIndexes = new List<int>();

            // 取得所有選取到的 row
            foreach (DataGridViewRow row in dataGridView1.SelectedRows) {
                try {
                    deleteIndexes.Add((int)row.Cells["fAttractionRecommendationId"].Value);
                }
                catch (Exception ex) {

                }
            }

            if (deleteIndexes.Count == 0) return;

            string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";

            // 刪除的 SQL
            string sql = "DELETE FROM tAttractionRecommendations WHERE fAttractionRecommendationId IN (";
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

            displayAttractionRecommendation(getSqlOfAllRecommendation(), false);
        }

        // 點擊"修改"按鈕
        private void tsbEdit_Click(object sender, EventArgs e) {
            showEditView();
        }

        // 顯示編輯畫面
        private void showEditView() {
            if (dataGridView1.CurrentCell.RowIndex < 0) return;

            string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            string sql = "SELECT * FROM tAttractionRecommendations WHERE fAttractionRecommendationId=@K_fAttractionRecommendationId";
            // 防止 SQL Injection
            SqlParameter fAttractionRecommendationId = new SqlParameter("K_fAttractionRecommendationId", dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["fAttractionRecommendationId"].Value);

            CAttractionRecommendation x = null;
            try {
                using (SqlConnection connection = new SqlConnection(connectString)) {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.Add(fAttractionRecommendationId);
                    command.ExecuteNonQuery();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read()) {
                        x = new CAttractionRecommendation();
                        x.fAttractionRecommendationId = (int)reader["fAttractionRecommendationId"];
                        x.fAttractionId = (int)reader["fAttractionId"];
                        x.fRecommendationId = (int)reader["fRecommendationId"];
                        x.fReason = reader["fReason"].ToString();
                        x.fCreatedDate = (DateTime)reader["fCreatedDate"];
                    }
                }
            }
            catch (Exception ex) {
            }

            if (x == null) return;

            FormAttractionRecommendationEditor f = new FormAttractionRecommendationEditor();
            f.attractionRecommendation = x;
            f.ShowDialog();

            if (f.isOk == DialogResult.OK) {
                (new CAttractionManager()).updateAttractionRecommendation(f.attractionRecommendation);
                displayAttractionRecommendation(getSqlOfAllRecommendation(), false);
            }
        }

        private void tsbSearch_Click(object sender, EventArgs e) {
            string sql = getSqlOfAllRecommendation();
            sql += " WHERE a1.fAttractionName LIKE @K_KEYWORD ";
            sql += "OR a2.fAttractionName LIKE @K_KEYWORD ";
            sql += "OR fReason LIKE @K_KEYWORD ";

            displayAttractionRecommendation(sql, true);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            showEditView();
        }
    }
}
