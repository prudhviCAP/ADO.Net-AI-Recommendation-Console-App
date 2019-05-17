using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIRecommendationEngine.Entities;
using AIRecommendationEngine.Integration;

namespace AIRecommendationEngine.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            AIRecommendationEngineRecommender recommender = new AIRecommendationEngineRecommender();
            Preference preference = new Preference();
            Console.WriteLine("Enter Your Details to get Recommendations");
            Console.Write("Book ISBN : ");
            preference.ISBN = Console.ReadLine();
            Console.Write("Your Age : ");
            preference.Age = int.Parse(Console.ReadLine());
            Console.Write("State Where you reside : ");
            preference.State = Console.ReadLine();
            Console.WriteLine("Enter Number of books you wish to look : ");
            int limit = int.Parse(Console.ReadLine());

            Console.WriteLine("Book Title\t\t\t\tBook Author\tISBN\tPublisher\tYearOfPublication");
            List<Book> books = new List<Book>();
            books = recommender.Recommend(preference, limit);
            foreach (var bo in books)
            {
                Console.WriteLine(bo.BookTitle + "\t\t\t" + bo.BookAuthor + "\t" + bo.ISBN + "\t" + bo.Publisher + "\t" + bo.YearOfPublication);
            }
        }
    }
}
