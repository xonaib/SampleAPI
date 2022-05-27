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
        public int Value { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}