using Attractions.Models;
using prjGroupB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;

namespace Attractions.Views
{
    public partial class FormAttractionImageEditor : Form
    {
        //private string pipe = "np:\\\\.\\pipe\\LOCALDB#B5FE6A17\\tsql\\query;";

        // 圖片
        private int _imageIndex = 0;
        private CAttractionImage _attractionImage;
        public DialogResult isOk { get; set; }
        public FormAttractionImageEditor()
        {
            InitializeComponent();
        }

        public CAttractionImage attractionImage
        {
            get
            {
                if (_attractionImage == null) _attractionImage = new CAttractionImage();
                if (int.TryParse(fbAttractionId.fieldValue, out int attractionId)) _attractionImage.fAttractionId = attractionId;
                else _attractionImage.fAttractionId = 0;

                // pictureBox 的圖片轉換型別為 byte[]

                //if (pbImage.Image != null) {
                //    using (MemoryStream ms = new MemoryStream()) {
                //        pbImage.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                //        byte[] imageBytes = ms.ToArray();

                //        if (_attractionImage.fImage == null) _attractionImage.fImage = new List<byte[]>();
                //        _attractionImage.fImage.Add(imageBytes);
                //    }
                //}

                return _attractionImage;
            }
            set
            {
                _attractionImage = value;
                fbAttractionId.fieldValue = _attractionImage.fAttractionId.ToString();
                if (_attractionImage.fImage != null)
                {
                    try
                    {
                        Stream streamImage = new MemoryStream(_attractionImage.fImage[0]);
                        pbImage.Image = Bitmap.FromStream(streamImage);
                    }
                    catch { }
                }
            }
        }

        private void pbImage_DoubleClick(object sender, EventArgs e)
        {
            // 只有 *.png 或 *.jpg 的檔案可以被使用者看到並選取
            openFileDialog1.Filter = "景點照片(*.png)|*.png|景點照片(*.jpg)|*.jpg";
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            pbImage.Image = Bitmap.FromFile(openFileDialog1.FileName);

            // 串流
            FileStream imgStream = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(imgStream);

            if (this._attractionImage == null) this._attractionImage = new CAttractionImage();
            if (this._attractionImage.fImage == null) this._attractionImage.fImage = new List<byte[]>();
            this._attractionImage.fImage.Add(reader.ReadBytes((int)imgStream.Length));
            reader.Close();
            imgStream.Close();
        }

        private void btnPreviousImage_Click(object sender, EventArgs e)
        {
            //this._imageIndex--;
            //if (this._attractionImage == null || this._attractionImage.fImage == null) return;
            //if (this._imageIndex < 0) this._imageIndex = 0;
            //showSavedImage(this._attractionImage.fAttractionId, this._imageIndex);
        }

        private void btnNextImage_Click(object sender, EventArgs e)
        {
            //this._imageIndex++;
            //if (this._attractionImage == null || this._attractionImage.fImage == null) return;
            //if (this._imageIndex >= this._attractionImage.fImage.Count) this._imageIndex -= 1;
            //showSavedImage(this._attractionImage.fAttractionId, this._imageIndex);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.isOk = DialogResult.OK;
            Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        // 顯示已儲存的圖片
        public void showSavedImage(int id, int index)
        {
            if (this._attractionImage.fImage != null) this._attractionImage.fImage.Clear();
            // 連線
            //string connectionString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True;";
            string connectionString = @"Data Source = .; Initial Catalog = dbGroupB; Integrated Security = True;";

            // SQL 查詢語句
            string query = "SELECT fImage FROM tAttractionImages WHERE fAttractionImageId = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (this._attractionImage.fImage == null)
                            this._attractionImage.fImage = new List<byte[]>();
                        while (reader.Read())
                        {
                            // 取得圖片資料 
                            this._attractionImage.fImage.Add(reader["fImage"] as byte[]);
                        }
                        if (this._attractionImage.fImage.Count > 0)
                        {

                            // 將二進制資料轉換成圖片
                            using (MemoryStream ms = new MemoryStream(this._attractionImage.fImage[index]))
                            {
                                pbImage.Image = Image.FromStream(ms);
                            }

                        }
                    }
                }
            }
        }
    }
}
