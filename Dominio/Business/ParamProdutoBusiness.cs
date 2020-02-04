using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Business
{
    public class ParamProdutoBusiness
    {
        public string ConcatenaNomes(string nome1, string nome2) {

            if (nome1.Length > 20)
            {
                return nome1;
            }

            return nome1 + nome2;
        }
    }
}
