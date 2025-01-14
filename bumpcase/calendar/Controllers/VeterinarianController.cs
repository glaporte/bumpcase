using calendar.Entites;
using calendar.Repository;
using Microsoft.AspNetCore.Mvc;

namespace calendar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VeterinarianController : ControllerBase
    {
        private readonly ILogger<VeterinarianController> _logger;
        private readonly VeterinarianRepository _veterinarianRepository;
        private readonly SlotRepository _slotRepository;


        public VeterinarianController(ILogger<VeterinarianController> logger, VeterinarianRepository repository
            , SlotRepository slot)
        {
            _veterinarianRepository = repository ?? throw new ArgumentNullException(nameof(repository));
            _slotRepository = slot;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<Patient>> Get()
        {
            return Ok(_veterinarianRepository.GetVeterinarians());
        }

        [HttpGet]
        [Route("slots")]
        public ActionResult<List<Slot>> GetSlots(int vete)
        {
            if (_veterinarianRepository.GetVeterinarian(vete) == null)
            {
                return BadRequest($"Invalid Veterinarian");
            }
            return Ok(_slotRepository.GetSlots(vete));
        }
    }
}
