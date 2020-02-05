using System;

namespace Dominio.Models
{
    public class Parametros
    {

        // Propriedades
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Grupo { get; set; }
        public int IdProduto { get; set; }

        public Parametros(int Id, string Nome, string Grupo, int IdProduto)
        {
            this.Id = Id;
            this.Nome = Nome;
            this.Grupo = Grupo;
            this.IdProduto = IdProduto;
        }

        public Parametros()
        {

        }
    }
}