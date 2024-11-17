using prjGroupB.Views;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjGroupB.Models
{
    public class CPostManager
    {
        private string _connectionString = @"Data Source=.;Initial Catalog=dbGroupB;Integrated Security=True";
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
            SqlConnection con = new SqlConnection();
            con.ConnectionString = _connectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.Parameters.Add(new SqlParameter("K_FUSERID", (object)CUserSession.fUserId));
            SqlDataReader reader;
            int categoryId = 0;

            string sql = "INSERT INTO tPosts(";
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
                    cmd.Parameters.Clear();
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
                }
            }
            if (p.fTags != null)
            {
                foreach (string tag in p.fTags)
                {
                    cmd.Parameters.Clear();
                    sql = "INSERT INTO tPostTags(";
                    sql += "fTagName";
                    sql += ")VALUES(";
                    sql += "@K_FTAGNAME)";
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SqlParameter("K_FTAGNAME", (object)tag));
                    cmd.ExecuteNonQuery();
                    sql = "SELECT MAX(fTagId) AS TagId FROM tPostTags";
                    cmd.CommandText = sql;
                    reader = cmd.ExecuteReader();
                    int tagId = 0;
                    if (reader.Read())
                    {
                        tagId = (int)reader["TagId"];
                    }
                    reader.Close();
                    sql = "INSERT INTO tPostAndTag(";
                    sql += "fPostId,";
                    sql += "fTagId";
                    sql += ")VALUES(";
                    sql += "@K_FPOSTID,";
                    sql += "@K_FTAGID)";
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SqlParameter("K_FPOSTID", (object)postId));
                    cmd.Parameters.Add(new SqlParameter("K_FTAGID", (object)tagId));
                    cmd.ExecuteNonQuery();
                }
            }
            con.Close();
        }
        public void delete(CPost post)
        {
            MessageBox.Show(post.fPostId.ToString());
        }
        public List<CPost> getUserPosts()
        {
            string sql = "SELECT * FROM tPosts ";
            sql += "WHERE fUserId = @K_FUSERID ";
            sql += "ORDER BY fPostId";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = _connectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = sql;
            cmd.Parameters.Add(new SqlParameter("K_FUSERID", (object)CUserSession.fUserId));
            SqlDataReader reader = cmd.ExecuteReader();
            List<CPost> userPosts = new List<CPost>();
            while (reader.Read())
            {
                CPost post = new CPost();
                post.fPostId = Convert.ToInt32(reader["fPostId"]);
                post.fTitle = reader["fTitle"].ToString();
                post.fContent = reader["fContent"].ToString();
                post.fCreatedAt = Convert.ToDateTime(reader["fCreatedAt"]);
                if (reader["fUpdatedAt"] != DBNull.Value)
                    post.fUpdatedAt = Convert.ToDateTime(reader["fUpdatedAt"]);
                post.fIsPublic = Convert.ToBoolean(reader["fIsPublic"]);
                userPosts.Add(post);
            }
            reader.Close();
            foreach (CPost post in userPosts)
            {
                cmd.Parameters.Clear();
                sql = "SELECT fImage FROM tPosts AS p ";
                sql += "JOIN tPostImages AS i ";
                sql += "ON p.fPostId = i.fPostId ";
                sql += "WHERE fUserId = @K_FUSERID AND p.fPostId = @K_FPOSTID";
                cmd.CommandText = sql;
                cmd.Parameters.Add(new SqlParameter("K_FUSERID", (object)CUserSession.fUserId));
                cmd.Parameters.Add(new SqlParameter("K_FPOSTID", (object)post.fPostId));
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    post.fImages.Add((byte[])reader["fImage"]);
                }
                reader.Close();
            }
            reader.Close();
            foreach (CPost post in userPosts)
            {
                reader.Close();
                cmd.Parameters.Clear();
                sql = "SELECT fName FROM tPosts AS p ";
                sql += "LEFT JOIN tPostCategories AS c ";
                sql += "ON p.fCategoryId = c.fCategoryId ";
                sql += "WHERE fPostId = @K_FPOSTID";
                cmd.CommandText = sql;
                cmd.Parameters.Add(new SqlParameter("K_FPOSTID", (object)post.fPostId));
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (reader["fName"] == DBNull.Value)
                        continue;
                    post.fCategory = reader["fName"].ToString();
                }
            }
            con.Close();
            return userPosts;
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
            con.ConnectionString = _connectionString;
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
            con.ConnectionString = _connectionString;
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
