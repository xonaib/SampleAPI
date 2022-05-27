namespace SiteActivityReporting.API.BackgroundService
{
    public class ActivityCleaner
    {
        public ActivityCleaner()
        {

        }
        public async void CleanOlderData()
        {
            var timer = new PeriodicTimer(TimeSpan.FromSeconds(60));

            while (await timer.WaitForNextTickAsync())
            {

            }
        }
    }
}
