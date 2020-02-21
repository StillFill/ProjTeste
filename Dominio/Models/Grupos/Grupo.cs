using System;

namespace Dominio.Models
{
    public class Grupo
    {

        // Propriedades
        public int Id { get; set; }
        public string Nome { get; set; }
        public int IdProduto { get; set; }

        public Grupo(int Id, string Nome, int IdProduto)
        {
            this.Id = Id;
            this.Nome = Nome;
            this.IdProduto = IdProduto;
        }

        public Grupo()
        {

        }
    }
}