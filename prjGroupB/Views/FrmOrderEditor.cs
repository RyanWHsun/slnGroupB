using prjGroupB.Models;
using prjGroupB.Models.Namespace.B;
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
    public partial class FrmOrderEditor : Form
    {
        public FrmOrderEditor()
        {
            InitializeComponent();
        }
        public DialogResult isOK { get; internal set; }
        private COrder _order;

        public COrder order
        {
            get 
            {
                if (_order == null)
                    _order = new COrder();
                _order.fOrderId = Convert.ToInt32(txtOrderId.Text);
                _order.fShipAddress = txtAddress.Text;
                _order.fOrderStatusId = Convert.ToInt32(cmbOrderStatus.SelectedValue);
                // 獲取 ComboBox 的顯示值
                if (cmbOrderStatus.SelectedItem != null) //檢查 ComboBox 的 SelectedItem 是否不為 null。SelectedItem 是 ComboBox 選中的資料。
                {
                    _order.fStatusName = ((DataRowView)cmbOrderStatus.SelectedItem)["fStatusName"].ToString();
                }    //使用了 DataTable 作為資料來源，SelectedItem 的實際類型是 DataRowView。
                else
                {
                    _order.fStatusName = cmbOrderStatus.Text;
                }                
                return _order;
            }            
            set 
            {
                _order = value;
                txtOrderId.Text = _order.fOrderId.ToString();
                txtUserName.Text = _order.fUserName;
                txtOrderDate.Text = _order.fOrderDate.ToString("yyyy-MM-dd");
                txtAddress.Text = _order.fShipAddress;
                cmbOrderStatus.SelectedValue = _order.fOrderStatusId;
                cmbOrderStatus.SelectedItem = _order.fStatusName;
            }        
        }
        private void FrmOrderEditor_Load(object sender, EventArgs e)
        {
            DataTable dt = loadOrderStatus();
            cmbOrderStatus.DataSource = dt;
            cmbOrderStatus.DisplayMember = "fStatusName"; // 顯示在 ComboBox 中的名稱
            cmbOrderStatus.ValueMember = "fOrderStatusId"; // 實際選擇的值

        }
        private DataTable loadOrderStatus() // 讀取資料表的選項並返回 DataTable
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
        private void btnConfirm_Click(object sender, EventArgs e)
        {                  
            this.isOK = DialogResult.OK;
            Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
