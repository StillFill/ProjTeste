using Dominio.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IParamProdutoRepository : IRepositoryBase<ParamProduto> {
        public Task<IEnumerable<ParamProduto>> BuscarPorIdProduto(object id);
    }
}
