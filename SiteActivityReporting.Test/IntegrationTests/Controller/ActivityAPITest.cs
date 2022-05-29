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
                var activityCleaner = scope.ServiceProvider.GetService<ActivitySceduler>();

                ActivityController controller = new ActivityController(logger, repository, activityCleaner);

                string key = "test1";
                ActivityDTO activityDTO = new ActivityDTO("3");

                var postResult = controller.Post(key, activityDTO);
                Assert.IsType<OkResult>(postResult);

                Thread.Sleep(1000);

                var getResult = controller.Get(key);
                Assert.IsType<OkObjectResult>(getResult);

                var objectResult = (OkObjectResult)getResult;
                var result = (ActivityDTO)objectResult.Value;

                Assert.Equal(result.Value, activityDTO.Value);
            }
        }

        [Fact]
        public void SaveAndGet_Pruned()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var logger = scope.ServiceProvider.GetService<ILogger<ActivityController>>();
                var repository = scope.ServiceProvider.GetService<IRepository<ActivityDTO>>();
                var activitySceduler = scope.ServiceProvider.GetService<ActivitySceduler>();

                ActivityController controller = new ActivityController(logger, repository, activitySceduler);

                string key = "test";
                int count1 = 3,
                    count2 = 4,
                    count3 = 5;

                int total = count2 + count3;
                       
                var activity1 = new ActivityDTO(count1.ToString());
                

                controller.Post(key, activity1);

                activitySceduler.ResetTimer(1);
                activitySceduler.CleanOlderData(1);

                Thread.Sleep(TimeSpan.FromSeconds(80));

                //return;
                var activity2 = new ActivityDTO(count2.ToString());
                var activity3 = new ActivityDTO(count3.ToString());
                controller.Post(key, activity2);
                controller.Post(key, activity3);

                Thread.Sleep(TimeSpan.FromSeconds(1));
                activitySceduler.ResetTimer(0);
                var getResult = controller.Get(key);

                var objectResult = (OkObjectResult)getResult;
                var result = (ActivityDTO)objectResult.Value;

                Assert.Equal(total.ToString(), result.Value);
            }
        }
    }
}
