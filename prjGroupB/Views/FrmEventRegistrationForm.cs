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
            string query = "SELECT TOP 1 fEventId, fEventStartDate, fEventEndDate, fEventActivityFee FROM tEvents WHERE fEventName LIKE @fEventName";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@fEventName", "%" + partialEventName + "%");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                textBox1.Text = reader["fEventId"].ToString();
                                txtEventStartDate.Text = reader["fEventStartDate"]?.ToString(); // 直接讀取字串
                                txtEventEndDate.Text = reader["fEventEndDate"]?.ToString();   // 直接讀取字串
                                txtEventFee.Text = reader["fEventActivityFee"]?.ToString();
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
            string registrationDate = DateTime.Now.ToString("yyyy-MM-dd"); // 當前日期轉為字串

            // 檢查日期格式
            //if (!IsValidDate(txtEventStartDate.Text) || !IsValidDate(txtEventEndDate.Text))
            //{
            //    MessageBox.Show("請輸入有效的日期格式（yyyy-MM-dd）！");
            //    return;
            //}

            string connectionString = @"Data Source=.;Initial Catalog=dbGroupB;Integrated Security=True;";
            string query = "INSERT INTO tEventRegistrationForm (fEventId,fUserId, fEventRegistrationCount, fEventContact, fEventContactPhone, fEregistrationDate) " +
                           "VALUES (@EventId,@UserId,@Count, @Contact, @Phone, @Date)";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EventId", eventId);
                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.Parameters.AddWithValue("@Count", eventRegistrationCount);
                        cmd.Parameters.AddWithValue("@Contact", eventContact);
                        cmd.Parameters.AddWithValue("@Phone", eventPhone);
                        cmd.Parameters.AddWithValue("@Date", registrationDate);

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
        private bool IsValidDate(string dateValue)
        {
            if (string.IsNullOrEmpty(dateValue)) return false;
            return DateTime.TryParseExact(dateValue, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out _);
        }
    }
}