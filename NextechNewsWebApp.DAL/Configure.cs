using Microsoft.Extensions.DependencyInjection;
using NextechNewsWebApp.Core.Interfaces;
using NextechNewsWebApp.DAL.Repository;
using NextechNewsWebApp.DAL.Service;
using System;

namespace NextechNewsWebApp.DAL
{
    public static class Configure
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IDataService, DataService>();
            services.AddTransient<IStoryRepository, StoryRepository>();
        }
    }
}
