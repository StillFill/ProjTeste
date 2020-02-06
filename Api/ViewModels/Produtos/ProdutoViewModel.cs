using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ViewModels.Produtos
{
    public class ProdutoViewModel
    {
        public ProdutoViewModel(int id, string? nome, List<GrupoViewModel> campos, int versao)
        {
            Id = id;
            Nome = nome;
            Campos = campos;
            Versao = versao;
        }

        public int Id { get; set; }

        public string Nome { get; set; }

        public List<GrupoViewModel> Campos { get; set; }

        public int Versao { get; set; }
    }
}
