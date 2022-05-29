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

namespace SiteActivityReporting.Test.UnitTests.Repository
{
    public class ActivityRepositoryTest : IClassFixture<DependencySetupFixture>
    {
        private readonly Mock<IStore<Activity>> _mockStore;
        private readonly IStore<Activity> _store;

        //private readonly Mock<IRepository<ActivityDTO>> _mockRepository;
        private readonly IRepository<ActivityDTO> _repository;
        public ActivityRepositoryTest()
        {
            _mockStore = new Mock<IStore<Activity>>();
            _store = _mockStore.Object;

            //_mockRepository = new Mock<IRepository<ActivityDTO>>();
            //_repository = _mockRepository.Object;

            _repository = new ActivityRepository(_store);
        }

        [Fact]
        public void GetActivity_Valid()
        {
            string key = "test";
            int value = 3;
            ActivityDTO activityDTO = new ActivityDTO(value.ToString());
            Activity activity = new Activity(key, value);
            _mockStore.Setup(s => s.Get(key)).Returns(activity);

            var resultDTO = _repository.Get(key);
            //var result = activity.ToDTO();

            Assert.Equal(resultDTO.Value, activityDTO.Value);
        }

        //[Fact]
        public void SaveActivity_Valid()
        {
            string key = "test";
            int value = 3;
            ActivityDTO activityDTO = new ActivityDTO(value.ToString());
            Activity activity = activityDTO.ToModel(key);

            _mockStore.Setup(s => s.Save(activity)).Returns(true);

            var result = _repository.Save(key, activityDTO);

            Assert.True(result);
        }


    }
}
