using NextechNewsWebApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NextechNewsWebApp.Angular.Service
{
    public interface ICachedData
    {
        Dictionary<int, Story> cachedData { get; set; }
    }
}
