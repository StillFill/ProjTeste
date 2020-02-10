using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ViewModels.Comum
{
    public class Campos
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Tipo { get; set; }
        [Required]
        public string Tamanho { get; set; }
        
        public List<string> Dominio { get; set; }
        
        [Required]
        public Boolean Obrigatorio { get; set; }
        
        [Required]
        public string Grupo { get; set; }
        
        public string Mascara { get; set; }

        public short Inteiro { get; set; }
        
        public short Decimal { get; set; }
        
        [Required]
        public string ChaveBaseCorporativa { get; set; }

        public string Valor { get; set; }
    }
}
