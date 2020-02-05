using System;
using Dominio.Interfaces;
using Microsoft.Extensions.Configuration;
using Repositorio.Common;
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
            string sql = "SELECT * FROM Produtos WHERE IdProduto='" + id + "' AND DataRemocao IS NULL";
            return conn.QueryAsync<Produtos>(sql);
        }

        public Task<int> AdicionarProduto(Produtos obj)
        {
            string sql = "INSERT INTO Produtos values('"+ obj.Nome +"', '"+ obj.DataRemocao + "', " + obj.IdProduto + ")";
            return conn.ExecuteAsync(sql);
        }

        public Task<int> DesativarProduto(int id)
        {
            string sql = "UPDATE Produtos SET DataRemocao='" + DateTime.Now + "' WHERE Id=" + id;
            return conn.ExecuteAsync(sql);
        }
    }
}