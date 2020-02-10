using Domain.Interfaces.Produtos;
using Domain.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Services
{
    public class TipoProdutoService : ITipoProdutoService
    {
        public IEnumerable<TipoProduto> BuscarTiposProdutos()
        {
            /** 
                var client = new RestClient($"http://api.football-data.org/v1/competitions/{id}/leagueTable");
                var request = new RestRequest(Method.GET);
                IRestResponse response = await client.ExecuteAsync(request);
            */

            return JsonConvert
                    .DeserializeObject<IEnumerable<TipoProduto>> ("[{ \"idProduto\": 1, \"nome\": \"Debenture\" }]");
        }
    }
}
