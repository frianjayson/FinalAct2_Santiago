
using System;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace frianlogin
{
    public static class DatabaseHelper
    {
        private static readonly string DatabasePath =
            Path.GetFullPath(
                Path.Combine(
                    AppContext.BaseDirectory,
                    "..",
                    "..",
                    "..",
                    "frianlogin.accdb"
                )
            );

        private static readonly string ConnectionString =
            $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={DatabasePath};Persist Security Info=False;";

        public static string GetDatabasePath()
        {
            return DatabasePath;
        }

        public static OleDbConnection GetConnection()
        {
            if (!File.Exists(DatabasePath))
            {
                throw new FileNotFoundException(
                    "Database file not found:\n" + DatabasePath
                );
            }

            return new OleDbConnection(ConnectionString);
        }

        public static bool TestConnection()
        {
            using (OleDbConnection connection = GetConnection())
            {
                connection.Open();
                return connection.State == ConnectionState.Open;
            }
        }

        public static bool UsernameExists(string username)
        {
            using (OleDbConnection connection = GetConnection())
            {
                connection.Open();

                string query =
                    "SELECT COUNT(*) FROM AppUsers WHERE Username = ?";

                using (OleDbCommand command =
                    new OleDbCommand(query, connection))
                {
                    command.Parameters.Add(
                        "?", OleDbType.VarWChar, 50
                    ).Value = username.Trim();

                    return Convert.ToInt32(
                        command.ExecuteScalar()
                    ) > 0;
                }
            }
        }

        public static bool RegisterUser(
            string fullName,
            string username,
            string password)
        {
            using (OleDbConnection connection = GetConnection())
            {
                connection.Open();

                string query =
                    "INSERT INTO AppUsers " +
                    "(FullName, Username, PasswordHash) " +
                    "VALUES (?, ?, ?)";

                using (OleDbCommand command =
                    new OleDbCommand(query, connection))
                {
                    command.Parameters.Add(
                        "?", OleDbType.VarWChar, 100
                    ).Value = fullName.Trim();

                    command.Parameters.Add(
                        "?", OleDbType.VarWChar, 50
                    ).Value = username.Trim();

                    command.Parameters.Add(
                        "?", OleDbType.LongVarWChar
                    ).Value = PasswordHasher.HashPassword(password);

                    int result = command.ExecuteNonQuery();

                    return result == 1;
                }
            }
        }

        public static (string FullName, string PasswordHash)?
            FindUser(string username)
        {
            using (OleDbConnection connection = GetConnection())
            {
                connection.Open();

                string query =
                    "SELECT FullName, PasswordHash " +
                    "FROM AppUsers WHERE Username = ?";

                using (OleDbCommand command =
                    new OleDbCommand(query, connection))
                {
                    command.Parameters.Add(
                        "?", OleDbType.VarWChar, 50
                    ).Value = username.Trim();

                    using (OleDbDataReader reader =
                        command.ExecuteReader())
                    {
                        if (reader != null && reader.Read())
                        {
                            return (
                                reader["FullName"].ToString() ?? "",
                                reader["PasswordHash"].ToString() ?? ""
                            );
                        }
                    }
                }
            }

            return null;
        }
    }
}
