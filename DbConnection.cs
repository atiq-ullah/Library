using Microsoft.Data.Sqlite;

namespace Library
{
    public sealed class DbConnection
    {
        public DbConnection() { }
        public static void Connect()
        {
            using (var connection = new SqliteConnection("Data Source=db.sqlite3"))
            {
                connection.Open();
                var id = 2;
                var command = connection.CreateCommand();
                command.CommandText =
                @"
        SELECT id
        FROM user
        WHERE id = $id
    ";
                command.Parameters.AddWithValue("$id", id);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader.GetString(0);

                        Console.WriteLine($"Hello, {name}!");
                    }
                }
            }
        }

        private string filename = "./db.sqlite3";
    }
}
