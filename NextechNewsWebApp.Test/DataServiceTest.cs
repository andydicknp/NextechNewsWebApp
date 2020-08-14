using Microsoft.VisualStudio.TestTools.UnitTesting;
using NextechNewsWebApp.Core.Entities;
using NextechNewsWebApp.DAL.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NextechNewsWebApp.Test
{
    [TestClass]
    public class DataServiceTest
    {
        private static string _baseAPIUrl = "https://hacker-news.firebaseio.com";
        
        IDataService _dataService;

        public DataServiceTest(IDataService dataService)
        {
            _dataService = dataService;
        }

        [TestMethod]
        public async Task GetData_ValidEndpoint()
        {
            int validId = 8863;
            string itemEndpoint = $"/v0/item/{validId}.json?print=pretty";

            var item = await _dataService.GetData<Story>(_baseAPIUrl, itemEndpoint);

        }
    }
}
