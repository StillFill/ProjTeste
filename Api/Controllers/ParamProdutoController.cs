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

namespace Api.Controllers
{
    [ApiController]
    [Route("parametros")]
    public class ParamProdutoController : Controller
    {
        private readonly IParamProdutoRepository produtoRepository;

        public ParamProdutoController(IParamProdutoRepository produtoRepository, ITipoProdutoService tipoProdutoService)
        {
            this.produtoRepository = produtoRepository;
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
        public async Task<ActionResult> BuscarPorId(string id)
        {
            IEnumerable<ParamProduto> produtos = await produtoRepository.BuscarPorIdProduto(id);


            var columns = new Dictionary<string, List<ParamProduto>> {};

            foreach (ParamProduto produto in produtos)
            {
                if (columns.ContainsKey(produto.Grupo)) {
                    List<ParamProduto> paramProduto = columns.GetValueOrDefault(produto.Grupo);
                    
                    columns.Remove(produto.Grupo);

                    paramProduto.Add(produto);

                    columns.Add(produto.Grupo, paramProduto);
                } else
                {
                    columns.Add(produto.Grupo, new List<ParamProduto>() { produto });
                }
            }

            return Ok(columns);
        }
    }
}
