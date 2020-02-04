using Dominio.Models;
using System.Collections.Generic;
using System.Linq;

namespace Api.ViewModels
{
    public class ParamProdutoViewModel
    {
        public string Grupo { get; set; }
        public List<ParamProduto> Parametros { get; set; }

        public ParamProdutoViewModel(string grupo, List<ParamProduto> parametros)
        {
            this.Grupo = grupo;
            this.Parametros = parametros;
        }
        
        public ParamProdutoViewModel()
        {

        }

        public List<ParamProdutoViewModel> AgruparProdutos(List<ParamProduto> produtos)
        {
            return produtos.GroupBy(x => new { x.Grupo })
                .Select(d =>
                {
                    List<ParamProduto> listaParams = d.ToList();
                    return new ParamProdutoViewModel(d.Key.Grupo, listaParams);
                })
                .ToList();
        }
    }
}
