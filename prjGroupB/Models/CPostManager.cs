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
        public void insert(CPost p)
        {
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
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbGroupB;Integrated Security=True";
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = sql;
            cmd.Parameters.Add(new SqlParameter("K_FUSERID", (object)CUserSession.fUserId));
            cmd.Parameters.Add(new SqlParameter("K_FTITLE", (object)p.fTitle));
            cmd.Parameters.Add(new SqlParameter("K_FCONTENT", (object)p.fContent));
            cmd.Parameters.Add(new SqlParameter("K_FCREATEDAT", (object)DateTime.Now));
            cmd.Parameters.Add(new SqlParameter("K_FISPUBLIC", (object)p.fIsPublic));
            cmd.ExecuteNonQuery();
            sql = "SELECT MAX(fPostId) AS PostId FROM tPosts WHERE fUserId = @K_FUSERID";
            cmd.CommandText = sql;
            SqlDataReader reader = cmd.ExecuteReader();
            int postId = 0;
            if (reader.Read())
            {
                postId = (int)reader["PostId"];
            }
            reader.Close();

            sql = "INSERT INTO tPostImages(";
            sql += "fPostId,";
            sql += "fImage";
            sql += ")VALUES(";
            sql += "@K_FPOSTID,";
            sql += "@K_FIMAGE)";
            cmd.CommandText = sql;
            cmd.Parameters.Add(new SqlParameter("K_FPOSTID", (object)postId));
            cmd.Parameters.Add(new SqlParameter("K_FIMAGE", (object)p.fImage));
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
