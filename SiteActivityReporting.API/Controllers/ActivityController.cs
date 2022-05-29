using Microsoft.AspNetCore.Mvc;
using SiteActivityReporting.API.BackgroundService;
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
        private readonly IRepository<ActivityDTO> _activityRepository;
        private ActivityCleaner _activityCleaner;
        public ActivityController(ILogger<ActivityController> logger, IRepository<ActivityDTO> activityRepository, ActivityCleaner activityCleaner)
        {
            _logger = logger;
            _activityRepository = activityRepository;

            _activityCleaner = activityCleaner;
            Task.Factory.StartNew(() => _activityCleaner.CleanOlderData());
        }

        /*
         * in memory data save
         * scheduler
         * testing
         * logging
         * warnings?
         * event bus?
         * 
         * on startup, build, in memory?
         * or on every call, scedule update?
         * lookup in O(1), pre-calculate results by key
         * ConcurrentDictionary!!!
         */

        [HttpGet("{key}/total")]
        public IActionResult Get(string key)
        {
            if (string.IsNullOrEmpty(key))
                return BadRequest();

            ActivityDTO activityDTO = _activityRepository.Get(key);

            return Ok(activityDTO);
        }

        [HttpPost("{key}")]
        public IActionResult Post(string key, [FromBody] ActivityDTO activity)
        {
            if (string.IsNullOrEmpty(key))
                return BadRequest();
            
            bool result = _activityRepository.Save(key, activity);

            return result ? Ok() : BadRequest();
        }

    }
}
