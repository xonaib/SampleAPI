using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiteActivityReporting.Model.Model;
using SiteActivityReporting.API.DAL;

namespace SiteActivityReporting.Test.IntegrationTests.Repository
{
    public class ActivityStoreTest
    {
        private readonly ActivityStore _activityStore;

        public ActivityStoreTest()
        {
            _activityStore = new ActivityStore();
        }

        [Fact]
        public void SaveAndGet_Valid()
        {
            Activity activity = new Activity("key1", 3);

            bool result = _activityStore.Save(activity);
            Assert.True(result);

            Thread.Sleep(1000);

            Activity resultActivity = _activityStore.Get(activity.Key);

            Assert.NotNull(resultActivity);
            Assert.Equal(activity.Key, resultActivity.Key);
            Assert.Equal(activity.Value, resultActivity.Value);
        }

        [Fact]
        public void SaveAndGet_Valid_RunningSum()
        {
            string key = "key1";
            int count1 = 3;
            int count2 = 2;
            int total = count1 + count2;

            var activity1 = new Activity(key, count1);
            var activity2 = new Activity(key, count2);

            bool result = _activityStore.Save(activity1);
            Assert.True(result);

            result = _activityStore.Save(activity2);
            Assert.True(result);

            Thread.Sleep(1000);

            Activity resultActivity = _activityStore.Get(key);
            Assert.NotNull(resultActivity);
            Assert.Equal(key, resultActivity.Key);
            Assert.Equal(total, resultActivity.Value);
        }

        [Fact]
        public void SaveAndGet_Valid_NonInterferingSum()
        {
            string key = "key1";
            int count1 = 3;
            int count2 = 2;
            int total = count1 + count2;

            var activity1 = new Activity(key, count1);
            var activity2 = new Activity(key, count2);

            _activityStore.Save(activity1);
            _activityStore.Save(activity2);

            string key3 = "key3";
            int count3 = 4;
            var activity3 = new Activity(key3, count3);
            _activityStore.Save(activity3);

            Thread.Sleep(1000);

            Activity resultActivity = _activityStore.Get(key3);

            Assert.NotNull(resultActivity);
            Assert.Equal(key3, resultActivity.Key);
            Assert.Equal(count3, resultActivity.Value);
        }

        [Fact]
        public void SaveAndGet_RunningSumMinusOlderData()
        {

        }
    }
}
