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
            Map(i => i.Id).ToColumn("Id").IsKey().IsIdentity();
            Map(i => i.Nome).ToColumn("Nome");
            Map(i => i.Grupo).ToColumn("Grupo");
            Map(i => i.IdProduto).ToColumn("IdProduto");
        }
    }
}
