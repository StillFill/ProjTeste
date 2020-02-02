
using Dominio;
using System.Collections.Generic;

namespace Repositório
{
    public interface ICategoriaRepository
    {
        void Adicionar(Categoria categoria);
        void Salvar(Categoria categoria);
        IEnumerable<Categoria> ListarTodos();
        Categoria BuscarPor(int idCategoria);
    }
}
