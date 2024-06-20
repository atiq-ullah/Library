using Microsoft.Data.Sqlite;

namespace Library
{
    public sealed class DbConnection
    {
        public DbConnection() { }
        public static void GetBooks()
        {
            try
            {
                using (var connection = new SqliteConnection($"Data Source={dataSource}"))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM books";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var str = reader.GetString(1);
                            Console.WriteLine("Title: " + str);
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error encountered: " + e.Message);
            }
        }

        public static void RemoveBook(string title)
        {
            try
            {
                Boolean exists = CheckIfBookAlreadyExists(title);

                if (!exists)
                {
                    Console.WriteLine("Book does not exist, nothing to remove.");
                    return;
                }
                else
                {
                    using (var connection = new SqliteConnection($"Data Source={dataSource}"))
                    {
                        connection.Open();
                        var command = connection.CreateCommand();
                        command.CommandText = "DELETE FROM books WHERE title = @title";
                        command.Parameters.AddWithValue("@title", title);
                        command.ExecuteNonQuery();
                        Console.WriteLine($"{title} removed successfully");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error encountered: " + e.Message);
            }
        }


        public static void AddBook(string title)
        {
            try
            {
                Boolean exists = CheckIfBookAlreadyExists(title);

                if (exists)
                {
                    Console.WriteLine("Book already exists");
                    return;
                }
                else
                {
                    using (var connection = new SqliteConnection($"Data Source={dataSource}"))
                    {
                        connection.Open();
                        var command = connection.CreateCommand();
                        command.CommandText = "INSERT INTO books (title) VALUES (@title)";
                        command.Parameters.AddWithValue("@title", title);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error encountered: " + e.Message);
            }
        }

        private static string dataSource = "db.sqlite3";

        public static Boolean CheckIfBookAlreadyExists(string title)
        {
            try
            {
                using (var connection = new SqliteConnection($"Data Source={dataSource}"))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM books WHERE title = @title ";
                    command.Parameters.AddWithValue("@title", title);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return true;
                        }
                    }

                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error encountered: " + e.Message);
                return false;
            }
        }

        public static List<string> ListBooks()
        {
            List<string> titles = new List<string>();
            try
            {
                using (var connection = new SqliteConnection($"Data Source={dataSource}"))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM books";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            titles.Add(reader.GetString(1));
                        }
                    }

                }
                return titles;

            }
            catch (Exception e)
            {
                Console.WriteLine("Error encountered: " + e.Message);
                return titles;
            }
        }

    }
}
