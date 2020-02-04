using Dommel;
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
    public class ParamProdutoRepository : RepositoryBase<ParamProduto>, IParamProdutoRepository
    {
        public ParamProdutoRepository(IConfiguration configuration) : base(configuration) {}

        public Task<IEnumerable<ParamProduto>> BuscarPorIdProduto(object id)
        {
            string sql = "SELECT * FROM Produtos WHERE IdProduto='111'";
            return conn.QueryAsync<ParamProduto> (sql);
        }
    }
}