using Dommel;
using Dapper.FluentMap.Dommel.Mapping;
using Dominio.Models;

namespace Repository.Mappings
{
    public class ParametrosMap : DommelEntityMap<Parametro>
    {
        public ParametrosMap()
        {
            ToTable("Parametros");
            Map(i => i.Id).ToColumn("ID_PARAM").IsKey().IsIdentity();
            Map(i => i.Nome).ToColumn("NAME_PARAM");
            Map(i => i.Grupo).ToColumn("GRUPO_PARAM");
            Map(i => i.IdProduto).ToColumn("ID_PROD");
        }
    }
}
