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
            displayProductsBySql("SELECT * FROM tProduct",false);
        }

        private void displayProductsBySql(string sql, bool isKeyword)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Database = dbGroupB; " + "Integrated Security = SSPI";
            con.Open();
            _da = new SqlDataAdapter(sql, con);
            if (isKeyword)
                _da.SelectCommand.Parameters.Add(new SqlParameter("@keyword","%" + (object)txtQuery.Text + "%"));
            _builder = new SqlCommandBuilder();
            _builder.DataAdapter = _da;

            DataSet ds = new DataSet();
            _da.Fill(ds);
            con.Close();

            dgvProduct.DataSource = ds.Tables[0];            
            resetGridStyle();            
        }
        private void resetGridStyle()
        {
            dgvProduct.Columns[0].Width = 80;
            dgvProduct.Columns[1].Width = 80;
            dgvProduct.Columns[2].Width = 80;
            dgvProduct.Columns[3].Width = 150;
            dgvProduct.Columns[4].Width = 300;
            dgvProduct.Columns[5].Width = 100;
            dgvProduct.Columns[6].Width = 80;
            dgvProduct.Columns[7].Width = 180;
            dgvProduct.Columns[8].Width = 180;

            dgvProduct.Columns["fProductId"].HeaderText = "產品ID";
            dgvProduct.Columns["fProductId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProduct.Columns["fUserId"].HeaderText = "賣家ID";
            dgvProduct.Columns["fUserId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProduct.Columns["fProductCategoryId"].HeaderText = "類別ID";
            dgvProduct.Columns["fProductCategoryId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProduct.Columns["fProductName"].HeaderText = "產品名稱";
            dgvProduct.Columns["fProductName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProduct.Columns["fProductDescription"].HeaderText = "產品描述";
            dgvProduct.Columns["fProductPrice"].HeaderText = "產品價格";
            dgvProduct.Columns["fProductPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvProduct.Columns["fIsOnSales"].HeaderText = "上架狀態";
            dgvProduct.Columns["fProductDateAdd"].HeaderText = "創建日期";
            dgvProduct.Columns["fProductUpdated"].HeaderText = "更新日期";
            dgvProduct.Columns["fStock"].HeaderText = "庫存";
            dgvProduct.Columns["fStock"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            bool isColorChanged = false;

            foreach (DataGridViewRow r in dgvProduct.Rows)
            {
                isColorChanged = !isColorChanged;

                r.DefaultCellStyle.Font = new Font("微軟正黑體", 12);
                r.DefaultCellStyle.BackColor = Color.White;
                if (isColorChanged)
                    r.DefaultCellStyle.BackColor = Color.FromArgb(145, 189, 237);
            }
        }
        private void FrmProducts_Paint(object sender, PaintEventArgs e)
        {
           
            resetGridStyle();
            
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
                displayProductsBySql("SELECT * FROM tProduct", false);
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
            if (dgvProduct.SelectedRows.Count == 0)
            {
                MessageBox.Show("請選擇要刪除的產品。");
                return;
            }

            SqlConnection con = new SqlConnection(@"Data Source=.;Database = dbGroupB; Integrated Security = SSPI");
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            try
            {
                // 獲取選定行的 fProductId 值
                DataGridViewRow selectedRow = dgvProduct.SelectedRows[0];
                int productId = Convert.ToInt32(selectedRow.Cells["fProductId"].Value);

                // 刪除圖片
                string imageSql = "DELETE FROM tProductImage WHERE fProductId = @ProductId";
                SqlCommand imageCmd = new SqlCommand(imageSql, con, transaction);
                imageCmd.Parameters.AddWithValue("@ProductId", productId);
                imageCmd.ExecuteNonQuery();

                // 刪除產品
                string productSql = "DELETE FROM tProduct WHERE fProductId = @ProductId";
                SqlCommand productCmd = new SqlCommand(productSql, con, transaction);
                productCmd.Parameters.AddWithValue("@ProductId", productId);
                productCmd.ExecuteNonQuery();

                transaction.Commit();
                MessageBox.Show("產品和相關圖片已成功刪除。");
            }
            catch (SqlException ex)
            {
                // 檢查是否為外鍵約束錯誤
                if (ex.Number == 547) // 547 是 SQL Server 中的外鍵約束錯誤代碼
                {
                    MessageBox.Show("已有訂單不可刪除。");
                }
                else
                {
                    MessageBox.Show("SQL 錯誤：" + ex.Message);
                }
                // 回滾交易
                transaction.Rollback();
            }
            catch (Exception ex)
            {
                // 處理其他非 SQL 錯誤
                MessageBox.Show("刪除過程中發生錯誤：" + ex.Message);

                // 回滾交易
                transaction.Rollback();
            }
            finally
            {
                con.Close();
            }
            displayProductsBySql("SELECT * FROM tProduct", false);
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
            string sql = "SELECT * FROM tProduct WHERE fProductName LIKE @keyword OR fProductDescription LIKE @keyword";
            displayProductsBySql(sql,true);
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
            displayProductsBySql(sql,false);
        }
    }
}
