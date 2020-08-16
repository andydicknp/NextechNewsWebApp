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

        private static Dictionary<int, Story> cachedIds = new Dictionary<int, Story>();

        public StoryController(ILogger<StoryController> logger, IStoryRepository storyRepository)
        {
            _logger = logger;
            _storyRepository = storyRepository;
        }

        [HttpGet]
        public async Task<ActionResult<StoryTableResult>> GetStories(string pageIndex, string pageSize)
        {
            int _pageIndex = !String.IsNullOrEmpty(pageIndex) ? int.Parse(pageIndex) : 0;
            int _pageSize = !String.IsNullOrEmpty(pageSize) ? int.Parse(pageSize) : 0;

            List<Story> stories = cachedIds.Select(i => i.Value).ToList<Story>();

            int fromIndex = (_pageIndex * _pageSize);
            int count = _pageSize > stories.Count ? stories.Count : _pageSize;

            StoryTableResult result = new StoryTableResult(stories.GetRange(fromIndex, count),
                                              _pageIndex,
                                              _pageSize,
                                              cachedIds.Count);
            return result;
        }

        [HttpGet]
        [Route("GetFilteredStories")]
        public async Task<ActionResult<StoryTableResult>> GetFilteredStories([FromQuery] string filter)
        {
            int _pageIndex = 0;
            int _pageSize = 10;
            
            List<Story> stories = new List<Story>();

            List<int> storiesIds = !String.IsNullOrEmpty(filter) ? 
                                    new List<int>(cachedIds.Keys) : 
                                    await _storyRepository.GetNewStoriesAsync();


            
            if (!String.IsNullOrEmpty(filter))
            {
                stories = cachedIds.Where( x => !String.IsNullOrEmpty(x.Value.url) && 
                                                x.Value.title.ToLower().Contains(filter) ).Select(i => i.Value)
                                    .ToList<Story>();
            }
            else
            {
                var semaphore = new SemaphoreSlim(20);
                var searchedStories = new ConcurrentDictionary<int, Story>();
                var taskRequests = storiesIds.Select(async item =>
                {
                    try
                    {
                        await semaphore.WaitAsync();
                        var story = await _storyRepository.GetByIdAsync(item);
                        if (story != null)
                        {
                            if (!String.IsNullOrEmpty(filter))
                            {
                                if (!String.IsNullOrEmpty(story.url) &&
                                      story.title.ToLower().Contains(filter))
                                {
                                    searchedStories.TryAdd(story.id, story);
                                }
                            }
                            else
                            {
                                searchedStories.TryAdd(story.id, story);
                            }
                        }
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                });

                await Task.WhenAll(taskRequests);

                stories = searchedStories.OrderByDescending(i => i.Key).Select(i => i.Value).ToList<Story>();
                
                cachedIds = searchedStories.OrderByDescending(i => i.Key).ToDictionary(x => x.Key, x => x.Value);
            }

            int fromIndex = (_pageIndex * _pageSize);
            int count = _pageSize > stories.Count ? stories.Count : _pageSize;

            StoryTableResult result = new StoryTableResult(stories.GetRange(fromIndex, count),
                                              _pageIndex,
                                              _pageSize,
                                              !String.IsNullOrEmpty(filter) ? stories.Count : cachedIds.Count);
            return result;
        }
    }
}
