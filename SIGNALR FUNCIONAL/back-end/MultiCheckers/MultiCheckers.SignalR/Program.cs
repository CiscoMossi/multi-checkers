using Dominio;
using Dominio.Entidades;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using MultiCheckers.Repositorios;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiCheckers.SignalR
{
    class Program
    {
        static void Main(string[] args)
        {
            // This will *ONLY* bind to localhost, if you want to bind to all addresses
            // use http://*:8080 to bind to all addresses. 
            // See http://msdn.microsoft.com/en-us/library/system.net.httplistener.aspx 
            // for more information.
            string url = "http://localhost:9090";
            using (WebApp.Start(url))
            {
                Console.WriteLine("Server running on {0}", url);
                Console.ReadLine();
            }
        }
    }

    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }

    [HubName("HubMessage")]
    public class MyHub : Hub
    {
        private static Dictionary<string, Partida> SALAS = new Dictionary<string, Partida>();
        private static List<Usuario> USUARIOS = new List<Usuario>();

        public override Task OnConnected()
        {
            USUARIOS.Add(new Usuario("CheckersKing", "email@email.com", "senha"));
            USUARIOS.Add(new Usuario("Mr_Winner", "winner@email.com", "senha"));

            return base.OnConnected();
        }

        public void CriarSala(string login)
        {
            Usuario jogadorBrancas = USUARIOS.FirstOrDefault(u => u.Login == login);
            Partida partida = new Partida(jogadorBrancas);
            GeradorUrl gerador = new GeradorUrl();

            string salaHash = gerador.GerarUrl();
            SALAS.Add(salaHash, partida);

            Clients.Caller.criarSala(salaHash);
        }

        public void Consultar(string salaHash)
        {
            Partida partida = SALAS.FirstOrDefault(s => s.Key == salaHash).Value;

            Clients.All.buscarJogo(partida);
            if (partida.PartidaFinalizada)
            {
                Clients.All.fimJogo(String.Concat("Jogo Finalizado. ",
                                      (partida.Tabuleiro.CorTurnoAtual == Cor.BRANCA ? Cor.PRETA : Cor.BRANCA).ToString(),
                                       "S venceram."));
                return;
            }           
        }
        
        public void Atualizar(Jogada jogada, int cor, string salaHash)
        {
            Partida partida = SALAS.FirstOrDefault(s => s.Key == salaHash).Value;
            Tabuleiro tabuleiro = partida.Tabuleiro;
            int numPecas = tabuleiro.Pecas.Count;

            if ((Cor)cor != tabuleiro.CorTurnoAtual)
                Clients.All.alterarTabuleiro("Turno do adversário");

            if (!tabuleiro.AtualizarJogada(jogada))
                Clients.All.alterarTabuleiro("Jogada inválida");

            if (tabuleiro.Pecas.Count < numPecas)
                tabuleiro.AplicarRodadaBonus(jogada);
            else
                tabuleiro.PercorrerTabuleiro(tabuleiro.CorTurnoAtual);

            partida.EditarTabuleiro(tabuleiro);

            if (partida.ValidarFimJogo(tabuleiro.CorTurnoAtual))
                Clients.All.alterarTabuleiro("Você venceu!");

            Clients.All.alterarTabuleiro(tabuleiro.CorTurnoAtual.ToString());
        }

        public void InserirUsuario(string login, string salaHash)
        {
            Usuario usuario = USUARIOS.FirstOrDefault(u => u.Login == login);
            Partida partida = SALAS.FirstOrDefault(s => s.Key == salaHash).Value;
            partida.InserirJogadorPretas(usuario);
        }
    }
}
