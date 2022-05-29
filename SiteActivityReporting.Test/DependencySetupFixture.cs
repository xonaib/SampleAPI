using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiteActivityReporting.API.BackgroundService;
using SiteActivityReporting.API.Controllers;
using SiteActivityReporting.API.DAL;
using SiteActivityReporting.API.Services;
using SiteActivityReporting.Model.DTO;
using SiteActivityReporting.Model.Model;

namespace SiteActivityReporting.Test
{
    public class DependencySetupFixture
    {
        public ServiceProvider ServiceProvider { get; private set; }
        public DependencySetupFixture()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IRepository<ActivityDTO>, ActivityRepository>();
            serviceCollection.AddSingleton<IStore<Activity>, ActivityStore>();
            serviceCollection.AddSingleton<ActivitySceduler, ActivitySceduler>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
