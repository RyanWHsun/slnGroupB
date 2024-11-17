﻿using System;
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
    }
}
