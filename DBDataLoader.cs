using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIRecommendationEngine.Entities;
using System.Configuration;
using System.Data.Common;
using System.Threading;

namespace AIRecommendationEngine.DataAccess
{
    public class DBDataLoader : IDataLoader
    {
        public BookDetails Load()
        {
            BookDetails bookDetails = new BookDetails();
            IDbConnection connection = null;
            string provider = ConfigurationManager.ConnectionStrings["BookUserRatingDatabase"].ProviderName;
            DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory(provider);
            connection = dbProviderFactory.CreateConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["BookUserRatingDatabase"].ConnectionString;
            IDbCommand cmd = connection.CreateCommand();
            cmd.Connection = connection;

            string getUsersQuery = "Select * from bx_users";
            string getBooksQuery = "Select * from bx_books";
            string getBookRatingsQuery = "Select * from bx_book_ratings";
            List<User> users = new List<User>();
            List<Book> books = new List<Book>();
            List<BookUserRating> bookUserRatings = new List<BookUserRating>();

            IDataReader reader = null;
            using (connection)
            {
                connection.Open();
                //try
                {
                    cmd.CommandText = getUsersQuery;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        User user = new User();

                        user.UserId = int.Parse($"{reader[0]}");
                        user.City = reader.GetString(1).Split(',')[0].Trim();
                        user.State = reader.GetString(1).Split(',')[1].Trim();
                        user.Country = reader.GetString(1).Split(',')[2].Trim();
                        int? val = reader["Age"] as int?;
                        user.Age = (val == null ? 0 : (int)val);

                        users.Add(user);
                    }
                    reader.Close();
                    cmd.CommandText = getBooksQuery;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Book book = new Book
                        {
                            ISBN = $"{reader[0]}",
                            BookTitle = $"{reader[1]}",
                            BookAuthor = $"{reader[2]}",
                            YearOfPublication = int.Parse($"{reader[3]}"),
                            Publisher = $"{reader[4]}",
                            Image_URL_S = $"{reader[5]}",
                            Image_URL_M = $"{reader[6]}",
                            Image_URL_L = $"{reader[7]}"
                        };

                        books.Add(book);
                    }
                    reader.Close();
                    cmd.CommandText = getBookRatingsQuery;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        BookUserRating bookUserRating = new BookUserRating
                        {
                            UserId = int.Parse($"{reader[0]}"),
                            ISBN = $"{reader[1]}",
                            BookRating = long.Parse($"{reader[2]}")
                        };

                        bookUserRatings.Add(bookUserRating);
                    }
                }
                //catch (SystemException se)
                //{
                //    throw se;
                //} 
                //finally
                //{
                    reader.Close();
                //}
            }
            bookDetails.users = users;
            bookDetails.books = books;
            bookDetails.bookUserRatings = bookUserRatings;
            return bookDetails;
        }
    }
}
