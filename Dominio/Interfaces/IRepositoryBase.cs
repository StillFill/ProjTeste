using System;

namespace Dominio.Interfaces
{
    public interface IRepositoryBase<TEntity>: IDisposable where TEntity : class
    {
        void Add(TEntity obj);
    }
}
