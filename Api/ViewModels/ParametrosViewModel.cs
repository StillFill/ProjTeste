using Dominio.Models;
using System.Collections.Generic;
using System.Linq;

namespace Api.ViewModels
{
    public class ParametrosViewModel
    {
        public string Grupo { get; set; }
        public List<Parametros> Parametros { get; set; }

        public ParametrosViewModel(string grupo, List<Parametros> parametros)
        {
            this.Grupo = grupo;
            this.Parametros = parametros;
        }
        
        public ParametrosViewModel()
        {

        }

        public List<ParametrosViewModel> AgruparParametrosPorGrupo(List<Parametros> produtos)
        {
            return produtos.GroupBy(x => new { x.Grupo })
                .Select(d =>
                {
                    List<Parametros> listaParams = d.ToList();
                    return new ParametrosViewModel(d.Key.Grupo, listaParams);
                })
                .ToList();
        }
    }
}
