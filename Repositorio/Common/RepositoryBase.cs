using System;
using Dommel;
using Microsoft.Extensions.Configuration;
using Dominio.Interfaces;
using System.Data.SqlClient;
using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using Dapper;
using Dominio.Models;

namespace Repositorio.Common
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {

        private readonly IConfiguration _configuration;
        protected SqlConnection conn;

        public RepositoryBase(IConfiguration configuration)
        {
            _configuration = configuration;

            conn = new SqlConnection("Server=DESKTOP-6T3AR1E\\MSSQLSERVER01;Database=master;Trusted_Connection=True;");

            conn.Open();
        }

        public void Add(TEntity obj)
        {
            conn.Insert(obj);
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
    }
}
