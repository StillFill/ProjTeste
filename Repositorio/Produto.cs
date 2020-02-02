using Dominio;
using System.Collections.Generic;

namespace Repositorio
{
    public interface IProdutoRepository
    {
        void Adicionar(Produto produto);
        void Salvar(Produto produto);
        IEnumerable<Produto> ListarTodos();
        Produto BuscarPor(int idProduto);
    }
}