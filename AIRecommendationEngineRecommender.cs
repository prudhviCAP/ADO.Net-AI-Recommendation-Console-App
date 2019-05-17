using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIRecommendationEngine.Entities;
using AIRecommendationEngine.RatingAggregator;
using AIRecomendationEngine.CoreEngine;
using AIRecommendationEngine.DataAccess;
using AIRecommendation.BooksDataService;

namespace AIRecommendationEngine.Integration
{
    public class AIRecommendationEngineRecommender
    {
        public List<Book> Recommend(Preference preference,int limit)
        {
            BooksDataService dataService = new BooksDataService();
            Dictionary<string, double> pairs = new Dictionary<string, double>();
            List<Book> books = new List<Book>();
            IDataLoader dataLoader = new CSVDataLoader();
            BookDetails bookDetails = new BookDetails();
            bookDetails = dataService.GetBookDetails();
            Dictionary<string, List<long>> keyValuePairs = new Dictionary<string, List<long>>();
            IRatingAggregator aggregator = new RatingAggregatorClass();
            keyValuePairs = aggregator.Aggregate(bookDetails, preference);
            IRecommender recommender = new PearsonRecommender();
            List<long> baseData = new List<long>();
            foreach(BookUserRating rating in bookDetails.bookUserRatings)
            {
                if(string.Compare(rating.ISBN,preference.ISBN)==0)
                {
                    baseData.Add(rating.BookRating);
                }
            }
            foreach (var keyValue in keyValuePairs)
            {
                double corValue = recommender.GetCorrelation(baseData, keyValue.Value);
                pairs.Add(keyValue.Key, corValue);
            }
            pairs = pairs.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
            int count = 0;
            foreach (var item in pairs)
            {
                if (count == limit)
                {
                    break;
                }
                count++;
                books.Add(bookDetails.books.Find(b => b.ISBN == item.Key));
            }
            return books;
        }
    }
}
