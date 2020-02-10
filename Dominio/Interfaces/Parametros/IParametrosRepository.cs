using Dominio.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IParametrosRepository : IRepositoryBase<Parametros> {
        Task<IEnumerable<Parametros>> BuscarPorIdProduto(object id);
    }
}
