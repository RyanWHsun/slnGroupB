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

namespace prjGroupB.Views
{
    public partial class FrmEventsList : Form
    {
        SqlDataAdapter _da;
        DataSet _ds = null;
        SqlCommandBuilder _builder;
        int _position = -1;
        public FrmEventsList()
        {
            InitializeComponent();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
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
            string sql = "SELECT * FROM tEvents WHERE ";
            sql += " fEventName LIKE @K_KEYWORD";
            sql += " OR fEventDescription LIKE @K_KEYWORD";
            sql += " OR fEventLocation LIKE @K_KEYWORD";


            displayEventsBySql(sql, true);
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


            resetGridStyle();
        }
        private void resetGridStyle()
        {
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 50;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].Width = 100;
            dataGridView1.Columns[5].Width = 100;
            dataGridView1.Columns[6].Width = 100;
            dataGridView1.Columns[7].Width = 100;
            dataGridView1.Columns[8].Width = 100;
            dataGridView1.Columns[9].Width = dataGridView1.Width - 50 - 50 - 100 * 7 - dataGridView1.RowHeadersWidth;
            bool isColorChanged = false;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                isColorChanged = !isColorChanged;

                r.DefaultCellStyle.Font = new Font("微軟正黑體", 14);
                r.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                if (isColorChanged)
                    r.DefaultCellStyle.BackColor = Color.FromArgb(238, 238, 242);
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            List<CEvents> activities = new List<CEvents>
        {
            new CEvents { fEventName = "", fEventCreatedDate = DateTime.Now.AddHours(2) },
            new CEvents { fEventName = "", fEventCreatedDate = DateTime.Now.AddHours(1) },
            new CEvents { fEventName = "", fEventCreatedDate = DateTime.Now.AddHours(3) }
        };

            var sortedActivities = activities.OrderBy(a => a.fEventName).ToList();

            foreach (var activity in sortedActivities)
            {
                Console.WriteLine(activity.fEventName);
            }
        }

        private void FrmEventsList_Load(object sender, EventArgs e)
        {
            displayEventsBySql("SELECT * FROM tEvents ", false);
        }

        private void FrmEventsList_FormClosed(object sender, FormClosedEventArgs e)
        {
            _da.Update(dataGridView1.DataSource as DataTable);
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            _position = e.RowIndex;
        }

        private void FrmEventsList_Paint(object sender, PaintEventArgs e)
        {
            resetGridStyle();
        }
    }
}
