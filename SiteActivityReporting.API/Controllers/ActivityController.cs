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

        [HttpGet("{key}/total")]
        public string Get(string key)
        {
            return $"get {key}";
        }

        [HttpPost("{key}")]
        public string Post(string key, [FromBody] ActivityDTO activity)
        {
            bool result = _activityRepository.Save(key, activity);
            return $"post {key} {activity.Value}";
        }

    }
}
