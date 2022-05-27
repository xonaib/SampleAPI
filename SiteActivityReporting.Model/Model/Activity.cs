using SiteActivityReporting.Model.DTO;

namespace SiteActivityReporting.Model.Model
{
    public class Activity
    {
        public Activity()
        {
            CreatedOn = new DateTime();
        }

        public Activity(string key, int value)
        {
            Key = key;
            Value = value;
            CreatedOn = new DateTime();
        }

        public string Key { get; set; }
        public int Value { get; set; } = 0;
        public DateTime CreatedOn { get; set; }
    }

    public static class ActivityExtension
    {
        public static ActivityDTO ToDTO(this Activity activity)
        {
            ActivityDTO activityDTO = new ActivityDTO(activity.Value.ToString());

            return activityDTO;
        }
    }
}