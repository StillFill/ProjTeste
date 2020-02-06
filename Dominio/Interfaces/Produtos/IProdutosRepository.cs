using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IProdutosRepository : IRepositoryBase<Produto>
    {
        public Task<IEnumerable<Produto>> BuscarPorIdProduto(int id);

        public Task<int> AdicionarProduto(Produto obj);

        public Task<int> DesativarProduto(int id);
    }
}
