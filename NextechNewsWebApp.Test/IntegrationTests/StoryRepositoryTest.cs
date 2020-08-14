using Microsoft.VisualStudio.TestTools.UnitTesting;
using NextechNewsWebApp.Core.Entities;
using NextechNewsWebApp.Core.Interfaces;
using NextechNewsWebApp.DAL.Service;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NextechNewsWebApp.Test.IntegrationTests
{
    [TestClass]
    public class StoryRepositoryTest
    {
        IDataService _dataService;

        string _baseAPIUrl;

        public int VALID_ID_8863;
        public int INVALID_ID;
        public Story STORY_8863;

        public Story STORY_DUMMY;

        [TestInitialize]
        public void SetUp()
        {
            _baseAPIUrl = "https://hacker-news.firebaseio.com";

            System.Random random = new System.Random();

            VALID_ID_8863 = 8863;
            INVALID_ID = -1;

            STORY_8863 = new Story();
            STORY_8863.by = "dhouston";
            STORY_8863.descendants = 71;
            STORY_8863.id = 8863;
            STORY_8863.kids = new int[] { 9224, 8917, 8952, 8958, 8884, 8887, 8869, 8940, 8908, 9005, 8873, 9671, 9067, 9055, 8865, 8881, 8872, 8955, 10403, 8903, 8928, 9125, 8998, 8901, 8902, 8907, 8894, 8870, 8878, 8980, 8934, 8943, 8876 };
            STORY_8863.score = 104;
            STORY_8863.time = 1175714200;
            STORY_8863.title = "My YC app: Dropbox - Throw away your USB drive";
            STORY_8863.type = "story";
            STORY_8863.url = "http://www.getdropbox.com/u/2/screencast.html";

            STORY_DUMMY = new Story();
            STORY_DUMMY.by = "system";
            STORY_DUMMY.descendants = random.Next(0,99);
            STORY_DUMMY.id = -1;


        }

        public StoryRepositoryTest(IDataService dataService)
        {
            _dataService = dataService;
        }

        [TestMethod]
        public async Task GetStoryById()
        {
            string itemEndpoint = $"/v0/item/{VALID_ID_8863}.json?print=pretty";

            var result = await _dataService.GetData<Story>(_baseAPIUrl, itemEndpoint);

            Assert.AreEqual(STORY_8863, result, "The expected result is correct.");
            Assert.AreNotEqual(STORY_DUMMY, result, "The expected result is not same to the response correct.");
        }

        /*[TestMethod]
        public async Task GetNewStories()
        {
            var result = await _dataService.GetNewStoriesAsync();

            Assert.IsNotNull(result, "The result is not empty.");
        }*/
    }
}
