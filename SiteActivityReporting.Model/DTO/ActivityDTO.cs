using SiteActivityReporting.API.Util;
using SiteActivityReporting.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteActivityReporting.Model.DTO
{
    public class ActivityDTO
    {
        public ActivityDTO() { }

        public ActivityDTO(string value)
        {
            Value = value;
        }
        public string Value { get; set; } = "";

    }

    public static class ActivityDTOExtension
    {
        public static Activity ToModel(this ActivityDTO activityDTO, string key)
        {
            int value = Converter.StringToNearestInt(activityDTO.Value);

            Activity activity = new Activity(key, value);

            return activity;
        }
    }
}
