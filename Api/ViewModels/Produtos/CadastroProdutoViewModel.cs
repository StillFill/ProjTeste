using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ViewModels.Produtos
{
    public class CadastroProdutoViewModel
    {
        public int IdProdutoExterno { get; set; }

        public string NomeProduto { get; set; }

        public IEnumerable<Grupo> Campos { get; set; }
    }
}
