using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime? DataRemocao { get; set; }

        public int IdProduto { get; set; }

        public int Versao { get; set; }

        public Produto(string nome, int idProduto, int versao)
        {
            this.Nome = nome;
            this.IdProduto = idProduto;
            this.DataRemocao = null;
            this.Versao = versao;
        }

        public Produto()
        {

        }
    }
}
