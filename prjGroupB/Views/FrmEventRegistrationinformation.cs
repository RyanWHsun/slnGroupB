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
                    r.fEventRegistrationCount LIKE @Keyword ";

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

                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("搜尋時發生錯誤：" + ex.Message);
            }
        }
    }
}