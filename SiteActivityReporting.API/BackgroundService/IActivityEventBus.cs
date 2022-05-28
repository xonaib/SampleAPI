using SiteActivityReporting.Model.Model;

namespace SiteActivityReporting.API.BackgroundService
{
    public interface IActivityEventBus
    {
        public void NewActivity(Activity activity);
    }
}
