using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGroupB.Models
{
    public class CPostManager
    {
        private List<string> _Categories;
        public List<string> Categories
        {
            get
            {
                if (_Categories == null)
                    _Categories = new List<string>();
                return _Categories;
            }
            set { _Categories = value; }
        }
        public void insert(CPost p)
        {
            string sql;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbGroupB;Integrated Security=True";
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.Parameters.Add(new SqlParameter("K_FUSERID", (object)CUserSession.fUserId));
            SqlDataReader reader;
            int categoryId = 0;

            sql = "INSERT INTO tPosts(";
            sql += "fUserId,";
            sql += "fTitle,";
            sql += "fContent,";
            sql += "fCreatedAt,";
            sql += "fIsPublic";
            sql += ")VALUES(";
            sql += "@K_FUSERID,";
            sql += "@K_FTITLE,";
            sql += "@K_FCONTENT,";
            sql += "@K_FCREATEDAT,";
            sql += "@K_FISPUBLIC)";

            cmd.CommandText = sql;
            cmd.Parameters.Add(new SqlParameter("K_FTITLE", (object)p.fTitle));
            cmd.Parameters.Add(new SqlParameter("K_FCONTENT", (object)p.fContent));
            cmd.Parameters.Add(new SqlParameter("K_FCREATEDAT", (object)DateTime.Now));
            cmd.Parameters.Add(new SqlParameter("K_FISPUBLIC", (object)p.fIsPublic));
            cmd.ExecuteNonQuery();
            if (!string.IsNullOrEmpty(p.fCategory))
            {
                sql = "SELECT * FROM tPostCategories ";
                sql += "WHERE fUserId = @K_FUSERID AND fName = @K_FCATEGORY";
                cmd.CommandText = sql;
                cmd.Parameters.Add(new SqlParameter("K_FCATEGORY", (object)p.fCategory));
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    categoryId = (int)reader["fCategoryId"];
                }
                reader.Close();
                sql = "UPDATE tPosts ";
                sql += "SET fCategoryId = @K_FCATEGORYID ";
                sql += "WHERE fPostId = (SELECT MAX(fPostId) FROM tPosts)";
                cmd.CommandText = sql;
                cmd.Parameters.Add(new SqlParameter("K_FCATEGORYID", (object)categoryId));
                cmd.ExecuteNonQuery();
            }

            sql = "SELECT MAX(fPostId) AS PostId FROM tPosts WHERE fUserId = @K_FUSERID";
            cmd.CommandText = sql;
            reader = cmd.ExecuteReader();
            int postId = 0;
            if (reader.Read())
            {
                postId = (int)reader["PostId"];
            }
            reader.Close();
            if (p.fImages != null)
            {
                foreach (byte[] image in p.fImages)
                {
                    sql = "INSERT INTO tPostImages(";
                    sql += "fPostId,";
                    sql += "fImage";
                    sql += ")VALUES(";
                    sql += "@K_FPOSTID,";
                    sql += "@K_FIMAGE)";
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SqlParameter("K_FPOSTID", (object)postId));
                    cmd.Parameters.Add(new SqlParameter("K_FIMAGE", (object)image));
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }
            con.Close();
        }
        public void insertCategory(string p)
        {
            string sql = "INSERT INTO tPostCategories(";
            sql += "fUserId,";
            sql += "fName";
            sql += ")VALUES(";
            sql += "@K_FUSERID,";
            sql += "@K_FNAME)";

            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbGroupB;Integrated Security=True";
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = sql;
            cmd.Parameters.Add(new SqlParameter("K_FUSERID", (object)CUserSession.fUserId));
            cmd.Parameters.Add(new SqlParameter("K_FNAME", (object)p));
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void getCategory()
        {
            string sql = "SELECT * FROM tPostCategories WHERE fUserId = @K_FUSERID";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbGroupB;Integrated Security=True";
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = sql;
            cmd.Parameters.Add(new SqlParameter("K_FUSERID", (object)CUserSession.fUserId));
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                this.Categories.Add(reader["fName"].ToString());
            }
            con.Close();
        }
    }
}
