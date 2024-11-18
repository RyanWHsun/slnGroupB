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
    SELECT TOP 10
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
                        EventImageBox imageBox = new EventImageBox
                        {
                            Image = new CEventImage
                            {
                                fEventImageId = Convert.ToInt32(row["fEventImageId"]),
                                fEventId = Convert.ToInt32(row["fEventId"]),
                                fEventImage = row["fEventImage"] != DBNull.Value ? (byte[])row["fEventImage"] : null
                            },
                            Event = new CEvents
                            {
                                fEventId = Convert.ToInt32(row["fEventId"]),
                                fEventName = row["fEventName"]?.ToString() ?? "無名稱",
                                fEventLocation = row["fEventLocation"]?.ToString() ?? "無地點"
                            }
                        };
                        imageBox.orderImage += img =>
                        {
                            _selected = img;
                        };

                        imageBox.orderevent += evt =>
                        {
                        };

                        flowLayoutPanel1.Controls.Add(imageBox);
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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (_selected == null)
            {
                MessageBox.Show("請先選擇要刪除的圖片");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=dbGroupB;Integrated Security=True;"))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM tEventImage WHERE fEventImageId = @fEventImageId", con);
                    cmd.Parameters.AddWithValue("@fEventImageId", _selected.fEventImageId);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("圖片已成功刪除！");
                    btnSearch_Click(null, null); // 重新載入
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"刪除過程中發生錯誤：{ex.Message}");
            }
        }
    }
}