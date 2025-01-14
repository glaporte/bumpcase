using calendar.Entites;
using calendar.Repository;
using Microsoft.AspNetCore.Mvc;

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
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
        }

        [BindProperties]
        public class MeetingUpdateParameter
        {
            public int MeetingId { get; set; }
            public DateTime Start { get; set; }
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


        [HttpDelete]
        public ActionResult<List<Meeting>> CancelMeeting(int meetingId)
        {
            try
            {
                _meetingRepository.CancelMeeting(meetingId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<List<Meeting>> Get(int veteId)
        {
            try
            {
                var meetings = _meetingRepository.GetMeetings(veteId);
                return Ok(meetings);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("reschedule")]
        [Produces(typeof(Meeting))]
        public async Task<IActionResult> ReScheduleMeeting([FromBody] MeetingUpdateParameter meetingParameter)
        {
            var meeting = _meetingRepository.GetMeeting(meetingParameter.MeetingId);
            if (meeting == null)
                return BadRequest("meeting not found");

            try
            {
                return Ok(_meetingRepository.RescheduleMeeting(meetingParameter));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
