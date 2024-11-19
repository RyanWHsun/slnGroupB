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
using static System.Collections.Specialized.BitVector32;

namespace prjGroupB.Views
{
    public partial class FrmEventsList : Form
    {
        private SqlDataAdapter _da;
        private DataSet _ds = null;
        private DataTable _eventTable;
        private SqlCommandBuilder _builder;
        private int _position = -1;

        public class ComboBoxItem
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public override string ToString() => Name;
        }

        public FrmEventsList()
        {
            InitializeComponent();
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FrmEventsEditor f = new FrmEventsEditor();
            f.ShowDialog();
            if (f.IsOk == DialogResult.OK)
            {
                LoadEvents();

                MessageBox.Show("活動已成功儲存");
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DeleteEvent();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            string keyword = toolStripTextBox1.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                // 如果關鍵字為空，重新載入全部資料
                displayEventsBySql("SELECT * FROM tEvents", false);
            }
            else
            {
                // 根據關鍵字搜尋
                string sql = @"
            SELECT * FROM tEvents
            WHERE fEventName LIKE @K_KEYWORD
            OR fEventLocation LIKE @K_KEYWORD";

                displayEventsBySql(sql, true);
            }
        }

        private void FrmEventsList_Load(object sender, EventArgs e)

        {
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
            LoadCategoriesIntoComboBox();
            LoadEvents();
            CustomizeDataGridView();
            CustomizeDataGridViewRowColors();
            displayEventsBySql("SELECT * FROM tEvents ", false);
            DataTable dt = _ds.Tables[0];
            if (dt != null)
            {
                dataGridView1.DataSource = dt;
            }
        }

        private void LoadCategoriesIntoComboBox()
        {
            string query = "SELECT fEventCategoryId, fEventCategoryName FROM tEventCategories";
            using (var conn = new SqlConnection(@"Data Source=.;Initial Catalog=dbGroupB;Integrated Security=True;"))
            {
                conn.Open();
                using (var cmd = new SqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    // 將第一個 ComboBox 項目設置為 "全部"
                    ComboBoxItem allItem = new ComboBoxItem { Id = -1, Name = "全部" };
                    comboBox1.Items.Add(allItem);

                    while (reader.Read())
                    {
                        ComboBoxItem item = new ComboBoxItem
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                        comboBox1.Items.Add(item);
                    }
                }
            }

            comboBox1.SelectedIndex = 0; // 預設選中第一項 "全部"
        }

        private void LoadEvents()
        {
            string connectionString = @"Data Source=.;Initial Catalog=dbGroupB;Integrated Security=True;";
            string query = "SELECT * FROM tEvents";

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                _da = new SqlDataAdapter(query, conn);
                SqlCommandBuilder builder = new SqlCommandBuilder(_da);
                _da.UpdateCommand = new SqlCommand();

                // 確保自動生成的 UpdateCommand 是正確的
                _da.UpdateCommand = builder.GetInsertCommand();
                _da.InsertCommand = builder.GetInsertCommand();
                _da.DeleteCommand = builder.GetDeleteCommand();

                _ds = new DataSet();
                _da.Fill(_ds, "tEvents");
                _eventTable = _ds.Tables["tEvents"];
                _eventTable.PrimaryKey = new DataColumn[] { _eventTable.Columns["fEventId"] };
                dataGridView1.DataSource = _eventTable; // 綁定 DataTable 到 DataGridView
            }
        }

        private void FrmEventsList_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (dataGridView1.DataSource is DataTable dt)
            {
                try
                {
                    _da?.Update(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"儲存資料時發生錯誤：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            CustomizeDataGridView();
            CustomizeDataGridViewRowColors();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                _position = e.RowIndex; // 更新當前選中的行索引
            }
            else
            {
                _position = -1; // 重設為無效值
            }
        }

        private void FrmEventsList_Paint(object sender, PaintEventArgs e)
        {
            CustomizeDataGridView();
            CustomizeDataGridViewRowColors();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (_position < 0)
            {
                MessageBox.Show("請選擇要修改的項目！");
                return;
            }

            DataRow row = _eventTable.Rows[_position];
            FrmEventsEditor f = new FrmEventsEditor
            {
                Event = new CEvents
                {
                    fEventId = Convert.ToInt32(row["fEventId"]),
                    fEventName = row["fEventName"].ToString(),
                    fEventDescription = row["fEventDescription"].ToString(),
                    fEventStartDate = row["fEventStartDate"].ToString(),
                    fEventEndDate = row["fEventEndDate"].ToString(),
                    fEventLocation = row["fEventLocation"].ToString(),
                    fEventActivityfee = Convert.ToDecimal(row["fEventActivityfee"]),
                    fEventURL = row["fEventURL"].ToString(),
                    fEventCreatedDate = Convert.ToDateTime(row["fEventCreatedDate"]),
                    fEventUpdatedDate = Convert.ToDateTime(row["fEventUpdatedDate"])
                }
            };

            f.ShowDialog();
            if (f.IsOk == DialogResult.OK)
            {
                row["fEventName"] = f.Event.fEventName;
                row["fEventDescription"] = f.Event.fEventDescription;
                row["fEventStartDate"] = f.Event.fEventStartDate;
                row["fEventEndDate"] = f.Event.fEventEndDate;
                row["fEventLocation"] = f.Event.fEventLocation;
                row["fEventActivityfee"] = f.Event.fEventActivityfee;
                row["fEventURL"] = f.Event.fEventURL;
                row["fEventUpdatedDate"] = DateTime.Now;

                //if (row.RowState == DataRowState.Unchanged)
                //{
                //    row.SetModified(); // 確保狀態為修改
                //}

                //try
                //{
                //    _da.Update(_eventTable);// 更新資料庫
                //    _eventTable.AcceptChanges();
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show($"更新資料時發生錯誤：{ex.Message}");
                //}

                LoadEvents(); // 重新載入資料
                MessageBox.Show("修改成功！");
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            {
                DataTable dt = dataGridView1.DataSource as DataTable;

                if (dt == null) return;

                // 建立 DataView 並按更新日期排序
                DataView dv = new DataView(dt);
                dv.Sort = "fEventStartDate ASC"; // 如果需要降序，改為 DESC

                // 更新排序後的結果到 DataGridView
                dataGridView1.DataSource = dv.ToTable();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is ComboBoxItem selectedCategory)
            {
                int categoryId = selectedCategory.Id;

                if (categoryId == -1) // 如果選擇的是 "全部"
                {
                    LoadEvents(); // 加載所有活動
                }
                else
                {
                    SearchEventsByCategory(categoryId); // 按類別篩選
                }
            }
        }

        private void SearchEventsByCategory(int categoryId)
        {
            string query = @"
        SELECT e.*
        FROM tEvents e
        JOIN tEventCategoryMapping m ON e.fEventId = m.fEventId
        WHERE m.fEventCategoryId = @CategoryId";

            using (var conn = new SqlConnection(@"Data Source=.;Initial Catalog=dbGroupB;Integrated Security=True;"))
            {
                conn.Open();
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);

                    var adapter = new SqlDataAdapter(cmd);
                    var table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table; // 更新 DataGridView 顯示
                }
            }
        }

        //private void InitializeDataAdapter()
        //{
        //    string connectionString = @"Data Source=.;Initial Catalog=dbGroupB;Integrated Security=True;";
        //    string selectQuery = "SELECT * FROM tEvents";

        //    SqlConnection connection = new SqlConnection(connectionString);

        //    // 初始化 SqlDataAdapter
        //    _da = new SqlDataAdapter(selectQuery, connection);

        //    // 自動產生 InsertCommand、UpdateCommand 和 DeleteCommand
        //    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(_da);

        //    _ds = new DataSet();
        //    _da.Fill(_ds, "tEvents");
        //    _eventTable = _ds.Tables["tEvents"];

        //    // 設置主鍵（假設 fEventId 是主鍵）
        //    _eventTable.PrimaryKey = new DataColumn[] { _eventTable.Columns["fEventId"] };
        //}

        private void displayEventsBySql(string sql, bool isKeyword)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=dbGroupB;Integrated Security=True;"))
            {
                con.Open();
                _da = new SqlDataAdapter(sql, con);

                if (isKeyword)
                {
                    // 使用參數化查詢避免 SQL Injection
                    _da.SelectCommand.Parameters.Add(new SqlParameter("@K_KEYWORD", "%" + toolStripTextBox1.Text.Trim() + "%"));
                }

                _builder = new SqlCommandBuilder(_da);
                _builder.DataAdapter = _da;

                _ds = new DataSet();
                _da.Fill(_ds);

                // 更新 DataGridView 的資料來源
                dataGridView1.DataSource = _ds.Tables[0];
                CustomizeDataGridView();
                CustomizeDataGridViewRowColors();
            }
        }

        private void DeleteEvent()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    // 從選中的資料列獲取活動 ID
                    int eventId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["fEventId"].Value);

                    // 定義刪除的 SQL 語句
                    string deleteImageQuery = "DELETE FROM tEventImage WHERE fEventId = @fEventId"; // 刪除圖片
                    string deleteEventQuery = "DELETE FROM tEvents WHERE fEventId = @fEventId"; // 刪除活動

                    string connectionString = @"Data Source=.;Initial Catalog=dbGroupB;Integrated Security=True;";

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        // 開啟交易
                        using (SqlTransaction transaction = conn.BeginTransaction())
                        {
                            try
                            {
                                // Step 1: 刪除關聯的圖片資料
                                using (SqlCommand cmd = new SqlCommand(deleteImageQuery, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@fEventId", eventId);
                                    int imageRowsAffected = cmd.ExecuteNonQuery();
                                    Console.WriteLine($"刪除了 {imageRowsAffected} 行圖片資料");
                                }

                                // Step 2: 刪除活動資料
                                using (SqlCommand cmd = new SqlCommand(deleteEventQuery, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@fEventId", eventId);
                                    int eventRowsAffected = cmd.ExecuteNonQuery();
                                    Console.WriteLine($"刪除了 {eventRowsAffected} 行活動資料");
                                }

                                // 提交交易
                                transaction.Commit();
                                MessageBox.Show("活動及相關圖片已成功刪除！");
                            }
                            catch (Exception ex)
                            {
                                // 回滾交易
                                transaction.Rollback();
                                MessageBox.Show($"刪除活動時發生錯誤：{ex.Message}");
                            }
                        }
                    }

                    // 刷新資料
                    LoadEvents();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"刪除活動時發生錯誤：{ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("請選擇要刪除的活動！");
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.ColumnIndex < dataGridView1.Columns.Count)
            {
                // 確保欄位名稱匹配
                if (dataGridView1.Columns[e.ColumnIndex].Name == "fEventStartDate" ||
                    dataGridView1.Columns[e.ColumnIndex].Name == "fEventEndDate")
                {
                    if (e.Value != null && DateTime.TryParse(e.Value.ToString(), out DateTime dateValue))
                    {
                        // 格式化日期為 "yyyy-MM-dd"
                        e.Value = dateValue.ToString("yyyy-MM-dd");
                        e.FormattingApplied = true;
                    }
                }
            }
        }

        private void CustomizeDataGridView()
        {
            // 字體設置
            dataGridView1.Font = new Font("微軟正黑體", 12);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("微軟正黑體", 14, FontStyle.Bold);

            // 中文標題
            dataGridView1.Columns["fEventId"].HeaderText = "活動編號";
            dataGridView1.Columns["fuserId"].HeaderText = "會員編號";
            dataGridView1.Columns["fEventName"].HeaderText = "活動名稱";
            dataGridView1.Columns["fEventDescription"].HeaderText = "活動描述";
            dataGridView1.Columns["fEventStartDate"].HeaderText = "開始日期";
            dataGridView1.Columns["fEventEndDate"].HeaderText = "結束日期";
            dataGridView1.Columns["fEventLocation"].HeaderText = "活動地點";
            dataGridView1.Columns["fEventCreatedDate"].HeaderText = "創建日期";
            dataGridView1.Columns["fEventUpdatedDate"].HeaderText = "更新日期";
            dataGridView1.Columns["fEventActivityfee"].HeaderText = "活動費用";
            dataGridView1.Columns["fEventURL"].HeaderText = "活動網址";

            // 自動調整列寬
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            dataGridView1.Columns["fEventURL"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void CustomizeDataGridViewRowColors()
        {
            // 偶數行顏色
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.FromArgb(255, 234, 241);
            // 奇數行顏色
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(252, 244, 231);

            // 選中行顏色
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkBlue;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
        }

        private void FrmEventsList_Scroll(object sender, ScrollEventArgs e)
        {
            CustomizeDataGridView();
            CustomizeDataGridViewRowColors();
        }
    }
}