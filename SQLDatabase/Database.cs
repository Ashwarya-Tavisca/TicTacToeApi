using System;
using System.Data.SqlClient;

namespace SQLDatabase
{
    public class Database
    {
        public SqlConnection Connect()
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Data Source = TAVDESK092\\SQLEXPRESS; Initial Catalog = TicTacToeDB ; Integrated Security = True";
                return conn;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.StackTrace);
                return null;
            }
        }
        public bool AddUsers(string firstName, string lastName, string userName, string accessToken)
        {
            SqlConnection conn = null;
            try
            {
                conn = Connect();
                conn.Open();
                string query = "Insert into Users(FirstName , LastName , UserName , AccessToken) values ( @firstName , @lastName , @userName , @accessToken)";
                SqlCommand sqlCommand = new SqlCommand(query, conn);
                sqlCommand.Parameters.Add(new SqlParameter("firstName", firstName));
                sqlCommand.Parameters.Add(new SqlParameter("lastName", lastName));
                sqlCommand.Parameters.Add(new SqlParameter("userName", userName));
                sqlCommand.Parameters.Add(new SqlParameter("accessToken", accessToken));
                sqlCommand.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.StackTrace);
                return false;
            }
        }
    }
}
