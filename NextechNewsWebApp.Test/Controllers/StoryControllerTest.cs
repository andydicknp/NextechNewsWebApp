using Microsoft.VisualStudio.TestTools.UnitTesting;
using NextechNewsWebApp.Angular.Controllers;
using NextechNewsWebApp.Angular.Service;
using NextechNewsWebApp.Core.Entities;
using NextechNewsWebApp.Core.Interfaces;
using NextechNewsWebApp.DAL.Service;
using NextechNewsWebApp.Test.IntegrationTests.FakeRepository;
using NextechNewsWebApp.Test.IntegrationTests.FakeService;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NextechNewsWebApp.Test.IntegrationTests
{
    [TestClass]
    public class StoryControllerTest
    {
        IDataService fakeDataService;
        IStoryRepository fakeStoryRepository;
        StoryController storyController;
        ICachedData fakeCachedData;

        string fakePageIndex;
        string fakeSize;

        [TestInitialize]
        public void SetUp()
        {
            fakeCachedData = new FakeCachedData();
            fakeDataService = new FakeDataService(); 
            fakeStoryRepository = new FakeStoryRepository(fakeDataService);
            storyController = new StoryController(fakeStoryRepository, fakeCachedData);

            fakePageIndex = "0";
            fakeSize = "10";
        }

        public StoryControllerTest()
        {
            
        }

        [TestMethod]
        public async Task Test_GetStories()
        {
            //Arrange
            var expectedCount = 3;

            //Act
            var stories = await storyController.GetStories(fakePageIndex, fakeSize);
            var resultStoriesCount = stories.Value.Data.Count;

            //Assert
            Assert.AreEqual(resultStoriesCount, expectedCount, "The expected row counts is not equals to the result data.");
        }

        
        [TestMethod]
        public async Task Test_GetFilteredStories()
        {
            //Arrange
            string testfilter = "title 1";
            var expectedFilteredCount = 1;

            //Act
            var filteredStories = await storyController.GetFilteredStories(testfilter);
            var resultFilteredStoriesCount = filteredStories.Value.Data.Count;

            //Assert
            Assert.AreEqual(resultFilteredStoriesCount, expectedFilteredCount, "The expected row counts is not equals to the result data.");
        }
    }
}
