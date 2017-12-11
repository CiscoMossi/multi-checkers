using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dominio;
using Dominio.Entidades;
using System.Drawing;

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

            Assert.AreEqual(tabuleiro.Pecas[0].PosicaoAtual, new Point(1,1));
            Assert.AreEqual(tabuleiro.Pecas[1].PosicaoAtual, new Point(3,1));
            Assert.AreEqual(tabuleiro.Pecas[2].PosicaoAtual, new Point(5,1));
            Assert.AreEqual(tabuleiro.Pecas[3].PosicaoAtual, new Point(7,1));

            Assert.AreEqual(tabuleiro.Pecas[4].PosicaoAtual, new Point(2,2));
            Assert.AreEqual(tabuleiro.Pecas[5].PosicaoAtual, new Point(4,2));
            Assert.AreEqual(tabuleiro.Pecas[6].PosicaoAtual, new Point(6,2));
            Assert.AreEqual(tabuleiro.Pecas[7].PosicaoAtual, new Point(8,2));

            Assert.AreEqual(tabuleiro.Pecas[8].PosicaoAtual, new Point(1,3));
            Assert.AreEqual(tabuleiro.Pecas[9].PosicaoAtual, new Point(3,3));
            Assert.AreEqual(tabuleiro.Pecas[10].PosicaoAtual, new Point(5,3));
            Assert.AreEqual(tabuleiro.Pecas[11].PosicaoAtual, new Point(7,3));

            Assert.AreEqual(tabuleiro.Pecas[12].PosicaoAtual, new Point(2,6));
            Assert.AreEqual(tabuleiro.Pecas[13].PosicaoAtual, new Point(4,6));
            Assert.AreEqual(tabuleiro.Pecas[14].PosicaoAtual, new Point(6,6));
            Assert.AreEqual(tabuleiro.Pecas[15].PosicaoAtual, new Point(8,6));

            Assert.AreEqual(tabuleiro.Pecas[16].PosicaoAtual, new Point(1,7));
            Assert.AreEqual(tabuleiro.Pecas[17].PosicaoAtual, new Point(3,7));
            Assert.AreEqual(tabuleiro.Pecas[18].PosicaoAtual, new Point(5,7));
            Assert.AreEqual(tabuleiro.Pecas[19].PosicaoAtual, new Point(7,7));

            Assert.AreEqual(tabuleiro.Pecas[20].PosicaoAtual, new Point(2,8));
            Assert.AreEqual(tabuleiro.Pecas[21].PosicaoAtual, new Point(4,8));
            Assert.AreEqual(tabuleiro.Pecas[22].PosicaoAtual, new Point(6,8));
            Assert.AreEqual(tabuleiro.Pecas[23].PosicaoAtual, new Point(8,8));
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
        public void Nao_Mover_Peca_Branca_Para_Casa_Ocupada_Por_Branca()
        {
            Peca peca1 = new Peca(new Point(5, 5), Cor.BRANCA);
            Peca peca2 = new Peca(new Point(4, 4), Cor.BRANCA);
            Tabuleiro tabuleiro = new Tabuleiro();

            tabuleiro.AdicionarPeca(peca1);
            tabuleiro.AdicionarPeca(peca2);
            tabuleiro.PercorrerTabuleiro(Cor.BRANCA);

            Assert.AreEqual(peca2.PosicoesPossiveis.Count, 1);
            Assert.AreEqual(peca2.PosicoesPossiveis[0], new Point(3, 5));
        }

        [TestMethod]
        public void Nao_Mover_Peca_Preta_Para_Casa_Ocupada_Por_Preta()
        {
            Peca peca1 = new Peca(new Point(5, 5), Cor.PRETA);
            Peca peca2 = new Peca(new Point(4, 4), Cor.PRETA);
            Tabuleiro tabuleiro = new Tabuleiro();

            tabuleiro.AdicionarPeca(peca1);
            tabuleiro.AdicionarPeca(peca2);
            tabuleiro.PercorrerTabuleiro(Cor.PRETA);

            Assert.AreEqual(peca1.PosicoesPossiveis.Count, 1);
            Assert.AreEqual(peca1.PosicoesPossiveis[0], new Point(6, 4));
        }

        [TestMethod]
        public void Nao_Mover_Peca_Branca_Para_Casa_Ocupada_Por_Preta()
        {
            Peca peca1 = new Peca(new Point(5, 5), Cor.PRETA);
            Peca peca2 = new Peca(new Point(6, 6), Cor.PRETA);
            Peca peca3 = new Peca(new Point(4, 4), Cor.BRANCA);
            Tabuleiro tabuleiro = new Tabuleiro();

            tabuleiro.AdicionarPeca(peca1);
            tabuleiro.AdicionarPeca(peca2);
            tabuleiro.AdicionarPeca(peca3);
            tabuleiro.PercorrerTabuleiro(Cor.BRANCA);

            Assert.AreEqual(peca3.PosicoesPossiveis.Count, 1);
            Assert.AreEqual(peca3.PosicoesPossiveis[0], new Point(3, 5));
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
        public void Mover_Peca_Branca_Saltando_Peca_Inimiga()
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
        public void Mover_Peca_Branca_Podendo_Saltar_Duas_Peca_Inimigas()
        {
            Peca peca1 = new Peca(new Point(4, 4), Cor.BRANCA);
            Peca peca2 = new Peca(new Point(5, 5), Cor.PRETA);
            Peca peca3 = new Peca(new Point(3, 5), Cor.PRETA);
            Tabuleiro tabuleiro = new Tabuleiro();

            Point posicaoEsperadaEsquerda = new Point(2, 6);
            Point posicaoEsperadaDireita = new Point(6, 6);

            tabuleiro.AdicionarPeca(peca1);
            tabuleiro.AdicionarPeca(peca2);
            tabuleiro.AdicionarPeca(peca3);
            tabuleiro.PercorrerTabuleiro(Cor.BRANCA);

            Assert.AreEqual(peca1.PosicoesPossiveis.Count, 2);
            Assert.AreEqual(peca2.PosicoesPossiveis.Count, 0);
            Assert.AreEqual(peca2.PosicoesPossiveis.Count, 0);

            Assert.AreEqual(peca1.PosicoesPossiveis[0], posicaoEsperadaDireita);
            Assert.AreEqual(peca1.PosicoesPossiveis[1], posicaoEsperadaEsquerda);
        }

        [TestMethod]
        public void Mover_Dama_Branca_Todas_Direcoes()
        {
            Peca peca = new Peca(new Point(4, 4), Cor.BRANCA);
            Tabuleiro tabuleiro = new Tabuleiro();

            tabuleiro.AdicionarPeca(peca);
            peca.TransformarEmDama();
            tabuleiro.PercorrerTabuleiro(Cor.BRANCA);

            Assert.AreEqual(peca.PosicoesPossiveis.Count, 4);

            Assert.IsTrue(peca.PosicoesPossiveis[0].Equals(new Point(5, 5)));
            Assert.IsTrue(peca.PosicoesPossiveis[1].Equals(new Point(3, 5)));
            Assert.IsTrue(peca.PosicoesPossiveis[2].Equals(new Point(5, 3)));
            Assert.IsTrue(peca.PosicoesPossiveis[3].Equals(new Point(3, 3)));
        }

        [TestMethod]
        public void Mover_Dama_Branca_Todas_Direcoes_Saltando_Pecas_Inimigas()
        {
            Peca peca1 = new Peca(new Point(4, 4), Cor.BRANCA);
            Peca peca2 = new Peca(new Point(5, 5), Cor.PRETA);
            Peca peca3 = new Peca(new Point(3, 5), Cor.PRETA);
            Peca peca4 = new Peca(new Point(5, 3), Cor.PRETA);
            Peca peca5 = new Peca(new Point(3, 3), Cor.PRETA);

            Tabuleiro tabuleiro = new Tabuleiro();

            tabuleiro.AdicionarPeca(peca1);
            tabuleiro.AdicionarPeca(peca2);
            tabuleiro.AdicionarPeca(peca3);
            tabuleiro.AdicionarPeca(peca4);
            tabuleiro.AdicionarPeca(peca5);

            peca1.TransformarEmDama();
            tabuleiro.PercorrerTabuleiro(Cor.BRANCA);

            Assert.AreEqual(peca1.PosicoesPossiveis.Count, 4);

            Assert.IsTrue(peca1.PosicoesPossiveis[0].Equals(new Point(6, 6)));
            Assert.IsTrue(peca1.PosicoesPossiveis[1].Equals(new Point(2, 6)));
            Assert.IsTrue(peca1.PosicoesPossiveis[2].Equals(new Point(6, 2)));
            Assert.IsTrue(peca1.PosicoesPossiveis[3].Equals(new Point(2, 2)));
        }

        [TestMethod]
        public void Mover_Peca_Preta_Saltando_Peca_Inimiga()
        {
            Peca peca1 = new Peca(new Point(5, 5), Cor.PRETA);
            Peca peca2 = new Peca(new Point(4, 4), Cor.BRANCA);
            Tabuleiro tabuleiro = new Tabuleiro();

            Point posicaoEsperadaEsquerda = new Point(3, 3);
            Point posicaoEsperadaDireita = new Point(6, 4);

            tabuleiro.AdicionarPeca(peca1);
            tabuleiro.AdicionarPeca(peca2);
            tabuleiro.PercorrerTabuleiro(Cor.PRETA);

            Assert.AreEqual(peca1.PosicoesPossiveis.Count, 2);
            Assert.AreEqual(peca2.PosicoesPossiveis.Count, 0);

            Assert.AreEqual(peca1.PosicoesPossiveis[0], posicaoEsperadaDireita);
            Assert.AreEqual(peca1.PosicoesPossiveis[1], posicaoEsperadaEsquerda);
        }

        [TestMethod]
        public void Mover_Peca_Preta_Podendo_Saltar_Duas_Peca_Inimigas()
        {
            Peca peca1 = new Peca(new Point(6, 6), Cor.PRETA);
            Peca peca2 = new Peca(new Point(5, 5), Cor.BRANCA);
            Peca peca3 = new Peca(new Point(7, 5), Cor.BRANCA);
            Tabuleiro tabuleiro = new Tabuleiro();

            Point posicaoEsperadaEsquerda = new Point(4, 4);
            Point posicaoEsperadaDireita = new Point(8, 4);

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
        public void Mover_Dama_Preta_Todas_Direcoes()
        {
            Peca peca = new Peca(new Point(4, 4), Cor.PRETA);
            Tabuleiro tabuleiro = new Tabuleiro();

            tabuleiro.AdicionarPeca(peca);
            peca.TransformarEmDama();
            tabuleiro.PercorrerTabuleiro(Cor.PRETA);

            Assert.AreEqual(peca.PosicoesPossiveis.Count, 4);

            Assert.IsTrue(peca.PosicoesPossiveis[0].Equals(new Point(5, 3)));
            Assert.IsTrue(peca.PosicoesPossiveis[1].Equals(new Point(3, 3)));
            Assert.IsTrue(peca.PosicoesPossiveis[2].Equals(new Point(5, 5)));
            Assert.IsTrue(peca.PosicoesPossiveis[3].Equals(new Point(3, 5)));
        }

        [TestMethod]
        public void Mover_Dama_Preta_Todas_Direcoes_Saltando_Pecas_Inimigas()
        {
            Peca peca1 = new Peca(new Point(4, 4), Cor.PRETA);
            Peca peca2 = new Peca(new Point(5, 5), Cor.BRANCA);
            Peca peca3 = new Peca(new Point(3, 5), Cor.BRANCA);
            Peca peca4 = new Peca(new Point(5, 3), Cor.BRANCA);
            Peca peca5 = new Peca(new Point(3, 3), Cor.BRANCA);

            Tabuleiro tabuleiro = new Tabuleiro();

            tabuleiro.AdicionarPeca(peca1);
            tabuleiro.AdicionarPeca(peca2);
            tabuleiro.AdicionarPeca(peca3);
            tabuleiro.AdicionarPeca(peca4);
            tabuleiro.AdicionarPeca(peca5);

            peca1.TransformarEmDama();
            tabuleiro.PercorrerTabuleiro(Cor.PRETA);

            Assert.AreEqual(peca1.PosicoesPossiveis.Count, 4);

            Assert.IsTrue(peca1.PosicoesPossiveis[0].Equals(new Point(6, 2)));
            Assert.IsTrue(peca1.PosicoesPossiveis[1].Equals(new Point(2, 2)));
            Assert.IsTrue(peca1.PosicoesPossiveis[2].Equals(new Point(6, 6)));
            Assert.IsTrue(peca1.PosicoesPossiveis[3].Equals(new Point(2, 6)));
        }

        [TestMethod]
        public void Eliminar_Peca_Branca_Quando_Preta_Saltar_Sobre()
        {
            Peca peca1 = new Peca(new Point(6, 6), Cor.PRETA);
            Peca peca2 = new Peca(new Point(5, 5), Cor.BRANCA);
            Tabuleiro tabuleiro = new Tabuleiro();

            Point posicaoEsperada = new Point(4, 4);
            Jogada jogada = new Jogada(posicaoEsperada, new Point(6, 6));

            tabuleiro.AdicionarPeca(peca1);
            tabuleiro.AdicionarPeca(peca2);

            int numPecasAntes = tabuleiro.Pecas.Count;
            tabuleiro.PercorrerTabuleiro(Cor.PRETA);

            tabuleiro.AtualizarJogada(jogada);
            int numPecasDepois = tabuleiro.Pecas.Count;

            Assert.AreEqual(numPecasAntes, 2);
            Assert.AreEqual(numPecasDepois, 1);
            Assert.AreEqual(peca1.PosicoesPossiveis.Count, 2);
            Assert.AreEqual(peca2.PosicoesPossiveis.Count, 0);
        }

        [TestMethod]
        public void Eliminar_Peca_Preta_Quando_Branca_Saltar_Sobre()
        {
            Peca peca1 = new Peca(new Point(1, 1), Cor.BRANCA);
            Peca peca2 = new Peca(new Point(2, 2), Cor.PRETA);
            Tabuleiro tabuleiro = new Tabuleiro();

            Point posicaoEsperada = new Point(3, 3);
            Jogada jogada = new Jogada(posicaoEsperada, new Point(1, 1));

            tabuleiro.AdicionarPeca(peca1);
            tabuleiro.AdicionarPeca(peca2);

            int numPecasAntes = tabuleiro.Pecas.Count;
            tabuleiro.PercorrerTabuleiro(Cor.BRANCA);

            tabuleiro.AtualizarJogada(jogada);
            int numPecasDepois = tabuleiro.Pecas.Count;

            Assert.AreEqual(numPecasAntes, 2);
            Assert.AreEqual(numPecasDepois, 1);
            Assert.AreEqual(peca1.PosicoesPossiveis.Count, 1);
            Assert.AreEqual(peca2.PosicoesPossiveis.Count, 0);
        }

        [TestMethod]
        public void Nao_Mover_Peca_Com_Coordenadas_Incorretas()
        {
            Peca peca1 = new Peca(new Point(1, 1), Cor.BRANCA);
            Peca peca2 = new Peca(new Point(2, 2), Cor.PRETA);
            Tabuleiro tabuleiro = new Tabuleiro();

            Point posicaoEsperada = new Point(3, 3);
            Jogada jogada = new Jogada(posicaoEsperada, new Point(7, 7));

            tabuleiro.AdicionarPeca(peca1);
            tabuleiro.AdicionarPeca(peca2);

            tabuleiro.PercorrerTabuleiro(Cor.BRANCA);
            bool estado = tabuleiro.AtualizarJogada(jogada);

            Assert.IsFalse(estado);
            Assert.AreEqual(peca1.PosicoesPossiveis.Count, 1);
            Assert.AreEqual(peca2.PosicoesPossiveis.Count, 0);
        }

        [TestMethod]
        public void Nao_Mover_Peca_Para_Posicao_Nao_Calculada()
        {
            Peca peca1 = new Peca(new Point(1, 1), Cor.BRANCA);
            Peca peca2 = new Peca(new Point(2, 2), Cor.PRETA);
            Tabuleiro tabuleiro = new Tabuleiro();

            Jogada jogada = new Jogada(new Point(4, 1), new Point(1, 1));

            tabuleiro.AdicionarPeca(peca1);
            tabuleiro.AdicionarPeca(peca2);

            tabuleiro.PercorrerTabuleiro(Cor.BRANCA);
            bool estado = tabuleiro.AtualizarJogada(jogada);

            Assert.IsFalse(estado);
            Assert.AreEqual(peca1.PosicoesPossiveis.Count, 1);
            Assert.AreEqual(peca2.PosicoesPossiveis.Count, 0);
        }

        [TestMethod]
        public void Peca_Branca_Se_Torna_Dama()
        {
            Peca peca1 = new Peca(new Point(7, 7), Cor.BRANCA);
            Tabuleiro tabuleiro = new Tabuleiro();

            Jogada jogada1 = new Jogada(new Point(8, 8), new Point(7, 7));

            tabuleiro.AdicionarPeca(peca1);
            tabuleiro.PercorrerTabuleiro(Cor.BRANCA);

            bool estado1 = tabuleiro.AtualizarJogada(jogada1);

            Assert.IsTrue(estado1);
            Assert.IsTrue(peca1.IsDama);
        }

        [TestMethod]
        public void Peca_Preta_Se_Torna_Dama()
        {
            Peca peca2 = new Peca(new Point(2, 2), Cor.PRETA);
            Tabuleiro tabuleiro = new Tabuleiro();

            Jogada jogada2 = new Jogada(new Point(1, 1), new Point(2, 2));

            tabuleiro.AdicionarPeca(peca2);
            tabuleiro.PercorrerTabuleiro(Cor.PRETA);

            bool estado2 = tabuleiro.AtualizarJogada(jogada2);

            Assert.IsTrue(estado2);
            Assert.IsTrue(peca2.IsDama);
        }

        [TestMethod]
        public void Aplicar_Rodada_Bonus_Com_Proxima_Peca()
        {
            Peca peca1 = new Peca(new Point(1, 1), Cor.BRANCA);
            Peca peca2 = new Peca(new Point(2, 2), Cor.PRETA);
            Peca peca3 = new Peca(new Point(4, 4), Cor.PRETA);
            Tabuleiro tabuleiro = new Tabuleiro();

            Point posicaoEsperada1 = new Point(3, 3);
            Point posicaoEsperada2 = new Point(5, 5);
            Jogada jogada1 = new Jogada(posicaoEsperada1, new Point(1, 1));
            Jogada jogada2 = new Jogada(posicaoEsperada2, new Point(3, 3));

            tabuleiro.AdicionarPeca(peca1);
            tabuleiro.AdicionarPeca(peca2);
            tabuleiro.AdicionarPeca(peca3);

            int numPecasSalto1 = tabuleiro.Pecas.Count;
            tabuleiro.PercorrerTabuleiro(Cor.BRANCA);
            int numPosicoes1 = tabuleiro.Pecas[0].PosicoesPossiveis.Count;
            tabuleiro.AtualizarJogada(jogada1);
            int numPosicoes2 = tabuleiro.Pecas[0].PosicoesPossiveis.Count;

            int numPecasSalto2 = tabuleiro.Pecas.Count;
            tabuleiro.AplicarRodadaBonus(jogada1);

            tabuleiro.AtualizarJogada(jogada2);
            int numPecasAtual = tabuleiro.Pecas.Count;

            Assert.AreEqual(numPecasSalto1, 3);
            Assert.AreEqual(numPecasSalto2, 2);
            Assert.AreEqual(numPecasAtual, 1);
            Assert.AreEqual(numPosicoes1, 1);
            Assert.AreEqual(numPosicoes2, 1);
            Assert.AreEqual(tabuleiro.Pecas[0].PosicoesPossiveis.Count, 1);
        }

        [TestMethod]
        public void Nao_Aplicar_Rodada_Bonus_Sem_Proxima_Peca()
        {
            Peca peca1 = new Peca(new Point(1, 1), Cor.BRANCA);
            Peca peca2 = new Peca(new Point(2, 2), Cor.PRETA);
            Tabuleiro tabuleiro = new Tabuleiro();

            Point posicaoEsperada1 = new Point(3, 3);
            Jogada jogada1 = new Jogada(posicaoEsperada1, new Point(1, 1));

            tabuleiro.AdicionarPeca(peca1);
            tabuleiro.AdicionarPeca(peca2);

            int numPecasSalto1 = tabuleiro.Pecas.Count;
            tabuleiro.PercorrerTabuleiro(Cor.BRANCA);
            int numPosicoes1 = tabuleiro.Pecas[0].PosicoesPossiveis.Count;
            tabuleiro.AtualizarJogada(jogada1);
            tabuleiro.PercorrerTabuleiro(Cor.BRANCA);
            int numPosicoes2 = tabuleiro.Pecas[0].PosicoesPossiveis.Count;
            int numPecasAtual = tabuleiro.Pecas.Count;

            Assert.AreEqual(numPecasSalto1, 2);
            Assert.AreEqual(numPecasAtual, 1);
            Assert.AreEqual(numPosicoes1, 1);
            Assert.AreEqual(numPosicoes2, 2);
        }

        [TestMethod]
        public void Trocar_De_Turno_Apos_Movimento()
        {
            Peca peca1 = new Peca(new Point(4, 4), Cor.BRANCA);
            Peca peca2 = new Peca(new Point(5, 5), Cor.PRETA);
            Tabuleiro tabuleiro = new Tabuleiro();

            Point posicaoEsperada1 = new Point(6, 6);
            Jogada jogada1 = new Jogada(posicaoEsperada1, new Point(4, 4));

            tabuleiro.AdicionarPeca(peca1);
            tabuleiro.AdicionarPeca(peca2);

            tabuleiro.PercorrerTabuleiro(Cor.BRANCA);
            Cor cor1 = tabuleiro.CorTurnoAtual;

            tabuleiro.AtualizarJogada(jogada1);
            Cor cor2 = tabuleiro.CorTurnoAtual;

            tabuleiro.AplicarRodadaBonus(jogada1);
            Cor cor3 = tabuleiro.CorTurnoAtual;

            Assert.AreEqual(cor1, Cor.BRANCA);
            Assert.AreEqual(cor2, Cor.BRANCA);
            Assert.AreEqual(cor3, Cor.PRETA);
        }

        [TestMethod]
        public void Nao_Trocar_De_Turno_Em_Rodada_Bonus()
        {
            Peca peca1 = new Peca(new Point(4, 4), Cor.BRANCA);
            Peca peca2 = new Peca(new Point(5, 5), Cor.PRETA);
            Peca peca3 = new Peca(new Point(7, 7), Cor.PRETA);
            Tabuleiro tabuleiro = new Tabuleiro();

            Point posicaoEsperada1 = new Point(6, 6);
            Point posicaoEsperada2 = new Point(8, 8);
            Jogada jogada1 = new Jogada(posicaoEsperada1, new Point(4, 4));
            Jogada jogada2 = new Jogada(posicaoEsperada2, new Point(6, 6));

            tabuleiro.AdicionarPeca(peca1);
            tabuleiro.AdicionarPeca(peca2);
            tabuleiro.AdicionarPeca(peca3);

            Cor cor1 = tabuleiro.CorTurnoAtual;
            tabuleiro.PercorrerTabuleiro(Cor.BRANCA);

            Cor cor2 = tabuleiro.CorTurnoAtual;
            tabuleiro.AtualizarJogada(jogada1);

            Cor cor3 = tabuleiro.CorTurnoAtual;
            tabuleiro.AplicarRodadaBonus(jogada1);

            Cor cor4 = tabuleiro.CorTurnoAtual;
            tabuleiro.AtualizarJogada(jogada2);

            Cor cor5 = tabuleiro.CorTurnoAtual;
            tabuleiro.AplicarRodadaBonus(jogada2);
            Cor cor6 = tabuleiro.CorTurnoAtual;

            Assert.AreEqual(cor1, Cor.BRANCA);
            Assert.AreEqual(cor2, Cor.BRANCA);
            Assert.AreEqual(cor2, Cor.BRANCA);
            Assert.AreEqual(cor4, Cor.BRANCA);
            Assert.AreEqual(cor5, Cor.BRANCA);
            Assert.AreEqual(cor6, Cor.PRETA);
            Assert.IsTrue(peca1.IsDama);
        }

    }
}
