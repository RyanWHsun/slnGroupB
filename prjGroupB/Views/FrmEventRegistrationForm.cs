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
        }

        private void FrmEventRegistrationFormList_Load(object sender, EventArgs e)
        {
            int userId = CUserSession.fUserId;

            if (userId > 0)
            {
                CUser currentUser = GetMemberInfo(userId);

                if (currentUser != null)
                {
                    textBox2.Text = currentUser.fUserId.ToString();       // 會員 ID
                    txtMemberName.Text = currentUser.fUserName;           // 姓名
                    txtMemberPhone.Text = currentUser.fUserPhone.ToString(); // 電話
                    txtMemberEmail.Text = currentUser.fUserEmail;         // Email
                }
                else
                {
                    MessageBox.Show("無法載入會員資訊！");
                }
            }
            else
            {
                MessageBox.Show("尚未登入會員！");
                this.Close();
            }
        }

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
                                txtEventStartDate.Text = reader["fEventStartDate"] != DBNull.Value
                                    ? Convert.ToDateTime(reader["fEventStartDate"]).ToString("yyyy-MM-dd")
                                    : string.Empty;

                                txtEventEndDate.Text = reader["fEventEndDate"] != DBNull.Value
                                    ? Convert.ToDateTime(reader["fEventEndDate"]).ToString("yyyy-MM-dd")
                                    : string.Empty;
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
            string query = "SELECT fUserId, fUserName, fUserPhone, fUserEmail FROM tUser WHERE fUserId = @UserId";

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
                                user = new CUser
                                {
                                    fUserId = reader.GetInt32(0),
                                    fUserName = reader.GetString(1),
                                    fUserPhone = reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
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
            decimal eventFee = decimal.Parse(txtEventFee.Text);
            string registrationDate = DateTime.Now.ToString("yyyy-MM-dd"); // 當前日期轉為字串

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
                MessageBox.Show($"報名成功！總費用為：{eventFee * eventRegistrationCount:C}");
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

        
    }
}