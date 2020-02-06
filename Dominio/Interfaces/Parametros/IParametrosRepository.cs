using Dominio.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IParametrosRepository : IRepositoryBase<Parametro> {
        public Task<IEnumerable<Parametro>> BuscarPorIdProduto(object id);
    }
}
