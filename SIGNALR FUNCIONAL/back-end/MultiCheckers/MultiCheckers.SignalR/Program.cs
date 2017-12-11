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
        private static PartidaRepository partidaRepository = new PartidaRepository();

        public override Task OnConnected()
        {
            this.Consultar();
            InserirUsuario();
            return base.OnConnected();
        }

        public void Consultar()
        {
            Partida partida = partidaRepository.ObterPartida();

            Clients.All.buscarJogo(partida);
            if (partida.PartidaFinalizada)
            {
                Clients.All.fimJogo(String.Concat("Jogo Finalizado. ",
                                      (partida.Tabuleiro.CorTurnoAtual == Cor.BRANCA ? Cor.PRETA : Cor.BRANCA).ToString(),
                                       "S venceram."));
                return;
            }           
        }
        
        public void Atualizar(Jogada jogada, int cor)
        {
            Partida partida = partidaRepository.ObterPartida();
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

        public void InserirUsuario()
        {
            Usuario usuario = new Usuario("Hoffmann", "bruno.siqueira.hoffmann@gmail.com", "senha");
            Partida partida = partidaRepository.ObterPartida();

            partida.InserirJogadorPretas(usuario);
        }
    }
}
