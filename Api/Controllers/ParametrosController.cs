using System;
using Microsoft.AspNetCore.Mvc;
using Dominio.Interfaces;
using Dominio.Models;
using System.Collections;
using System.Threading.Tasks;
using System.Text.Json;
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

            int idProduto;

            if (produtos.Count() == 0)
            {
                Produtos novoProduto = new Produtos(nomeProduto, idProdutoExterno);
                idProduto = (int) (await produtosRepository.Adicionar(novoProduto));
            } 
            else idProduto = produtos.First().Id;

            foreach (Parametros paramProduto in paramProdutos)
            {
                paramProduto.IdProduto = idProduto;
                await parametrosRepository.Adicionar(paramProduto);
            }

            return Ok("Inserido com sucesso!, Id inserido");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<ParametrosViewModel>>> BuscarPorId(string id)
        {
            List<Parametros> parametros = (await parametrosRepository.BuscarPorIdProduto(id)).ToList();

            if (parametros.Count() == 0) return Ok(new List<ParametrosViewModel>());

            List<ParametrosViewModel> prods = viewModelServices.AgruparParametrosPorGrupo(parametros);

            return Ok(prods);
        }
    }
}
