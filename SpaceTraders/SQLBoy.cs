namespace SpaceTraders;

using Microsoft.Data.Sqlite;
using Dapper;
using SpaceTraders.Models;

public static class SQLBoy {
    static string connectionString = "Trade.sqlite;";

    public static string insertAgent(Agent agent) {
        using (var connection = new SqliteConnection(connectionString)) {
            connection.Open();
            using (var command = connection.CreateCommand()) {
                command.CommandText = "PRAGMA foreign_keys = ON;";
                command.ExecuteNonQuery();
            }
            var sql =
                @"INSERT INTO Agents (AgentAccountID, AgentSymbol, AgentHeadquarters, AgentCredits, AgentFaction, AgentShipCount) VALUES (@accountID, @symbol, @headquarters, @credits, @faction, @shipCount)";
            var rowsAffected = connection.Execute(sql, agent);
            return $"{rowsAffected} row(s) inserted.";
        }
    }
}