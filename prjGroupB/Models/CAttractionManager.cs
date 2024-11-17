using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace prjGroupB.Models {
    public class CAttractionManager {
        public string pipe = "np:\\\\.\\pipe\\LOCALDB#B5FE6A17\\tsql\\query;";

        // 新增景點分類
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
            SqlParameter fAttractionCategoryName = new SqlParameter("K_fAttractionCategoryName", (object)category.fAttractionCategoryName);
            SqlParameter fDescription = new SqlParameter("K_fDescription", (object)category.fDescription);

            string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            try {
                using (SqlConnection connection = new SqlConnection(connectString)) {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.Add(fAttractionCategoryName);
                    command.Parameters.Add(fDescription);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex) {
            }
        }

        // 更新景點分類
        public void updateAttractionCategory(CAttractionCategory category) {
            string sql = "Update tAttractionCategories SET ";
            sql += "fAttractionCategoryName = @K_fAttractionCategoryName,";
            sql += "fDescription = @K_fDescription ";
            sql += "WHERE fAttractionCategoryId = @K_fAttractionCategoryId";

            // 防止 SQL Injection
            SqlParameter fAttractionCategoryName = new SqlParameter("K_fAttractionCategoryName", (object)category.fAttractionCategoryName);
            SqlParameter fDescription = new SqlParameter("K_fDescription", (object)category.fDescription);
            SqlParameter fAttractionCategoryId = new SqlParameter("K_fAttractionCategoryId", (object)category.fAttractionCategoryId);

            string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            try {
                using (SqlConnection connection = new SqlConnection(connectString)) {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.Add(fAttractionCategoryName);
                    command.Parameters.Add(fDescription);
                    command.Parameters.Add(fAttractionCategoryId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex) {
            }
        }

        // 新增景點標籤
        public void createAttractionTag(CAttractionTag tag) {
            string sql = "INSERT tAttractionTags (";
            sql += "fTagName, ";
            sql += "fCreatedDate";
            sql += " ) VALUES (";
            sql += "@K_fTagName, ";
            sql += "GETDATE())";

            // 防止 SQL Injection
            SqlParameter fTagName = new SqlParameter("K_fTagName", (object)tag.fTagName);

            string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            try {
                using (SqlConnection connection = new SqlConnection(connectString)) {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.Add(fTagName);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex) {
            }
        }

        // 更新景點標籤
        public void updateAttractionTag(CAttractionTag tag) {
            string sql = "Update tAttractionTags SET ";
            sql += "fTagName = @K_fTagName ";
            sql += "WHERE fTagId = @K_fTagId";

            // 防止 SQL Injection
            SqlParameter fTagName = new SqlParameter("K_fTagName", (object)tag.fTagName);
            SqlParameter fTagId = new SqlParameter("K_fTagId", (object)tag.fTagId);

            string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            try {
                using (SqlConnection connection = new SqlConnection(connectString)) {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.Add(fTagName);
                    command.Parameters.Add(fTagId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex) {
            }
        }

        // 新增推薦
        public void createAttractionRecommendation(CAttractionRecommendation recommendation) {
            string sql = "INSERT tAttractionRecommendations (";
            sql += "fAttractionId, ";
            sql += "fRecommendationId, ";
            sql += "fReason, ";
            sql += "fCreatedDate";
            sql += ") VALUES (";
            sql += "@K_fAttractionId, ";
            sql += "@K_fRecommendationId, ";
            sql += "@K_fReason, ";
            sql += "GETDATE())";

            // 防止 SQL Injection
            SqlParameter fAttractionId = new SqlParameter("K_fAttractionId", (object)recommendation.fAttractionId);
            SqlParameter fRecommendationId = new SqlParameter("K_fRecommendationId", (object)recommendation.fRecommendationId);
            SqlParameter fReason = new SqlParameter("K_fReason", (object)recommendation.fReason);

            string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            try {
                using (SqlConnection connection = new SqlConnection(connectString)) {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.Add(fAttractionId);
                    command.Parameters.Add(fRecommendationId);
                    command.Parameters.Add(fReason);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex) {
                MessageBox.Show("景點或被推薦景點並不存在");
            }
        }

        // 更新推薦
        public void updateAttractionRecommendation(CAttractionRecommendation recommendation) {
            string sql = "Update tAttractionRecommendations SET ";
            sql += "fAttractionId = @K_fAttractionId, ";
            sql += "fRecommendationId = @K_fRecommendationId, ";
            sql += "fReason = @K_fReason ";
            sql += "WHERE fAttractionRecommendationId = @K_fAttractionRecommendationId";

            // 防止 SQL Injection
            SqlParameter fAttractionId = new SqlParameter("K_fAttractionId", (object)recommendation.fAttractionId);
            SqlParameter fRecommendationId = new SqlParameter("K_fRecommendationId", (object)recommendation.fRecommendationId);
            SqlParameter fReason = new SqlParameter("K_fReason", (object)recommendation.fReason);
            SqlParameter fAttractionRecommendationId = new SqlParameter("K_fAttractionRecommendationId", (object)recommendation.fAttractionRecommendationId);

            string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            try {
                using (SqlConnection connection = new SqlConnection(connectString)) {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.Add(fAttractionId);
                    command.Parameters.Add(fRecommendationId);
                    command.Parameters.Add(fReason);
                    command.Parameters.Add(fAttractionRecommendationId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex) {
                MessageBox.Show("景點或被推薦景點並不存在");
            }
        }


        public void createAttractionTicket(CAttractionTicket ticket) {
            string sql = "INSERT tAttractionTickets (";
            sql += "fAttractionId, ";
            sql += "fTicketType, ";
            sql += "fPrice, ";
            sql += "fDiscountInformation, ";
            sql += "fCreatedDate";
            sql += ") VALUES (";
            sql += "@K_fAttractionId, ";
            sql += "@K_fTicketType, ";
            sql += "@K_fPrice, ";
            sql += "@K_fDiscountInformation, ";
            sql += "GETDATE())";

            // 防止 SQL Injection
            SqlParameter fAttractionId = new SqlParameter("K_fAttractionId", (object)ticket.fAttractionId);
            SqlParameter fTicketType = new SqlParameter("K_fTicketType", (object)ticket.fTicketType);
            SqlParameter fPrice = new SqlParameter("K_fPrice", (object)ticket.fPrice);
            SqlParameter fDiscountInformation = new SqlParameter("K_fDiscountInformation", (object)ticket.fDiscountInformation);

            string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            try {
                using (SqlConnection connection = new SqlConnection(connectString)) {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.Add(fAttractionId);
                    command.Parameters.Add(fTicketType);
                    command.Parameters.Add(fPrice);
                    command.Parameters.Add(fDiscountInformation);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex) {
                MessageBox.Show("景點並不存在");
            }
        }

        public void updateAttractionTicket(CAttractionTicket ticket) {
            string sql = "Update tAttractionTickets SET ";
            sql += "fAttractionId = @K_fAttractionId, ";
            sql += "fTicketType = @K_fTicketType, ";
            sql += "fPrice = @K_fPrice, ";
            sql += "fDiscountInformation = @K_fDiscountInformation, ";
            sql += "fCreatedDate = @K_fCreatedDate ";
            sql += "WHERE fAttractionTicketId = @K_fAttractionTicketId";

            // 防止 SQL Injection
            SqlParameter fAttractionId = new SqlParameter("K_fAttractionId", (object)ticket.fAttractionId);
            SqlParameter fTicketType = new SqlParameter("K_fTicketType", (object)ticket.fTicketType);
            SqlParameter fPrice = new SqlParameter("K_fPrice", (object)ticket.fPrice);
            SqlParameter fDiscountInformation = new SqlParameter("K_fDiscountInformation", (object)ticket.fDiscountInformation);
            SqlParameter fCreatedDate = new SqlParameter("K_fCreatedDate", (object)ticket.fCreatedDate);
            SqlParameter fAttractionTicketId = new SqlParameter("K_fAttractionTicketId", (object)ticket.fAttractionTicketId);

            string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            try {
                using (SqlConnection connection = new SqlConnection(connectString)) {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.Add(fAttractionId);
                    command.Parameters.Add(fTicketType);
                    command.Parameters.Add(fPrice);
                    command.Parameters.Add(fDiscountInformation);
                    command.Parameters.Add(fCreatedDate);
                    command.Parameters.Add(fAttractionTicketId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex) {
                MessageBox.Show("景點並不存在");
            }
        }

        public void createAttractionComment(CAttractionComment comment) {
            string sql = "INSERT tAttractionComments (";
            sql += "fAttractionId, ";
            sql += "fUserId, ";
            sql += "fRating, ";
            sql += "fComment, ";
            sql += "fCreatedDate";
            sql += ") VALUES (";
            sql += "@K_fAttractionId, ";
            sql += "@K_fUserId, ";
            sql += "@K_fRating, ";
            sql += "@K_fComment, ";
            sql += "GETDATE())";

            // 防止 SQL Injection
            SqlParameter fAttractionId = new SqlParameter("K_fAttractionId", (object)comment.fAttractionId);
            SqlParameter fUserId = new SqlParameter("K_fUserId", (object)comment.fUserId);
            SqlParameter fRating = new SqlParameter("K_fRating", (object)comment.fRating);
            SqlParameter fComment = new SqlParameter("K_fComment", (object)comment.fComment);

            string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            try {
                using (SqlConnection connection = new SqlConnection(connectString)) {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.Add(fAttractionId);
                    command.Parameters.Add(fUserId);
                    command.Parameters.Add(fRating);
                    command.Parameters.Add(fComment);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex) {
                MessageBox.Show("景點或使用者並不存在");
            }
        }

        public void updateAttractionComment(CAttractionComment comment) {
            string sql = "Update tAttractionComments SET ";
            sql += "fAttractionId = @K_fAttractionId, ";
            sql += "fUserId = @K_fUserId, ";
            sql += "fRating = @K_fRating, ";
            sql += "fComment = @K_fComment, ";
            sql += "fCreatedDate = @K_fCreatedDate ";
            sql += "WHERE fCommentId = @K_fCommentId";

            // 防止 SQL Injection
            SqlParameter fAttractionId = new SqlParameter("K_fAttractionId", (object)comment.fAttractionId);
            SqlParameter fUserId = new SqlParameter("K_fUserId", (object)comment.fUserId);
            SqlParameter fRating = new SqlParameter("K_fRating", (object)comment.fRating);
            SqlParameter fComment = new SqlParameter("K_fComment", (object)comment.fComment);
            SqlParameter fCreatedDate = new SqlParameter("K_fCreatedDate", (object)comment.fCreatedDate);
            SqlParameter fCommentId = new SqlParameter("K_fCommentId", (object)comment.fCommentId);

            string connectString = @"Data Source=" + pipe + "Initial Catalog=dbGroupB;Integrated Security=True";
            try {
                using (SqlConnection connection = new SqlConnection(connectString)) {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.Add(fAttractionId);
                    command.Parameters.Add(fUserId);
                    command.Parameters.Add(fRating);
                    command.Parameters.Add(fComment);
                    command.Parameters.Add(fCreatedDate);
                    command.Parameters.Add(fCommentId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex) {
                MessageBox.Show("景點或使用者並不存在");
            }
        }
    }
}
