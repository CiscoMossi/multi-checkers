using System;
using MultiCheckers.Api;
using Microsoft.AspNet.SignalR.Hubs;
using Moq;
using System.Dynamic;
using Microsoft.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dominio;
using Infra;
using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using MultiCheckers.Api.Models;

namespace MultiCheckers.Testes
{
    [TestClass]
    public class MyHubTest
    {
        public MultiCheckersContext context;
        public MyHubTest()
        {
            context = new MultiCheckersContext("MultiCheckersTest");
            CleanUp.LimparTabelas(context);
            context.Usuarios.Add(new Usuario("teste1", "testeeee@email.com", "12345"));
            context.Usuarios.Add(new Usuario("teste2", "teste2@email.com", "teste"));
            context.Usuarios.Add(new Usuario("teste3", "teste3@email.com", "teste"));
            context.Usuarios.Add(new Usuario("teste4", "teste4@email.com", "teste"));
            context.SaveChanges();

        }
        [TestMethod]
        public void CriarSala()
        {
            bool sendCalled = false;
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            var hub = new MyHub(context);
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
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            var hub = new MyHub(context);
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
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            var hub = new MyHub(context);
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
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            var hub = new MyHub(context);
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
        [TestMethod]
        public void Inserir_Usuario_Partida_Inexistente()
        {
            string login = "teste1";
            string hashSala = "";
            bool sendCalled = false;
            string resposta = null;
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            var hub = new MyHub(context);
            hub.Clients = mockClients.Object;
            dynamic all = new ExpandoObject();
            all.partidaInexistente = new Action<string>((mesagem) => {
                sendCalled = true;
                resposta = mesagem;
            });
            mockClients.Setup(m => m.Caller).Returns((ExpandoObject)all);
            hub.InserirUsuario(login, hashSala);
            Assert.IsTrue(sendCalled);
            Assert.IsNotNull(resposta);
        }
        [TestMethod]
        public void Inserir_Usuario_Partida_Existente()
        {
            var groupManagerMock = new Mock<IGroupManager>();
            var connectionId = Guid.NewGuid().ToString();
            var groupsJoined = new List<string>();
            groupManagerMock.Setup(g => g.Add(connectionId, It.IsAny<string>()))
                    .Returns(Task.FromResult<object>(null))
                    .Callback<string, string>((cid, groupToJoin) =>
                        groupsJoined.Add(groupToJoin));
            var hub = new MyHub(context);
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            hub.Clients = mockClients.Object;
            hub.Context = new HubCallerContext(request: null,
                                         connectionId: connectionId);
            hub.Groups = groupManagerMock.Object;
            dynamic all = new ExpandoObject();
            string login = "teste1";
            bool sendCalled = false;
            string resposta = null;
            string salaHashCriada = null;
            all.criarSala = new Action<string>((salaHash) => {
                sendCalled = true;
                salaHashCriada = salaHash;
            });
            mockClients.Setup(m => m.Caller).Returns((ExpandoObject)all);
            hub.CriarSala();
            all.infoJogador = new Action<string>((jogador) => {
                sendCalled = true;
                resposta = jogador;
            });
            mockClients.Setup(m => m.Caller).Returns((ExpandoObject)all);
            hub.InserirUsuario(login, salaHashCriada);
            Assert.IsTrue(sendCalled);
            Assert.IsNotNull(resposta);
            Assert.AreEqual("BRANCAS", resposta);

        }
        [TestMethod]
        public void Atualizar_Jogador()
        {
            var groupManagerMock = new Mock<IGroupManager>();
            var connectionId = Guid.NewGuid().ToString();
            var groupsJoined = new List<string>();
            groupManagerMock.Setup(g => g.Add(connectionId, It.IsAny<string>()))
                    .Returns(Task.FromResult<object>(null))
                    .Callback<string, string>((cid, groupToJoin) =>
                        groupsJoined.Add(groupToJoin));
            var hub = new MyHub(context);
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            hub.Clients = mockClients.Object;
            hub.Context = new HubCallerContext(request: null,
                                         connectionId: connectionId);
            hub.Groups = groupManagerMock.Object;
            dynamic all = new ExpandoObject();
            bool sendCalled1 = false;
            bool sendCalled2 = false;
            bool sendCalled3 = false;
            string resposta1 = null;
            string resposta2 = null;
            string resposta3 = null;
            string login = "teste1";
            string salaHashCriada = null;
            all.criarSala = new Action<string>((salaHash) => {
                salaHashCriada = salaHash;
            });
            mockClients.Setup(m => m.Caller).Returns((ExpandoObject)all);
            hub.CriarSala();
            all.infoJogador = new Action<string>((resposta) => {
                
            });
            hub.InserirUsuario(login, salaHashCriada);
            mockClients.Setup(m => m.Caller).Returns((ExpandoObject)all);
            all.infoJogador = new Action<string>((resposta) => {
                sendCalled1 = true;
                resposta1 = resposta;
            });
            all.ativaSom = new Action<string>((resposta) => {
                sendCalled2 = true;
                resposta2 = resposta;
            });
            all.alterarTabuleiro = new Action<string>((resposta) => {
                sendCalled3 = true;
                resposta3 = resposta;
            });
            mockClients.Setup(m => m.Client(connectionId)).Returns((ExpandoObject)all);
            JogadorModel jogador = new JogadorModel(connectionId, "BRANCAS");
            hub.AtualizarJogadores(jogador);
            Assert.IsTrue(sendCalled1);
            Assert.IsTrue(sendCalled2);
            Assert.IsTrue(sendCalled3);
        }
        [TestMethod]
        public void Finalizar_Jogo()
        {
            var groupManagerMock = new Mock<IGroupManager>();
            var connectionId = Guid.NewGuid().ToString();
            var groupsJoined = new List<string>();
            groupManagerMock.Setup(g => g.Add(connectionId, It.IsAny<string>()))
                    .Returns(Task.FromResult<object>(null))
                    .Callback<string, string>((cid, groupToJoin) =>
                        groupsJoined.Add(groupToJoin));
            var hub = new MyHub(context);
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            hub.Clients = mockClients.Object;
            hub.Context = new HubCallerContext(request: null,
                                         connectionId: connectionId);
            hub.Groups = groupManagerMock.Object;
            dynamic all = new ExpandoObject();
            string login = "teste4";
            string salaHashCriada = null;
            bool sendCalled = false;
            all.criarSala = new Action<string>((salaHash) => {
                salaHashCriada = salaHash;
            });
            mockClients.Setup(m => m.Caller).Returns((ExpandoObject)all);
            hub.CriarSala();
            all.infoJogador = new Action<string>((jogador) => {
            });
            mockClients.Setup(m => m.Caller).Returns((ExpandoObject)all);
            hub.InserirUsuario(login, salaHashCriada);
            string mensagem = null;
            var historico = new HistoricoModel();
            historico.LoginUsuario = "teste4";
            historico.PecasEliminadas = 12;
            historico.PecasRestantes = 10;
            historico.Venceu = true;
            all.ativaSom = new Action<string>((resposta) => {
                mensagem = resposta;
                sendCalled = true;
            });
            mockClients.Setup(m => m.Group(salaHashCriada)).Returns((ExpandoObject)all);
            hub.FinalizarJogo(historico);
            Assert.IsTrue(sendCalled);
            Assert.AreEqual("vitoria", mensagem);
        }
    }
}