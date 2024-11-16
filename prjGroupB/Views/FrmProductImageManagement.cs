using prjGroupB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjGroupB.Views
{
    
    
    public partial class FrmProductImageManagement : Form
    {        
        private CProductImage _image = new CProductImage();
        private CProduct _product;

        SqlDataAdapter _da;
        SqlCommandBuilder _builder;
        int _position = -1;
        public FrmProductImageManagement()
        {
            InitializeComponent();
        }        
        public CProduct product
        {
            get { return _product; }
            set
            {
                _product = value;
                txtProductId.Text = _product.fProductId.ToString();
                txtProductNamePic.Text = _product.fProductName.ToString();
            }
        }
        public CProductImage image
        {
            get
            {
                _image.fProductId = Convert.ToInt32(txtProductId.Text);
                //_productImage.fImage = this._productImage.fImage;
                return _image;
            }
            set 
            {
                if (_image.fImage != null)
                {
                    try
                    {
                        Stream streamImage = new MemoryStream(_image.fImage);
                        picProductBox.Image = Bitmap.FromStream(streamImage);
                    }
                    catch { }
                }
            }
        }    

        private void btnUpload_Click(object sender, EventArgs e)
        {
            // 檢查 DataGridView 中的行數
            if (dgvProductPic.Rows.Count >= 3)
            {
                MessageBox.Show("單件商品限上傳三張照片。");
                return;
            }

            //設置過濾器，只允許圖片格式
            openFileDialog1.Filter = "Image Files| *.jpg; *.jpeg; *.png; *.bmp";
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            //讀取並顯示圖片
            picProductBox.Image = Bitmap.FromFile(openFileDialog1.FileName);

            //打開文件串流並讀取圖片數據
            FileStream imgStram = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(imgStram);
            this.image.fImage = reader.ReadBytes((int)imgStram.Length);
            reader.Close();
            imgStram.Close();
        }

        private void FrmProductImageManagement_Load(object sender, EventArgs e)
        {
            displayProductsBySql("SELECT * FROM tProductImage WHERE fProductId ="+ txtProductId.Text);
        }

        private void displayProductsBySql(string sql)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Database = dbGroupB; " + "Integrated Security = SSPI";
            con.Open();
            _da = new SqlDataAdapter(sql, con);
            _builder = new SqlCommandBuilder();
            _builder.DataAdapter = _da;

            DataSet ds = new DataSet();
            _da.Fill(ds);
            con.Close();

            dgvProductPic.DataSource = ds.Tables[0];
        }
        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            DataTable dt = dgvProductPic.DataSource as DataTable;
            DataRow row = dt.NewRow();
            row["fProductId"] = Convert.ToInt32(txtProductId.Text);
            row["fImage"] = _image.fImage;
            dt.Rows.Add(row);

            _da.Update(dgvProductPic.DataSource as DataTable);
            displayProductsBySql("SELECT * FROM tProductImage WHERE fProductId =" + txtProductId.Text);
        }

        private void btnDeletePic_Click(object sender, EventArgs e)
        {
            if (_position < 0)
                return;
            DataTable dt = dgvProductPic.DataSource as DataTable;
            DataRow row = dt.Rows[_position];
            row.Delete();
            _da.Update(dgvProductPic.DataSource as DataTable);
        }

        private void dgvProductPic_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            _position = e.RowIndex;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
