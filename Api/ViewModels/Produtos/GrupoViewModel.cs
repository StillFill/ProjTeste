using Dominio.Models;
using System.Collections.Generic;
using System.Linq;

namespace Api.ViewModels.Produtos
{
    public class GrupoViewModel
    {
        public string Grupo { get; set; }
        public List<Parametro> Parametros { get; set; }

        public GrupoViewModel(string grupo, List<Parametro> parametros)
        {
            this.Grupo = grupo;
            this.Parametros = parametros;
        }
        
        public GrupoViewModel()
        {

        }

        public List<GrupoViewModel> AgruparParametrosPorGrupo(List<Parametro> produtos)
        {
            return produtos.GroupBy(x => new { x.Grupo })
                .Select(d =>
                {
                    List<Parametro> listaParams = d.ToList();
                    return new GrupoViewModel(d.Key.Grupo, listaParams);
                })
                .ToList();
        }
    }
}
