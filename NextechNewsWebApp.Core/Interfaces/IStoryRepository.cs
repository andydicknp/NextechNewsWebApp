using NextechNewsWebApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextechNewsWebApp.Core.Interfaces
{
    public interface IStoryRepository : IAsyncRepository<Story>
    {
    }
}
