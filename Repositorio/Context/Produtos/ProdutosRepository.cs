﻿using Dommel;
using Dominio.Interfaces;
using Dominio.Models;
using Microsoft.Extensions.Configuration;
using Repositorio.Common;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dapper;
using Domain.Models;

namespace AspnetCore.EFCore_Dapper.Data.Repositories.Dapper
{
    public class ProdutosRepository : RepositoryBase<Produtos>, IProdutosRepository
    {
        public ProdutosRepository(IConfiguration configuration) : base(configuration) { }

        public Task<IEnumerable<Produtos>> BuscarPorIdProduto(int id) {
            string sql = "SELECT * FROM Produtos WHERE IdProduto='" + id + "'";
            return conn.QueryAsync<Produtos>(sql);
        }
    }
}