using System;
using MultiCheckers.Api;
using Microsoft.AspNet.SignalR.Hubs;
using Moq;
using System.Dynamic;
using Microsoft.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dominio;

namespace MultiCheckers.Testes
{
    [TestClass]
    public class MyHubTest
    {
        [TestMethod]
        public void CriarSala()
        {
            bool sendCalled = false;
            var hub = new MyHub();
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            hub.Clients = mockClients.Object;
            dynamic all = new ExpandoObject();
            string salaHashCriada = null;
            all.criarSala = new Action<string>((salaHash) => {
                sendCalled = true;
                salaHashCriada = salaHash;
            });
            mockClients.Setup(m => m.Caller).Returns((ExpandoObject)all);
            hub.CriarSala();
            Assert.IsTrue(sendCalled);
            Assert.IsNotNull(salaHashCriada);
        }
       [TestMethod]
       public void IsConnect()
       {
            bool sendCalled = false;
            var hub = new MyHub();
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            hub.Clients = mockClients.Object;
            dynamic all = new ExpandoObject();
            bool connected = false;
            all.isConnect = new Action<bool>((connect) => {
                sendCalled = true;
                connected = connect;
            });
            mockClients.Setup(m => m.Caller).Returns((ExpandoObject)all);
            hub.OnConnected();
            Assert.IsTrue(sendCalled);
            Assert.IsTrue(connected);
       }
        [TestMethod]
        public void Consultar_Partida_Inexistente()
        {
            bool sendCalled = false;
            var hub = new MyHub();
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            hub.Clients = mockClients.Object;
            dynamic all = new ExpandoObject();
            string mensagemPartidaInexistente = null;
            all.partidaInexistente = new Action<string>((mensagem) => {
                sendCalled = true;
                mensagemPartidaInexistente = mensagem;
            });
            mockClients.Setup(m => m.Caller).Returns((ExpandoObject)all);
            hub.Consultar("");
            Assert.IsTrue(sendCalled);
            Assert.AreEqual("Esta partida não existe.", mensagemPartidaInexistente);
        }
        [TestMethod]
        public void Consultar_Partida_Existente()
        {
            bool sendCalled = false;
            var hub = new MyHub();
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            hub.Clients = mockClients.Object;
            dynamic all = new ExpandoObject();
            string salaHashCriada = null;
            all.criarSala = new Action<string>((salaHash) => {
                sendCalled = true;
                salaHashCriada = salaHash;
            });
            mockClients.Setup(m => m.Caller).Returns((ExpandoObject)all);
            hub.CriarSala();
            Partida partida = null;
            all.buscarJogo = new Action<Partida>((mensagem) => {
                sendCalled = true;
                partida = mensagem;
            });
            mockClients.Setup(m => m.Group(salaHashCriada)).Returns((ExpandoObject)all);
            hub.Consultar(salaHashCriada);
            Assert.IsTrue(sendCalled);
            Assert.IsNotNull(partida);
        }
    }
}