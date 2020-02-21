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
using AutoMapper;

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
        private readonly IMapper _mapper;

        private readonly GrupoViewModel viewModelServices = new GrupoViewModel();

        public ProdutosController(IMapper mapper, IParametrosRepository parametrosRepository, IProdutosRepository produtosRepository, ITipoProdutoService tipoProdutoService)
        {
            this.parametrosRepository = parametrosRepository;
            this.produtosRepository = produtosRepository;
            this.produtoServices = tipoProdutoService;
            _requestHandler = new RequestHandler();
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult BuscarTiposProdutos()
        {
            IEnumerable<TipoProduto> tiposProdutos = produtoServices.BuscarTiposProdutos();
            return Ok(tiposProdutos);
        }

        [HttpGet("labels")]
        public async Task<ActionResult<IEnumerable<string>>> BuscarLabels()
        {
            IEnumerable<string> labels = await parametrosRepository.BuscarTodosNomes();
            return Ok(labels.Distinct());
        }

        [HttpGet("{idProdutoExterno}")]
        public async Task<ActionResult<ProdutoViewModel>> BuscarPorId(int idProdutoExterno)
        {

            IEnumerable<Produto> produtos = produtosRepository.BuscarPorIdProduto(idProdutoExterno);

            List<GrupoViewModel> campos = new List<GrupoViewModel>();

            if (produtos.Count() == 0) return Ok(new ProdutoViewModel(idProdutoExterno, null, campos, 0));

            Produto produto = produtos.First();

            int idProduto = produto.Id;

            List<Parametro> parametros = (await parametrosRepository.BuscarPorIdProduto(idProduto)).ToList();

            campos = viewModelServices.AgruparParametrosPorGrupo(parametros);

            ProdutoViewModel result = new ProdutoViewModel(idProdutoExterno, produto.Nome, campos, produto.Versao);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CadastrarParametros([FromBody] CadastroProdutoViewModel paramProdutos)
        {

            IEnumerable<Produto> produtos = produtosRepository.BuscarPorIdProduto(paramProdutos.IdProdutoExterno);

            int ultimaVersao = 0;

            if (produtos.Count() > 0)
            {
                Produto ultimoProduto = produtos.Last();

                ultimaVersao = ultimoProduto.Versao;
                produtosRepository.DesativarProduto(ultimoProduto.Id);
            }

            int novaVersao = ultimaVersao + 1;

            Produto novoProduto = _mapper.Map<Produto>(paramProdutos);
            novoProduto.Versao = novaVersao;

            int idProduto = (int) (await produtosRepository.Adicionar(novoProduto));

            foreach (Grupo paramProduto in paramProdutos.Campos)
            {
                paramProduto.IdProduto = idProduto;
                await parametrosRepository.Adicionar(paramProduto);
            }

            return Ok(_requestHandler.Send("Inserido com sucesso!"));
        }
    }
}
