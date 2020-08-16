using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NextechNewsWebApp.Core.Entities;
using NextechNewsWebApp.Core.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NextechNewsWebApp.Angular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoryController : ControllerBase
    {
        private readonly ILogger<StoryController> _logger;
        private readonly IStoryRepository _storyRepository;

        public StoryController(ILogger<StoryController> logger, IStoryRepository storyRepository)
        {
            _logger = logger;
            _storyRepository = storyRepository;
        }

        [HttpGet]
        public async Task<ActionResult<StoryTableResult>> GetStories(string pageIndex, string pageSize, string filter)
        {
            int _pageIndex = !String.IsNullOrEmpty(pageIndex) ? int.Parse(pageIndex) : 0;
            int _pageSize = !String.IsNullOrEmpty(pageSize) ? int.Parse(pageSize) : 0;

            List <Story> stories = new List<Story>();
            List<int> allStoriesIds = await _storyRepository.GetNewStoriesAsync();

            var allStories = new ConcurrentBag<Story>();
            var semaphore = new SemaphoreSlim(20);
            var taskRequests = allStoriesIds.GetRange( ( _pageIndex * _pageSize ), _pageSize).Select(async item =>
            {
                try
                {
                    await semaphore.WaitAsync();
                    var story = await _storyRepository.GetByIdAsync(item);
                    if( story != null && (
                        ( !String.IsNullOrEmpty(story.url) &&  
                          !String.IsNullOrEmpty(filter) && 
                          story.title.ToLower().Contains(filter) )  || 
                        (!String.IsNullOrEmpty(story.url) && String.IsNullOrEmpty(filter)) )  
                      )
                    {
                        allStories.Add(story);
                    }                    
                }
                finally
                {
                    semaphore.Release();
                }
            });
            await Task.WhenAll(taskRequests);

            StoryTableResult result = new StoryTableResult(allStories.ToList<Story>(),
                                              _pageIndex,
                                              _pageSize,
                                              !String.IsNullOrEmpty(filter) ? allStories.Count : allStoriesIds.Count);
            return result;
        }
    }
}
