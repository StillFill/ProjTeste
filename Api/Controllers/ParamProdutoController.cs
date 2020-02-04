using System;
using Microsoft.AspNetCore.Mvc;
using Dominio.Interfaces;
using Dominio.Models;
using System.Collections;
using System.Threading.Tasks;
using System.Text.Json;
using Domain.Models.Produtos;
using System.Collections.Generic;
using Services;
using Domain.Interfaces.Produtos;
using System.Linq;
using Api.ViewModels;

namespace Api.Controllers
{
    [ApiController]
    [Route("parametros")]
    public class ParamProdutoController : Controller
    {
        private readonly IParamProdutoRepository produtoRepository;
        private readonly ParamProdutoViewModel viewModel;

        public ParamProdutoController(IParamProdutoRepository produtoRepository, ITipoProdutoService tipoProdutoService)
        {
            this.produtoRepository = produtoRepository;
            this.viewModel = new ParamProdutoViewModel();
        }
        
        [HttpPost]
        public async Task<ActionResult> CadastrarParametros([FromBody] IEnumerable<ParamProduto> paramProdutos)
        {
            foreach (ParamProduto paramProduto in paramProdutos)
            {
                await produtoRepository.Adicionar(paramProduto);
            }

            return Ok("Inserido com sucesso!, Id inserido");
        }

        [HttpGet]
        public async Task<ActionResult> BuscarTodos()
        {
            IEnumerable<ParamProduto> produtos = await produtoRepository.BuscarTodos();

            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<ParamProdutoViewModel>>> BuscarPorId(string id)
        {
            List<ParamProduto> produtos = (await produtoRepository.BuscarPorIdProduto(id)).ToList();

            if (produtos.Count() == 0) return Ok(new List<ParamProdutoViewModel>());

            List<ParamProdutoViewModel> prods = viewModel.AgruparProdutos(produtos);

            return Ok(prods);
        }
    }
}
