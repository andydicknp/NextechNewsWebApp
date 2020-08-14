using System;
using System.Collections.Generic;
using System.Linq;
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
      
        // GET: api/<StoryController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Story>>> GetStories()
        {
            List<Story> stories = new List<Story>();

            List<int> allStories = await _storyRepository.GetNewStoriesAsync();
            
            foreach (var storyId in allStories.GetRange(0, 10))
            {
                var story = await _storyRepository.GetByIdAsync(storyId);

                if (story != null)
                {
                    stories.Add(story);
                }
            }

            return stories;
        }
    }
}
