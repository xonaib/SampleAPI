using SiteActivityReporting.API.DAL;
using SiteActivityReporting.Model.DTO;
using SiteActivityReporting.Model.Model;

namespace SiteActivityReporting.API.Services
{
    public class ActivityRepository : IRepository<ActivityDTO>
    {
        private readonly IStore<Activity> _store;

        public ActivityRepository(IStore<Activity> store)
        {
            _store = store;
        }
        public ActivityDTO Get(string key)
        {
            Activity activity = _store.Get(key);
            ActivityDTO activityDTO = activity.ToDTO();

            return activityDTO;
        }

        public bool Save(string key, ActivityDTO activityDTO)
        {
            Activity activity = activityDTO.ToModel(key);
            bool result = _store.Save(activity);
            return result;
        }

        public bool PruneData(int dataOlderThanSeconds)
        {
            return _store.PruneData(dataOlderThanSeconds);
        }
    }
}
