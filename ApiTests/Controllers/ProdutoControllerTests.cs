using Microsoft.VisualStudio.TestTools.UnitTesting;
using Api.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework.Internal;
using Dominio.Models;
using Dominio.Business;

namespace Api.Controllers.Tests
{
    [TestClass()]
    public class ProdutoControllerTests
    {
        [TestMethod()]
        public void PostTest()
        {
            ParamProduto produto1 = new ParamProduto(1, "Produto1");
            ParamProduto produto2 = new ParamProduto(1, "Produto2");

            ParamProdutoBusiness ac = new ParamProdutoBusiness();

            string result = ac.ConcatenaNomes(produto1.Nome, produto2.Nome);

            Assert.AreEqual(result, "Produto1Produto2");
        }
    }
}