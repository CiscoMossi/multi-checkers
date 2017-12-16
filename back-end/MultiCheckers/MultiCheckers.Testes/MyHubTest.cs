using System;
using MultiCheckers.Api;
using Microsoft.AspNet.SignalR.Hubs;
using Moq;
using System.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MultiCheckers.Testes
{
    [TestClass]
    public class MyHubTest
    {
        public interface IClientContract
        {
            string criarSala(string name);
            void isConnect(bool isConnect);
            void infoJogador(string info);
        }
        [TestMethod]
        public void CriarSala()
        {
            var hub = new MyHub();
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            var all = new Mock<IClientContract>();
            hub.Clients = mockClients.Object;
            all.Setup(m => m.criarSala(It.IsAny<string>())).Verifiable();
            mockClients.Setup(m => m.Caller).Returns(all.Object);
            hub.CriarSala();
            all.VerifyAll();
        }
        [TestMethod]
        public void IsConnect()
        {
            var hub = new MyHub();
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            var all = new Mock<IClientContract>();
            hub.Clients = mockClients.Object;
            all.Setup(m => m.isConnect(It.IsAny<bool>())).Verifiable();
            mockClients.Setup(m => m.Caller).Returns(all.Object);
            hub.OnConnected();
            all.VerifyAll();
        }
        [TestMethod]
        public void Consultar_Partida_Inexistente()
        {
            var hub = new MyHub();
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            var all = new Mock<IClientContract>();
            hub.Clients = mockClients.Object;
            all.Setup(m => m.infoJogador(It.IsAny<string>())).Verifiable();
            mockClients.Setup(m => m.Caller).Returns(all.Object);
            hub.Consultar("");
            all.Verify();
        }
    }
}