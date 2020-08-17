using NextechNewsWebApp.Angular.Service;
using NextechNewsWebApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextechNewsWebApp.Test.IntegrationTests.FakeService
{
    public class FakeCachedData : ICachedData
    {
        private Dictionary<int, Story> _cachedData;

        public FakeCachedData()
        {
            _cachedData = new Dictionary<int, Story>(
                new List<KeyValuePair<int, Story>>()
                {
                    new KeyValuePair<int, Story>( 1, new Story(1, "Title 1", "www.url1.com/story1")),
                    new KeyValuePair<int, Story>( 2, new Story(1, "Title 2", "www.url1.com/story2")),
                    new KeyValuePair<int, Story>( 3, new Story(1, "Title 3", "www.url1.com/story3"))
                }
            );
        }

        public Dictionary<int, Story> cachedData
        {
            get { return _cachedData; }

            set { _cachedData = value; }
        }
    }
}
