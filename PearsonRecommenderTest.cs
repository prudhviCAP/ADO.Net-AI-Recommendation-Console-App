using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AIRecomendationEngine.CoreEngine;
using System.Collections.Generic;

namespace AIRecommendationEngine.CoreEngine.Test
{
    [TestClass]
    public class PearsonRecommenderTest
    {
        PearsonRecommender pearsonRecommender = null;

        [TestInitialize]
        public void Init()
        {
            pearsonRecommender = new PearsonRecommender();
        }

        [TestCleanup]
        public void CleanUp()
        {
            pearsonRecommender = null;
        }
        [TestMethod]
        public void GetCorrelation_SmallerOtherList_CorrelationCalculatedByAddingOneToBothLists()
        {
            List<int> myData = new List<int>() { 2, 5, 3, 7, 3, 8, 5 };
            List<int> testData = new List<int>() { 3, 7, 5, 8, 4, 8 };
            Assert.AreEqual(0.526402,Math.Round(pearsonRecommender.GetCorrelation(myData, testData),6));
        }

        [TestMethod]
        public void GetCorrelation_ValidListsOfEqualLength_ActualCorrelationEqualToExpectedCorrelationValue()
        {
            List<int> myData = new List<int>() { 2, 5, 3, 7, 3, 8, 6 };
            List<int> testData = new List<int>() { 3, 7, 5, 8, 4, 8,1 };
            Assert.AreEqual(0.526402, Math.Round(pearsonRecommender.GetCorrelation(myData, testData), 6));
        }

        [TestMethod]
        public void GetCorrelation_BaseDataListsWithZeroValuesInBetween_ListsWithNonZeroInputs()
        {
            List<int> myData = new List<int>() { 2, 5, 3, 7, 3, 8, 5,0 };
            List<int> testData = new List<int>() { 3, 7, 5, 8, 1, 8,1,1 };
            Assert.AreEqual(0.723497, Math.Round(pearsonRecommender.GetCorrelation(myData, testData), 6));
        }

        [TestMethod]
        public void RemoveZeroValues_OtherDataListsWithZeroValuesInBetween_ListsWithNonZeroInputs()
        {
            List<int> myData = new List<int>() { 2, 5, 3, 6, 3, 8, 5 };
            List<int> testData = new List<int>() { 3, 0, 5, 8, 0, 8, 1 };
            Assert.AreEqual(0.433413649, Math.Round(pearsonRecommender.GetCorrelation(myData, testData), 9));
        }

        [TestMethod]
        public void SmallerLengthOtherList_SmallerOtherDataList_EqualLengthLists()
        {
            List<int> myData = new List<int>() { 2, 5, 3, 6, 3, 8, 4 };
            List<int> testData = new List<int>() { 3, 0, 5, 8, 0,8 };
            Assert.AreEqual(0.433413649, Math.Round(pearsonRecommender.GetCorrelation(myData, testData), 9));
        }

        [TestMethod]
        public void GreaterOtherListLength_GreaterOtherArray_EqualLengthLists()
        {
            List<int> myData = new List<int>() { 2, 5, 3, 6, 3, 8, 4 };
            List<int> testData = new List<int>() { 3, 0, 5, 8, 0, 8, 3, 8, 10, 2, 6 };
            Assert.AreEqual(0.498768602, Math.Round(pearsonRecommender.GetCorrelation(myData, testData), 9));
        }
    }
}
