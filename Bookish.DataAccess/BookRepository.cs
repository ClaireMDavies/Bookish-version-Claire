using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System;


namespace Bookish.DataAccess
{
    public class BookRepository
    {
        private string connectionString = @"Server=localhost;Database=bookish;Trusted_Connection=True;";
        

        public IEnumerable<Book> GetAllBooks()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection.Query<Book>("SELECT * FROM books");
        }

        public IEnumerable<Book>GetCopiesBorrowedByUser(int user_id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection.Query<Book>($"SELECT * FROM copies INNER JOIN books ON copies.book_id=books.id WHERE user_id= { user_id }");
        }

        public IEnumerable<Book>searchBooks(string searchString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection.Query<Book>($"SELECT * FROM books WHERE title='{searchString }' OR author= '{searchString}'");
        }
        

        public Book GetBook(int book_id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection.QuerySingle<Book>($"SELECT * FROM books WHERE id={book_id}");
        }

        public IEnumerable<Copy> GetCopiesofBook(int book_id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection.Query<Copy>($"SELECT * FROM copies WHERE book_id={book_id} AND user_id IS NULL");
        }
        public IEnumerable<Copy> GetCopiesofBookAssignedToUser(int book_id, int user_id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection.Query<Copy>($"SELECT * FROM copies WHERE book_id={book_id} AND user_id={user_id}");
        }
        public int GetNumberofAvailableCopies(int book_id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection.Query<Copy>($"SELECT * FROM copies WHERE book_id={book_id} AND user_id IS NULL").Count();
        }

        //AddBook(Book book, int numCopies)
        public int CreateBook(string title, string author)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            var book_id = connection.QuerySingle<int>("INSERT INTO books(title, author) values (@title, @author); SELECT SCOPE_IDENTITY()", new { title, author });
            
            return book_id;        
        }

        //BorrowBook(int book-id, int user-id, DateTime due-date)

        public int GetUseridByName(string searchName)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection.QuerySingle<User>($"SELECT id FROM users WHERE name='{searchName}'").id;
        }

        public void CreateUser(string name)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Execute("INSERT INTO users(name) values (@name)", new { name });
        }

        public void CreateCopies(int book_id, int amount)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            for (int i = 0; i < amount; i++)
            {
                connection.Execute("INSERT INTO copies(book_id) values (@book_id)", new { book_id });
            }
        }

        public void AssignCopy(int book_id, int user_id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            IEnumerable<Copy> copies = GetCopiesofBook(book_id);
            string date = DateTime.Today.AddDays(21).Date.ToString("yyyy-MM-dd");
            if (copies.Any())
            {
                connection.Execute($"UPDATE copies SET user_id={user_id}, due_date='{date}' WHERE id={copies.First().id}");
            }
        }

        public void ReturnCopy(int copy_id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Execute($"UPDATE copies SET user_id=null, due_date=null WHERE id={copy_id}");
        }

    }
}
