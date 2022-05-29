using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using SiteActivityReporting.API.BackgroundService;
using SiteActivityReporting.API.Controllers;
using SiteActivityReporting.API.DAL;
using SiteActivityReporting.API.Services;
using SiteActivityReporting.Model.DTO;
using SiteActivityReporting.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteActivityReporting.Test.UnitTests.Controller
{
    
    
    public class ActivityAPITest : IClassFixture<DependencySetupFixture>
    {
        private readonly Mock<ILogger<ActivityController>> _mockLogger;
        private readonly ILogger<ActivityController> _logger;

        private readonly Mock<IRepository<ActivityDTO>> _mockRepository;
        private readonly IRepository<ActivityDTO> _repository;

        private readonly Mock<ActivitySceduler> _mockActivityCleaner;
        private readonly ActivitySceduler _activityCleaner;

        private readonly Mock<IStore<Activity>> _mockStore;
        private readonly IStore<Activity> _store;

        private readonly ActivityController _controller;

        private ServiceProvider _serviceProvider;
        public ActivityAPITest(DependencySetupFixture fixture)
        {
            _mockLogger = new Mock<ILogger<ActivityController>>();

            _mockStore = new Mock<IStore<Activity>>();
            _store = _mockStore.Object;
            
            _mockRepository = new Mock<IRepository<ActivityDTO>>();
            _repository = _mockRepository.Object;

            // since this is a concrete object
            _mockActivityCleaner = new Mock<ActivitySceduler>(_repository);
            _activityCleaner = _mockActivityCleaner.Object;

            _logger = _mockLogger.Object;
            _controller = new ActivityController(_logger, _repository, _activityCleaner);

            _serviceProvider = fixture.ServiceProvider;
        }

        [Fact]
        public void Get_InvalidKey()
        {
            var badResult = _controller.Get("");

            Assert.IsType<BadRequestResult>(badResult);
        }

        [Fact]
        public void Get_ValidResult()
        {
            //
            string key = "test";
            string value = "3";
            ActivityDTO activity = new ActivityDTO(value);

            _mockRepository.Setup(s => s.Get(key)).Returns(activity);

            //
            var okResult = _controller.Get(key);
            var objectResult = (OkObjectResult)okResult;
            
            //
            Assert.IsType<OkObjectResult>(okResult);
            Assert.Same(objectResult.Value, activity);
        }

        [Fact]
        public void Post_InvalidKey()
        {
            string key = "";
            ActivityDTO activityDTO = new ActivityDTO("3");

            var badResult = _controller.Post(key, activityDTO);
            Assert.IsType<BadRequestResult>(badResult);
        }
        [Fact]
        public void Post_ValidResult()
        {
            string key = "test";
            ActivityDTO activityDTO = new ActivityDTO("3");

            _mockRepository.Setup(s => s.Save(key, activityDTO)).Returns(true);

            var okResult = _controller.Post(key, activityDTO);
            Assert.IsType<OkResult>(okResult);
        }

        //[Fact]
        public void Get_InvalidKey3()
        {
            

            using (var scope = _serviceProvider.CreateScope())
            {
                var logger = scope.ServiceProvider.GetService<ILogger<ActivityController>>();
                var repository = scope.ServiceProvider.GetService<IRepository<ActivityDTO>>();
                var activityCleaner = scope.ServiceProvider.GetService<ActivitySceduler>();

                ActivityController controller = new ActivityController(logger, repository, activityCleaner);

                var badResult = controller.Get("");

                Assert.IsType<BadRequestResult>(badResult);
            }

        }
    }
}
