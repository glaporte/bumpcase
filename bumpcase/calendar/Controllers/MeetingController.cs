using calendar.Entites;
using calendar.Repository;
using Microsoft.AspNetCore.Mvc;

namespace calendar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeetingController : ControllerBase
    {
        private readonly ILogger<MeetingController> _logger;
        private readonly MeetingRepository _meetingRepository;

        public MeetingController(ILogger<MeetingController> logger, MeetingRepository repository)
        {
            _meetingRepository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger;
        }

        [HttpPost]
        [Produces(typeof(Patient))]
        public async Task<IActionResult> ScheduleMeeting(Meeting patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _meetingRepository.AddMeeting(patient);

            return Ok(patient);
        }
    }
}
