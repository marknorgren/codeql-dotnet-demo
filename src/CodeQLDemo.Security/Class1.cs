using System;
using System.Data.SqlClient;

namespace CodeQLDemo.Security
{
    public class SecurityUtils
    {
        // Security issue: SQL Injection vulnerability
        public static void ExecuteQuery(string userInput)
        {
            string connectionString = "Server=myserver;Database=mydb;User Id=sa;Password=123456;";
            string query = "SELECT * FROM Users WHERE Username = '" + userInput + "'";
            
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Security issue: XSS vulnerability
        public static string FormatUserInput(string userInput)
        {
            // Quality issue: Unnecessary string interpolation
            return $"{userInput}";
        }

        // Quality issue: Empty catch block
        public static void ValidateUser(string username)
        {
            try
            {
                // Some validation logic
                throw new Exception("Validation failed");
            }
            catch (Exception)
            {
                // Empty catch block - bad practice
            }
        }
    }
}
