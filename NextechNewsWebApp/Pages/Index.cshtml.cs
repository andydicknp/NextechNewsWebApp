using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using NextechNewsWebApp.Core.Entities;
using NextechNewsWebApp.Core.Interfaces;

namespace NextechNewsWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IStoryRepository _storyRepository;

        public IndexModel(ILogger<IndexModel> logger, IStoryRepository storyRepository)
        {
            _logger = logger;
            _storyRepository = storyRepository;
        }

        public async Task<PartialViewResult> OnGetStoryPartial()
        {
            List<Story> stories = new List<Story>();

            List<int> allStories = await _storyRepository.GetNewStoriesAsync();
            foreach(var storyId in allStories.GetRange(0,10))
            {
                var story = await _storyRepository.GetByIdAsync(storyId);
                
                if(story != null)
                {
                    stories.Add(story);
                }
            }

            return new PartialViewResult
            {
                ViewName = "_StoryPartial",
                ViewData = new ViewDataDictionary<List<Story>>(ViewData, stories)
            };
        }
    }
}
