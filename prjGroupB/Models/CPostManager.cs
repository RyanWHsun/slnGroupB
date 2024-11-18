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
        public void insert(CPost post)
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
            cmd.Parameters.Add(new SqlParameter("K_FTITLE", (object)post.fTitle));
            cmd.Parameters.Add(new SqlParameter("K_FCONTENT", (object)post.fContent));
            cmd.Parameters.Add(new SqlParameter("K_FCREATEDAT", (object)DateTime.Now));
            cmd.Parameters.Add(new SqlParameter("K_FISPUBLIC", (object)post.fIsPublic));
            cmd.ExecuteNonQuery();
            if (!string.IsNullOrEmpty(post.fCategory))
            {
                sql = "SELECT * FROM tPostCategories ";
                sql += "WHERE fUserId = @K_FUSERID AND fName = @K_FCATEGORY";
                cmd.CommandText = sql;
                cmd.Parameters.Add(new SqlParameter("K_FCATEGORY", (object)post.fCategory));
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
            if (post.fImages != null)
            {
                foreach (byte[] image in post.fImages)
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
            if (post.fTags != null)
            {
                foreach (string tag in post.fTags)
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
            string sql = "SELECT fTagId FROM tPostAndTag WHERE fPostId = @K_FPOSTID";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = _connectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = sql;
            cmd.Parameters.Add(new SqlParameter("K_FPOSTID", (object)post.fPostId));
            SqlDataReader reader = cmd.ExecuteReader();
            List<int> tagIds = new List<int>();
            while (reader.Read())
            {
                tagIds.Add(Convert.ToInt32(reader["fTagId"]));
            }
            reader.Close();
            sql = "DELETE FROM tPostAndTag WHERE fPostId = @K_FPOSTID";
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            foreach (int tagId in tagIds)
            {
                cmd.Parameters.Clear();
                sql = "DELETE FROM tPostTags WHERE fTagId = @K_FTAGID";
                cmd.Parameters.Add(new SqlParameter("K_FTAGID", (object)tagId));
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("K_FPOSTID", (object)post.fPostId));
            sql = "DELETE FROM tPostImages WHERE fPostId = @K_FPOSTID";
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            sql = "DELETE FROM tPosts WHERE fPostId = @K_FPOSTID";
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void update(CPost post)
        {
            string sql = "UPDATE tPosts ";
            sql += "SET ";
            sql += "fTitle = @K_FTITLE,";
            sql += "fContent = @K_FCONTENT,";
            sql += "fUpdatedAt = @K_FUPDATEDAT,";
            sql += "fIsPublic = @K_FISPUBLIC ";
            sql += "WHERE fPostId = @K_FPOSTID";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = _connectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = sql;
            cmd.Parameters.Add(new SqlParameter("K_FTITLE", (object)post.fTitle));
            cmd.Parameters.Add(new SqlParameter("K_FCONTENT", (object)post.fContent));
            cmd.Parameters.Add(new SqlParameter("K_FUPDATEDAT", (object)DateTime.Now));
            cmd.Parameters.Add(new SqlParameter("K_FISPUBLIC", (object)post.fIsPublic));
            cmd.Parameters.Add(new SqlParameter("K_FPOSTID", (object)post.fPostId));
            cmd.Parameters.Add(new SqlParameter("K_FUSERID", (object)CUserSession.fUserId));
            cmd.ExecuteNonQuery();
            int categoryId = 0;
            SqlDataReader reader;
            if (!string.IsNullOrEmpty(post.fCategory))
            {
                sql = "SELECT * FROM tPostCategories ";
                sql += "WHERE fUserId = @K_FUSERID AND fName = @K_FCATEGORY";
                cmd.CommandText = sql;
                cmd.Parameters.Add(new SqlParameter("K_FCATEGORY", (object)post.fCategory));
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    categoryId = (int)reader["fCategoryId"];
                }
                reader.Close();
                sql = "UPDATE tPosts ";
                sql += "SET fCategoryId = @K_FCATEGORYID ";
                sql += "WHERE fPostId = @K_FPOSTID";
                cmd.CommandText = sql;
                cmd.Parameters.Add(new SqlParameter("K_FCATEGORYID", (object)categoryId));
                cmd.ExecuteNonQuery();
            }

            sql = "SELECT fTagId FROM tPostAndTag WHERE fPostId = @K_FPOSTID";
            cmd.CommandText = sql;
            reader = cmd.ExecuteReader();
            List<int> tagIds = new List<int>();
            while (reader.Read())
            {
                tagIds.Add(Convert.ToInt32(reader["fTagId"]));
            }
            reader.Close();
            sql = "DELETE FROM tPostAndTag WHERE fPostId = @K_FPOSTID";
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            foreach (int tagId in tagIds)
            {
                cmd.Parameters.Clear();
                sql = "DELETE FROM tPostTags WHERE fTagId = @K_FTAGID";
                cmd.Parameters.Add(new SqlParameter("K_FTAGID", (object)tagId));
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            cmd.Parameters.Clear();
            sql = "DELETE FROM tPostImages WHERE fPostId = @K_FPOSTID";
            cmd.Parameters.Add(new SqlParameter("K_FPOSTID", (object)post.fPostId));
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            if (post.fImages != null)
            {
                foreach (byte[] image in post.fImages)
                {
                    cmd.Parameters.Clear();
                    sql = "INSERT INTO tPostImages(";
                    sql += "fPostId,";
                    sql += "fImage";
                    sql += ")VALUES(";
                    sql += "@K_FPOSTID,";
                    sql += "@K_FIMAGE)";
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SqlParameter("K_FPOSTID", (object)post.fPostId));
                    cmd.Parameters.Add(new SqlParameter("K_FIMAGE", (object)image));
                    cmd.ExecuteNonQuery();
                }
            }
            if (post.fTags != null)
            {
                foreach (string tag in post.fTags)
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
                    cmd.Parameters.Add(new SqlParameter("K_FPOSTID", (object)post.fPostId));
                    cmd.Parameters.Add(new SqlParameter("K_FTAGID", (object)tagId));
                    cmd.ExecuteNonQuery();
                }
            }
            con.Close();
        }
    public List<CPost> getUserPosts()
    {
        string sql = "SELECT * FROM tPosts AS p ";
        sql += "LEFT JOIN tPostCategories AS c ";
        sql += "ON p.fCategoryId = c.fCategoryId ";
        sql += "WHERE p.fUserId = @K_FUSERID ";
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
            if (reader["fName"] != DBNull.Value)
                post.fCategory = reader["fName"].ToString();
            userPosts.Add(post);
        }
        reader.Close();
        sql = "SELECT i.fPostId, fImage FROM tPosts AS p ";
        sql += "JOIN tPostImages AS i ";
        sql += "ON p.fPostId = i.fPostId ";
        sql += "WHERE fUserId = @K_FUSERID";
        cmd.CommandText = sql;
        reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            foreach (CPost post in userPosts)
            {
                if (Convert.ToInt32(reader["fPostId"]) == post.fPostId)
                {
                    post.fImages.Add((byte[])reader["fImage"]);
                }
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
