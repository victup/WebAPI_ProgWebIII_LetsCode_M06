using CRUD_Clientes.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Clientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes ("application/json")] //define que a entrada é em json
    [Produces("application/json")] //define que a saida é em json
    public class CustomerController : ControllerBase
    {
        public CustomerRepository _customerRepository;
        public CustomerController(IConfiguration configuration)
        {
            _customerRepository = new CustomerRepository(configuration);
        }
         

        [HttpPost("/cadastrar")] //https://localhost:7156/api/Customer CREATE
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult CreateCustomer(Customer customer)
        {
            if (!_customerRepository.InsertCustomer(customer))
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(CreateCustomer), customer);
        }

        [HttpGet("/todosOsClientes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Customer>> GetCustomers()
        {
            return Ok(_customerRepository.GetCustomers());
        }

        [HttpGet("/{cpf}/pesquisarPorCpf")] //https://localhost:7156/api/Customer GET
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Customer>> ReadCustomer(string cpf)
        {
            var customer = _customerRepository.GetCustomer(cpf);
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }


        [HttpPut("/{cpf}/atualizar")] //https://localhost:7156/api/Customer PUT
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Customer> Update(string cpf, Customer who)
        {
            if (!_customerRepository.UpdateCustomer(cpf, who))
                return NotFound();

            return Ok(who);


        }

        //https://localhost:7156/api/Customer DELETE
        [HttpDelete("/{cpf}/deletar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delet(string cpf)
        {
            if (!_customerRepository.DeleteCustomer(cpf))
            {
                return NotFound();
            }
            return Ok();
        }

 
    }

   
}
