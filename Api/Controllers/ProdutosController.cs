using Microsoft.AspNetCore.Mvc;
using Dominio.Interfaces;
using Dominio.Models;
using System.Threading.Tasks;
using Domain.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Linq;
using Api.ViewModels.Produtos;
using Domain.Interfaces.Produtos;
using Api.ViewModels;

namespace Api.Controllers
{
    [ApiController]
    [Route("produtos")]
    public class ProdutosController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IParametrosRepository parametrosRepository;
        private readonly IProdutosRepository produtosRepository;
        private readonly ITipoProdutoService produtoServices;
        private readonly RequestHandler _requestHandler;

        private readonly GrupoViewModel viewModelServices = new GrupoViewModel();

        public ProdutosController(IParametrosRepository parametrosRepository, IProdutosRepository produtosRepository, ITipoProdutoService tipoProdutoService)
        {
            this.parametrosRepository = parametrosRepository;
            this.produtosRepository = produtosRepository;
            this.produtoServices = tipoProdutoService;
            _requestHandler = new RequestHandler();
        }

        [HttpGet]
        public ActionResult BuscarTiposProdutos()
        {
            IEnumerable<TipoProduto> tiposProdutos = produtoServices.BuscarTiposProdutos();
            return Ok(tiposProdutos);
        }

        [HttpGet("{idProdutoExterno}")]
        public async Task<ActionResult<ProdutoViewModel>> BuscarPorId(int idProdutoExterno)
        {
            IEnumerable<Produto> produtos = await produtosRepository.BuscarPorIdProduto(idProdutoExterno);

            if (produtos.Count() == 0) return Ok(new ProdutoViewModel(idProdutoExterno, null, new List<GrupoViewModel>(), 1));

            Produto produto = produtos.First();

            int idProduto = produto.Id;

            List<Parametro> parametros = (await parametrosRepository.BuscarPorIdProduto(idProduto)).ToList();

            List<GrupoViewModel> prods = viewModelServices.AgruparParametrosPorGrupo(parametros);

            ProdutoViewModel result = new ProdutoViewModel(idProdutoExterno, produto.Nome, prods, produto.Versao);

            return Ok(result);
        }

        [HttpGet("labels")]
        public async Task<ActionResult<IEnumerable<string>>> BuscarLabels()
        {
            IEnumerable<string> labels = await parametrosRepository.BuscarTodosNomes();
            return Ok(labels);
        }

        [HttpPost]
        public async Task<ActionResult> CadastrarParametros([FromBody] CadastroProdutoViewModel paramProdutos)
        {

            IEnumerable<Produto> produtos = await produtosRepository.BuscarPorIdProduto(paramProdutos.IdProdutoExterno);

            int ultimaVersao = 0;

            if (produtos.Count() > 0)
            {
                Produto ultimoProduto = produtos.Last();

                ultimaVersao = ultimoProduto.Versao;
                await produtosRepository.DesativarProduto(ultimoProduto.Id);
            }

            int novaVersao = ultimaVersao + 1;

            Produto novoProduto = new Produto(paramProdutos.NomeProduto, paramProdutos.IdProdutoExterno, novaVersao);
            int idProduto = (int) (await produtosRepository.Adicionar(novoProduto));

            foreach (Parametro paramProduto in paramProdutos.Campos)
            {
                paramProduto.IdProduto = idProduto;
                await parametrosRepository.Adicionar(paramProduto);
            }

            return Ok(_requestHandler.Send("Inserido com sucesso!"));
        }
    }
}
