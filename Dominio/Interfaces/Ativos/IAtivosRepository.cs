using Domain.Models.Ativos;
using Dominio.Interfaces;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAtivosRepository : IRepositoryBase<Ativos>
    {
        Task<Ativos> BuscarPorIdProduto(int id);
    }
}
