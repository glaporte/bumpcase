using calendar.Entites;
using calendar.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace calendar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeetingController : ControllerBase
    {
        [BindProperties]
        public class MeetingParameter
        {
            public Meeting Meeting { get; set; } = null!;
            public DateTime Start {  get; set; }
            public DateTime End { get; set; }
        }

        private readonly ILogger<MeetingController> _logger;
        private readonly MeetingRepository _meetingRepository;

        public MeetingController(ILogger<MeetingController> logger, MeetingRepository repository)
        {
            _meetingRepository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger;
        }

        [HttpPost]
        [Produces(typeof(Meeting))]
        public async Task<IActionResult> AddMeeting(Meeting meeting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _meetingRepository.AddMeeting(meeting);

            return Ok(meeting);
        }

        [HttpPost]
        [Route("schedule")]
        [Produces(typeof(Meeting))]
        public async Task<IActionResult> ScheduleMeeting([FromBody] MeetingParameter meeting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _meetingRepository.AddMeetingWithCustomDate(meeting);
            return Ok(meeting);
        }

        [HttpGet]
        public ActionResult<List<Meeting>> Get(int veteId)
        {
            return Ok(_meetingRepository.GetMeetings(veteId));
        }
    }
}
