using Dominio.Models;
using System.Collections.Generic;

namespace Dominio
{
    public class Categoria
    {
        // Propriedades
        public int Id_Categoria { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public List<Produto> Produtos { get; set; }
    }
}