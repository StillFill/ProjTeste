using Dommel;
using Dominio.Interfaces;
using Dominio.Models;
using Microsoft.Extensions.Configuration;
using Repositorio.Common;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dapper;
using Repository.Mappings;
using Repository.Context;

namespace AspnetCore.EFCore_Dapper.Data.Repositories.Dapper
{
    public class ParametrosRepository : RepositoryBase<Parametro>, IParametrosRepository
    {
        SqlBuilder<Parametro, ParametrosMap> _sqlBuilder;
        public ParametrosRepository(IConfiguration configuration) : base(configuration) {
            _sqlBuilder = new SqlBuilder<Parametro, ParametrosMap>();
        }

        public Task<IEnumerable<Parametro>> BuscarPorIdProduto(object id)
        {
            var builder = _sqlBuilder.Select().Where(x => x.IdProduto, id);
            return conn.QueryAsync<Parametro> (builder.GetQuery(), builder.GetArgs());
        }

        public Task<IEnumerable<string>> BuscarTodosNomes()
        {
            var builder = _sqlBuilder.Select(new Parametro { Nome = "" });
            return conn.QueryAsync<string>(builder.GetQuery(), builder.GetArgs());
        }
    }
}