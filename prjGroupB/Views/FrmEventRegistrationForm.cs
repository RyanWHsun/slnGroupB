using prjGroupB.Models;
using prjGroupB.Views.User;
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
    public partial class FrmEventRegistrationForm : Form
    {
        // private int _currentUserId;

        public FrmEventRegistrationForm()
        {
            InitializeComponent();
            //MessageBox.Show($"收到的 UserId: {userId}");
            //_currentUserId = userId;
        }

        //public FrmEventRegistrationForm()
        //{
        //    InitializeComponent();
        //    _currentUserId = -1; // 設置預設值，例如 -1 表示未登入
        //}

        private void FrmEventRegistrationFormList_Load(object sender, EventArgs e)
        {
            int userId = 8;
            //int event
            // 從資料庫取得活動資訊

            try
            {
                // 從資料庫取得會員資訊
                CUser currentUser = GetMemberInfo(userId);

                // 驗證使用者資訊是否存在
                if (currentUser != null)
                {
                    textBox2.Text = currentUser.fUserId.ToString();
                    txtMemberName.Text = currentUser.fUserName;       // 姓名
                    txtMemberPhone.Text = currentUser.fUserPhone.ToString(); // 電話
                    txtMemberEmail.Text = currentUser.fUserEmail;       // Email
                }
                else
                {
                    MessageBox.Show("無法載入會員資料，請確認會員是否存在！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("載入會員資料時發生錯誤：" + ex.Message);
            }

            txtEventName.TextChanged += txtEventName_TextChanged_1;
        }// 綁定 TextChanged 事件

        private void LoadEventDetails(string partialEventName)
        {
            string connectionString = @"Data Source=.;Initial Catalog=dbGroupB;Integrated Security=True;";
            string query = "SELECT TOP 1 fEventId,fEventStartDate, fEventEndDate, fEventActivityFee FROM tEvents WHERE fEventName LIKE @fEventName";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // 使用模糊搜尋匹配活動名稱
                        cmd.Parameters.AddWithValue("@fEventName", "%" + partialEventName + "%");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                textBox1.Text = reader.GetInt32(0).ToString();
                                txtEventStartDate.Text = reader.GetDateTime(1).ToString("yyyy-MM-dd"); // 開始日期
                                txtEventEndDate.Text = reader.GetDateTime(2).ToString("yyyy-MM-dd");   // 結束日期
                                txtEventFee.Text = reader.GetDecimal(3).ToString();                  // 活動費用
                            }
                            else
                            {
                                txtEventStartDate.Clear();
                                txtEventEndDate.Clear();
                                txtEventFee.Clear();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("載入活動資訊時發生錯誤：" + ex.Message);
            }
        }

        private CUser GetMemberInfo(int userId)
        {
            CUser user = null;
            string connectionString = @"Data Source=.;Initial Catalog=dbGroupB;Integrated Security=True;";
            string query = "SELECT fUserId, fUserName,fUserPhone,fUserEmail FROM tUser WHERE fUserId = @UserId";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // 將讀取的資料填充到 CUser 物件中
                                user = new CUser
                                {
                                    fUserId = reader.GetInt32(0),
                                    fUserName = reader.GetString(1),
                                    fUserPhone = reader.IsDBNull(2) ? 0 : reader.GetInt32(2), // 處理 NULL
                                    fUserEmail = reader.IsDBNull(3) ? string.Empty : reader.GetString(3)
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("無法取得會員資料：" + ex.Message);
            }

            return user;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int eventId = int.Parse(textBox1.Text);
            int userId = int.Parse(textBox2.Text);
            string eventContact = txtEventContact.Text;
            string eventPhone = txtEventPhone.Text;
            int eventRegistrationCount = int.Parse(txtRegistrationCount.Text);
            DateTime registrationDate = DateTime.Now; // 報名日期設定為當前時間

            // 2. 建立 SQL 連線字串
            string connectionString = @"Data Source=.;Initial Catalog=dbGroupB;Integrated Security=True;";

            // 3. SQL Insert 指令
            string query = "INSERT INTO tEventRegistrationForm (fEventId,fUserId, fEventRegistrationCount, fEventContact, fEventContactPhone, fEregistrationDate) " +
                           "VALUES (@EventId,@UserId,@Count, @Contact, @Phone, @Date)";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // 設定參數
                        cmd.Parameters.AddWithValue("@EventId", eventId);
                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.Parameters.AddWithValue("@Count", eventRegistrationCount);
                        cmd.Parameters.AddWithValue("@Contact", eventContact);
                        cmd.Parameters.AddWithValue("@Phone", eventPhone);
                        cmd.Parameters.AddWithValue("@Date", registrationDate);

                        // 執行指令
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("報名成功！");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("資料有誤，請再確認：" + ex.Message);
            }
        }

        private CEvents GetEventInfo(int eventId)
        {
            CEvents eventInfo = null;
            string connectionString = @"Data Source=.;Initial Catalog=dbGroupB;Integrated Security=True;";
            string query = "SELECT fEventId, fEventName, fEventStartDate, fEventEndDate, fEventActivityFee FROM tEvents WHERE fEventId = @EventId";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EventId", eventId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // 建立 CEvents 物件並填入資料
                                eventInfo = new CEvents
                                {
                                    fEventId = reader.GetInt32(0), // 活動 ID
                                    fEventName = reader.GetString(1), // 活動名稱
                                    fEventStartDate = reader.GetDateTime(2).ToString("yyyy-MM-dd"), // 開始日期
                                    fEventEndDate = reader.GetDateTime(3).ToString("yyyy-MM-dd"), // 結束日期
                                    fEventActivityfee = reader.GetDecimal(4) // 活動費用
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("無法載入活動資訊：" + ex.Message);
            }

            return eventInfo;
        }

        private void txtEventName_TextChanged_1(object sender, EventArgs e)
        {
            string enteredEventName = txtEventName.Text;

            if (!string.IsNullOrWhiteSpace(enteredEventName))
            {
                LoadEventDetails(enteredEventName);
            }
            else
            {
                // 清空欄位
                txtEventStartDate.Clear();
                txtEventEndDate.Clear();
                txtEventFee.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}