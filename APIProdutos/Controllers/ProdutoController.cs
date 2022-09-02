using APIProdutos.Repository;
using APIProdutos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ProdutoController : ControllerBase
{
    public List<Produto> ProdutoList { get; set; }

    public ProdutoRepository _repositoryProduto;

    public ProdutoController(IConfiguration configuration)
    {
        ProdutoList = new List<Produto>();
        _repositoryProduto = new ProdutoRepository(configuration);
    }

    [HttpGet("/produto/{descricao}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Produto> GetProduto(string descricao)
    {
        var produtos = _repositoryProduto.GetProduto(descricao);
        if (produtos == null)
            return BadRequest();
        return Ok(produtos);
        
    }

    [HttpGet("/produto")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<List<Produto>> GetProdutos()
    {
        return Ok(_repositoryProduto.GetProdutos());
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Produto> PostProduto([FromBody]Produto produto)
    {
        if (!_repositoryProduto.InsertProduto(produto))
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(PostProduto), produto);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateProduto(long id, Produto produto)
    {
        if(!_repositoryProduto.UpdateProduto(id, produto))
            return BadRequest();

        return Ok();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<List<Produto>> DeleteProduto(long id)
    {
        if (!_repositoryProduto.DeleteProduto(id))
        {
            return NotFound();
        }
        return NoContent();
    }
}
