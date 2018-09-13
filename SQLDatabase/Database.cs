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
        public bool AddLog(string requestData, string responseData, string exceptionData, DateTime date)
        {
            SqlConnection conn = null;
            try
            {
                conn = Connect();
                conn.Open();
                string query = "Insert into LogDatabase(RequestData,ResponseData,ExceptionData,Date) values ( @requestData , @responseData , @exceptionData , @date)";
                SqlCommand sqlCommand = new SqlCommand(query, conn);
                sqlCommand.Parameters.Add(new SqlParameter("requestData", requestData));
                sqlCommand.Parameters.Add(new SqlParameter("responseData", responseData));
                sqlCommand.Parameters.Add(new SqlParameter("exceptionData", exceptionData));
                sqlCommand.Parameters.Add(new SqlParameter("date", date));
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
        public bool CheckAccessToken(string accessToken)
        {
            SqlConnection conn = null;
            try
            {
                conn = Connect();
                conn.Open();
                string query = "Select * from Users where AccessToken=@accessToken";
                SqlCommand sqlCommand = new SqlCommand(query, conn);
                sqlCommand.Parameters.Add(new SqlParameter("@accessToken", accessToken));
                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                bool flag = false;
                if (dataReader.Read())
                    flag = true;
                conn.Close();
                return flag;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.StackTrace);
                return false;
            }
        }
        public string GetUsername(string accessToken)
        {
            SqlConnection conn = null;
            try
            {
                string username = "";
                conn = Connect();
                conn.Open();
                string query = "Select * from Users where AccessToken=@accessToken";
                SqlCommand sqlCommand = new SqlCommand(query, conn);
                sqlCommand.Parameters.Add(new SqlParameter("@accessToken", accessToken));
                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                if (dataReader.Read())
                    username = dataReader[3].ToString();
                conn.Close();
                return username;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.StackTrace);
                return "";
            }
        }







    }
}

