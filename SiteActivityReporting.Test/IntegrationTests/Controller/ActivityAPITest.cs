using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SiteActivityReporting.API.BackgroundService;
using SiteActivityReporting.API.Controllers;
using SiteActivityReporting.API.DAL;
using SiteActivityReporting.API.Services;
using SiteActivityReporting.Model.DTO;
using SiteActivityReporting.Model.Model;

namespace SiteActivityReporting.Test.IntegrationTests.Controller
{
    public class ActivityAPITest : IClassFixture<DependencySetupFixture>
    {
        private ServiceProvider _serviceProvider;

        public ActivityAPITest(DependencySetupFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }

        [Fact]
        public void SaveAndGet_Valid()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var logger = scope.ServiceProvider.GetService<ILogger<ActivityController>>();
                var repository = scope.ServiceProvider.GetService<IRepository<ActivityDTO>>();
                var activityCleaner = scope.ServiceProvider.GetService<ActivityCleaner>();

                ActivityController controller = new ActivityController(logger, repository, activityCleaner);

                string key = "test1";
                ActivityDTO activityDTO = new ActivityDTO("3");

                var postResult = controller.Post(key, activityDTO);
                Assert.IsType<OkResult>(postResult);

                Thread.Sleep(1000);

                var getResult = controller.Get(key);
                Assert.IsType<OkObjectResult>(getResult);

                var objectResult = (OkObjectResult)getResult;
                Assert.Same(objectResult.Value, activityDTO);
            }
        }
    }
}
