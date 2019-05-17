using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIRecomendationEngine.CoreEngine
{
    public class PearsonRecommender : IRecommender
    {
        public double GetCorrelation(List<long> baseData, List<long> otherData)
        {    
            if(AreEqualLength(baseData,otherData))
            {
                RemoveZeroValues(baseData, otherData);
            }
            else
            {
                if(baseData.Count > otherData.Count)
                {
                    SmallerLengthOtherList(baseData, otherData);
                    RemoveZeroValues(baseData, otherData);
                }
                else if (baseData.Count < otherData.Count)
                {
                    GreaterOtherListLength(baseData, otherData);
                    RemoveZeroValues(baseData, otherData);
                }
            }
            long productSum = 0;
            long squareSumOfX = 0;
            long squareSumOfY = 0;
            for(int i=0;i<baseData.Count;i++)
            {
                productSum += baseData[i] * otherData[i];
                squareSumOfX += baseData[i] * baseData[i];
                squareSumOfY += otherData[i] * otherData[i];
            }
            double relation = ((baseData.Count * productSum) - (baseData.Sum() * otherData.Sum())) / (Math.Sqrt((((baseData.Count) * squareSumOfX) - (baseData.Sum() * baseData.Sum())) * (((otherData.Count) * squareSumOfY) - (otherData.Sum() * otherData.Sum()))));
            return relation;
        }
        private bool AreEqualLength(List<long> baseData,List<long> otherData)
        {
            if (baseData.Count != otherData.Count)
                return false;
            return true;
        }
        private void RemoveZeroValues(List<long> baseData, List<long> otherData)
        {
            for(int i=0;i<baseData.Count;i++)
            {
                if(baseData[i] == 0 || otherData[i] == 0)
                {
                    baseData[i] += 1;
                    otherData[i] += 1;
                }
            }
        }
        private void SmallerLengthOtherList(List<long> baseData, List<long> otherData)
        {
            for(int i = otherData.Count;i<baseData.Count;i++)
            {
                baseData[i] += 1;
                otherData.Add(1);
            }
        }
        private void GreaterOtherListLength(List<long> baseData, List<long> otherData)
        {
            if(otherData.Count > baseData.Count)
            {
                otherData.RemoveRange(baseData.Count, (otherData.Count - baseData.Count));
            }
        }
    }
}
