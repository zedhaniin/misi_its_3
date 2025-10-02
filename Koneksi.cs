using System.Data.SqlClient;
namespace ProjectKI

{
    internal class Koneksi
    {
        public static SqlConnection GetConnection()
        {
        string connectionString = @"Data Source=LAPTOP-4AVD05EN\SQLEXPRESS;Initial Catalog=TokoDB;Integrated Security=True;Encrypt=False;";
            return new SqlConnection(connectionString);
        }
    }
}