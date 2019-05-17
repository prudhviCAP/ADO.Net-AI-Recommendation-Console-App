using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIRecommendationEngine.DataAccess;
using AIRecommendationEngine.Entities;

namespace AIRecommendationEngine.Recommendation
{
    public interface IRecommender
    {
        List<Book> Recommend(Preference preference, int limit);
    }
}
