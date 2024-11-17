﻿using prjGroupB.Models;
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
using static System.Collections.Specialized.BitVector32;

namespace prjGroupB.Views
{
    public partial class FrmEventsList : Form
    {
        private SqlDataAdapter _da;
        private DataSet _ds = null;
        private DataTable _eventTable = null;
        private SqlCommandBuilder _builder;
        private int _position = -1;

        public class ComboBoxItem
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public override string ToString() => Name;
        }

        public FrmEventsList()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (_eventTable == null)
            {
                MessageBox.Show("無法新增資料，資料表未正確初始化。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FrmEventsEditor f = new FrmEventsEditor();
            f.ShowDialog();
            if (f.IsOk == DialogResult.OK)
            {
                DataTable dt = dataGridView1.DataSource as DataTable;
                DataRow row = dt.NewRow();
                row["fEventName"] = f.Event.fEventName;
                row["fEventDescription"] = f.Event.fEventDescription;
                row["fEventStartDate"] = f.Event.fEventStartDate;
                row["fEventEndDate"] = f.Event.fEventEndDate;
                row["fEventLocation"] = f.Event.fEventLocation;
                row["fEventCreatedDate"] = f.Event.fEventCreatedDate;
                row["fEventUpdatedDate"] = f.Event.fEventUpdatedDate;
                row["fEventActivityfee"] = f.Event.fEventActivityfee;
                row["fEventURL"] = f.Event.fEventURL;
                dt.Rows.Add(row);
                _da.Update(dt);
                MessageBox.Show("活動已成功儲存");
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (_position < 0)
                return;
            DataRow row = (dataGridView1.DataSource as DataTable).Rows[_position];
            row.Delete();
            _da.Update(dataGridView1.DataSource as DataTable);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM tEevents WHERE ";
            sql += " fEventName LIKE @K_KEYWORD";
            sql += " OR fEventLocation LIKE @K_KEYWORD";

            displayEventsBySql(sql, true);
        }

        private void FrmEventsList_Load(object sender, EventArgs e)

        {
            displayEventsBySql("SELECT * FROM tEvents ", false);
            DataTable dt = _ds.Tables[0];
            if (dt != null)
            {
                dataGridView1.DataSource = dt;
            }
        }

        private void LoadCategoriesIntoComboBox()
        {
            string query = "SELECT fEventCategoryId, fEventCategoryName FROM tEventCategories";
            using (var conn = new SqlConnection(@"Data Source=.;Initial Catalog=dbGroupB;Integrated Security=True;"))
            {
                conn.Open();
                using (var cmd = new SqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(new ComboBoxItem
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        });
                    }
                }
            }
        }

        private void LoadEvents()
        {
            string query = "SELECT * FROM tEvents";
            using (var conn = new SqlConnection(@"Data Source=.;Initial Catalog=dbGroupB;Integrated Security=True;"))
            {
                conn.Open();
                _da = new SqlDataAdapter(query, conn);
                SqlCommandBuilder builder = new SqlCommandBuilder(_da); // 確保支持 Insert/Update/Delete 操作

                _ds = new DataSet();
                _da.Fill(_ds, "tEvents");
                _eventTable = _ds.Tables["tEvents"];
                dataGridView1.DataSource = _eventTable;// 綁定 DataTable 到 DataGridView
            }
        }

        private void FrmEventsList_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (dataGridView1.DataSource is DataTable dt)
            {
                try
                {
                    _da?.Update(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"儲存資料時發生錯誤：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            AdjustColumnWidths();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            _position = e.RowIndex;
        }

        private void FrmEventsList_Paint(object sender, PaintEventArgs e)
        {
            AdjustColumnWidths();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (_position < 0)
            {
                MessageBox.Show("請選擇要修改的類別項目");
                return;
            }

            DataRow row = (dataGridView1.DataSource as DataTable).Rows[_position];
            FrmEventsEditor f = new FrmEventsEditor
            {
                Event = new CEvents
                {
                    fEventId = Convert.ToInt32(row["fEventId"]),
                    fEventName = row["fEventName"].ToString(),
                    fEventDescription = row["fEventDescription"].ToString(),
                    fEventStartDate = row["fEventStartDate"] == DBNull.Value ? null : row["fEventStartDate"].ToString(),
                    fEventEndDate = row["fEventEndDate"] == DBNull.Value ? null : row["fEventEndDate"].ToString(),
                    fEventLocation = row["fEventLocation"].ToString(),
                    fEventActivityfee = Convert.ToDecimal(row["fEventActivityfee"]),
                    fEventURL = row["fEventURL"].ToString(),
                    fEventCreatedDate = DateTime.Parse(row["fEventCreatedDate"].ToString()),
                    fEventUpdatedDate = DateTime.Parse(row["fEventUpdatedDate"].ToString())
                }
            };

            f.ShowDialog();
            if (f.IsOk == DialogResult.OK)
            {
                row["fEventName"] = f.Event.fEventName;
                row["fEventDescription"] = f.Event.fEventDescription;
                row["fEventStartDate"] = f.Event.fEventStartDate;
                row["fEventEndDate"] = f.Event.fEventEndDate;
                row["fEventLocation"] = f.Event.fEventLocation;
                row["fEventActivityfee"] = f.Event.fEventActivityfee;
                row["fEventURL"] = f.Event.fEventURL;
                row["fEventUpdatedDate"] = DateTime.Now;
                _da.Update(dataGridView1.DataSource as DataTable);
                MessageBox.Show("活動已成功更新");
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            {
                DataTable dt = dataGridView1.DataSource as DataTable;

                if (dt == null) return;

                // 建立 DataView 並按建立時間排序
                DataView dv = dt.DefaultView;
                dv.Sort = "fEventUpdatedDate ASC"; // 或 DESC，如果需要降序
                dataGridView1.DataSource = dv.ToTable(); // 更新排序後的 DataTable 到 DataGridView
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_eventTable == null)
            {
                MessageBox.Show("資料表未正確初始化，無法執行篩選操作。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBox1.SelectedItem is ComboBoxItem selectedCategory)
            {
                SearchEvents(selectedCategory.Id, toolStripTextBox1.Text.Trim());
            }
            else
            {
                SearchEvents(null, ""); // 清空篩選
            }
        }

        private void SearchEvents(int? categoryId, string keyword)
        {
            string query = @"
        SELECT
            e.fEventId AS [活動編號],
            e.fEventName AS [活動名稱],
            e.fEventDescription AS [活動描述],
            e.fEventStartDate AS [開始日期],
            e.fEventEndDate AS [結束日期],
            e.fEventLocation AS [活動地點],
            e.fEventCreatedDate AS [創建日期],
            e.fEventUpdatedDate AS [更新日期],
            e.fEventActivityFee AS [活動費用],
            e.fEventURL AS [活動網址],
            m.fEventCategoryId -- 確保加入此欄位
        FROM
            tEvents e
        LEFT JOIN
            tEventCategoryMapping m ON e.fEventId = m.fEventId
        WHERE
            (@CategoryId IS NULL OR m.fEventCategoryId = @CategoryId)
            AND (e.fEventName LIKE @Keyword
                 OR e.fEventDescription LIKE @Keyword
                 OR e.fEventLocation LIKE @Keyword)";

            using (var conn = new SqlConnection(@"Data Source=.;Initial Catalog=dbGroupB;Integrated Security=True;"))
            using (var cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@CategoryId", categoryId.HasValue ? (object)categoryId.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@Keyword", $"%{keyword}%");

                var adapter = new SqlDataAdapter(cmd);
                var table = new DataTable();
                adapter.Fill(table);
                _eventTable = table; // 將結果存入 _eventTable
                dataGridView1.DataSource = _eventTable; // 綁定到 DataGridView
            }
        }

        private void AdjustColumnWidths()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoResizeColumns();

            // 對特定欄位設置自動調整
            if (dataGridView1.Columns.Count > 10)
            {
                dataGridView1.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void InitializeDataAdapter()
        {
            string connectionString = @"Data Source=.;Initial Catalog=dbGroupB;Integrated Security=True;";
            string selectQuery = "SELECT * FROM tEvents";

            SqlConnection connection = new SqlConnection(connectionString);

            // 初始化 SqlDataAdapter
            _da = new SqlDataAdapter(selectQuery, connection);

            // 自動產生 InsertCommand、UpdateCommand 和 DeleteCommand
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(_da);

            _ds = new DataSet();
            _da.Fill(_ds, "tEvents");
            _eventTable = _ds.Tables["tEvents"];

            // 設置主鍵（假設 fEventId 是主鍵）
            _eventTable.PrimaryKey = new DataColumn[] { _eventTable.Columns["fEventId"] };
        }

        private void displayEventsBySql(string sql, bool isKeyword)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbGroupB;Integrated Security=True;";
            con.Open();
            _da = new SqlDataAdapter(sql, con);
            if (isKeyword)
                _da.SelectCommand.Parameters.Add(new SqlParameter("K_KEYWORD",
                    "%" + (object)toolStripTextBox1.Text + "%"));
            _builder = new SqlCommandBuilder();
            _builder.DataAdapter = _da;

            _ds = new DataSet();
            _da.Fill(_ds);
            con.Close();
            dataGridView1.DataSource = _ds.Tables[0];

            AdjustColumnWidths();
        }
    }
}