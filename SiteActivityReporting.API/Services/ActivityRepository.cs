using SiteActivityReporting.API.DAL;
using SiteActivityReporting.Model.DTO;
using SiteActivityReporting.Model.Model;

namespace SiteActivityReporting.API.Services
{
    public class ActivityRepository : IRepository<ActivityDTO>
    {
        private readonly InMemoryActivity _inMemoryActivity;

        public ActivityRepository()
        {
            _inMemoryActivity = new InMemoryActivity();
        }
        public ActivityDTO Get(string key)
        {
            Activity activity = _inMemoryActivity.Get(key);
            ActivityDTO activityDTO = activity.ToDTO();

            return activityDTO;
        }

        public bool Save(string key, ActivityDTO activityDTO)
        {
            Activity activity = activityDTO.ToModel(key);
            bool result = _inMemoryActivity.Save(activity);
            return result;
        }

        public bool PruneData(int dataOlderThanSeconds)
        {
            return _inMemoryActivity.PruneData(dataOlderThanSeconds);
        }
    }
}
