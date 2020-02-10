using Dominio.Models;
using Microsoft.Extensions.Configuration;
using Repositorio.Common;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dapper;
using Domain.Models.Ativos;
using Domain.Interfaces;

namespace AspnetCore.EFCore_Dapper.Data.Repositories.Dapper
{
    public class AtivosRepository : RepositoryBase<Ativos>, IAtivosRepository
    {
        public AtivosRepository(IConfiguration configuration) : base(configuration) {}

        public Task<Ativos> BuscarPorIdProduto(int id)
        {
            string sql = "SELECT * FROM Ativos WHERE idAtivo = @id";
            return conn.QueryFirstAsync<Ativos>(sql, id);
        }
    }
}