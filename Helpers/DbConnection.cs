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
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error encountered: " + e.Message);
            }
        }

        private static void CreateBooksTable()
        {
            try

            {
                using (var connection = new SqliteConnection($"Data Source={dataSource}"))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "CREATE TABLE books(title TEXT, authors TEXT)";
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error encountered: " + e.Message);
            }
        }

        public static void AddBook(string title, string authorNames)
        {
            try
            {
                CreateBooksTable();
                Boolean exists = CheckIfBookAlreadyExists(title);

                if (exists)
                {
                    return;
                }
                else
                {
                    using (var connection = new SqliteConnection($"Data Source={dataSource}"))
                    {
                        connection.Open();
                        var command = connection.CreateCommand();
                        command.CommandText = "INSERT INTO books (title, authors) VALUES (@title, @authors)";
                        command.Parameters.AddWithValue("@title", title);
                        command.Parameters.AddWithValue("@authors", authorNames);
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
                        while (reader.Read())
                            return true;
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error encountered: " + e.Message);
                return false;
            }
        }

        public static List<Book> ListBooks()
        {
            var books = new List<Book>();
            string commandText = "SELECT * FROM books";
            try
            {
                using (var connection = new SqliteConnection($"Data Source={dataSource}"))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = commandText;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var title = reader.GetString(0);
                            var authors = reader.GetString(1);

                            Book b = new Book(title, authors);
                            books.Add(b);
                        }
                    }

                }
                return books;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error encountered: " + e.Message);
                return books;
            }
        }

    }
}
