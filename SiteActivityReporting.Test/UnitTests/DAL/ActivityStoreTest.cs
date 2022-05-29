using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiteActivityReporting.Model.DTO;
using SiteActivityReporting.Model.Model;
using SiteActivityReporting.API.DAL;

namespace SiteActivityReporting.Test.UnitTests.DAL
{
    public class ActivityStoreTest : IClassFixture<DependencySetupFixture>
    {
        private ServiceProvider _serviceProvider;
        public ActivityStoreTest(DependencySetupFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }

        [Fact]
        public void Save_Valid()
        {
            Activity activity = new Activity("test", 3);

            using (var scope = _serviceProvider.CreateScope())
            {
                var store = scope.ServiceProvider.GetService<IStore<Activity>>();

                bool result = store.Save(activity);

                Assert.True(result);
            }
        }

        [Fact]
        public void Get_InValid()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var store = scope.ServiceProvider.GetService<IStore<Activity>>();

                var result = store.Get("");

                Assert.Null(result);
            }
        }
    }
}
