﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Produtos
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string DataRemocao { get; set; }

        public int IdProduto { get; set; }

        public Produtos(string Nome, int IdProduto)
        {
            this.Nome = Nome;
            this.IdProduto = IdProduto;
        }

        public Produtos()
        {

        }
    }
}