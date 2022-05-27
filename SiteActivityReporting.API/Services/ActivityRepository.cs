using SiteActivityReporting.API.DAL;
using SiteActivityReporting.Model.DTO;
using SiteActivityReporting.Model.Model;

namespace SiteActivityReporting.API.Services
{
    public class ActivityRepository
    {
        private readonly InMemoryActivity _inMemoryActivity;

        public ActivityRepository()
        {
            _inMemoryActivity = new InMemoryActivity();
        }
        public IEnumerable<Activity> GetByKey(string key)
        {
            throw new NotImplementedException();
        }

        public bool Save(string key, ActivityDTO activityDTO)
        {
            Activity activity = activityDTO.ToActivity(key);
            bool result = _inMemoryActivity.Save(activity);
            return result;

        }
    }
}
