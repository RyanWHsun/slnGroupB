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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace prjGroupB.Views
{
    public partial class FrmEventImage : Form
    {
        private CEvents cEvents;
        private CEventImage _selected;

        

        public FrmEventImage()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string sql = @"
    SELECT 
        e.fEventId, 
        e.fEventName, 
        e.fEventLocation, 
        e.fEventStartDate, 
        e.fEventEndDate, 
        e.fEventDescription,
        e.fUserId,
        e.fEventCreatedDate,
        e.fEventUpdatedDate,
        e.fEventActivityfee,
        e.fEventURL,
        ei.fEventImageId,
        ei.fEventImage
    FROM tEventImage ei
    LEFT JOIN tEvents e ON ei.fEventId = e.fEventId
    WHERE 
        CAST(ei.fEventImageId AS NVARCHAR) LIKE @K_KEYWORD OR
        e.fEventLocation LIKE @K_KEYWORD OR
        e.fEventName LIKE @K_KEYWORD";

            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=dbGroupB;Integrated Security=True;"))
                {
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(sql, con);
                    da.SelectCommand.Parameters.Add(new SqlParameter("@K_KEYWORD", SqlDbType.NVarChar)
                    {
                        Value = "%" + txtSearch.Text.Trim() + "%"
                    });

                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    flowLayoutPanel1.Controls.Clear(); // 清空之前的控制項

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        // 初始化 CEvents 物件
                        CEvents events = new CEvents
                        {
                            fEventId = Convert.ToInt32(row["fEventId"]),
                            fEventName = row["fEventName"]?.ToString(),
                            fEventLocation = row["fEventLocation"]?.ToString(),
                            fEventStartDate = row["fEventStartDate"]?.ToString(),
                            fEventEndDate = row["fEventEndDate"]?.ToString(),
                            fEventDescription = row["fEventDescription"]?.ToString(),
                            fUserId = row["fUserId"] != DBNull.Value ? Convert.ToInt32(row["fUserId"]) : 0,
                            fEventCreatedDate = row["fEventCreatedDate"] != DBNull.Value ? (DateTime)row["fEventCreatedDate"] : DateTime.MinValue,
                            fEventUpdatedDate = row["fEventUpdatedDate"] != DBNull.Value ? (DateTime)row["fEventUpdatedDate"] : DateTime.MinValue,
                            fEventActivityfee = row["fEventActivityfee"] != DBNull.Value ? Convert.ToDecimal(row["fEventActivityfee"]) : 0,
                            fEventURL = row["fEventURL"]?.ToString()
                        };

                        // 初始化 CEventImage 物件
                        CEventImage image = new CEventImage
                        {
                            fEventImageId = Convert.ToInt32(row["fEventImageId"]),
                            fEventId = Convert.ToInt32(row["fEventId"]),
                            fEventImage = row["fEventImage"] != DBNull.Value ? (byte[])row["fEventImage"] : null
                        };

                        // 添加圖片到 EventImageBox
                        EventImageBox imageBox = new EventImageBox
                        {
                            Image = image
                        };
                        imageBox.orderImage += this.orderImage;
                        flowLayoutPanel1.Controls.Add(imageBox);

                        // 添加活動描述到 EventImageBox
                        EventImageBox eventBox = new EventImageBox
                        {
                            Image = image
                        };
                        eventBox.orderevent += this.orderEvent;
                        flowLayoutPanel1.Controls.Add(eventBox);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查詢過程中發生錯誤：{ex.Message}");
            }
        }
        public void orderImage(CEventImage p)
        {
            MessageBox.Show($"圖片 ID: {p.fEventImageId}");
        }
        public void orderEvent(CEvents p)
        {
            MessageBox.Show($"活動名稱: {p.fEventName}, 活動地點: {p.fEventLocation}");
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }
    }
    
}
