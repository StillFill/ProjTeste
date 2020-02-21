using Dommel;
using Dapper.FluentMap.Dommel.Mapping;
using Dominio.Models;

namespace Repository.Mappings
{
    public class GruposMap : DommelEntityMap<Grupo>
    {
        public GruposMap()
        {
            ToTable("Grupos");
            Map(i => i.Id).ToColumn("ID_GRUP").IsKey().IsIdentity();
            Map(i => i.Nome).ToColumn("NAME_GRUP");
            Map(i => i.IdProduto).ToColumn("PROD_ID");
        }
    }
}
