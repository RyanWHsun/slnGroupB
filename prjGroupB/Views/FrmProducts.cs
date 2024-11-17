using prjGroupB.Models;
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
    public partial class FrmProducts : Form
    {
        SqlDataAdapter _da;
        SqlCommandBuilder _builder;
        int _position = -1;

        public FrmProducts()
        {
            InitializeComponent();
        }

        private void FrmProducts_Load(object sender, EventArgs e)
        {
            displayProductsBySql("SELECT * FROM tProduct");
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

            dgvProduct.DataSource = ds.Tables[0];
        }

        private void btnCreateProduct_Click(object sender, EventArgs e)
        {
            FrmProductEditor f = new FrmProductEditor();
            f.ShowDialog();

            if (f.isOK == DialogResult.OK)
            {
                DataTable dt = dgvProduct.DataSource as DataTable;
                DataRow row = dt.NewRow();
                row["fUserId"] = f.product.fUserId;
                row["fProductCategoryId"] = f.product.fProductCategoryId;
                row["fProductName"] = f.product.fProductName;
                row["fProductDescription"] = f.product.fProductDescription;
                row["fProductPrice"] = f.product.fProductPrice;
                row["fIsOnSales"] = f.product.fIsOnSales;
                row["fProductDateAdd"] = f.product.fProductDateAdd;
                row["fStock"] = f.product.fStock;
                dt.Rows.Add(row);

                _da.Update(dgvProduct.DataSource as DataTable);
                displayProductsBySql("SELECT * FROM tProduct");
            }
        }

        private void FrmProducts_FormClosed(object sender, FormClosedEventArgs e)
        {
            _da.Update(dgvProduct.DataSource as DataTable);
        }

        private void dgvProduct_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            _position = e.RowIndex;
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_position < 0)
                return;
            DataTable dt = dgvProduct.DataSource as DataTable;
            DataRow row = dt.Rows[_position];
            row.Delete();
            
            _da.Update(dgvProduct.DataSource as DataTable);
        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            if (_position < 0)
                return;
            DataTable dt = dgvProduct.DataSource as DataTable;
            DataRow row = dt.Rows[_position];
            FrmProductEditor f = new FrmProductEditor();
            CProduct x = new CProduct();
            x.fUserId = (int)row["fUserId"];
            x.fProductId = (int)row["fProductId"];
            x.fProductCategoryId = (int)row["fProductCategoryId"];
            x.fProductName = row["fProductName"].ToString();
            x.fProductDescription = row["fProductDescription"].ToString();
            x.fProductPrice = (decimal)row["fProductPrice"];
            x.fIsOnSales = (bool)row["fIsOnSales"];
            x.fProductUpdated = DateTime.Now.ToString();
            x.fStock = (int)row["fStock"];
            f.product = x;
            f.ShowDialog();
            if (f.isOK == DialogResult.OK)
            {
                row["fUserId"] = f.product.fUserId;
                row["fProductCategoryId"] = f.product.fProductCategoryId;
                row["fProductName"] = f.product.fProductName;
                row["fProductDescription"] = f.product.fProductDescription;
                row["fProductPrice"] = f.product.fProductPrice;
                row["fIsOnSales"] = f.product.fIsOnSales;
                row["fProductUpdated"] = f.product.fProductUpdated;
                row["fStock"] = f.product.fStock;
            }
        }
        private void btnPicManagement_Click(object sender, EventArgs e)
        {
            if (_position < 0)
                return;
            DataTable dt = dgvProduct.DataSource as DataTable;
            DataRow row = dt.Rows[_position];
            FrmProductImageManagement f = new FrmProductImageManagement();
            CProduct x = new CProduct();            
            x.fProductId = (int)row["fProductId"];
            x.fProductName = row["fProductName"].ToString();
            f.product = x;
            f.ShowDialog();
        }
        private void btnQuery_Click(object sender, EventArgs e)
        {
            string keyword = txtQuery.Text.Trim();
            string sql = "SELECT * FROM tProduct WHERE ";
            sql += "fProductName LIKE '%" + keyword + "%' ";
            sql += "OR fProductDescription LIKE '%" + keyword + "%'";
            displayProductsBySql(sql);
        }
        private void txtQuery_Click(object sender, EventArgs e)
        {
            txtQuery.Text = string.Empty;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // 清除搜尋條件
            txtQuery.Text = string.Empty;

            // 重新載入所有資料
            string sql = "SELECT * FROM tProduct";
            displayProductsBySql(sql);
        }
    }
}
