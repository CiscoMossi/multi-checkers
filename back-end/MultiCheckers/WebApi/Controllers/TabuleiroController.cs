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
        public HttpResponseMessage Consultar() => ResponderOK(tabuleiroRepository.ObterTabuleiro());

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

            return ResponderOK(tabuleiro.CorTurnoAtual);
        }
    }
}
