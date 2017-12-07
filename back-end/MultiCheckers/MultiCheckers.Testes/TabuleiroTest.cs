﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dominio;
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
        public void Mover_Peca_Uma_Posicao_Dentro_Do_Tabuleiro()
        {
            Peca peca = new Peca(new Point(1,1), Cor.BRANCA);
            Tabuleiro tabuleiro = new Tabuleiro();
            Point posicaoEsperada = new Point(2,2);

            tabuleiro.AdicionarPeca(peca);
            tabuleiro.PercorrerTabuleiro(peca.Cor);

            Assert.AreEqual(peca.PosicoesPossiveis.Count, 1);
            Assert.AreEqual(peca.PosicoesPossiveis[0], posicaoEsperada);
        }

        [TestMethod]
        public void Mover_Peca_Duas_Posicoes_Dentro_Do_Tabuleiro()
        {
            Peca peca = new Peca(new Point(2,2), Cor.BRANCA);
            Tabuleiro tabuleiro = new Tabuleiro();
            Point posicaoEsperadaEsquerda = new Point(1,3);
            Point posicaoEsperadaDireita = new Point(3,3);

            tabuleiro.AdicionarPeca(peca);
            tabuleiro.PercorrerTabuleiro(peca.Cor);

            Assert.AreEqual(peca.PosicoesPossiveis.Count, 2);
            Assert.AreEqual(peca.PosicoesPossiveis[0], posicaoEsperadaDireita);
            Assert.AreEqual(peca.PosicoesPossiveis[1], posicaoEsperadaEsquerda);
        }

        [TestMethod]
        public void Mover_Varias_Pecas_Dentro_Do_Tabuleiro()
        {
            Peca peca1 = new Peca(new Point(2, 2), Cor.BRANCA);
            Peca peca2 = new Peca(new Point(4, 4), Cor.BRANCA);
            Tabuleiro tabuleiro = new Tabuleiro();

            Point posicaoEsperadaEsquerda1 = new Point(1, 3);
            Point posicaoEsperadaDireita1 = new Point(3, 3);

            Point posicaoEsperadaEsquerda2 = new Point(3, 5);
            Point posicaoEsperadaDireita2 = new Point(5, 5);

            tabuleiro.AdicionarPeca(peca1);
            tabuleiro.AdicionarPeca(peca2);
            tabuleiro.PercorrerTabuleiro(Cor.BRANCA);

            Assert.AreEqual(peca1.PosicoesPossiveis.Count, 2);
            Assert.AreEqual(peca2.PosicoesPossiveis.Count, 2);

            Assert.AreEqual(peca1.PosicoesPossiveis[0], posicaoEsperadaDireita1);
            Assert.AreEqual(peca1.PosicoesPossiveis[1], posicaoEsperadaEsquerda1);

            Assert.AreEqual(peca2.PosicoesPossiveis[0], posicaoEsperadaDireita2);
            Assert.AreEqual(peca2.PosicoesPossiveis[1], posicaoEsperadaEsquerda2);
        }

        [TestMethod]
        public void Nao_Mover_Peca_Fora_Do_Tabuleiro()
        {
            Peca peca = new Peca(new Point(8, 8), Cor.BRANCA);
            Tabuleiro tabuleiro = new Tabuleiro();

            tabuleiro.AdicionarPeca(peca);
            tabuleiro.PercorrerTabuleiro(Cor.BRANCA);

            Assert.AreEqual(peca.PosicoesPossiveis.Count, 0);
        }

        [TestMethod]
        public void Nao_Mover_Peca_De_Outra_Cor()
        {
            Peca peca1 = new Peca(new Point(2, 2), Cor.PRETA);
            Peca peca2 = new Peca(new Point(4, 4), Cor.BRANCA);
            Tabuleiro tabuleiro = new Tabuleiro();

            Point posicaoEsperadaEsquerda2 = new Point(3, 5);
            Point posicaoEsperadaDireita2 = new Point(5, 5);

            tabuleiro.AdicionarPeca(peca1);
            tabuleiro.AdicionarPeca(peca2);
            tabuleiro.PercorrerTabuleiro(Cor.BRANCA);

            Assert.AreEqual(peca1.PosicoesPossiveis.Count, 0);
            Assert.AreEqual(peca2.PosicoesPossiveis.Count, 2);

            Assert.AreEqual(peca2.PosicoesPossiveis[0], posicaoEsperadaDireita2);
            Assert.AreEqual(peca2.PosicoesPossiveis[1], posicaoEsperadaEsquerda2);
        }

        [TestMethod]
        public void Mover_Peca_Saltando_Peca_Inimiga()
        {
            Peca peca1 = new Peca(new Point(5, 5), Cor.PRETA);
            Peca peca2 = new Peca(new Point(4, 4), Cor.BRANCA);
            Tabuleiro tabuleiro = new Tabuleiro();

            Point posicaoEsperadaEsquerda2 = new Point(3, 5);
            Point posicaoEsperadaDireita2 = new Point(6, 6);

            tabuleiro.AdicionarPeca(peca1);
            tabuleiro.AdicionarPeca(peca2);
            tabuleiro.PercorrerTabuleiro(Cor.BRANCA);

            Assert.AreEqual(peca1.PosicoesPossiveis.Count, 0);
            Assert.AreEqual(peca2.PosicoesPossiveis.Count, 2);

            Assert.AreEqual(peca2.PosicoesPossiveis[0], posicaoEsperadaDireita2);
            Assert.AreEqual(peca2.PosicoesPossiveis[1], posicaoEsperadaEsquerda2);
        }

        [TestMethod]
        public void Mover_Peca_Podendo_Saltar_Duas_Peca_Inimigas()
        {
            Peca peca1 = new Peca(new Point(4, 4), Cor.PRETA);
            Peca peca2 = new Peca(new Point(5, 5), Cor.BRANCA);
            Peca peca3 = new Peca(new Point(3, 5), Cor.BRANCA);
            Tabuleiro tabuleiro = new Tabuleiro();

            Point posicaoEsperadaEsquerda = new Point(2, 6);
            Point posicaoEsperadaDireita = new Point(6, 6);

            tabuleiro.AdicionarPeca(peca1);
            tabuleiro.AdicionarPeca(peca2);
            tabuleiro.AdicionarPeca(peca3);
            tabuleiro.PercorrerTabuleiro(Cor.PRETA);

            Assert.AreEqual(peca1.PosicoesPossiveis.Count, 2);
            Assert.AreEqual(peca2.PosicoesPossiveis.Count, 0);
            Assert.AreEqual(peca2.PosicoesPossiveis.Count, 0);

            Assert.AreEqual(peca1.PosicoesPossiveis[0], posicaoEsperadaDireita);
            Assert.AreEqual(peca1.PosicoesPossiveis[1], posicaoEsperadaEsquerda);
        }

        [TestMethod]
        public void Mover_Dama_Todas_Direcoes()
        {
            Peca peca = new Peca(new Point(4, 4), Cor.PRETA);
            Tabuleiro tabuleiro = new Tabuleiro();

            Point posicaoEsperadaEsquerdaAvanco = new Point(5, 5);
            Point posicaoEsperadaDireitaAvanco = new Point(3, 5);

            Point posicaoEsperadaEsquerdaRecuo = new Point(5, 3);
            Point posicaoEsperadaDireitaRecuo = new Point(3, 3);

            tabuleiro.AdicionarPeca(peca);
            peca.TransformarEmDama();
            tabuleiro.PercorrerTabuleiro(Cor.PRETA);

            Assert.AreEqual(peca.PosicoesPossiveis.Count, 4);

            Assert.AreEqual(peca.PosicoesPossiveis[0], posicaoEsperadaEsquerdaAvanco);
            Assert.AreEqual(peca.PosicoesPossiveis[1], posicaoEsperadaDireitaAvanco);
            Assert.AreEqual(peca.PosicoesPossiveis[2], posicaoEsperadaEsquerdaRecuo);
            Assert.AreEqual(peca.PosicoesPossiveis[3], posicaoEsperadaDireitaRecuo);
        }

        [TestMethod]
        public void Mover_Dama_Todas_Direcoes_Saltando_Pecas_Inimigas()
        {
            Peca peca1 = new Peca(new Point(4, 4), Cor.PRETA);
            Peca peca2 = new Peca(new Point(5, 5), Cor.BRANCA);
            Peca peca3 = new Peca(new Point(3, 5), Cor.BRANCA);
            Peca peca4 = new Peca(new Point(5, 3), Cor.BRANCA);
            Peca peca5 = new Peca(new Point(3, 3), Cor.BRANCA);

            Tabuleiro tabuleiro = new Tabuleiro();

            Point posicaoEsperadaEsquerdaAvanco = new Point(6, 6);
            Point posicaoEsperadaDireitaAvanco = new Point(2, 6);

            Point posicaoEsperadaEsquerdaRecuo = new Point(6, 2);
            Point posicaoEsperadaDireitaRecuo = new Point(2, 2);

            tabuleiro.AdicionarPeca(peca1);
            tabuleiro.AdicionarPeca(peca2);
            tabuleiro.AdicionarPeca(peca3);
            tabuleiro.AdicionarPeca(peca4);
            tabuleiro.AdicionarPeca(peca5);

            peca1.TransformarEmDama();
            tabuleiro.PercorrerTabuleiro(Cor.PRETA);

            Assert.AreEqual(peca1.PosicoesPossiveis.Count, 4);

            Assert.AreEqual(peca1.PosicoesPossiveis[0], posicaoEsperadaEsquerdaAvanco);
            Assert.AreEqual(peca1.PosicoesPossiveis[1], posicaoEsperadaDireitaAvanco);
            Assert.AreEqual(peca1.PosicoesPossiveis[2], posicaoEsperadaEsquerdaRecuo);
            Assert.AreEqual(peca1.PosicoesPossiveis[3], posicaoEsperadaDireitaRecuo);
        }
    }
}
