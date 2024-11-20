using Attractions.Models;
using Attractions.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Attractions
{
    public partial class FormAttractionList : Form
    {
        private int _lastfAttractionId = 0;

        //public string pipe = "np:\\\\.\\pipe\\LOCALDB#B5FE6A17\\tsql\\query;";
        public SqlDataAdapter _da;
        public SqlCommandBuilder _builder;
        public int _position = -1;
        DataSet _ds = null;

        public FormAttractionList()
        {
            InitializeComponent();
        }

        private void FormAttractionList_Load(object sender, EventArgs e)
        {
            getfAttractionCategory();

            string sql = "";
            sql += "SELECT ";
            sql += "fAttractionId,";
            sql += "fAttractionName,";
            sql += "fDescription,";
            sql += "fAddress,";
            sql += "fPhoneNumber,";
            sql += "fOpeningTime,";
            sql += "fClosingTime,";
            sql += "fWebsiteURL,";
            sql += "fLongitude,";
            sql += "fLatitude,";
            sql += "fRegion,";
            sql += "fCategoryId,";
            sql += "fCreatedDate,";
            sql += "fUpdatedDate,";
            sql += "fStatus,";
            sql += "fTransformInformation ";
            sql += "FROM tAttractions;";
            displayAttractionsBySql(sql, false);
        }

        private void getfAttractionCategory()
        {
            //string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True;";
            string connectString = @"Data Source = .; Initial Catalog = dbGroupB; Integrated Security = True;";

            string sql = "";
            sql += "SELECT ";
            sql += "fAttractionCategoryId,";
            sql += "fAttractionCategoryName ";
            sql += "FROM tAttractionCategories;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectString))
                {
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        cbCategoryName.Items.Add(new
                        {
                            fAttractionCategoryId = reader["fAttractionCategoryId"],
                            fAttractionCategoryName = reader["fAttractionCategoryName"].ToString()
                        });
                    }

                    reader.Close();

                    // 設定 ComboBox 的顯示屬性
                    cbCategoryName.DisplayMember = "fAttractionCategoryName"; // 顯示的名稱
                    cbCategoryName.ValueMember = "fAttractionCategoryId";     // 儲存的值
                }
            }
            catch 
            {
                MessageBox.Show("ERROR");
            }
        }

        // 顯示所有欄位
        private void displayAttractionsBySql(string sql, bool isKeyword)
        {

            // 連線
            SqlConnection con = new SqlConnection();
            //con.ConnectionString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True;"; ;
            con.ConnectionString = @"Data Source = .; Initial Catalog = dbGroupB; Integrated Security = True;";

            con.Open();

            _da = new SqlDataAdapter(sql, con);

            // sql injection
            if (isKeyword)
            {
                //_da.SelectCommand.Parameters.Add(new SqlParameter("K_KEYWORD", "%" + (object)toolStripTextBox1.Text + "%"));
            }

            _builder = new SqlCommandBuilder();
            _builder.DataAdapter = _da;

            _ds = new DataSet();
            _da.Fill(_ds);

            con.Close();

            dataGridView1.DataSource = _ds.Tables[0];
            //resetGridStyle();
        }

        // 按下"新增景點"按鈕
        private void tsbInsert_Click(object sender, EventArgs e)
        {
            FormAttractionEditor f = new FormAttractionEditor();
            _lastfAttractionId = getLastfAttractionId();

            f.ShowDialog();
            if (f.isOk == DialogResult.OK)
            {
                DataTable dt = dataGridView1.DataSource as DataTable;

                DataRow row = dt.NewRow();
                // attraction 的 get{}
                // row["fAttractionId"] = f.attraction.fAttractionId;
                row["fAttractionName"] = f.attraction.fAttractionName;
                row["fDescription"] = f.attraction.fDescription;
                row["fAddress"] = f.attraction.fAddress;
                row["fPhoneNumber"] = f.attraction.fPhoneNumber;
                row["fOpeningTime"] = f.attraction.fOpeningTime;
                row["fClosingTime"] = f.attraction.fClosingTime;
                row["fWebsiteURL"] = f.attraction.fWebsiteURL;
                row["fLongitude"] = f.attraction.fLongitude;
                row["fLatitude"] = f.attraction.fLatitude;
                row["fRegion"] = f.attraction.fRegion;
                row["fCategoryId"] = f.attraction.fCategoryId;
                row["fCreatedDate"] = f.attraction.fCreatedDate;
                row["fUpdatedDate"] = f.attraction.fUpdatedDate;
                row["fStatus"] = f.attraction.fStatus;
                row["fTransformInformation"] = f.attraction.fTransformInformation;

                dt.Rows.Add(row);

                // 把 CAttrationImage 的圖片資料放到新的 Datable
                saveImageFromCAttrationImageToDataTable(f);
            }
        }

        // 把 CAttrationImage 的照片存到 DataTable
        private void saveImageFromCAttrationImageToDataTable(FormAttractionEditor f)
        {
            _da.Update(dataGridView1.DataSource as DataTable);
            // 新建一個 image 的 table
            DataTable imagetable = new DataTable();

            // 定義 imagetable 的資料欄位
            imagetable.Columns.Add("fAttractionId", typeof(int));
            imagetable.Columns.Add("fImage", typeof(byte[]));

            DataRow row = imagetable.NewRow();

            if (f.attraction.fAttractionId > 0)
                row["fAttractionId"] = f.attraction.fAttractionId;
            else row["fAttractionId"] = _lastfAttractionId + 1;

            if (f.attractionImage.fImage == null || f.attractionImage.fImage.Count == 0) return;
            row["fImage"] = f.attractionImage.fImage[f.attractionImage.fImage.Count - 1];

            imagetable.Rows.Add(row);

            // 連線
            SqlConnection con = new SqlConnection();
            // con.ConnectionString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True;"; ;
            con.ConnectionString = @"Data Source = .; Initial Catalog = dbGroupB; Integrated Security = True;";

            con.Open();

            string sql = "SELECT fAttractionId, fImage FROM tAttractionImages;";
            SqlDataAdapter imageDataAdapter = new SqlDataAdapter(sql, con);

            imageDataAdapter.InsertCommand = new SqlCommand(
                "INSERT INTO tAttractionImages (fAttractionId, fImage) VALUES (@fAttractionId, @fImage)",
                con
                );

            // 定義參數
            imageDataAdapter.InsertCommand.Parameters.Add("@fAttractionId", SqlDbType.Int, 0, "fAttractionId");
            imageDataAdapter.InsertCommand.Parameters.Add("@fImage", SqlDbType.VarBinary, -1, "fImage");

            // 將 DataTable 的變更應用到資料庫
            imageDataAdapter.Update(imagetable);

            con.Close();
        }

        // 從 dataGridView1 取得最後一個 AttractionId
        private int getLastfAttractionId()
        {
            // dataGridView1 沒資料直接回傳
            if (dataGridView1.Rows.Count <= 0) return 0;

            int lastIndex = dataGridView1.Rows.Count - 1;
            // 檢查最後一列是否是新的輸入列（如果允許新增列）
            if (dataGridView1.Rows[lastIndex].IsNewRow) lastIndex--;

            DataGridViewRow lastRow = dataGridView1.Rows[lastIndex];
            try
            {
                return (int)lastRow.Cells[0].Value;
            }
            catch { return 0; }
        }

        // 按下"刪除景點"按鈕
        private void tsbDelete_Click(object sender, EventArgs e)
        {
            List<int> deleteIndexes = new List<int>();

            // 取得所有選取到的 row
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                try
                {
                    deleteIndexes.Add((int)row.Cells["fAttractionId"].Value);
                }
                catch 
                {

                }
            }

            if (deleteIndexes.Count == 0) return;

            // 刪掉 attraction 之前，要先刪相關的 attraction 的 Image  
            deleteRelatedAttractionsImage(deleteIndexes);

            // 刪掉 attraction 之前，要把相關的 recommendations 刪掉
            deleteRelatedRecommendation(deleteIndexes);

            // 刪掉 attraction 之前，要把相關的 tickets 刪掉
            deleteRelatedTicket(deleteIndexes);

            // 刪掉 attraction 之前，要把相關的 comments 刪掉
            deleteRelatedComment(deleteIndexes);

            // 刪掉 attraction 之前，要把相關的 favorites 刪掉
            deleteRelatedFavorite(deleteIndexes);

            //string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            string connectString = @"Data Source = .; Initial Catalog = dbGroupB; Integrated Security = True;";

            // 刪除的 SQL
            string sql = "DELETE FROM tAttractions WHERE fAttractionId IN (";
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

            displayAttractionsBySql("SELECT * FROM tAttractions;", false);
        }

        // 刪掉相關的 favorite
        private void deleteRelatedFavorite(List<int> deleteIndexes)
        {
            //string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            string connectString = @"Data Source = .; Initial Catalog = dbGroupB; Integrated Security = True;";

            string sql = "DELETE FROM tAttractionUserFavorites WHERE fAttractionId IN (";
            for (int i = 0; i < deleteIndexes.Count; i++)
            {
                sql += $"@id{i},";
            }
            sql = sql.TrimEnd(',') + ");";

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
        }

        // 刪掉相關的 comment
        private void deleteRelatedComment(List<int> deleteIndexes)
        {
            //string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            string connectString = @"Data Source = .; Initial Catalog = dbGroupB; Integrated Security = True;";

            string sql = "DELETE FROM tAttractionComments WHERE fAttractionId IN (";
            for (int i = 0; i < deleteIndexes.Count; i++)
            {
                sql += $"@id{i},";
            }
            sql = sql.TrimEnd(',') + ");";

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
        }

        // 刪掉相關的 ticket
        private void deleteRelatedTicket(List<int> deleteIndexes)
        {
            //string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            string connectString = @"Data Source = .; Initial Catalog = dbGroupB; Integrated Security = True;";

            string sql = "DELETE FROM tAttractionTickets WHERE fAttractionId IN (";
            for (int i = 0; i < deleteIndexes.Count; i++)
            {
                sql += $"@id{i},";
            }
            sql = sql.TrimEnd(',') + ");";

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
        }

        // 刪掉相關的 recommendation
        private void deleteRelatedRecommendation(List<int> deleteIndexes)
        {
            //string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            string connectString = @"Data Source = .; Initial Catalog = dbGroupB; Integrated Security = True;";

            string sql = "DELETE FROM tAttractionRecommendations WHERE fAttractionId IN (";
            for (int i = 0; i < deleteIndexes.Count; i++)
            {
                sql += $"@id{i},";
            }
            sql = sql.TrimEnd(',') + ");";

            string sql2 = "DELETE FROM tAttractionRecommendations WHERE fRecommendationId IN (";
            for (int i = 0; i < deleteIndexes.Count; i++)
            {
                sql2 += $"@id{i},";
            }
            sql2 = sql2.TrimEnd(',') + ");";

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

            try
            {
                using (SqlConnection connection = new SqlConnection(connectString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sql2, connection))
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
        }

        // 刪除相關景點的圖片
        private void deleteRelatedAttractionsImage(List<int> indexes)
        {
            //string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            string connectString = @"Data Source = .; Initial Catalog = dbGroupB; Integrated Security = True;";

            string sql = "DELETE FROM tAttractionImages WHERE fAttractionId IN (";
            for (int i = 0; i < indexes.Count; i++)
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
                        for (int i = 0; i < indexes.Count; i++)
                        {
                            command.Parameters.AddWithValue($"@id{i}", indexes[i]);
                        }
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch 
            {
                //MessageBox.Show("ERROR: Can't Set Related Attractions CategeryId Is Null");
            }
        }

        // 按下"修改景點"按鈕
        private void tsbEdit_Click(object sender, EventArgs e)
        {
            showEditView();
        }

        // 表單關閉後，把資料存進 Database
        private void FormAttractionList_FormClosed(object sender, FormClosedEventArgs e)
        {
            _da.Update(dataGridView1.DataSource as DataTable);
        }

        // 按下"重新整理"按鈕
        private void tsbReload_Click(object sender, EventArgs e)
        {
            _da.Update(dataGridView1.DataSource as DataTable);
            //_da.Update(imagetable)
            string sql = "";
            sql += "SELECT ";
            sql += "fAttractionId,";
            sql += "fAttractionName,";
            sql += "fDescription,";
            sql += "fAddress,";
            sql += "fPhoneNumber,";
            sql += "fOpeningTime,";
            sql += "fClosingTime,";
            sql += "fWebsiteURL,";
            sql += "fLongitude,";
            sql += "fLatitude,";
            sql += "fRegion,";
            sql += "fCategoryId,";
            sql += "fCreatedDate,";
            sql += "fUpdatedDate,";
            sql += "fStatus,";
            sql += "fTransformInformation ";
            sql += "FROM tAttractions;";
            displayAttractionsBySql(sql, false);
        }

        // 點擊 GridView，取得滑鼠點到的 row 位置
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            _position = e.RowIndex; // 在 GridView 中點到的位置
        }

        //
        private void tsbSearch_Click(object sender, EventArgs e)
        {
            //string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            string connectString = @"Data Source = .; Initial Catalog = dbGroupB; Integrated Security = True;";

            string sql = "SELECT ";
            sql += "fAttractionId, ";
            sql += "fAttractionName, ";
            sql += "a.fDescription, ";
            sql += "fAddress, ";
            sql += "fPhoneNumber, ";
            sql += "fOpeningTime, ";
            sql += "fClosingTime, ";
            sql += "fWebsiteURL, ";
            sql += "fLongitude, ";
            sql += "fLatitude, ";
            sql += "fRegion, ";
            sql += "a.fCategoryId, ";
            sql += "c.fAttractionCategoryName, ";
            sql += "fCreatedDate, ";
            sql += "fUpdatedDate, ";
            sql += "fStatus, ";
            sql += "fTransformInformation ";
            sql += "FROM tAttractions as a ";
            sql += "JOIN tAttractionCategories as c ";
            sql += "ON a.fCategoryId = c.fAttractionCategoryId ";
            sql += "WHERE 1=@number ";

            if (tstbAttractionName.Text != "") sql += "OR fAttractionName LIKE @fAttractionName ";
            if (tstbDescription.Text != "") sql += "OR a.fDescription LIKE @fDescription ";
            if (tstbAddress.Text != "") sql += "OR fAddress LIKE @fAddress ";
            if (tstbPhone.Text != "") sql += "OR fPhoneNumber LIKE @fPhoneNumber ";
            if (tstbURL.Text != "") sql += "OR fWebsiteURL LIKE @fWebsiteURL ";
            if (tstbRegion.Text != "") sql += "OR fRegion LIKE @fRegion ";
            if (cbCategoryName.Text != "") sql += "OR fAttractionCategoryName LIKE @fAttractionCategoryName ";
            if (cbStatus.Text != "") sql += "OR fStatus = @fStatus ";

            if (cbBeforeAndAfterOpeningTime.SelectedItem != null)
            {
                switch (cbBeforeAndAfterOpeningTime.SelectedItem.ToString())
                {
                    case "前":
                        sql += "OR fOpeningTime < @fOpeningTime ";
                        break;
                    case "後":
                        sql += "OR fOpeningTime > @fOpeningTime ";
                        break;
                    default:
                        break;
                }
            }

            if (cbBeforeAndAfterClosingTime.SelectedItem != null)
            {
                switch (cbBeforeAndAfterClosingTime.SelectedItem.ToString())
                {
                    case "前":
                        sql += "OR fClosingTime < @fClosingTime ";
                        break;
                    case "後":
                        sql += "OR fClosingTime > @fClosingTime ";
                        break;
                    default:
                        break;
                }
            }

            // 防止 SQL Injection
            int num = 1;
            if (tstbAttractionName.Text != "" || tstbDescription.Text != "" || tstbAddress.Text != "" || tstbPhone.Text != "" || tstbURL.Text != ""
                || tstbRegion.Text != "" || cbCategoryName.Text != "" || cbStatus.Text != "" || cbBeforeAndAfterOpeningTime.SelectedItem != null
                || cbBeforeAndAfterClosingTime.SelectedItem != null) num = 0;

            SqlParameter number = new SqlParameter("number", num);
            SqlParameter fAttractionName = new SqlParameter("fAttractionName", "%" + (object)tstbAttractionName.Text + "%");
            SqlParameter fDescription = new SqlParameter("fDescription", "%" + (object)tstbDescription.Text + "%");
            SqlParameter fAddress = new SqlParameter("fAddress", "%" + (object)tstbAddress.Text + "%");
            SqlParameter fPhoneNumber = new SqlParameter("fPhoneNumber", "%" + (object)tstbPhone.Text + "%");
            SqlParameter fWebsiteURL = new SqlParameter("fWebsiteURL", "%" + (object)tstbURL.Text + "%");
            SqlParameter fRegion = new SqlParameter("fRegion", "%" + (object)tstbRegion.Text + "%");
            SqlParameter fAttractionCategoryName = new SqlParameter("fAttractionCategoryName", cbCategoryName.Text);
            SqlParameter fStatus = new SqlParameter("fStatus", cbStatus.Text);
            SqlParameter fOpeningTime = new SqlParameter("fOpeningTime", dtpOpeningTime.Text);
            SqlParameter fClosingTime = new SqlParameter("fClosingTime", dtpClosingTime.Text);

            try
            {
                using (SqlConnection connection = new SqlConnection(connectString))
                {
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.Add(number);
                    command.Parameters.Add(fAttractionName);
                    command.Parameters.Add(fDescription);
                    command.Parameters.Add(fAddress);
                    command.Parameters.Add(fPhoneNumber);
                    command.Parameters.Add(fWebsiteURL);
                    command.Parameters.Add(fRegion);
                    command.Parameters.Add(fAttractionCategoryName);
                    command.Parameters.Add(fStatus);
                    command.Parameters.Add(fOpeningTime);
                    command.Parameters.Add(fClosingTime);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);

                    // 將資料綁定到 DataGridView
                    dataGridView1.DataSource = dataTable;
                }
            }
            catch
            {
                MessageBox.Show("ERROR");
            }

            //resetGridStyle();
        }

        // 雙擊 dataGridView1 欄位，開啟編輯頁面
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            showEditView();
        }

        // 顯示編輯頁面
        private void showEditView()
        {
            if (_position < 0) return;

            // 選取指定的 row
            DataRow row = (dataGridView1.DataSource as DataTable).Rows[_position];

            FormAttractionEditor f = new FormAttractionEditor();
            CAttraction x = new CAttraction();

            // FormAttractionList 的 DataGridView 的資料傳到 CAttraction 物件 x 上
            try
            {
                x.fAttractionId = (int)row["fAttractionId"];
            }
            catch
            {
                x.fAttractionId = 0;
            }
            x.fAttractionName = (string)row["fAttractionName"];
            x.fDescription = (string)row["fDescription"];
            x.fPhoneNumber = (string)row["fPhoneNumber"];
            x.fOpeningTime = (TimeSpan)row["fOpeningTime"];
            x.fClosingTime = (TimeSpan)row["fClosingTime"];
            x.fWebsiteURL = (string)row["fWebsiteURL"];
            x.fLongitude = (string)row["fLongitude"];
            x.fLatitude = (string)row["fLatitude"];
            x.fRegion = (string)row["fRegion"];
            try
            {
                x.fCategoryId = (int)row["fCategoryId"];
            }
            catch
            {

            }
            x.fCreatedDate = (DateTime)row["fCreatedDate"];
            x.fUpdatedDate = (DateTime)row["fUpdatedDate"];
            x.fStatus = (string)row["fStatus"];
            x.fTransformInformation = (string)row["fTransformInformation"];

            // 把剛剛傳到 CAttraction 物件 x 上的資料，再傳到 FormAttractionEditor
            f.attraction = x;
            try
            {
                f.showSavedImage((int)row["fAttractionId"], 0);
            }
            catch
            {
                MessageBox.Show("請先更新剛才修改的資料");
                return;
            }

            f.ShowDialog();

            // 點擊"確定"按鈕後，把資料寫到 DataRow
            if (f.isOk == DialogResult.OK)
            {
                row["fAttractionName"] = f.attraction.fAttractionName;
                row["fDescription"] = f.attraction.fDescription;
                row["fAddress"] = f.attraction.fAddress;
                row["fPhoneNumber"] = f.attraction.fPhoneNumber;
                row["fOpeningTime"] = f.attraction.fOpeningTime;
                row["fClosingTime"] = f.attraction.fClosingTime;
                row["fWebsiteURL"] = f.attraction.fWebsiteURL;
                row["fLongitude"] = f.attraction.fLongitude;
                row["fLatitude"] = f.attraction.fLatitude;
                row["fRegion"] = f.attraction.fRegion;
                row["fCategoryId"] = f.attraction.fCategoryId;
                row["fCreatedDate"] = f.attraction.fCreatedDate;
                row["fUpdatedDate"] = f.attraction.fUpdatedDate;
                row["fStatus"] = f.attraction.fStatus;
                row["fTransformInformation"] = f.attraction.fTransformInformation;
                saveImageFromCAttrationImageToDataTable(f);
            }
        }

        private void tsbReset_Click(object sender, EventArgs e)
        {
            tstbAttractionName.Text = "";
            tstbDescription.Text = "";
            tstbAddress.Text = "";
            tstbPhone.Text = "";
            tstbURL.Text = "";
            tstbRegion.Text = "";
            cbCategoryName.SelectedItem = null;
            cbStatus.SelectedItem = null;
            dtpOpeningTime.Value = DateTime.Now;
            dtpClosingTime.Value = DateTime.Now;
            cbBeforeAndAfterOpeningTime.SelectedItem = null;
            cbBeforeAndAfterClosingTime.SelectedItem = null;
        }

        private void resetGridStyle()
        {
            dataGridView1.Columns["fAttractionId"].HeaderText = "景點ID";
            dataGridView1.Columns["fAttractionId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fAttractionName"].HeaderText = "景點名稱";
            dataGridView1.Columns["fAttractionName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fDescription"].HeaderText = "描述";
            dataGridView1.Columns["fDescription"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.Columns["fAddress"].HeaderText = "地址";
            dataGridView1.Columns["fAddress"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.Columns["fPhoneNumber"].HeaderText = "電話";
            dataGridView1.Columns["fPhoneNumber"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fOpeningTime"].HeaderText = "開放時間";
            dataGridView1.Columns["fOpeningTime"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fClosingTime"].HeaderText = "關閉時間";
            dataGridView1.Columns["fClosingTime"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fWebsiteURL"].HeaderText = "網址";
            dataGridView1.Columns["fWebsiteURL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.Columns["fLongitude"].HeaderText = "經度";
            dataGridView1.Columns["fLongitude"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fLatitude"].HeaderText = "緯度";
            dataGridView1.Columns["fLatitude"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fRegion"].HeaderText = "地區";
            dataGridView1.Columns["fRegion"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fCategoryId"].HeaderText = "分類ID";
            dataGridView1.Columns["fCategoryId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fCreatedDate"].HeaderText = "建立時間";
            dataGridView1.Columns["fCreatedDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fUpdatedDate"].HeaderText = "更新時間";
            dataGridView1.Columns["fUpdatedDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fStatus"].HeaderText = "狀態";
            dataGridView1.Columns["fStatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["fTransformInformation"].HeaderText = "交通資訊";
            dataGridView1.Columns["fTransformInformation"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            if (dataGridView1.Columns["fAttractionCategoryName"] != null)
            {
                dataGridView1.Columns["fAttractionCategoryName"].HeaderText = "分類名稱";
                dataGridView1.Columns["fAttractionCategoryName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

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

        private void FormAttractionList_Paint(object sender, PaintEventArgs e)
        {
            resetGridStyle();
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            resetGridStyle();
        }
    }
}
