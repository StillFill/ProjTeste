using Dominio.Interfaces;
using Dominio.Models;
using Microsoft.Extensions.Configuration;
using Repositorio.Common;

namespace AspnetCore.EFCore_Dapper.Data.Repositories.Dapper
{
    public class ProdutoRepository : RepositoryBase<Produto>, IProdutoRepository
    {
        public ProdutoRepository(IConfiguration configuration) : base(configuration) { }
    }
}