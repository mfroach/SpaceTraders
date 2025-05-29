namespace SpaceTraders;

using Microsoft.Data.Sqlite;
using Dapper;

public class SQLBoy {
    public void connectDB() {
        string connectionString = "Data Source=Trade.sqlite; ForeignKeys=True";
        using (var connection = new SqliteConnection(connectionString)) {
            connection.Open();

            using (var command = connection.CreateCommand()) {
                command.CommandText = "PRAGMA foreign_keys = ON;";
                command.ExecuteNonQuery();
            }
        }
    }
}