using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace CRUD_Clientes.Repository
{
    public class CustomerRepository
    {

        private readonly IConfiguration _configuration;

        public CustomerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Customer> GetCustomers()
        {
            var query = "SELECT * FROM clientes";

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<Customer>(query).ToList();
        }

        public Customer GetCustomer(string cpf)
        {
            var query = "SELECT * FROM clientes WHERE cpf = @cpf";

            var parameters = new DynamicParameters(new { cpf });

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.QueryFirstOrDefault<Customer>(query, parameters);
        }

        public bool InsertCustomer(Customer customer)
        {

            var query = $"INSERT INTO clientes VALUES(@cpf, @nome, @nascimento, @idade)";

            var parameters = new DynamicParameters();
            parameters.Add("cpf", customer.Cpf);
            parameters.Add("nome", customer.Nome);
            parameters.Add("nascimento", customer.DataNascimento);
            parameters.Add("idade", customer.Idade);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;

        }

        public bool DeleteCustomer(string cpf)

        {
            var query = "DELETE FROM clientes where cpf = @cpf";

            var parameters = new DynamicParameters(new { cpf });

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool UpdateCustomer(string cpf, Customer customer)
        {
            var query = "UPDATE clientes SET cpf = @cpf, nome = @nome, dataNascimento = @nascimento, idade = @idade WHERE cpf = @cpf";

            var parameters = new DynamicParameters();

            parameters.Add("cpf", customer.Cpf);
            parameters.Add("nome", customer.Nome);
            parameters.Add("nascimento", customer.DataNascimento);
            parameters.Add("idade", customer.Idade);
   

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

    }
}
