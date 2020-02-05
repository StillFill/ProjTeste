using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces.Produtos;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("tiposProdutos")]
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