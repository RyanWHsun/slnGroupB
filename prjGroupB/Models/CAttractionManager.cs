using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGroupB.Models {
    public class CAttractionManager {
        public string pipe = "np:\\\\.\\pipe\\LOCALDB#7C99B448\\tsql\\query;";

        public void createAttractionCategory(CAttractionCategory category) {
            string sql = "INSERT tAttractionCategories (";
            sql += "fAttractionCategoryName, ";
            sql += "fDescription, ";
            sql += "fCreateDate";
            sql += " ) VALUES (";
            sql += "@K_fAttractionCategoryName, ";
            sql += "@K_fDescription, ";
            sql += "GETDATE())";

            // 防止 SQL Injection
            SqlParameter categoryName = new SqlParameter("K_fAttractionCategoryName", (object)category.fAttractionCategoryName);
            SqlParameter categoryDescription = new SqlParameter("K_fDescription", (object)category.fDescription);

            string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            try {
                using (SqlConnection connection = new SqlConnection(connectString)) {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.Add(categoryName);
                    command.Parameters.Add(categoryDescription);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex) {
            }
        }
    }
}
