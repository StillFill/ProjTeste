﻿using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IProdutosRepository : IRepositoryBase<Produtos>
    {
        public Task<IEnumerable<Produtos>> BuscarPorIdProduto(int id);
    }
}