using Dominio;
using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiCheckers.Repositorios
{
    public class TabuleiroRepository
    {
        private static Tabuleiro TABULEIRO_ATUAL;

        public TabuleiroRepository()
        {
            TABULEIRO_ATUAL = new Tabuleiro();
            TABULEIRO_ATUAL.PosicionarInicioPartida();
            TABULEIRO_ATUAL.PercorrerTabuleiro(Cor.BRANCA);
        }

        public Tabuleiro ObterTabuleiro()
        {
            return TABULEIRO_ATUAL;
        }

        public void EditarTabuleiro(Tabuleiro novoTabuleiro)
        {
            TABULEIRO_ATUAL = novoTabuleiro;
        }
    }
}
