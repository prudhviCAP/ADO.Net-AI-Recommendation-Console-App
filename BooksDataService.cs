using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIRecommendationEngine.Entities;
using AIRecommendationEngine.Cacher;
using AIRecommendationEngine.DataAccess;

namespace AIRecommendation.BooksDataService
{
    public class BooksDataService
    {
        
        public BookDetails GetBookDetails()
        {
            IDataCacher dataCacher = new MemDataCacher();
            if (dataCacher.GetData() == null)
            {
                IDataLoader dataLoader = new CSVDataLoader();
                BookDetails bookDetails = new BookDetails();
                bookDetails = dataLoader.Load();
                dataCacher.SetData(bookDetails);
                return bookDetails;
            }
            else
                return dataCacher.GetData();
        }
    }
}
