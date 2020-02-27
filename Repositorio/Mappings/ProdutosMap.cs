using Dommel;
using Domain.Models;
using Dapper.FluentMap.Dommel.Mapping;

namespace Repository.Mappings
{
    public class ProdutosMap : DommelEntityMap<Produto>, IDommelEntityMap
    {
        public ProdutosMap()
        {
            ToTable("Produtos");
            Map(i => i.Id).ToColumn("ID_PRODUT").IsKey().IsIdentity();
            Map(i => i.Nome).ToColumn("NAME_PROD");
            Map(i => i.DataRemocao).ToColumn("REMOC_DATE");
            Map(i => i.IdProduto).ToColumn("ID_PROD_EXT");
            Map(i => i.Versao).ToColumn("VERSAO");
        }
    }
}
