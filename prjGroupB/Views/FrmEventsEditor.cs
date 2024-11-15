using prjGroupB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
                _Events.fEventDescription = textBox3.Text;
                DateTime startDate, endDate;
                if (DateTime.TryParse(textBox5.Text, out startDate))
                    _Events.fEventStartDate = startDate.ToString();
                else
                    MessageBox.Show("請輸入有效的開始日期");

                if (DateTime.TryParse(textBox6.Text, out endDate))
                    _Events.fEventEndDate = endDate.ToString();
                else
                    MessageBox.Show("請輸入有效的結束日期");
                _Events.fEventLocation = textBox4.Text;
                _Events.fEventCreatedDate = DateTime.UtcNow;
                _Events.fEventUpdatedDate = DateTime.UtcNow;
                decimal activityFee;
                if (decimal.TryParse(textBox9.Text, out activityFee))
                    _Events.fEventActivityfee = activityFee;
                else
                    MessageBox.Show("活動費用必須為數字");

                _Events.fEventURL = textBox10.Text;
                return _Events;
            }
            set
            {
                _Events = value;
                textBox1.Text = _Events.fEventId.ToString();
                textBox2.Text = _Events.fEventName;
                textBox3.Text = _Events.fEventDescription.ToString();
                textBox4.Text = _Events.fEventStartDate.ToString();
                textBox5.Text = _Events.fEventEndDate.ToString();
                textBox6.Text = _Events.fEventLocation.ToString();
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
            decimal fee;
            if (!decimal.TryParse(textBox9.Text, out fee))
                message += "\r\n費用必須為數字";

            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message);
                return;
            }

            this.IsOk = DialogResult.OK;
            Close();
        }

        //private bool IsNumber(string p)
        //{
        //    try
        //    {
        //        double d = Convert.ToDouble(p);
        //        return  true;
        //    }
        //    catch { return false; }
        //}

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
            //FileStream imgStram = new FileStream(openFileDialog1.FileName,
            //    FileMode.Open, FileAccess.Read);
            //BinaryReader reader = new BinaryReader(imgStram);
            //this.Image.fEventImage = reader.ReadBytes((int)imgStram.Length);
            //reader.Close();
            //imgStram.Close();

            using (FileStream imgStream = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader reader = new BinaryReader(imgStream))
                {
                    this.Image.fEventImage = reader.ReadBytes((int)imgStream.Length);
                }
            }
        }

        public CEventImage Image
        {
            get
            {
                if (_EventImage == null)
                    _EventImage = new CEventImage();
                return _EventImage;
            }
            set
            {
                _EventImage = value;
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
    }
}