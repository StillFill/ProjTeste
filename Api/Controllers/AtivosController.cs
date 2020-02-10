using Api.ViewModels.Ativos;
using Api.ViewModels.Comum;
using Domain.Interfaces;
using Domain.Models.Ativos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;

namespace Api.Controllers
{
    [ApiController]
    [Route("ativos")]
    public class AtivosController : ControllerBase
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IAtivosRepository _AtivosRepository;
        private readonly IMapper _mapper;

        public AtivosController(ILogger<HomeController> logger, IMapper mapper, IAtivosRepository ativosRepository)
        {
            _logger = logger;
            _AtivosRepository = ativosRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> BuscarPorId(AtivosViewModel ativosViewModel)
        {
            foreach (Campos campos in ativosViewModel.Campos)
            {
                ativosViewModel.CamposString.Add(JsonConvert.SerializeObject(campos));
            }

            Ativos ativos = _mapper.Map<Ativos>(ativosViewModel);

            // Ativos result = await _AtivosRepository.BuscarPorIdProduto(idAtivo);

            Console.WriteLine(JsonConvert.SerializeObject(ativos))
;
            return Ok(ativos);
        }

        [HttpGet("essetipo")]
        public async Task<ActionResult<List<AtivosViewModel>>> BuscarPorId2(int idAtivo)
        {
            // AtivosViewModel result = await _AtivosRepository.BuscarPorIdProduto(idAtivo);
            Ativos result = await _AtivosRepository.BuscarPorIdProduto(idAtivo);

            return (new List<AtivosViewModel>());
        }
    }
}

