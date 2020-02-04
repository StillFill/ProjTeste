﻿using Domain.Interfaces.Produtos;
using Domain.Models.Produtos;
using System.Collections.Generic;
using System.Text.Json;

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

            return JsonSerializer
                    .Deserialize<IEnumerable<TipoProduto>> ("[{ \"idProduto\": 1, \"nome\": \"Debenture\" }]");
        }
    }
}
