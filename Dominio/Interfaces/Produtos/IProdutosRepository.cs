using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IProdutosRepository : IRepositoryBase<Produto>
    {
        public IEnumerable<Produto> BuscarPorIdProduto(int id);

        public int DesativarProduto(int id);
    }
}
