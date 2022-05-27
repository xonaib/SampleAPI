using SiteActivityReporting.Model.Model;

namespace SiteActivityReporting.API.DAL
{
    public class InMemoryActivity
    {
        private Dictionary<string, Activity> _activities;

        public InMemoryActivity()
        {
            _activities = new Dictionary<string, Activity>();
        }

        public IEnumerable<Activity> GetByKey(string key)
        {
            return _activities.Where(k => k.Key.Equals(key)).Select(s => s.Value).ToList();
        }

        public bool Save(Activity activity)
        {
            _activities.Add(activity.Key, activity);
            return true;
        }
    }
}
