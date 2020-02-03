using System;
using Microsoft.AspNetCore.Mvc;
using Dominio.Interfaces;
using Dominio.Models;

namespace Api.Controllers
{
    [ApiController]
    [Route("inserir")]
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepository produtoRepository;

        public ProdutoController(IProdutoRepository produtoRepository)
        {
            this.produtoRepository = produtoRepository;
        }
        
        [HttpPost]
        public ActionResult Post([FromBody] Produto obj)
        {

            produtoRepository.Add(obj);

            return Ok("Inserido com sucesso!!");
        }
    }
}
