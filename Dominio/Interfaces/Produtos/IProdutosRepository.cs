using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IProdutosRepository : IRepositoryBase<Produtos>
    {
        Task<IEnumerable<Produtos>> BuscarPorIdProduto(int id);

        Task<int> AdicionarProduto(Produtos obj);

        Task<int> DesativarProduto(int id);
    }
}
