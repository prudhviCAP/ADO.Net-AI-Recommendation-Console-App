using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AIRecommendationEngine.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace AIRecommendationEngine.DataAccess
{
    public class CSVDataLoader : IDataLoader
    {
        public BookDetails Load()
        {
            List<Book> books = new List<Book>();
            List<User> users = new List<User>();
            List<BookUserRating> bookUserRatings = new List<BookUserRating>();            
            StreamReader reader1 = new StreamReader(@"D:\MyFolder\workspace\MyLearningDemos\AIRecommendationEngine.DataAccess\DataFiles\BX-Books.csv");
            StreamReader reader2 = new StreamReader(@"D:\MyFolder\workspace\MyLearningDemos\AIRecommendationEngine.DataAccess\DataFiles\BX-Users.csv");
            StreamReader reader3 = new StreamReader(@"D:\MyFolder\workspace\MyLearningDemos\AIRecommendationEngine.DataAccess\DataFiles\BX-Book-Ratings.csv");
            Thread t1 = new Thread(() => {
                reader1.ReadLine();

                string[] words;
                while (!reader1.EndOfStream)
                {
                    words = reader1.ReadLine().Split(';');
                    books.Add(new Book { ISBN = words[0].Trim('"'), BookTitle = words[1].Trim('"'), BookAuthor = words[2].Trim('"'), YearOfPublication = int.Parse(words[3].Trim('"')), Publisher = words[4].Trim('"'), Image_URL_S = words[5].Trim('"'), Image_URL_M = words[6].Trim('"'), Image_URL_L = words[7].Trim('"') });
                }
                reader1.Close();
            });
            Thread t2 = new Thread(() => {
                reader2.ReadLine();
                string[] words;
                while (!reader2.EndOfStream)
                {
                    words = reader2.ReadLine().Split(';');
                    users.Add(new User { UserId = int.Parse(words[0].Trim('"')), City = words[1].Trim('"').Split(',')[0],State = words[1].Trim('"').Split(',')[1].Trim(),Country = words[1].Trim('"').Split(',')[2].Trim(), Age = int.Parse(words[2].Trim('"')) });
                }
                reader2.Close();
            });
            Thread t3 = new Thread(() => {
                reader3.ReadLine();

                string[] words;
                while (!reader3.EndOfStream)
                {
                    words = reader3.ReadLine().Split(';');
                    bookUserRatings.Add(new BookUserRating { UserId = int.Parse(words[0].Trim('"')), ISBN = words[1].Trim('"'), BookRating = long.Parse(words[2].Trim('"')) });
                }
                reader3.Close();
            });
            BookDetails bookDetails = new BookDetails();       
            t1.Start();
            t2.Start();
            t3.Start();
            t1.Join();
            t2.Join();
            t3.Join();
            bookDetails.books = books;
            bookDetails.users = users;
            bookDetails.bookUserRatings = bookUserRatings;
            return bookDetails;
        }
    }
}
