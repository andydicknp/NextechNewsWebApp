using NextechNewsWebApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NextechNewsWebApp.Core.Interfaces
{
    public interface IAsyncRepository<T> where T : Entity
    {
        Task<T> GetByIdAsync(int id);

        Task<List<int>> GetNewStoriesAsync();
    }
}
