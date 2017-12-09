using Dominio;
using Dominio.Entidades;
using MultiCheckers.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class TabuleiroController : BaseController
    {
        private static TabuleiroRepository tabuleiroRepository = new TabuleiroRepository();

        [HttpGet]
        public HttpResponseMessage Consultar()
        {
            Tabuleiro tabuleiro = tabuleiroRepository.ObterTabuleiro();

            if (tabuleiro.JogoFinalizado)
                return ResponderOK(String.Concat("Jogo Finalizado. ", tabuleiro.CorTurnoAtual.ToString(), "s venceram."));

            return ResponderOK(tabuleiro);
        }

        [HttpPost]
        public HttpResponseMessage Atualizar([FromBody] Jogada jogada, int cor)
        {
            Tabuleiro tabuleiro = tabuleiroRepository.ObterTabuleiro();
            int numPecas = tabuleiro.Pecas.Count;

            if ((Cor) cor != tabuleiro.CorTurnoAtual)
                return ResponderErro("Turno do adversário");

            if(!tabuleiro.AtualizarJogada(jogada))
                return ResponderErro("Jogada inválida");

            if (tabuleiro.Pecas.Count < numPecas)
                tabuleiro.AplicarRodadaBonus(jogada);
            else
                tabuleiro.PercorrerTabuleiro(tabuleiro.CorTurnoAtual);

            tabuleiroRepository.EditarTabuleiro(tabuleiro);

            if (tabuleiro.ValidarFimJogo())
                return ResponderOK("Você venceu!");

            return ResponderOK(tabuleiro.CorTurnoAtual);
        }
    }
}
