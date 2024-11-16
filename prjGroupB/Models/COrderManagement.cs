using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjGroupB.Models
{
    public class COrderManagement
    {
        public void creatOrder(COrder order) 
        {
            SqlConnection con = new SqlConnection(@"Data Source=.;Database = dbGroupB; Integrated Security = SSPI");
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            try
            {
                // 插入 Order
                string orderSql = "INSERT INTO tOrders (fUserId, fOrderStatusId, fOrderDate , fShipAddress) OUTPUT INSERTED.fOrderId VALUES (@UserId, 1, @OrderDate , @ShipAddress)";
                SqlCommand orderCmd = new SqlCommand(orderSql, con, transaction);
                orderCmd.Parameters.AddWithValue("@UserId", order.fUserId);
                orderCmd.Parameters.AddWithValue("@OrderDate", order.fOrderDate);
                orderCmd.Parameters.AddWithValue("@ShipAddress", order.fShipAddress);
                object result = orderCmd.ExecuteScalar();

                if (result == null)
                {
                    throw new InvalidOperationException("插入訂單失敗，未能返回訂單ID。");
                }

                int orderId = Convert.ToInt32(result);

                // 插入 OrderDetails
                string detailsSql = "INSERT INTO tOrdersDetails (fOrderId, fProductId, fOrderQty, fUnitPrice) VALUES (@OrderId, @ProductId, @Quantity, @UnitPrice)";
                SqlCommand detailsCmd = new SqlCommand(detailsSql, con, transaction);
                detailsCmd.Parameters.AddWithValue("@OrderId", orderId);
                detailsCmd.Parameters.AddWithValue("@ProductId", order.fProductId);
                detailsCmd.Parameters.AddWithValue("@Quantity", order.fQrderQty);
                detailsCmd.Parameters.AddWithValue("@UnitPrice", order.fUnitPrice);
                detailsCmd.ExecuteNonQuery();

                // 提交交易
                transaction.Commit();
                MessageBox.Show("訂單創建成功！待付款後發貨。");
            }
            catch (Exception ex)
            {
                // 發生錯誤時回滾交易
                transaction.Rollback();
                MessageBox.Show("創建訂單失敗：" + ex.Message);
            }
        }
    }
}
