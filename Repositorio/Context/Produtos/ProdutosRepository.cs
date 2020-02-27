// cs0834.cs
using System;
using Dominio.Interfaces;
using Repositorio.Common;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Models;
using Dapper;
using Microsoft.Extensions.Configuration;

using Repository.Context;
using Repository.Mappings;
using Dapper.Extensions.Linq.Extensions;
using Dominio.Models;
using DapperExtensions;

namespace AspnetCore.EFCore_Dapper.Data.Repositories.Dapper
{
    public class ProdutosRepository : RepositoryBase<Produto>, IProdutosRepository
    {
        SqlBuilder<Produto, ProdutosMap> _sqlBuilder;
        public ProdutosRepository(IConfiguration configuration) : base(configuration) {
            _sqlBuilder = new SqlBuilder<Produto, ProdutosMap>(conn);
        }

        public IEnumerable<Produto> BuscarPorIdProduto(int id) 
        {
            var builder = _sqlBuilder.Select()
                                .Innerjoin<ParametrosMap>()
                                .On<Parametro>(a => a.Id, b => b.IdProduto)
                                .Innerjoin<GruposMap>()
                                .On<Grupo>(a => a.Id, b => b.IdProduto);

            string sql = builder.GetQuery();

            return conn.Query<Produto, Parametro, Grupo, Produto>(builder.GetQuery(), param: builder.GetArgs(), map: (prod, param, grupo) => {
                return prod;
            });
        }

        public int DesativarProduto(int id)
        {
            var builder = _sqlBuilder.Update()
                                .Set(new Produto { DataRemocao = DateTime.Now })
                                .Where(x => x.IdProduto, id);

            return conn.Execute(builder.GetQuery(), builder.GetArgs());
        }
    }
}