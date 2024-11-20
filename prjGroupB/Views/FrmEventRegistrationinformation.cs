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
    public partial class FrmEventRegistrationinformation : Form
    {
        public FrmEventRegistrationinformation()
        {
            InitializeComponent();
        }

        private void FrmEventRegistrationinformation_Load(object sender, EventArgs e)
        {
            LoadRegistrationData();
            CustomizeDataGridView();
            CustomizeDataGridViewRowColors();
        }

        private void LoadRegistrationData()
        {
            string connectionString = @"Data Source=.;Initial Catalog=dbGroupB;Integrated Security=True;";
            string query = @"
    SELECT
        r.fEventRegistrationFormId AS [報名編號],
        m.fUserName AS [會員姓名],
        m.fUserPhone AS [會員電話],
        m.fUserEmail AS [會員Email],
        e.fEventName AS [活動名稱],
        e.fEventStartDate AS [開始日期],
        e.fEventEndDate AS [結束日期],
        e.fEventActivityFee AS [活動費用],
        r.fEventRegistrationCount AS [報名人數],
        r.fEregistrationDate AS [報名日期],
        r.fEventContact AS [聯絡人姓名],
        r.fEventContactPhone AS [聯絡人電話]
    FROM
        tEventRegistrationForm r
    LEFT JOIN
        tUser m ON r.fUserId = m.fUserId
    LEFT JOIN
        tEvents e ON r.fEventId = e.fEventId";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // 動態新增一個計算欄位
                        dataTable.Columns.Add("總費用", typeof(decimal), "[活動費用] * [報名人數]");

                        // 將資料綁定到 DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("載入報名資料時發生錯誤：" + ex.Message);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // 確認資料行名稱是否正確
                int registrationId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

                string connectionString = @"Data Source=.;Initial Catalog=dbGroupB;Integrated Security=True;";
                string query = "DELETE FROM tEventRegistrationForm WHERE fEventRegistrationFormId = @fEventRegistrationFormId";

                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            // 修正參數名稱
                            cmd.Parameters.AddWithValue("@fEventRegistrationFormId", registrationId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("資料已刪除！");
                    LoadRegistrationData(); // 刷新資料
                }
                catch (Exception ex)
                {
                    MessageBox.Show("刪除資料時發生錯誤：" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("請選擇要刪除的資料！");
            }
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            string connectionString = @"Data Source=.;Initial Catalog=dbGroupB;Integrated Security=True;";
            string query = @"
                SELECT
                    r.fEventRegistrationFormId AS [報名編號],
                    m.fUserName AS [會員姓名],
                    m.fUserPhone AS [會員電話],
                    m.fUserEmail AS [會員Email],
                    e.fEventName AS [活動名稱],
                    e.fEventStartDate AS [開始日期],
                    e.fEventEndDate AS [結束日期],
                    e.fEventActivityFee AS [活動費用],
                    r.fEventRegistrationCount AS [報名人數],
                    r.fEregistrationDate AS [報名日期],
                    r.fEventContact AS [聯絡人姓名],
                    r.fEventContactPhone AS [聯絡人電話]
                FROM
                    tEventRegistrationForm r
                LEFT JOIN
                    tUser m ON r.fUserId = m.fUserId
                LEFT JOIN
                    tEvents e ON r.fEventId = e.fEventId
                WHERE
                    m.fUserName LIKE @Keyword "; 
                    
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataTable.Columns.Add("總費用", typeof(decimal), "[活動費用] * [報名人數]");
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("搜尋時發生錯誤：" + ex.Message);
            }
        }

        private void CustomizeDataGridView()
        {
            // 字體設置
            dataGridView1.Font = new Font("微軟正黑體", 12);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("微軟正黑體", 14, FontStyle.Bold);

            // 自動調整列寬
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            dataGridView1.Columns["總費用"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black; // 設置文字顏色
            dataGridView1.EnableHeadersVisualStyles = false; // 確保顏色生效
        }

        private void FrmEventRegistrationinformation_Scroll(object sender, ScrollEventArgs e)
        {
            CustomizeDataGridView();
            CustomizeDataGridViewRowColors();
        }
    }
}