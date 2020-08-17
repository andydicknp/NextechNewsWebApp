using NextechNewsWebApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NextechNewsWebApp.Angular.Service
{
    public class CachedData : ICachedData
    {
        private Dictionary<int, Story> _cachedData = new Dictionary<int, Story>();

        public Dictionary<int, Story> cachedData
        {
            get { return _cachedData; }

            set { _cachedData = value; }
        }
    }
}
