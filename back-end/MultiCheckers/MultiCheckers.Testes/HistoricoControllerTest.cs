using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiCheckers.Api.Controllers;
using Infra;
using System.Web.Http.Results;
using Dominio;

namespace MultiCheckers.Testes
{
    [TestClass]
    public class HistoricoControllerTest
    {
        public MultiCheckersContext contexto;
        public HistoricoControllerTest()
        {
            contexto = new MultiCheckersContext("MultiCheckersTest");
            CleanUp.LimparTabelas(contexto);
            Usuario usuario = contexto.Usuarios.Add(new Usuario("teste", "teste@email.com", "teste"));
            contexto.Historicos.Add(new Historico(usuario, true, 10, 12));
            contexto.SaveChanges();
        }
        [TestMethod]
        public void Obter_Pontos_Usuario()
        {
            //Arrange
            HistoricoController controller = new HistoricoController(contexto);
            //Act
            var result = controller.ObterPontosDoUsuario(1);
            //Assert
            Assert.IsNotInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void Obter_Historico()
        {
            //Arrange
            HistoricoController controller = new HistoricoController(contexto);
            //Act
            var result = controller.Listar(1);
            //Assert
            Assert.IsNotInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void Nao_Obter_Historico()
        {
            //Arrange
            HistoricoController controller = new HistoricoController(contexto);
            contexto.Historicos.RemoveRange(contexto.Historicos);
            contexto.SaveChanges();
            //Act
            var result = controller.Listar(214748364);
            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
        }
    }
}
