using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces.Produtos;
using Domain.Models.Produtos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("tipoProdutos")]
    public class TipoProdutoController : Controller
    {
        private readonly ITipoProdutoService produtoServices;

        public TipoProdutoController(ITipoProdutoService tipoProdutoService)
        {
            this.produtoServices = tipoProdutoService;
        }
        [HttpGet]
        public ActionResult BuscarTiposProdutos()
        {
            IEnumerable<TipoProduto> tiposProdutos = produtoServices.BuscarTiposProdutos();

            return Ok(tiposProdutos);
        }
    }
}