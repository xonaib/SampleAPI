using SiteActivityReporting.API.Services;
using SiteActivityReporting.Model.DTO;
using SiteActivityReporting.Model.Model;

namespace SiteActivityReporting.API.BackgroundService
{
    public class ActivityCleaner : IObservable<Activity>, IActivityEventBus
    {
        private List<IObserver<Activity>> _observers;
        private readonly IRepository<ActivityDTO> _activityRepository;
        private bool _isRunning = false;
        public ActivityCleaner(IRepository<ActivityDTO> activityRepository)
        {
            //_observers = new List<IObserver<Activity>>();
            _activityRepository = activityRepository;
        }
        public async void CleanOlderData()
        {
            if (_isRunning == true) return;

            _isRunning = true;
            var timer = new PeriodicTimer(TimeSpan.FromSeconds(60));

            while (await timer.WaitForNextTickAsync())
            {
                Console.WriteLine("hee haw");
                _activityRepository.PruneData(60);
            }
        }

        public IDisposable Subscribe(IObserver<Activity> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
                
            }

            return new Unsubscriber<Activity>(_observers, observer);
        }

        public void NewActivity(Activity activity)
        {
            foreach (var observer in _observers)
                observer.OnNext(activity);
        }
    }

    internal class Unsubscriber<Activity> : IDisposable
    {
        private List<IObserver<Activity>> _observers;
        private IObserver<Activity> _observer;

        internal Unsubscriber(List<IObserver<Activity>> observers, IObserver<Activity> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}
