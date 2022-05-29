using SiteActivityReporting.Model.DTO;
using System.Diagnostics.CodeAnalysis;

namespace SiteActivityReporting.Model.Model
{
    [ExcludeFromCodeCoverage]
    public class Activity
    {
        public Activity()
        {
            CreatedOn = DateTime.Now;
        }

        public Activity(string key, int value)
        {
            Key = key;
            Value = value;
            CreatedOn = DateTime.Now;
        }

        
        public string Key { get; set; }
        public int Value { get; set; } = 0;
        public DateTime CreatedOn { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public static class ActivityExtension
    {
        public static ActivityDTO ToDTO(this Activity activity)
        {
            ActivityDTO activityDTO = new ActivityDTO(activity.Value.ToString());

            return activityDTO;
        }
    }
}