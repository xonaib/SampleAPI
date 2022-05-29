using SiteActivityReporting.API.BackgroundService;
using SiteActivityReporting.Model.DTO;
using SiteActivityReporting.Model.Model;
using System.Diagnostics.CodeAnalysis;

namespace SiteActivityReporting.API.DAL
{
    public class ActivityStore : IStore<Activity> //, IObserver<Activity>
    {
        private Dictionary<string, List<Activity>> _activities;
        private Dictionary<string, int> _runningCounts;

        //private IObservable<Activity> _activityEventsBus;
        //private IDisposable _cancellation;

        // private ActivityCleaner _cleaner;

        public ActivityStore() //ActivityCleaner cleaner
        {
            _activities = new Dictionary<string, List<Activity>>();
            _runningCounts = new Dictionary<string, int>();
            //_cleaner = cleaner;

            //_cancellation  = _cleaner.Subscribe(this);
            //_eventBus = eventBus;
            //_activities = new List<Activity>();
        }

        public Activity Get(string key)
        {
            Activity activity = null;

            int? count = null;

            if (_runningCounts.ContainsKey(key))
                count = _runningCounts.Where(k => k.Key.Equals(key)).Select(s => s.Value).FirstOrDefault();

            if (count != null)
                activity = new Activity(key, count ?? 0);

            return activity;
        }

        public bool Save(Activity activity)
        {
            List<Activity> activities = _activities.Where(k => k.Key.Equals(activity.Key)).Select(s => s.Value).FirstOrDefault();

            if (activities == null)
            {
                activities = new List<Activity>();
                _activities.Add(activity.Key, activities);
            }

            activities.Add(activity);

            Task.Factory.StartNew(() => UpdateCounts(activity));
            
            return true;
        }

        public bool PruneData(int dataOlderThanMinutes)
        {
            int negativeCount = dataOlderThanMinutes * -1;

            _runningCounts = new Dictionary<string, int>();

            foreach (var activity in _activities)
            {
                List<Activity> items = activity.Value;
                items = items.Where(s => s.CreatedOn >= DateTime.Now.AddMinutes(negativeCount)).ToList();

                int total = items.Select(s => s.Value).Sum();
                // update with new items
                _activities[activity.Key] = items;
                _runningCounts.Add(activity.Key, total);
            }

            return true;
        }
        [ExcludeFromCodeCoverage]
        private async Task<bool> UpdateCounts(Activity activity)
        {
            List<Activity> activities = _activities.Where(v => v.Key.Equals(activity.Key)).Select(s => s.Value).FirstOrDefault();
            int total = 0;

            if (activities == null)
                return await Task.FromResult(true);

            total = activities.Select(s => s.Value).Sum();

            if (!_runningCounts.ContainsKey(activity.Key))
                _runningCounts.Add(activity.Key, total);
            else
                _runningCounts[activity.Key] = total;

            return await Task.FromResult(true);
        }
    }
}
