using prjGroupB.Models;
using prjGroupB.Models.Namespace.B;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjGroupB.Views
{
    public partial class FrmOrders : Form
    {
        public FrmOrders()
        {
            InitializeComponent();
        }

        private void FrmOrders_Load(object sender, EventArgs e)
        {
            // 添加 CheckBox 列
            DataGridViewCheckBoxColumn chkColumn = new DataGridViewCheckBoxColumn();
            chkColumn.HeaderText = "選取";
            chkColumn.Name = "chkSelect";
            dgvOrders.Columns.Insert(0, chkColumn);

            // 設置 CurrentCellDirtyStateChanged 事件
            dgvOrders.CurrentCellDirtyStateChanged += new EventHandler(dgvOrders_CurrentCellDirtyStateChanged);

            DataTable dt = loadOrderStatus();
            cmbOrderStatus.ComboBox.DataSource = dt;
            cmbOrderStatus.ComboBox.DisplayMember = "fStatusName"; // 顯示在 ComboBox 中的名稱
            cmbOrderStatus.ComboBox.ValueMember = "fOrderStatusId"; // 實際選擇的值
            cmbOrderStatus.ComboBox.SelectedIndex = -1; // 設置初始選定項為空

            DisplayOrders();
        }
        private void FrmOrders_Paint(object sender, PaintEventArgs e)
        {
            restGridStyle();
        }
        public DataTable loadOrderStatus() // 讀取資料表的選項並返回 DataTable
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection(@"Data Source=.;Database = dbGroupB; Integrated Security = SSPI");
            string sql = "SELECT * FROM tOrderStatus";
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(dt);
                con.Close();
            }
            return dt;
        }
        private void dgvOrders_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvOrders.IsCurrentCellDirty)
            {
                dgvOrders.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private List<COrder> GetOrders(string keyword, int fOrderStatusId = -1)
        {
            List<COrder> orderList = new List<COrder>();
            SqlConnection con = new SqlConnection(@"Data Source=.;Database = dbGroupB; Integrated Security = SSPI");

            // 動態構建 SQL 查詢語句

            string sql = "SELECT o.fOrderId, u.fUserName, o.fOrderDate, o.fShipAddress, os.fOrderStatusId, os.fStatusName, SUM(od.fOrderQty * od.fUnitPrice ) as TotalAmount " +
                "FROM tOrders o " +
                "JOIN tOrderStatus os ON o.fOrderStatusId = os.fOrderStatusId " +
                "JOIN tUser u ON o.fUserId = u.fUserId " +
                "JOIN tOrdersDetails od ON o.fOrderId = od.fOrderId " +
                "WHERE 1=1 ";

            // 添加搜尋條件
            if (!string.IsNullOrEmpty(keyword))
            {
                sql += "AND (u.fUserName LIKE @Keyword OR o.fOrderId LIKE @Keyword OR os.fStatusName LIKE @Keyword) ";
            }
            // 篩選條件
            if (fOrderStatusId != -1)
            {
                sql += "AND o.fOrderStatusId = @OrderStatusId ";
            }
            sql += "GROUP BY o.fOrderId, u.fUserName, o.fOrderDate, o.fShipAddress, os.fOrderStatusId , os.fStatusName ";


            SqlCommand cmd = new SqlCommand(sql, con);
            if (!string.IsNullOrEmpty(keyword))
            {
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
            }
            if (fOrderStatusId != -1)
            {
                cmd.Parameters.AddWithValue("@OrderStatusId", fOrderStatusId);
            }

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                COrder o = new COrder();
                o.fOrderId = (int)reader["fOrderId"];
                o.fUserName = reader["fUserName"] != DBNull.Value ? reader["fUserName"].ToString() : null;
                o.fOrderDate = Convert.ToDateTime(reader["fOrderDate"]);
                o.fShipAddress = reader["fShipAddress"] != DBNull.Value ? reader["fShipAddress"].ToString() : null;
                o.fOrderStatusId = (int)reader["fOrderStatusId"];
                o.fStatusName = reader["fStatusName"].ToString();
                o.fTotalAmount = (decimal)reader["TotalAmount"];
                orderList.Add(o);
            }
            con.Close();
            return orderList;
        }
        private void DisplayOrders(string keyword = "" , int fOrderStatusId = -1) //若是沒有要搜尋就用空字串帶入
        {
            List<COrder> orderList = GetOrders(keyword, fOrderStatusId);
            dgvOrders.DataSource = orderList;
            restGridStyle();
        }

        private void restGridStyle()
        {
            dgvOrders.Columns["fOrderId"].HeaderText = "訂單ID";
            dgvOrders.Columns["fOrderId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrders.Columns["fUserName"].HeaderText = "買家姓名";
            dgvOrders.Columns["fUserName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrders.Columns["fOrderDate"].HeaderText = "訂購日期";
            dgvOrders.Columns["fOrderDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrders.Columns["fShipAddress"].HeaderText = "寄送地址";
            dgvOrders.Columns["fOrderStatusId"].HeaderText = "狀態ID";
            dgvOrders.Columns["fOrderStatusId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrders.Columns["fStatusName"].HeaderText = "訂單狀態";
            dgvOrders.Columns["fStatusName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrders.Columns["fTotalAmount"].HeaderText = "訂單金額";
            dgvOrders.Columns["fTotalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvOrders.Columns[0].Width = 50;
            dgvOrders.Columns[1].Width = 100;
            dgvOrders.Columns[2].Width = 120;
            dgvOrders.Columns[3].Width = 300;
            dgvOrders.Columns[4].Width = 400;
            dgvOrders.Columns[5].Width = 100;
            dgvOrders.Columns[6].Width = 120;

            bool isColorChanged = false;

            foreach (DataGridViewRow r in dgvOrders.Rows)
            {
                isColorChanged = !isColorChanged;

                r.DefaultCellStyle.Font = new Font("微軟正黑體", 12);
                r.DefaultCellStyle.BackColor = Color.White;
                r.DefaultCellStyle.SelectionBackColor = Color.OrangeRed;
                if (isColorChanged)
                    r.DefaultCellStyle.BackColor = Color.FromArgb(255, 225, 201);
            }
        }

        private void dgvOrders_CurrentCellChanged(object sender, EventArgs e)
        {      //強制更新 CheckBox 的值
            if (dgvOrders.IsCurrentCellDirty)
            {
                dgvOrders.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        private void btnEditOrder_Click(object sender, EventArgs e)
        {
            int selectedRowCount = 0; // 初始化選中行的計數器
            DataGridViewRow selectedRow = null; // 初始化選中行的引用
            foreach (DataGridViewRow row in dgvOrders.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells["chkSelect"];
                bool isChecked = Convert.ToBoolean(chk.Value); // 確認 CheckBox 是否被選中
                if (isChecked)
                {
                    selectedRowCount++; // 選中行的計數器加一
                    selectedRow = row; // 設置選中行的引用
                }
            }
            if (selectedRowCount > 1)
            {
                // 如果沒有或多於一個 CheckBox 被選中，顯示錯誤訊息
                MessageBox.Show("編輯訂單只能選一筆資料！");
                return;
            }
            if (selectedRowCount == 0)
            {
                // 如果沒有或多於一個 CheckBox 被選中，顯示錯誤訊息
                MessageBox.Show("編輯訂單至少要選一筆資料！");
                return;
            }
            // 處理選中的行資料
            COrder x = new COrder();
            x.fOrderId = Convert.ToInt32(selectedRow.Cells["fOrderId"].Value);
            x.fUserName = selectedRow.Cells["fUserName"].Value != null && selectedRow.Cells["fUserName"].Value != DBNull.Value
                     ? selectedRow.Cells["fUserName"].Value.ToString()
                     : null;
            x.fOrderDate = Convert.ToDateTime(selectedRow.Cells["fOrderDate"].Value);
            x.fShipAddress = selectedRow.Cells["fShipAddress"].Value.ToString();
            x.fStatusName = selectedRow.Cells["fStatusName"].Value.ToString();
            x.fOrderStatusId = Convert.ToInt32(selectedRow.Cells["fOrderStatusId"].Value);

            FrmOrderEditor f = new FrmOrderEditor();
            f.order = x;
            f.ShowDialog();
            if (f.isOK == DialogResult.OK)
            {
                new COrderManagement().UpdateOrder(f.order);
                DisplayOrders();
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string keyword = txtQuery.Text.Trim();
            DisplayOrders(keyword);
        }

        private void txtQuery_Click(object sender, EventArgs e)
        {
            txtQuery.Text = string.Empty;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // 清除搜尋條件
            txtQuery.Text = string.Empty;
            cmbOrderStatus.SelectedIndex = -1;
            DisplayOrders("");
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {            
            int fOrderStatusId = cmbOrderStatus.ComboBox.SelectedValue != null ? Convert.ToInt32(cmbOrderStatus.ComboBox.SelectedValue) : -1;
            DisplayOrders("", fOrderStatusId);
        }
    }
}
