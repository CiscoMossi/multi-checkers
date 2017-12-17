using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiCheckers.Api.Controllers;
using Infra;
using System.Web.Http.Results;
using Dominio;
using MultiCheckers.Api.Models;

namespace MultiCheckers.Testes
{
    [TestClass]
    public class UsuarioControllerTest
    {
        [TestMethod]
        public void Obter_Usuario()
        {
            //Arrange
            MultiCheckersContext contexto = new MultiCheckersContext();
            UsuarioController controller = new UsuarioController(contexto);
            //Act
            var result = controller.ObterUsuario(1);
            //Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void Nao_Obter_Usuario_Inexistente()
        {
            //Arrange
            MultiCheckersContext contexto = new MultiCheckersContext();
            UsuarioController controller = new UsuarioController(contexto);
            //Act
            var result = controller.ObterUsuario(0);
            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void Nao_Criar_Usuario()
        {
            //Arrange
            MultiCheckersContext contexto = new MultiCheckersContext();
            UsuarioController controller = new UsuarioController(contexto);
            UsuarioModel model = new UsuarioModel();
            //Act
            var result = controller.AdicionarUsuario(model);
            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void Criar_Usuario()
        {
            //Arrange
            MultiCheckersContext contexto = new MultiCheckersContext();
            UsuarioController controller = new UsuarioController(contexto);
            UsuarioModel model = new UsuarioModel();
            model.Email = "@@@";
            model.Login = "Login";
            model.Senha = "senha";
            //Act
            var result = controller.AdicionarUsuario(model);
            //Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void Nao_Obter_Usuario_Logado()
        {
            //Arrange
            MultiCheckersContext contexto = new MultiCheckersContext();
            UsuarioController controller = new UsuarioController(contexto);
            //Act
            var result = controller.Obter();
            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }
    }
}
