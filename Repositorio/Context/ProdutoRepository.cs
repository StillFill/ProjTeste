using Dominio;
using Microsoft.Extensions.Configuration;
using Repositorio;
using Repositorio.Common;

namespace AspnetCore.EFCore_Dapper.Data.Repositories.Dapper
{
    public class ProdutoRepository : RepositoryBase<Produto>, IProdutoRepository
    {
        public ProdutoRepository(IConfiguration configuration) : base(configuration) { }
    }
}