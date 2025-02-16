using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinimalAPI.Dominio.Servicos;
using MinimalAPI.Dominio.Entidades;
using MinimalAPI.Infraestrutura.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using MinimalAPI.DTOs;

namespace Test.Domain.Servico
{
    [TestClass]
    public class VeiculoServicoTest
    {
        private DbContexto CriarContextoDeTeste()
        {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var path = Path.GetFullPath(Path.Combine(assemblyPath ?? "", "..", "..", ".."));

            var builder = new ConfigurationBuilder()
                .SetBasePath(path ?? Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            var configuration = builder.Build();

            return new DbContexto(configuration);
        }
        [TestMethod]
        public void TestandoSalvarVeiculo()
        {
            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

            var veiculo = new Veiculo();
            veiculo.Id = 1;
            veiculo.Nome = "Fiesta";
            veiculo.Marca = "Ford";
            veiculo.Ano = 2013;
           

            var veiculoServico = new VeiculoServicos(context);

            // Act
            veiculoServico.Incluir(veiculo);

            // Assert
            Assert.AreEqual(1, veiculoServico.Todos(1).Count());
        }

        [TestMethod]
        public void TestandoBuscaPorId()
        {
            // Arrange
            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

            var veiculo = new Veiculo();
            veiculo.Id = 1;
            veiculo.Nome = "Fiesta";
            veiculo.Marca = "Ford";
            veiculo.Ano = 2013;
           

            var veiculoServico = new VeiculoServicos(context);

            // Act
            veiculoServico.Incluir(veiculo);
            var veiculoDoBanco = veiculoServico.BuscaPorId(veiculo.Id);

            // Assert
            Assert.AreEqual(1, veiculoDoBanco?.Id);
        }
        
        [TestMethod]
        public void TestandoAtualizar()
        {
            // Arrange
            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

            var veiculo = new Veiculo();
            veiculo.Id = 1;
            veiculo.Nome = "Fiesta";
            veiculo.Marca = "Ford";
            veiculo.Ano = 2013;

            var veiculoServico = new VeiculoServicos(context);

            // Adiciona ao banco
            veiculoServico.Incluir(veiculo);

            // Recarrega a entidade do banco
             var veiculoDoBanco = veiculoServico.BuscaPorId(veiculo.Id);
            
            // Atualiza os dados no mesmo objeto
            veiculoDoBanco.Ano = 2015;

            // Act
            veiculoServico.Atualizar(veiculoDoBanco);

            // Recarrega o veículo atualizado
            var veiculoAtualizado = veiculoServico.BuscaPorId(veiculo.Id);

            // Assert
            Assert.AreEqual(2015, veiculoAtualizado.Ano);
        }

        [TestMethod]
        public void TestandoApagar()
        {
            // Arrange
            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

            var veiculo = new Veiculo();
            veiculo.Id = 1;
            veiculo.Nome = "Fiesta";
            veiculo.Marca = "Ford";
            veiculo.Ano = 2013;
           

            var veiculoServico = new VeiculoServicos(context);

            // Act
            veiculoServico.Incluir(veiculo);
            veiculoServico.Apagar(veiculo);

            // Assert
            Assert.AreEqual(0, veiculoServico.Todos(1).Count());
        }
        
    }
}