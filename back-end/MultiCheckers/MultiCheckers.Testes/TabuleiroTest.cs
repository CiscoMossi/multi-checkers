using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dominio;
using System.Drawing;
using Dominio.Entidades;

namespace MultiCheckers.Testes
{
    [TestClass]
    public class TabuleiroTest
    {
        [TestMethod]
        public void Criar_Tabuleiro()
        {
            Tabuleiro tabuleiro = new Tabuleiro();

            Assert.AreEqual(tabuleiro.Pecas.Count, 0);
        }

        [TestMethod]
        public void Posicionar_Pecas_Inicio_Partida()
        {
            Tabuleiro tabuleiro = new Tabuleiro();

            tabuleiro.PosicionarInicioPartida();

            Assert.AreEqual(tabuleiro.Pecas[0].PosicaoAtual, new Point(1, 1));
            Assert.AreEqual(tabuleiro.Pecas[1].PosicaoAtual, new Point(1, 3));
            Assert.AreEqual(tabuleiro.Pecas[2].PosicaoAtual, new Point(1, 5));
            Assert.AreEqual(tabuleiro.Pecas[3].PosicaoAtual, new Point(1, 7));

            Assert.AreEqual(tabuleiro.Pecas[4].PosicaoAtual, new Point(2, 2));
            Assert.AreEqual(tabuleiro.Pecas[5].PosicaoAtual, new Point(2, 4));
            Assert.AreEqual(tabuleiro.Pecas[6].PosicaoAtual, new Point(2, 6));
            Assert.AreEqual(tabuleiro.Pecas[7].PosicaoAtual, new Point(2, 8));

            Assert.AreEqual(tabuleiro.Pecas[8].PosicaoAtual, new Point(3, 1));
            Assert.AreEqual(tabuleiro.Pecas[9].PosicaoAtual, new Point(3, 3));
            Assert.AreEqual(tabuleiro.Pecas[10].PosicaoAtual, new Point(3, 5));
            Assert.AreEqual(tabuleiro.Pecas[11].PosicaoAtual, new Point(3, 7));

            Assert.AreEqual(tabuleiro.Pecas[12].PosicaoAtual, new Point(6, 2));
            Assert.AreEqual(tabuleiro.Pecas[13].PosicaoAtual, new Point(6, 4));
            Assert.AreEqual(tabuleiro.Pecas[14].PosicaoAtual, new Point(6, 6));
            Assert.AreEqual(tabuleiro.Pecas[15].PosicaoAtual, new Point(6, 8));

            Assert.AreEqual(tabuleiro.Pecas[16].PosicaoAtual, new Point(7, 1));
            Assert.AreEqual(tabuleiro.Pecas[17].PosicaoAtual, new Point(7, 3));
            Assert.AreEqual(tabuleiro.Pecas[18].PosicaoAtual, new Point(7, 5));
            Assert.AreEqual(tabuleiro.Pecas[19].PosicaoAtual, new Point(7, 7));

            Assert.AreEqual(tabuleiro.Pecas[20].PosicaoAtual, new Point(8, 2));
            Assert.AreEqual(tabuleiro.Pecas[21].PosicaoAtual, new Point(8, 4));
            Assert.AreEqual(tabuleiro.Pecas[22].PosicaoAtual, new Point(8, 6));
            Assert.AreEqual(tabuleiro.Pecas[23].PosicaoAtual, new Point(8, 8));
        }

        [TestMethod]
        public void Mover_Peca_Dentro_Do_Tabuleiro()
        {
            Peca peca = new Peca(new Point(1, 1), Cor.BRANCA);
            Tabuleiro tabuleiro = new Tabuleiro();

            Point posicaoEsperada = new Point(2, 2);
            tabuleiro.CalcularMovimentos(peca, posicaoEsperada);

            Assert.AreEqual(peca.PosicoesPossiveis.Count, 1);
            Assert.AreEqual(peca.PosicoesPossiveis[0], posicaoEsperada);
        }

        [TestMethod]
        public void Nao_Mover_Peca_Fora_Do_Tabuleiro()
        {
            Peca peca = new Peca(new Point(8, 8), Cor.BRANCA);
            Tabuleiro tabuleiro = new Tabuleiro();

            tabuleiro.CalcularMovimentos(peca, new Point(9, 9));

            Assert.AreEqual(peca.PosicoesPossiveis.Count, 0);
        }

        [TestMethod]
        public void Mover_Peca_Na_Diagonal()
        {
            Peca peca = new Peca(new Point(2, 2), Cor.BRANCA);
            Tabuleiro tabuleiro = new Tabuleiro();

            Point posicaoEsperada = new Point(1, 3);
            tabuleiro.CalcularMovimentos(peca, posicaoEsperada);

            Assert.AreEqual(peca.PosicoesPossiveis.Count, 1);
            Assert.AreEqual(peca.PosicoesPossiveis[0], posicaoEsperada);
        }

        [TestMethod]
        public void Nao_Mover_Peca_Para_Longe()
        {
            Peca peca = new Peca(new Point(2, 2), Cor.BRANCA);
            Tabuleiro tabuleiro = new Tabuleiro();

            tabuleiro.CalcularMovimentos(peca, new Point(5, 5));

            Assert.AreEqual(peca.PosicoesPossiveis.Count, 0);
        }
    }
}
