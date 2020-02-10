using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.ViewModels.Ativos;
using AutoMapper;
using Domain.Models.Ativos;

namespace Api.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<AtivosViewModel, Ativos>()
                .ForMember(dest => dest.Campos, opt => opt.MapFrom(src => string.Join(",",src.CamposString)));
        }

    }
}
