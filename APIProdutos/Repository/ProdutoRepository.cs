using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace APIProdutos.Repository
{
    public class ProdutoRepository
    {
        private readonly IConfiguration _configuration;

        public ProdutoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Produto> GetProdutos()
        {
            var query = "SELECT * FROM Produtos";

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<Produto>(query).ToList();
        }

        public Produto GetProduto(string descricao)
        {
            var query = "SELECT * FROM produtos WHERE descricao = @descricao";

            var parameters = new DynamicParameters(new { descricao });

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.QueryFirstOrDefault<Produto>(query, parameters);
        }

        public bool InsertProduto(Produto produto)

        {
            // por seguranca nao se coloca nos values produto.Nome…. usa-se o @

            var query = $"INSERT INTO produtos VALUES(@descricao, @preco, @quantidade)";

            var parameters = new DynamicParameters();

            parameters.Add("descricao", produto.Descricao);

            parameters.Add("preco", produto.Preco);

            parameters.Add("quantidade", produto.Quantidade);

            //abrir conexao

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1; //== linhas afetadas

        }

        public bool DeleteProduto(long id)

        {

            var query = "DELETE FROM produtos where id = @id";
            
            var parameters = new DynamicParameters();

            parameters.Add("id", id);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool UpdateProduto(long id, Produto produto)
        {
            var query = "UPDATE produtos SET descricao = @descricao, preco = @preco, quantidade = @quantidade WHERE id = @id";

            var parameters = new DynamicParameters();

            parameters.Add("descricao", produto.Descricao);
            parameters.Add("preco", produto.Preco);
            parameters.Add("quantidade", produto.Quantidade);
            parameters.Add("id", id);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }


    }
}