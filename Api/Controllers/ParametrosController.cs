using Microsoft.AspNetCore.Mvc;
using Dominio.Interfaces;
using Dominio.Models;
using System.Threading.Tasks;
using Domain.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Linq;
using Api.ViewModels;

namespace Api.Controllers
{
    [ApiController]
    [Route("parametros")]
    public class ParametrosController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IParametrosRepository parametrosRepository;
        private readonly IProdutosRepository produtosRepository;
        private readonly ParametrosViewModel viewModelServices = new ParametrosViewModel();

        public ParametrosController(IParametrosRepository parametrosRepository, IProdutosRepository produtosRepository)
        {
            this.parametrosRepository = parametrosRepository;
            this.produtosRepository = produtosRepository;
        }
        
        [HttpPost("{idProdutoExterno}/{nomeProduto}")]
        public async Task<ActionResult> CadastrarParametros(int idProdutoExterno, string nomeProduto, [FromBody] IEnumerable<Parametros> paramProdutos)
        {

            IEnumerable<Produtos> produtos = await produtosRepository.BuscarPorIdProduto(idProdutoExterno);

            if (produtos.Count() > 0)
            {
                await produtosRepository.DesativarProduto(produtos.Last().Id);
            }

            Produtos novoProduto = new Produtos(nomeProduto, idProdutoExterno);
            int idProduto = (int) (await produtosRepository.Adicionar(novoProduto));

            foreach (Parametros paramProduto in paramProdutos)
            {
                paramProduto.IdProduto = idProduto;
                await parametrosRepository.Adicionar(paramProduto);
            }

            return Ok("Inserido com sucesso!, Id inserido");
        }

        [HttpGet("{idProdutoExterno}")]
        public async Task<ActionResult<List<ParametrosViewModel>>> BuscarPorId(int idProdutoExterno)
        {
            IEnumerable<Produtos> produtos = await produtosRepository.BuscarPorIdProduto(idProdutoExterno);

            if (produtos.Count() == 0) return NotFound();

            int idProduto = produtos.First().Id;

            List<Parametros> parametros = (await parametrosRepository.BuscarPorIdProduto(idProduto)).ToList();

            if (parametros.Count() == 0) return Ok(new List<ParametrosViewModel>());

            List<ParametrosViewModel> prods = viewModelServices.AgruparParametrosPorGrupo(parametros);

            return Ok(prods);
        }
    }
}
