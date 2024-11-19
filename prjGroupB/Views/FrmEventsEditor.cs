using prjGroupB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjGroupB.Views
{
    public partial class FrmEventsEditor : Form
    {
        public DialogResult IsOk { get; set; }
        private CEvents _Events;
        private CEventImage _EventImage;

        public CEvents Event
        {
            get
            {
                if (_Events == null)
                    _Events = new CEvents();

                _Events.fEventId = string.IsNullOrEmpty(textBox1.Text) ? 0 : Convert.ToInt32(textBox1.Text);
                _Events.fEventName = textBox2.Text;

                if (string.IsNullOrWhiteSpace(_Events.fEventName))
                {
                    MessageBox.Show("活動名稱不能為空！");
                    return null; // 阻止提交
                }

                if (!DateTime.TryParse(textBox5.Text, out DateTime startDate))
                {
                    MessageBox.Show("請輸入有效的開始日期");
                    return null; // 阻止提交
                }
                _Events.fEventStartDate = startDate.ToString();

                if (!DateTime.TryParse(textBox6.Text, out DateTime endDate))
                {
                    MessageBox.Show("請輸入有效的結束日期");
                    return null; // 阻止提交
                }
                _Events.fEventEndDate = endDate.ToString();

                if (!decimal.TryParse(textBox9.Text, out decimal activityFee))
                {
                    MessageBox.Show("活動費用必須為數字");
                    return null; // 阻止提交
                }
                _Events.fEventActivityfee = activityFee;

                _Events.fEventDescription = textBox3.Text;
                _Events.fEventLocation = textBox4.Text;
                _Events.fEventCreatedDate = DateTime.Now;
                _Events.fEventUpdatedDate = DateTime.Now;
                _Events.fEventURL = textBox10.Text;

                return _Events;
            }
            set
            {
                _Events = value;
                textBox1.Text = _Events.fEventId.ToString();
                textBox2.Text = _Events.fEventName;
                textBox3.Text = _Events.fEventDescription.ToString();
                textBox5.Text = _Events.fEventStartDate.ToString();
                textBox6.Text = _Events.fEventEndDate.ToString();
                textBox4.Text = _Events.fEventLocation.ToString();
                textBox7.Text = _Events.fEventCreatedDate.ToString();
                textBox8.Text = _Events.fEventUpdatedDate.ToString();
                textBox9.Text = _Events.fEventActivityfee.ToString();
                textBox10.Text = _Events.fEventURL.ToString();
            }
        }

        public FrmEventsEditor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string message = "";
            if (string.IsNullOrEmpty(textBox2.Text))
                message += "\r\n名稱不可空白";
            if (string.IsNullOrEmpty(textBox4.Text))
                message += "\r\n地點不可空白";
            if (string.IsNullOrEmpty(textBox9.Text))
                message += "\r\n費用不可空白";
            if (string.IsNullOrEmpty(textBox10.Text))
                message += "\r\n網址不可空白";

            if (!decimal.TryParse(textBox9.Text, out decimal fee))
                message += "\r\n費用必須為數字";

            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message, "輸入錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 更新 Event 和 Image 資料
            CEvents eventData = Event;
            CEventImage imageData = Image;

            try
            {
                // 儲存資料
                SaveEventWithImage(eventData, imageData);

                // 更新成功
                this.IsOk = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"儲存時發生錯誤：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "活動照片|*.png;*.jpg";
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            pictureBox1.Image = Bitmap.FromFile(openFileDialog1.FileName);
            FileStream imgStram = new FileStream(openFileDialog1.FileName,
                FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(imgStram);
            this.Image.fEventImage = reader.ReadBytes((int)imgStram.Length);
            reader.Close();
            imgStram.Close();

            //using (FileStream imgStream = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read))
            //{
            //    using (BinaryReader reader = new BinaryReader(imgStream))
            //    {
            //        this.Image.fEventImage = reader.ReadBytes((int)imgStream.Length);
            //    }
            //}
        }

        public CEventImage Image
        {
            get
            {
                if (_EventImage == null)
                    _EventImage = new CEventImage();
                _EventImage.fEventImageId = string.IsNullOrEmpty(textBox11.Text) ? 0 : Convert.ToInt32(textBox11.Text);
                _EventImage.fEventId = string.IsNullOrEmpty(textBox1.Text) ? 0 : Convert.ToInt32(textBox1.Text);
                return _EventImage;
            }
            set
            {
                _EventImage = value;
                textBox11.Text = _EventImage.fEventImageId.ToString();
                textBox1.Text = _EventImage.fEventId.ToString();
                if (_EventImage.fEventImage != null)
                {
                    try
                    {
                        using (Stream streamImage = new MemoryStream(_EventImage.fEventImage))
                        {
                            pictureBox1.Image = Bitmap.FromStream(streamImage);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("圖片讀取錯誤");
                    }
                }
            }
        }

        private void SaveEventWithImage(CEvents eventData, CEventImage imageData)
        {
            string connectionString = @"Data Source=.;Initial Catalog=dbGroupB;Integrated Security=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        if (eventData.fEventId > 0) // 判斷是否有 fEventId，若有則進行更新
                        {
                            // Step 1: 更新活動資訊到 tEvents
                            string updateEventQuery = @"
                        UPDATE tEvents
                        SET 
                            fEventName = @fEventName,
                            fEventDescription = @fEventDescription,
                            fEventStartDate = @fEventStartDate,
                            fEventEndDate = @fEventEndDate,
                            fEventLocation = @fEventLocation,
                            fEventCreatedDate = @fEventCreatedDate,
                            fEventUpdatedDate = @fEventUpdatedDate,
                            fEventActivityfee = @fEventActivityfee,
                            fEventURL = @fEventURL
                        WHERE 
                            fEventId = @fEventId;";

                            using (SqlCommand cmd = new SqlCommand(updateEventQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@fEventId", eventData.fEventId);
                                cmd.Parameters.AddWithValue("@fEventName", eventData.fEventName);
                                cmd.Parameters.AddWithValue("@fEventDescription", eventData.fEventDescription ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@fEventStartDate", eventData.fEventStartDate ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@fEventEndDate", eventData.fEventEndDate ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@fEventLocation", eventData.fEventLocation);
                                cmd.Parameters.AddWithValue("@fEventCreatedDate", eventData.fEventCreatedDate);
                                cmd.Parameters.AddWithValue("@fEventUpdatedDate", eventData.fEventUpdatedDate);
                                cmd.Parameters.AddWithValue("@fEventActivityfee", eventData.fEventActivityfee);
                                cmd.Parameters.AddWithValue("@fEventURL", eventData.fEventURL ?? (object)DBNull.Value);

                                cmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            // Step 2: 新增活動資訊到 tEvents
                            string insertEventQuery = @"
                        INSERT INTO tEvents
                            (fEventName, fEventDescription, fEventStartDate, fEventEndDate,
                             fEventLocation, fEventCreatedDate, fEventUpdatedDate, fEventActivityfee, fEventURL)
                        VALUES
                            (@fEventName, @fEventDescription, @fEventStartDate, @fEventEndDate,
                             @fEventLocation, @fEventCreatedDate, @fEventUpdatedDate, @fEventActivityfee, @fEventURL);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";

                            using (SqlCommand cmd = new SqlCommand(insertEventQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@fEventName", eventData.fEventName);
                                cmd.Parameters.AddWithValue("@fEventDescription", eventData.fEventDescription ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@fEventStartDate", eventData.fEventStartDate ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@fEventEndDate", eventData.fEventEndDate ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@fEventLocation", eventData.fEventLocation);
                                cmd.Parameters.AddWithValue("@fEventCreatedDate", eventData.fEventCreatedDate);
                                cmd.Parameters.AddWithValue("@fEventUpdatedDate", eventData.fEventUpdatedDate);
                                cmd.Parameters.AddWithValue("@fEventActivityfee", eventData.fEventActivityfee);
                                cmd.Parameters.AddWithValue("@fEventURL", eventData.fEventURL ?? (object)DBNull.Value);

                                int eventId = (int)cmd.ExecuteScalar();
                                imageData.fEventId = eventId;
                            }
                        }

                        // Step 3: 插入或更新圖片資訊到 tEventImage（如果圖片存在）
                        if (imageData.fEventImage != null)
                        {
                            string upsertImageQuery = @"
                        IF EXISTS (SELECT 1 FROM tEventImage WHERE fEventId = @fEventId)
                        BEGIN
                            UPDATE tEventImage
                            SET fEventImage = @fEventImage
                            WHERE fEventId = @fEventId;
                        END
                        ELSE
                        BEGIN
                            INSERT INTO tEventImage (fEventId, fEventImage)
                            VALUES (@fEventId, @fEventImage);
                        END";

                            using (SqlCommand cmd = new SqlCommand(upsertImageQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@fEventId", imageData.fEventId);
                                cmd.Parameters.AddWithValue("@fEventImage", imageData.fEventImage);

                                cmd.ExecuteNonQuery();
                            }
                        }

                        // 提交交易
                        transaction.Commit();
                        MessageBox.Show("活動資料已成功儲存！");
                    }
                    catch (Exception ex)
                    {
                        // 回滾交易
                        transaction.Rollback();
                        MessageBox.Show($"儲存過程中發生錯誤：{ex.Message}");
                    }
                }
            }
        }
    }
}