using Dominio.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IParametrosRepository : IRepositoryBase<Parametros> {
        public Task<IEnumerable<Parametros>> BuscarPorIdProduto(object id);
    }
}
