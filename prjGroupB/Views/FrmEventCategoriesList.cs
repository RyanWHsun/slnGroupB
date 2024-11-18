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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace prjGroupB.Views
{
    public partial class FrmEventCategoriesList : Form
    {
        private SqlDataAdapter _da;
        private DataSet _ds = null;
        private SqlCommandBuilder _builder;
        private int _position = -1;
        public DialogResult IsOk { get; set; }

        public FrmEventCategoriesList()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FrmEventCategoriesEditor f = new FrmEventCategoriesEditor();
            f.ShowDialog();
            if (f.IsOk == DialogResult.OK)
            {
                DataTable dt = dataGridView1.DataSource as DataTable;

                DataRow row = dt.NewRow();
                row["fEventCategoryName"] = f.Categories.fEventCategoryName;
                row["fCategoryDescription"] = f.Categories.fCategoryDescription;
                row["fEventCreatedDate"] = f.Categories.fEventCreatedDate;
                dt.Rows.Add(row);
                _da.Update(dt);
                MessageBox.Show("類別成功儲存");
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

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (_position < 0)
            {
                MessageBox.Show("請選擇要修改的類別項目");
                return;
            }

            DataRow row = (dataGridView1.DataSource as DataTable).Rows[_position];

            FrmEventCategoriesEditor f = new FrmEventCategoriesEditor();
            f.Categories = new CEventCategories
            {
                fEventCategoryId = Convert.ToInt32(row["fEventCategoryId"]),
                fEventCategoryName = row["fEventCategoryName"].ToString(),
                fCategoryDescription = row["fCategoryDescription"].ToString(),
                fEventCreatedDate = DateTime.Parse(row["fEventCreatedDate"].ToString())
            };

            f.ShowDialog();

            if (f.IsOk == DialogResult.OK)
            {
                row["fEventCategoryName"] = f.Categories.fEventCategoryName;
                row["fCategoryDescription"] = f.Categories.fCategoryDescription;

                _da.Update(dataGridView1.DataSource as DataTable);
                MessageBox.Show("類別已成功更新");
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM tEventCategories WHERE ";
            sql += " fEventCategoryName LIKE @K_KEYWORD";
            sql += " OR fCategoryDescription LIKE @K_KEYWORD";

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
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoResizeColumns();

            // 對特定欄位設置自動調整
            if (dataGridView1.Columns.Count > 10)
            {
                dataGridView1.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void FrmEventCategories_Load(object sender, EventArgs e)
        {
            displayEventsBySql("SELECT * FROM tEventCategories ", false);
            DataTable dt = _ds.Tables[0];
            if (dt != null)
            {
                dataGridView1.DataSource = dt;
            }
        }

        private void FrmEventCategories_FormClosed(object sender, FormClosedEventArgs e)
        {
            _da.Update(dataGridView1.DataSource as DataTable);
            resetGridStyle();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            _position = e.RowIndex;
        }

        private void FrmEventCategories_Paint(object sender, PaintEventArgs e)
        {
            resetGridStyle();
        }
    }
}