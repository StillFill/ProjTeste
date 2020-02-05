﻿using Dommel;
using Dominio.Interfaces;
using Dominio.Models;
using Microsoft.Extensions.Configuration;
using Repositorio.Common;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dapper;

namespace AspnetCore.EFCore_Dapper.Data.Repositories.Dapper
{
    public class ParametrosRepository : RepositoryBase<Parametros>, IParametrosRepository
    {
        public ParametrosRepository(IConfiguration configuration) : base(configuration) {}

        public Task<IEnumerable<Parametros>> BuscarPorIdProduto(object id)
        {
            string sql = "SELECT * FROM Parametros WHERE IdProduto='" + id + "'";
            return conn.QueryAsync<Parametros> (sql);
        }
    }
}