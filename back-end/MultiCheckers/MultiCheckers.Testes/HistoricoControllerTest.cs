using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiCheckers.Api.Controllers;
using Infra;
using System.Web.Http.Results;

namespace MultiCheckers.Testes
{
    [TestClass]
    public class HistoricoControllerTest
    {
        [TestMethod]
        public void Obter_Pontos_Usuario()
        {
            //Arrange
            MultiCheckersContext contexto = new MultiCheckersContext();
            HistoricoController controller = new HistoricoController(contexto);
            //Act
            var result = controller.ObterPontosDoUsuario(1);
            //Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void Obter_Historico()
        {
            //Arrange
            MultiCheckersContext contexto = new MultiCheckersContext();
            HistoricoController controller = new HistoricoController(contexto);
            //Act
            var result = controller.Listar(1);
            //Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void Nao_Obter_Historico()
        {
            //Arrange
            MultiCheckersContext contexto = new MultiCheckersContext();
            HistoricoController controller = new HistoricoController(contexto);
            //Act
            var result = controller.Listar(214748364);
            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }
    }
}
