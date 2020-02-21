using Dominio.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IParametrosRepository : IRepositoryBase<Grupo> {
        public Task<IEnumerable<Parametro>> BuscarPorIdProduto(object id);

        public Task<IEnumerable<string>> BuscarTodosNomes();
    }
}
