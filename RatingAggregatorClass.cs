using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIRecommendationEngine.Entities;

namespace AIRecommendationEngine.RatingAggregator
{
    public class RatingAggregatorClass : IRatingAggregator
    {
        public Dictionary<string, List<long>> Aggregate(BookDetails bookDetails, Preference preference)
        {
            Dictionary<string, List<long>> result = new Dictionary<string, List<long>>();
            int upperBound, lowerBound;
            GetAgeBounds(preference.Age, out upperBound, out lowerBound);
            foreach (var rating in bookDetails.bookUserRatings)
            {
                if ((bookDetails.users.Find(u => u.UserId == rating.UserId && u.State.Equals(preference.State) && u.Age <= upperBound && u.Age >= lowerBound) != null) /*&& (rating.ISBN == preference.ISBN)*/)
                {
                    if (result.ContainsKey(rating.ISBN))
                        result[rating.ISBN].Add(rating.BookRating);
                    else
                    {
                        List<long> list = new List<long>();
                        list.Add(rating.BookRating);
                        result.Add(rating.ISBN, list);
                    }
                }
            }
            return result;
        }

        void GetAgeBounds(int age, out int upperBound, out int lowerBound)
        {
            upperBound = 0;
            lowerBound = 0;
            if (age >= 0 && age <= 16)
            {
                lowerBound = 0;
                upperBound = 16;
            }
            else if (age >= 17 && age <= 30)
            {
                lowerBound = 17;
                upperBound = 30;
            }
            else if (age >= 31 && age <= 50)
            {
                lowerBound = 31;
                upperBound = 50;
            }
            else if (age >= 51 && age <= 60)
            {
                lowerBound = 51;
                upperBound = 60;
            }
            else if (age >= 61 && age <= 100)
            {
                lowerBound = 61;
                upperBound = 100;
            }
        }
        //public Dictionary<string, int> Aggregate(BookDetails bookDetails, Preference preference)
        //{
        //    Dictionary<string, int> BookRatings = new Dictionary<string, int>();
        //    for(int i=0;i<bookDetails.books.Count;i++)
        //    {
        //        Book book = bookDetails.books[i];
        //        if (book.bookUserRatings[i].ISBN == book.ISBN && (GetRange(preference.Age) == GetRange(book.bookUserRatings[i].user.Age)))
        //            BookRatings.Add(book.ISBN, book.bookUserRatings[i].BookRating);
        //    }
        //    return BookRatings;

        //}
        //private int GetRange(int age)
        //{
        //    if (age > 0 && age <= 16)
        //    {
        //        return 1;
        //    }
        //    else if (age > 16 && age <= 30)
        //    {
        //        return 2;
        //    }
        //    else if (age > 30 && age <= 50)
        //    {
        //        return 3;
        //    }
        //    else if (age > 50 && age <= 60)
        //    {
        //        return 4;
        //    }
        //    else
        //        return 5;
        //}
        //}
    }
}
