using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MES.Rastreabilidade.Core.Entities;
using MES.Rastreabilidade.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace MES.Rastreabilidade.Api.Controllers
{
    [ApiController]

    public class ProdutoController : ControllerBase
    {
        public readonly AppDbContext _context;

        public ProdutoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("Produto")]
        public IActionResult PostProduct(Produto produto)
        {
            _context.Add(produto);

            if (produto.Code.Length > 11)
            {
                return BadRequest(new { Erro = "O código do produto deve ter no máximo 10 dígitos" });
            }

            _context.SaveChanges();

            return Ok(produto);
        }

        [HttpGet("Produto/{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _context.Produtos.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet("Produto")]
        public IActionResult GetProduct()
        {
            var productsDB = _context.Produtos.ToList();

            return Ok(productsDB);
        }

        [HttpPut("Produto/{id}")]
        public IActionResult PutProduct(int id, Produto produto)
        {
            var productDB = _context.Produtos.Find(id);

            if (productDB == null)
            {
                return NotFound();
            }

            productDB.Code = produto.Code;
            productDB.Name = produto.Name;

            _context.Produtos.Update(productDB);
            _context.SaveChanges();

            return Ok(productDB);
        }

        [HttpDelete("Produto/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var productDB = _context.Produtos.Find(id);

            if (productDB == null)
            {
                return NotFound();
            }

            _context.Produtos.Remove(productDB);
            _context.SaveChanges();

            return NoContent();

        }

    }
}