using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces.Produtos
{
    public interface ITipoProdutoService
    {
        IEnumerable<TipoProduto> BuscarTiposProdutos();
    }
}
