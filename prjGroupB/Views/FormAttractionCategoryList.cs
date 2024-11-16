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

        // 點擊"新增"按鈕
        private void tsbInsert_Click(object sender, EventArgs e) {
            FormAttractionCategoryEditor f = new FormAttractionCategoryEditor();
            f.ShowDialog();
            if (f.isOk == DialogResult.OK) {
                (new CAttractionManager()).createAttractionCategory(f.attractionCategory);
                displayAttractionCategory("SELECT * FROM tAttractionCategories;", false);
            }
        }

        // 點擊"刪除"按鈕
        private void tsbDelete_Click(object sender, EventArgs e) {
            List<int> deleteIndexes = new List<int>();
            
            // 取得所有選取到的 row
            foreach (DataGridViewRow row in dataGridView1.SelectedRows) {
                try {
                    deleteIndexes.Add((int)row.Cells["fAttractionCategoryId"].Value);
                }catch (Exception ex) { 
                
                }
            }

            if (deleteIndexes.Count == 0) return;

            // 刪掉 category 之前，要把相關的 tAttractions 的 fCategoryId 改成 NULL 
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

        // 設定相關 attraction 的 categoryId 為 null
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

        // 點擊"修改"按鈕
        private void tsbEdit_Click(object sender, EventArgs e) {
            showEditView();
        }
        
        // 顯示編輯畫面
        private void showEditView() {
            //if (listBox1.SelectedIndex < 0) return;

            //int pk = _listPK[listBox1.SelectedIndex];

            //SqlConnection con = new SqlConnection();
            //con.ConnectionString = @"Data Source=" + pipe + "Initial Catalog=dbDemo;Integrated Security=True";
            //con.Open();

            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = con;
            //cmd.CommandText = "SELECT * FROM tCustomer WHERE fId=@K_FID";
            //cmd.Parameters.Add(new SqlParameter("K_FID", (object)pk));

            //SqlDataReader reader = cmd.ExecuteReader();
            //CCustomer x = null;
            //// List<CCustomer> list = new List<CCustomer>();
            //if (reader.Read()) {
            //    x = new CCustomer();
            //    // 把資料庫中讀到的資料存在物件 x (x 就是一整坨資料，要在 FrmCustomerList Form 和 FrmCustomerEditor Form 之間傳來傳去)
            //    // 為何不直接在 Editor Form 打開資料庫找資料?
            //    // 因為想要找到的資料需要其中一項資訊才能取得，就是 _listPK[listBox1.SelectedIndex]，只有 FrmCustomerList 有，在 FrmCustomerEditor 中沒有。
            //    // 所以必須先在 FrmCustomerList 中找到資料，再存進 x 物件中，
            //    // 再把這個物件傳給 FrmCustomerEditor，FrmCustomerEditor 用 public CCustomer customer {set;} 接收
            //    x.fId = (int)reader["fId"];
            //    x.fName = reader["fName"].ToString();
            //    x.fPhone = reader["fPhone"].ToString();
            //    x.fEmail = reader["fEmail"].ToString();
            //    x.fAddress = reader["fAddress"].ToString();
            //    x.fPassword = reader["fPassword"].ToString();
            //}
            //con.Close();

            //if (x == null) {
            //    return;
            //}
            //FrmCustomerEditor f = new FrmCustomerEditor();
            //f.customer = x; // f.customer 會觸發  public CCustomer customer {set;}
            //f.ShowDialog();

            //if (f.isOk == DialogResult.OK) {
            //    (new CCustomerManager()).update(f.customer);
            //    displayCustomersBySql("SELECT * FROM tCustomer", false);
            //}
        }

        private void tsbSearch_Click(object sender, EventArgs e) {

        }
    }
}
