using Dommel;
using Domain.Models;
using Dapper.FluentMap.Dommel.Mapping;

namespace Repository.Mappings
{
    public class ProdutosMap : DommelEntityMap<Produtos>
    {
        public ProdutosMap()
        {
            ToTable("Produtos");
            Map(i => i.Id).ToColumn("Id").IsKey().IsIdentity();
            Map(i => i.Nome).ToColumn("Nome");
            Map(i => i.DataRemocao).ToColumn("DataRemocao");
            Map(i => i.IdProduto).ToColumn("IdProduto");
        }
    }
}
