using calendar.Entites;
using calendar.Repository;
using Microsoft.AspNetCore.Mvc;

namespace calendar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly CustomerRepository _customerRepository;

        public CustomerController(ILogger<CustomerController> logger, CustomerRepository repository)
        {
            _customerRepository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger;
        }

        [HttpPost]
        [Produces(typeof(Customer))]
        public IActionResult AddCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _customerRepository.AddCustomer(customer);

            return Ok(customer);
        }

        [HttpGet]
        public ActionResult<List<Customer>> Get()
        {
            return Ok(_customerRepository.GetCustomers());
        }
    }
}
