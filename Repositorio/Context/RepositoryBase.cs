using System;
using Dommel;
using Microsoft.Extensions.Configuration;
using Dominio.Interfaces;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using Z.Dapper.Plus;

namespace Repositorio.Common
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {

        protected SqlConnection conn;

        public RepositoryBase(IConfiguration configuration)
        {
            conn = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        private Boolean _disposed = false;

        public void Dispose()
        {
            if (!_disposed)
            {
                conn.Close();
                conn.Dispose();
                _disposed = true;
            }

            GC.SuppressFinalize(this);
        }

        public Task<object> Adicionar(TEntity obj)
        {
            return conn.InsertAsync(obj);
        }

        public Task<bool> Atualizar(int id, TEntity obj)
        {
            return conn.UpdateAsync(obj);
        }

        public Task<bool> Remover(int id)
        {
            return conn.DeleteAsync(id);
        }

        public Task<IEnumerable<TEntity>> BuscarTodos()
        {
            return conn.GetAllAsync<TEntity>();
        }

        public void AdicionarMultiplos(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entitie in entities)
            {
               conn.InsertAsync(entitie);
            }
        }
    }
}
