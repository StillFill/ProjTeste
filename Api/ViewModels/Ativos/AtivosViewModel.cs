using Api.ViewModels.Comum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ViewModels.Ativos
{
    public class AtivosViewModel
    {
        public string NomeProduto { get; set; }
        public string NomeAtivo { get; set; }
        public List<Campos> Campos { get; set; }
        public List<String> CamposString { get; set; }
        public string Versao { get; set; }
        public string UrlPdf { get; set; }
    }
}
