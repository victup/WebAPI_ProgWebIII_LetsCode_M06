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
        public List<Customer> DatabaseCustomer { get; set; } = new List<Customer>()
        {
            new Customer() { Id = 1, Name = "Marcos", Cpf = " ", Birthday = new DateTime(2000, 12, 28) },
            new Customer() { Id = 2, Name = "Victor", Cpf = "789.456.123-21", Birthday = new DateTime(2000, 12, 28) },
            new Customer() { Id = 3, Name = "João", Cpf = "951.147.852-33", Birthday = new DateTime(2000, 12, 28) },
            new Customer() { Id = 4, Name = "Pedro", Cpf = "159.357.741-88", Birthday = new DateTime(2000, 12, 28) },
            new Customer() { Id = 5, Name = "Lucas", Cpf = "364.746.666-11", Birthday = new DateTime(2000, 12, 28) },
        };


        public CustomerController()
        {
        }
         

        [HttpPost("/cadastrar")] //https://localhost:7156/api/Customer CREATE
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Create(Customer customer)
        {
            if (CheckExisteceCustomer(customer.Id))
            {
                return Conflict();
            }
            else
            {
                DatabaseCustomer.Add(customer);

                return Created(nameof(Create), customer);
            }
        }


        [HttpGet("/{cpf}/pesquisar")] //https://localhost:7156/api/Customer GET
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Customer>> Read(string cpf)
        {
            List<Customer> listReturn = new();

            foreach (var customer in DatabaseCustomer)
            {
                 if (customer.Cpf.ToLower() == cpf.ToLower())
                 listReturn.Add(customer);
            }

            if (listReturn.Count != 0)
                return Ok(listReturn);
            else return NotFound();
        }


        [HttpPut("/{index}/atualizar")] //https://localhost:7156/api/Customer PUT
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Customer> Update(int index, Customer who)
        {
            if (CheckExisteceCustomer(who.Id))
            {
                return NotFound();
            }
            else
            {
                DatabaseCustomer[index] = who;
                return Ok(who);
            }

                
        }

        //https://localhost:7156/api/Customer DELETE
        [HttpDelete("/{index}/deletar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delet(int index)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            DatabaseCustomer.RemoveAt(index);
            return Ok();
        }

        private bool CheckExisteceCustomer(int id)
        {
            bool customerExists = false;

            foreach (var customer in DatabaseCustomer)
            {
                if(customer.Id == id)
                    customerExists = true;  
            }

            return customerExists;
        }

    }

   
}
