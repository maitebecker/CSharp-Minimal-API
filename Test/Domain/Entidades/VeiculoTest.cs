using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinimalAPI.Dominio.Entidades;

namespace Test.Domain.Entidades
{
    [TestClass]
    public class VeiculoTest
    {
        [TestMethod]
        public void TestarGetSetPropriedades()
        {
            var veiculo = new Veiculo();

            veiculo.Id = 1;
            veiculo.Nome = "Fiesta";
            veiculo.Marca = "Ford";
            veiculo.Ano = 2013;

            Assert.AreEqual(1, veiculo.Id);
            Assert.AreEqual("Fiesta", veiculo.Nome);
            Assert.AreEqual("Ford", veiculo.Marca);
            Assert.AreEqual(2013, veiculo.Ano);
        }
    }
}