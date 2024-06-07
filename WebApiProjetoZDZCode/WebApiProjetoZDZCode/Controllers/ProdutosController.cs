using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

using WebApiProjetoZDZCode.Models;

namespace WebApiZDZCode.Controllers
{
    [EnableCors(origins: "*", headers:"*", methods:"*")]
    public class ProdutosController : ApiController
    {
        private static readonly List<Produto> Produtos = new List<Produto>
        {
            new Produto { Id = 1, Nome = "Ice cream sandwich", Calorias =  237, Preco = 5, Quantidade = 10 },
            new Produto { Id = 2, Nome = "Eclair", Calorias =  262, Preco = 7, Quantidade = 5 },
            new Produto { Id = 3, Nome = "Cupcake", Calorias =  305, Preco = 4, Quantidade = 8 },
            new Produto { Id = 4, Nome = "Gingerbread", Calorias =  356, Preco = 6, Quantidade = 12 }
        };

        // GET: api/Produtos
        public IEnumerable<Produto> GetProdutos()
        {
            return Produtos;
        }

        // GET: api/Produtos/5
        public IHttpActionResult GetProduto(int id)
        {
            var produto = Produtos.FirstOrDefault(p => p.Id == id);
            if (produto == null)
            {
                return NotFound();
            }
            return Ok(produto);
        }

        // POST: api/Produtos
        public IHttpActionResult PostProduto(Produto produto)
        {
            produto.Id = Produtos.Count + 1; // Simulação de um novo ID
            Produtos.Add(produto);
            return CreatedAtRoute("DefaultApi", new { id = produto.Id }, produto);
        }

        // PUT: api/Produtos/5
        public IHttpActionResult PutProduto(int id, Produto produto)
        {
            var existingProduto = Produtos.FirstOrDefault(p => p.Id == id);
            if (existingProduto == null)
            {
                return NotFound();
            }
            existingProduto.Nome = produto.Nome;
            existingProduto.Preco = produto.Preco;
            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }

        // DELETE: api/Produtos/5
        public IHttpActionResult DeleteProduto(int id)
        {
            var produto = Produtos.FirstOrDefault(p => p.Id == id);
            if (produto == null)
            {
                return NotFound();
            }
            Produtos.Remove(produto);
            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }
    }
}
