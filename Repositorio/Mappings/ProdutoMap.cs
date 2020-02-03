using Dommel;
using Dominio.Models;
using Dapper.FluentMap.Dommel.Mapping;

public class ProdutoMap : DommelEntityMap<Produto>
{
    public ProdutoMap()
    {
        ToTable("Produtos");
        Map(i => i.Id_Produto).ToColumn("id").IsKey();
        Map(i => i.Nome).ToColumn("nome");
    }
}