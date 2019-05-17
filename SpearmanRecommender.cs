using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Statistics;

namespace AIRecomendationEngine.CoreEngine
{
    class SpearmanRecommender : IRecommender
    {
        public double GetCorrelation(List<long> baseData, List<long> otherData)
        {
            IEnumerable<double> baseDataEnumerable = (IEnumerable<double>)baseData;
            IEnumerable<double> otherDataEnumerable = (IEnumerable<double>)otherData;
            return Correlation.Spearman(baseDataEnumerable, otherDataEnumerable);
        }
    }
}
