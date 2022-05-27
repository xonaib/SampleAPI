using Microsoft.AspNetCore.Mvc;
using SiteActivityReporting.API.Services;
using SiteActivityReporting.Model.DTO;
using SiteActivityReporting.Model.Model;

namespace SiteActivityReporting.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActivityController : ControllerBase
    {
        private readonly ILogger<ActivityController> _logger;
        private readonly ActivityRepository _activityRepository;
        public ActivityController(ILogger<ActivityController> logger)
        {
            _logger = logger;
            _activityRepository = new ActivityRepository();
        }

        /*
         * in memory data save
         * scheduler
         * testing
         * logging
         * warnings?
         * 
         * on startup, build, in memory?
         * or on every call, scedule update?
         * lookup in O(1), pre-calculate results by key
         */ 

        [HttpGet("{key}/total")]
        public ActivityDTO Get(string key)
        {
            ActivityDTO activityDTO = _activityRepository.Get(key);

            return activityDTO;
        }

        [HttpPost("{key}")]
        public string Post(string key, [FromBody] ActivityDTO activity)
        {
            bool result = _activityRepository.Save(key, activity);
            return $"post {key} {activity.Value}";
        }

    }
}
