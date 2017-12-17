﻿using System;
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
        public MultiCheckersContext contexto;
        public Usuario usuario;
        public UsuarioControllerTest()
        {
            contexto = new MultiCheckersContext("MultiCheckersTest");
            CleanUp.LimparTabelas(contexto);
            usuario = contexto.Usuarios.Add(new Usuario("teste", "teste@email.com", "teste"));
            contexto.SaveChanges();
        }
        [TestMethod]
        public void Obter_Usuario()
        {
            //Arrange
            UsuarioController controller = new UsuarioController(contexto);
            //Act
            var result = controller.ObterUsuario(usuario.Id);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotInstanceOfType(result.GetType(), typeof(OkResult));
        }
        [TestMethod]
        public void Nao_Obter_Usuario_Inexistente()
        {
            //Arrange
            UsuarioController controller = new UsuarioController(contexto);
            //Act
            var result = controller.ObterUsuario(1000000);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotInstanceOfType(result.GetType(), typeof(OkResult));
        }
        [TestMethod]
        public void Nao_Obter_Usuario_Logado()
        {
            //Arrange
            UsuarioController controller = new UsuarioController(contexto);
            //Act
            var result = controller.Obter();
            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotInstanceOfType(result.GetType(), typeof(OkResult));
        }
    }
}