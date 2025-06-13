using System.Runtime.CompilerServices;

namespace SpaceTraders;

using Microsoft.Data.Sqlite;
using Dapper;
using SpaceTraders.Models;

public class SQLBoy {
    static string connectionString = "Data Source=Trade.sqlite;";

    public string InsertAccount(Account account) {
        if (account == null) {
            return "Cannot insert null account";
        }

        using (var connection = new SqliteConnection(connectionString)) {
            connection.Open();
            using (var command = connection.CreateCommand()) {
                command.CommandText = "PRAGMA foreign_keys = ON;";
                command.ExecuteNonQuery();
            }

            var sql =
                @"INSERT INTO Account (AccountID, AccountEmail, AccountCreatedAt) 
                  VALUES (@Id, @Email, @CreatedAt)";
            try {
                var rowsAffected = connection.Execute(sql, account);
                return $"{rowsAffected} row(s) inserted.";
            }
            catch (SqliteException ex) {
                return $"SQLite exception thrown. {ex.Message}";
            }
        }
    }

    public bool AccountExists(Account account) {
        using (var connection = new SqliteConnection(connectionString)) {
            connection.Open();
            if (account == null) {
                throw new Exception("Parameter given to accountExists was null. wtf.");
            }

            var sql = @"
                SELECT 
                    AccountID AS Id
                FROM Account 
                WHERE AccountID = @Id";

            var accountRow = connection.QuerySingleOrDefault<Account>(sql, new { account.Id });
            if (accountRow != null) {
                Console.WriteLine("Account exists.");
                return true;
            } else {
                return false;
            }
        }
    }

    public string InsertAgent(Agent agent) {
        if (agent == null) {
            return "Cannot insert null agent.";
        }

        using (var connection = new SqliteConnection(connectionString)) {
            connection.Open();
            using (var command = connection.CreateCommand()) {
                command.CommandText = "PRAGMA foreign_keys = ON;";
                command.ExecuteNonQuery();
            }

            var sql =
                @"INSERT INTO Agents (AgentAccountID, AgentSymbol, AgentHeadquarters, AgentCredits, AgentFaction, AgentShipCount) 
                  VALUES (@AccountId, @Symbol, @Headquarters, @Credits, @StartingFaction, @ShipCount)";
            try {
                var rowsAffected = connection.Execute(sql, agent);
                return $"{rowsAffected} row(s) inserted.";
            }
            catch (SqliteException ex) {
                return $"SQLite exception thrown. Maybe agent accountID does not match any account. {ex.Message}";
            }
        }
    }

    public bool AgentExists(Agent agent) {
        using (var connection = new SqliteConnection(connectionString)) {
            connection.Open();
            if (agent == null) {
                throw new Exception("Parameter given to agentExists was null. wtf.");
            }

            var sql = @"
                SELECT 
                    AgentAccountID AS AccountId, 
                    AgentSymbol AS Symbol, 
                    AgentHeadquarters AS Headquarters, 
                    AgentCredits AS Credits, 
                    AgentFaction AS StartingFaction, 
                    AgentShipCount AS ShipCount 
                FROM Agents 
                WHERE AgentAccountID = @AccountId";

            var agentRow = connection.QuerySingleOrDefault<Agent>(sql, new { agent.AccountId });
            Console.WriteLine(agentRow);
            if (agentRow != null) {
                Console.WriteLine("Agent exists.");
                return true;
            } else {
                return false;
            }
        }
    }

    public bool contractAccept(Agent contract) {
        throw new NotImplementedException();
    }
}