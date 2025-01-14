using calendar.Entites;
using calendar.Repository;
using Microsoft.AspNetCore.Mvc;

namespace calendar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly ILogger<PatientController> _logger;
        private readonly PatientRepository _patientRepository;

        public PatientController(ILogger<PatientController> logger, PatientRepository repository)
        {
            _patientRepository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger;
        }

        [HttpPost]
        [Produces(typeof(Patient))]
        public async Task<IActionResult> AddPatient(Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _patientRepository.AddPatient(patient);

            return Ok(patient);
        }

        [HttpGet]
        public ActionResult<List<Patient>> Get()
        {
            return Ok(_patientRepository.GetPatients());
        }
    }
}
