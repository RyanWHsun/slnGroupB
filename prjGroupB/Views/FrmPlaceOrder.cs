using prjGroupB.Models;
using prjGroupB.Models.Namespace.A;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace prjGroupB.Views
{
    public partial class FrmPlaceOrder : Form
    {
        public FrmPlaceOrder()
        {
            InitializeComponent();
        }
        private void FrmPlaceOrder_Load(object sender, EventArgs e)
        {
            DisplayProductDetails();
        }
        private List<CProductDisplay> GetProductDetails()
        {
            List<CProductDisplay> productList = new List<CProductDisplay>();

            using (SqlConnection con = new SqlConnection(@"Data Source=.;Database = dbGroupB; Integrated Security = SSPI"))
            {
                string sql = "SELECT p.fProductId, p.fUserId, c.fCategoryName, p.fProductName, p.fProductDescription, p.fProductPrice,p.fStock, " +
                             "MAX(CASE WHEN pi.RowNum = 1 THEN pi.fImage ELSE NULL END) AS fProductImage1, " +
                             "MAX(CASE WHEN pi.RowNum = 2 THEN pi.fImage ELSE NULL END) AS fProductImage2, " +
                             "MAX(CASE WHEN pi.RowNum = 3 THEN pi.fImage ELSE NULL END) AS fProductImage3 " +
                             "FROM tProduct p " +
                             "INNER JOIN tProductCategories c ON p.fProductCategoryId = c.fProductCategoryId " +
                             "LEFT JOIN (SELECT fProductId, fImage, ROW_NUMBER() OVER (PARTITION BY fProductId ORDER BY fProductId) AS RowNum " +
                             "FROM tProductImage) pi ON p.fProductId = pi.fProductId " +
                             "WHERE p.fIsOnSales = 1 " +
                             "GROUP BY p.fProductId, p.fProductName, p.fUserId, p.fProductDescription, p.fProductPrice, c.fCategoryName,p.fStock";

                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CProductDisplay d = new CProductDisplay();
                    d.fSellerId = (int)reader["fUserId"];
                    d.fProductId = (int)reader["fProductId"];
                    d.fProductName = reader["fProductName"].ToString();
                    d.fCategoryName = reader["fCategoryName"].ToString();
                    d.fProductDescription = reader["fProductDescription"].ToString();
                    d.fPrice = (decimal)reader["fProductPrice"];
                    d.fStock = (int)reader["fStock"];
                    d.fProductImage1 = reader["fProductImage1"] != DBNull.Value ? (byte[])reader["fProductImage1"] : null;
                    d.fProductImage2 = reader["fProductImage2"] != DBNull.Value ? (byte[])reader["fProductImage2"] : null;
                    d.fProductImage3 = reader["fProductImage3"] != DBNull.Value ? (byte[])reader["fProductImage3"] : null;
                    productList.Add(d);
                }
                con.Close();
            }
            return productList;
        }

        private void DisplayProductDetails()
        {
            List<CProductDisplay> productList = GetProductDetails();
            dgvProductDisplay.DataSource = productList;

            restGridView();
        }

        private void restGridView()
        {
            // 設置 DataGridView 列的屬性            
            dgvProductDisplay.Columns["fSellerId"].HeaderText = "賣家ID";
            dgvProductDisplay.Columns["fCategoryName"].HeaderText = "類別名稱";
            dgvProductDisplay.Columns["fProductName"].HeaderText = "產品名稱";
            dgvProductDisplay.Columns["fProductDescription"].HeaderText = "產品描述";
            dgvProductDisplay.Columns["fPrice"].HeaderText = "產品價格";
            dgvProductDisplay.Columns["fStock"].HeaderText = "庫存數量";
            dgvProductDisplay.Columns["fProductId"].Visible = false;
            dgvProductDisplay.Columns["fProductImage1"].HeaderText = "照片1";
            dgvProductDisplay.Columns["fProductImage2"].HeaderText = "照片2";
            dgvProductDisplay.Columns["fProductImage3"].HeaderText = "照片3";

            dgvProductDisplay.Columns[0].Width = 15;
            dgvProductDisplay.Columns[1].Width = 0;
            dgvProductDisplay.Columns[2].Width = 40;
            dgvProductDisplay.Columns[3].Width = 50;
            dgvProductDisplay.Columns[4].Width = 80;
            dgvProductDisplay.Columns[5].Width = 50;
            dgvProductDisplay.Columns[6].Width = 50;
            dgvProductDisplay.Columns[7].Width = 100;
            dgvProductDisplay.Columns[8].Width = 100;
            dgvProductDisplay.Columns[9].Width = 100;

            dgvProductDisplay.Columns["fSellerId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProductDisplay.Columns["fCategoryName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProductDisplay.Columns["fProductDescription"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProductDisplay.Columns["fProductName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProductDisplay.Columns["fPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvProductDisplay.Columns["fStock"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            int[] imageColumnIndexes = { 7, 8, 9 };
            foreach (int index in imageColumnIndexes)
            {
                if (dgvProductDisplay.Columns[index] is DataGridViewImageColumn imageColumn)
                {
                    // 設置圖片列的 ImageLayout 為 Zoom
                    imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
                }
            }
            dgvProductDisplay.Refresh();            
        }
        private void btnQuery_Click(object sender, EventArgs e)
        {
            string keyword = txtQuery.Text.Trim();

            // 過濾 DataGridView 中的資料
            List<CProductDisplay> filteredList = GetProductDetails()
                .Where(p => p.fProductName.Contains(keyword) ||
                            p.fCategoryName.Contains(keyword) ||
                            p.fProductDescription.Contains(keyword))
                .ToList();

            dgvProductDisplay.DataSource = filteredList;
        }
        private void dgvProductDisplay_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProductDisplay.SelectedRows.Count > 0)
            {
                // 獲取選中的行
                DataGridViewRow row = dgvProductDisplay.SelectedRows[0];
                // 將選中的行資料填充到 TextBox 中
                txtProductName.Text = row.Cells["fProductName"].Value.ToString();
                txtUnitPrice.Text = row.Cells["fPrice"].Value.ToString();
            }
        }
        private void btnBuy_Click(object sender, EventArgs e)
        {
            if (dgvProductDisplay.SelectedRows.Count > 0)
            {
                // 獲取選中的行
                DataGridViewRow row = dgvProductDisplay.SelectedRows[0];

                if (int.TryParse(txtQty.Text.Trim(), out int qty))
                {
                    if (qty == 0)
                    {
                        MessageBox.Show("數量不可為0！！\r\n請問是要訂還不要訂？？");
                        return;
                    }
                }
                // 確認訂單訊息
                DialogResult result = MessageBox.Show("確定要下訂嗎？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.OK)
                {
                    // 創建並填充 COrder 物件
                    COrder order = new COrder();
                    order.fUserId = Convert.ToInt32(CUserSession.fUserId);
                    order.fOrderStatusId = 1;
                    order.fOrderDate = DateTime.Now;
                    order.fShipAddress = txtAddress.Text;
                    order.fProductId = Convert.ToInt32(row.Cells["fProductId"].Value);
                    order.fQrderQty = Convert.ToInt32(txtQty.Text);
                    order.fUnitPrice = Convert.ToInt32(row.Cells["fPrice"].Value);
                    order.fProductName = row.Cells["fProductName"].ToString();
                    order.fTotalAmount = order.fQrderQty * order.fUnitPrice;

                    // 將 order 物件傳遞給寫入資料庫的方法
                    COrderManagement c = new COrderManagement();
                    c.creatOrder(order);
                }
                else // 使用者按下「取消」按鈕
                {                    
                    return;
                }                
            }
            else
            {
                MessageBox.Show("請先選擇一筆資料！");
            }
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            if (dgvProductDisplay.SelectedRows.Count > 0)
            {
                // 獲取選中的行
                DataGridViewRow row = dgvProductDisplay.SelectedRows[0];
                // 獲取 fStock 值
                int stock = Convert.ToInt32(row.Cells["fStock"].Value);
                int qty;
                // 檢查 txtQty 的值是否有效且不超過 fStock
                if (int.TryParse(txtQty.Text.Trim(), out qty))
                {
                    if (qty > stock)
                    {
                        MessageBox.Show($"數量不能超過庫存：{stock}");
                        txtQty.Text = "1";
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("請輸入有效的數量！");
                    txtQty.Text = "1"; // 默認為 1
                }
            }
            else
            {
                MessageBox.Show("請先選擇一行資料！");
                txtQty.Text = "1"; // 默認為 1
            }
            CalculatePay();
        }
        private void CalculatePay()
        {
            decimal unitPrice;
            int qty;

            string unitPriceText = txtUnitPrice.Text.Trim();
            string qtyText = txtQty.Text.Trim();

            if (decimal.TryParse(unitPriceText, out unitPrice) && int.TryParse(qtyText, out qty))
            {
                // 使用 Math.Floor 方法來移除小數點部分，並轉換為整數
                int unitPriceInt = (int)Math.Floor(unitPrice);
                int pay = unitPriceInt * qty;

                // 顯示計算結果或進一步處理
                lblPrice.Text = pay.ToString();
            }
            else
            {
                MessageBox.Show("請輸入有效的數字格式！");
            }
        }
        private void txtQty_Click(object sender, EventArgs e)
        {
            txtQty.SelectAll(); //點下去為選取狀態
        }
    }
}
