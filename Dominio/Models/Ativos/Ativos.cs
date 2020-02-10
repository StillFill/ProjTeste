using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Ativos
{
    public class Ativos
    {
        public string NomeProduto { get; set; }
        public string NomeAtivo { get; set; }
        public string Campos { get; set; }
        public string Versao { get; set; }
        public string UrlPdf { get; set; }
    }
}
