using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIRecommendationEngine.Entities;

namespace AIRecommendationEngine.DataAccess
{
    public interface IDataLoader
    {
        BookDetails Load();
    }
}
