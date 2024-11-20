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

namespace Attractions.Views
{
    public partial class FormAttractionTicketList : Form
    {
        //private string pipe = "np:\\\\.\\pipe\\LOCALDB#B5FE6A17\\tsql\\query;";
        public FormAttractionTicketList()
        {
            InitializeComponent();
        }

        // 取得票價資料的 SQL
        private string getSqlOfAllTicket()
        {
            string sql = "SELECT ";
            sql += "fAttractionTicketId, ";
            sql += "t.fAttractionId, ";
            sql += "a.fAttractionName, ";
            sql += "fTicketType, ";
            sql += "fPrice, ";
            sql += "fDiscountInformation, ";
            sql += "t.fCreatedDate ";
            sql += "FROM tAttractionTickets as t ";
            sql += "JOIN tAttractions as a ";
            sql += "ON t.fAttractionId = a.fAttractionId";
            return sql;
        }

        private void FormAttractionTicketList_Load(object sender, EventArgs e)
        {
            displayAttractionTicket(getSqlOfAllTicket(), false);
        }

        private void displayAttractionTicket(string sql, bool isKeyWord)
        {
            //string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            string connectString = @"Data Source = .; Initial Catalog = dbGroupB; Integrated Security = True;";

            using (SqlConnection connection = new SqlConnection(connectString))
            {
                SqlCommand command = new SqlCommand(sql, connection);

                // 防止 SQL Injection
                if (isKeyWord)
                {
                    command.Parameters.Add(new SqlParameter("K_KEYWORD", "%" + (object)toolStripTextBox1.Text.Trim() + "%"));
                }

                try
                {
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    // 創建 DataTable 並載入資料
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);

                    // 將資料綁定到 DataGridView
                    dataGridView1.DataSource = dataTable;
                }
                catch 
                {

                }
            }
        }

        private void tsbInsert_Click(object sender, EventArgs e)
        {
            FormAttractionTicketEditor f = new FormAttractionTicketEditor();
            f.ShowDialog();
            if (f.isOk == DialogResult.OK)
            {
                (new CAttractionManager()).createAttractionTicket(f.attractionTicket);
                displayAttractionTicket(getSqlOfAllTicket(), false);
            }
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            List<int> deleteIndexes = new List<int>();

            // 取得所有選取到的 row
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                try
                {
                    deleteIndexes.Add((int)row.Cells["fAttractionTicketId"].Value);
                }
                catch 
                {

                }
            }

            if (deleteIndexes.Count == 0) return;

            //string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            string connectString = @"Data Source = .; Initial Catalog = dbGroupB; Integrated Security = True;";

            // 刪除的 SQL
            string sql = "DELETE FROM tAttractionTickets WHERE fAttractionTicketId IN (";
            for (int i = 0; i < deleteIndexes.Count; i++)
            {
                sql += $"@id{i},";
            }
            sql = sql.TrimEnd(',') + ")";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // 動態添加參數
                        for (int i = 0; i < deleteIndexes.Count; i++)
                        {
                            command.Parameters.AddWithValue($"@id{i}", deleteIndexes[i]);
                        }
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch 
            {
            }

            displayAttractionTicket(getSqlOfAllTicket(), false);
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            showEditView();
        }

        private void showEditView()
        {
            if (dataGridView1.CurrentCell.RowIndex < 0) return;

            //string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            string connectString = @"Data Source = .; Initial Catalog = dbGroupB; Integrated Security = True;";

            string sql = "SELECT * FROM tAttractionTickets WHERE fAttractionTicketId=@K_fAttractionTicketId";
            // 防止 SQL Injection
            SqlParameter fAttractionTicketId = new SqlParameter("K_fAttractionTicketId", dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["fAttractionTicketId"].Value);

            CAttractionTicket x = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.Add(fAttractionTicketId);
                    command.ExecuteNonQuery();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        x = new CAttractionTicket();
                        x.fAttractionTicketId = (int)reader["fAttractionTicketId"];
                        x.fAttractionId = (int)reader["fAttractionId"];
                        x.fTicketType = reader["fTicketType"].ToString();
                        x.fPrice = (decimal)reader["fPrice"];
                        x.fDiscountInformation = reader["fDiscountInformation"].ToString();
                        x.fCreatedDate = (DateTime)reader["fCreatedDate"];
                    }
                }
            }
            catch 
            {
            }

            if (x == null) return;

            FormAttractionTicketEditor f = new FormAttractionTicketEditor();
            f.attractionTicket = x;
            f.ShowDialog();

            if (f.isOk == DialogResult.OK)
            {
                (new CAttractionManager()).updateAttractionTicket(f.attractionTicket);
                displayAttractionTicket(getSqlOfAllTicket(), false);
            }
        }

        private void tsbSearch_Click(object sender, EventArgs e)
        {
            string sql = getSqlOfAllTicket();
            sql += " WHERE a.fAttractionName LIKE @K_KEYWORD ";
            sql += "OR fTicketType LIKE @K_KEYWORD ";
            sql += "OR fPrice LIKE @K_KEYWORD ";
            sql += "OR fDiscountInformation LIKE @K_KEYWORD ";

            displayAttractionTicket(sql, true);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            showEditView();
        }

        private void resetGridStyle()
        {
            dataGridView1.Columns["fAttractionTicketId"].HeaderText = "景點門票ID";
            dataGridView1.Columns["fAttractionTicketId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fAttractionId"].HeaderText = "景點ID";
            dataGridView1.Columns["fAttractionId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fAttractionName"].HeaderText = "景點名稱";
            dataGridView1.Columns["fAttractionName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fTicketType"].HeaderText = "票種";
            dataGridView1.Columns["fTicketType"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fPrice"].HeaderText = "門票費用";
            dataGridView1.Columns["fPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fDiscountInformation"].HeaderText = "折扣資訊";
            dataGridView1.Columns["fDiscountInformation"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fCreatedDate"].HeaderText = "建立時間";
            dataGridView1.Columns["fCreatedDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            bool isColorChanged = false;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                isColorChanged = !isColorChanged;

                r.DefaultCellStyle.Font = new Font("微軟正黑體", 12);
                r.DefaultCellStyle.BackColor = Color.White;
                r.DefaultCellStyle.SelectionBackColor = Color.MediumSeaGreen;

                if (isColorChanged)
                {
                    r.DefaultCellStyle.BackColor = Color.MediumAquamarine;
                }
            }
        }

        private void FormAttractionTicketList_Paint(object sender, PaintEventArgs e)
        {
            resetGridStyle();
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            resetGridStyle();
        }
    }
}
