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
    public partial class FrmProductEditor : Form
    {
        public DialogResult isOK { get; internal set; }
        private CProduct _product;

        public CProduct product
        {
            get
            {
                {
                    if (_product == null)
                        _product = new CProduct();
                    _product.fProductId = Convert.ToInt32(txtProductId.Text);
                    _product.fUserId = Convert.ToInt32(txtSellerId.Text);
                    _product.fProductCategoryId = Convert.ToInt32(cmbProductCate.SelectedValue);
                    _product.fProductName = txtProductName.Text;
                    _product.fProductDescription = txtDescription.Text;
                    _product.fProductPrice = Convert.ToDecimal(txtPrice.Text);
                    _product.fIsOnSales = chkOnSales.Checked;
                    _product.fProductDateAdd = DateTime.Now.ToString();
                    _product.fStock = Convert.ToInt32(txtStock.Text);
                    return _product;
                }
            }
            set
            {
                _product = value;
                txtProductId.Text = _product.fProductId.ToString();
                txtSellerId.Text = _product.fUserId.ToString();
                txtProductName.Text = _product.fProductName;
                txtDescription.Text = _product.fProductDescription;
                txtPrice.Text = _product.fProductPrice.ToString();
                chkOnSales.Checked = _product.fIsOnSales;
                txtStock.Text = _product.fStock.ToString();
            }
        }

        public FrmProductEditor()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string message = "";
            if (string.IsNullOrEmpty(txtProductName.Text))
                message += "\r\n名稱不可空白";
            if (string.IsNullOrEmpty(txtPrice.Text))
                message += "\r\n價格不可空白";
            if (string.IsNullOrEmpty(txtStock.Text))
                message += "\r\n數量不可空白";
            else if (!isNumber(txtPrice.Text))
                message += "\r\n價格請填數字";
            else
            {
                if (!isNumber(txtStock.Text))
                    message += "\r\n數量請填數字";
            }
            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message);
                return;
            }

            this.isOK = DialogResult.OK;
            Close();
        }

        private bool isNumber(string p)
        {
            try
            {
                double d = Convert.ToDouble(p);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void FrmProductEditor_Load(object sender, EventArgs e)
        {
            LoadProductCategories();

            // 設置 ComboBox 的選中值
            if (_product != null)
            {
                cmbProductCate.SelectedValue = _product.fProductCategoryId;
            }
        }

        private void LoadProductCategories() //讀取資料表的選項並放入下拉選單
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Database = dbGroupB; " + "Integrated Security = SSPI";
            string sql = "SELECT * FROM tProductCategories";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbProductCate.DataSource = dt;
            cmbProductCate.DisplayMember = "fCategoryName"; // 顯示在 ComboBox 中的名稱
            cmbProductCate.ValueMember = "fProductCategoryId"; // 實際選擇的值
            con.Close();
        }
    }
}