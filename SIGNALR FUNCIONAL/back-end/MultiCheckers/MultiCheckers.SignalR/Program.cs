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
            string url = "http://192.168.0.101:8082";
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
        private static TabuleiroRepository tabuleiroRepository = new TabuleiroRepository();

        public void Consultar()
        {
            Tabuleiro tabuleiro = tabuleiroRepository.ObterTabuleiro();

            //if (tabuleiro.JogoFinalizado)
            //    Clients.All.buscarJogo(String.Concat("Jogo Finalizado. ",
            //                          (tabuleiro.CorTurnoAtual == Cor.BRANCA ? Cor.PRETA : Cor.BRANCA).ToString(),
            //                           "S venceram."), tabuleiro);

            Clients.All.buscarJogo(tabuleiro);
        }

        public void Atualizar(Jogada jogada, int cor)
        {
            Tabuleiro tabuleiro = tabuleiroRepository.ObterTabuleiro();
            int numPecas = tabuleiro.Pecas.Count;

            if ((Cor)cor != tabuleiro.CorTurnoAtual)
                Clients.All.alterarTabuleiro("Turno do adversário");

            if (!tabuleiro.AtualizarJogada(jogada))
                Clients.All.alterarTabuleiro("Jogada inválida");

            if (tabuleiro.Pecas.Count < numPecas)
                tabuleiro.AplicarRodadaBonus(jogada);
            else
                tabuleiro.PercorrerTabuleiro(tabuleiro.CorTurnoAtual);

            tabuleiroRepository.EditarTabuleiro(tabuleiro);

            if (tabuleiro.ValidarFimJogo())
                Clients.All.alterarTabuleiro("Você venceu!");

            Clients.All.alterarTabuleiro(tabuleiro.CorTurnoAtual.ToString());
        }
    }
}
