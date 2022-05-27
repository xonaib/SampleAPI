using SiteActivityReporting.Model.DTO;
using SiteActivityReporting.Model.Model;

namespace SiteActivityReporting.API.DAL
{
    public class InMemoryActivity : IStore<Activity>
    {
        private Dictionary<string, List<Activity>> _activities;
        private Dictionary<string, int> _runningCounts;
        //private List<Activity> _activities;
        public InMemoryActivity()
        {
            _activities = new Dictionary<string, List<Activity>>();
            _runningCounts = new Dictionary<string, int>();
            //_activities = new List<Activity>();
        }

        public Activity Get(string key)
        {
            Activity activity = new Activity();
            int? count = _runningCounts.Where(k => k.Key.Equals(key)).Select(s => s.Value).FirstOrDefault();
            
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
            }

            activities.Add(activity);
            _activities.Add(activity.Key, activities);
            return true;
        }

        public bool PruneData(int dataOlderThanSeconds)
        {
            int negativeCount = dataOlderThanSeconds;
            
            _runningCounts = new Dictionary<string, int>();

            foreach(var activity in _activities)
            {
                List<Activity> items = activity.Value;
                items = items.Where(s => s.CreatedOn >= DateTime.Now.AddSeconds(negativeCount)).ToList();

                int total = items.Select(s => s.Value).Sum();
                _runningCounts.Add(activity.Key, total);
            }

            return true;
        }
    }
}
