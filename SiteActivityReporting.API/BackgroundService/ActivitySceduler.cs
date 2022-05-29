using SiteActivityReporting.API.Services;
using SiteActivityReporting.Model.DTO;
using SiteActivityReporting.Model.Model;

namespace SiteActivityReporting.API.BackgroundService
{
    public class ActivitySceduler 
    {
        
        private readonly IRepository<ActivityDTO> _activityRepository;
        private bool _isRunning = false;
        private PeriodicTimer _timer;
        public ActivitySceduler(IRepository<ActivityDTO> activityRepository)
        {
            //_observers = new List<IObserver<Activity>>();
            _activityRepository = activityRepository;
        }

        public void ResetTimer(int dataOlderThanMinutes)
        {
            _timer.Dispose();
            _isRunning = false;

            if (dataOlderThanMinutes > 0)
                CleanOlderData(dataOlderThanMinutes);
        }

        public async void CleanOlderData(int dataOlderThanMinutes = 30)
        {
            if (_isRunning == true) return;

            _isRunning = true;
            _timer = new PeriodicTimer(TimeSpan.FromMinutes(dataOlderThanMinutes)); //

            while (await _timer.WaitForNextTickAsync())
            {
                Console.WriteLine("hee haw");
                _activityRepository.PruneData(dataOlderThanMinutes);
            }
        }
    }

    
}
