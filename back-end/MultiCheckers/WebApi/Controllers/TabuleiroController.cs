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
        private static Cor COR_ATUAL = Cor.BRANCA;
        private static TabuleiroRepository tabuleiroRepository = new TabuleiroRepository();

        [HttpGet]
        public HttpResponseMessage Consultar()
        {
            Tabuleiro tabuleiro = tabuleiroRepository.ObterTabuleiro();
            Cor cor = COR_ATUAL;

            Object resposta = new { tabuleiro, cor };
            return ResponderOK(resposta);
        }

        [HttpPost]
        public HttpResponseMessage Atualizar([FromBody] Tabuleiro novoTabuleiro, int cor)
        {
            if ((Cor) cor != COR_ATUAL)
                return ResponderErro("Turno do adversário");

            Tabuleiro tabuleiro = tabuleiroRepository.ObterTabuleiro();
            if (tabuleiro.Validar(novoTabuleiro))
                return ResponderErro("Apenas uma peça pode ser movimentada por jogada");

            tabuleiro.PercorrerTabuleiro((Cor) cor);
            tabuleiroRepository.EditarTabuleiro(novoTabuleiro);

            COR_ATUAL = (COR_ATUAL == Cor.BRANCA ? Cor.PRETA : Cor.BRANCA);

            return ResponderOK(novoTabuleiro);
        }
    }
}
