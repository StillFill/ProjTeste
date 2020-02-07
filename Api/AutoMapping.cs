using Api.ViewModels.Produtos;
using AutoMapper;
using Domain.Models;
using Dominio.Models;
using System.Collections.Generic;

public class AutoMapping : Profile
{

    IMapper _mapper;
    public AutoMapping()
    {
        CreateMap<CadastroProdutoViewModel, Produto>()
            .ForMember(d => d.IdProduto, s => s.MapFrom(a => a.IdProdutoExterno))
            .ForMember(d => d.Nome, s => s.MapFrom(a => a.NomeProduto));

    }
}