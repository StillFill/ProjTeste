using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Z.Dapper.Plus;

namespace Dominio.Interfaces
{
    public interface IRepositoryBase<TEntity>: IDisposable where TEntity : class
    {
        Task<object> Adicionar(TEntity obj);

        Task<bool> Atualizar(int id, TEntity obj);

        Task<bool> Remover(int id);

        Task<IEnumerable<TEntity>> BuscarTodos();

        void AdicionarMultiplos(IEnumerable<TEntity> entities);
    }
}
