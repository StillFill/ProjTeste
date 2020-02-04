using System;

namespace Dominio.Models
{
    public class ParamProduto
    {

        // Propriedades
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Grupo { get; set; }
        public string IdProduto { get; set; }

        public ParamProduto(int Id, string Nome, string Grupo, string IdProduto)
        {
            this.Id = Id;
            this.Nome = Nome;
            this.Grupo = Grupo;
            this.IdProduto = IdProduto;
        }

        public ParamProduto()
        {

        }
    }
}