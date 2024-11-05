using Microsoft.Data.SqlClient;
using System.Data;

namespace Library
{
    public class DataAccessLayer
    {
        #region Свойства
        public static string ServerName { get; set; } = @"MSI";
        public static string DatabaseName { get; set; } = "Print";
        public static string Login { get; set; } = @"MSI\slavv";
        public static string Password { get; set; } = "";
        public static string ConnectionString
        {
            get
            {
                SqlConnectionStringBuilder builder = new()
                {
                    DataSource = ServerName,
                    UserID = Login,
                    //Password = Password,
                    InitialCatalog = DatabaseName,
                    IntegratedSecurity = true,
                    TrustServerCertificate = true
                };
                return builder.ConnectionString;
            }
        }
        #endregion

        #region Методы
        public static bool IsPromocodeExist(string promocode)
        {
            SqlConnection connection = new(ConnectionString);
            connection.Open();

            string query = "SELECT * FROM Promocode WHERE Name = @promocode AND IsActivity = 1;";

            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@promocode", promocode);

            return command.ExecuteScalar() != null;
        }

        public static void PromocodeActivate(string promocode)
        {
            SqlConnection connection = new(ConnectionString);
            connection.Open();

            string query = "UPDATE Promocode SET IsActivity = 0 WHERE Name = @promocode;";

            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@promocode", promocode);
            
            command.ExecuteNonQuery();
        }

        public static int GetCountActivePromocodes()
        {
            SqlConnection connection = new(ConnectionString);
            connection.Open();

            string query = "SELECT COUNT(*) FROM Promocode WHERE IsActivity = 1;";

            SqlCommand command = new(query, connection);

            return Convert.ToInt32(command.ExecuteScalar());
        }
        #endregion
    }
}
