using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dominio;
using System.Drawing;
using Dominio.Entidades;

namespace MultiCheckers.Testes
{
    [TestClass]
    public class PartidaTest
    {
        [TestMethod]
        public void Editar_Tabuleiro()
        {
            Partida partida = new Partida();

            Tabuleiro tabuleiroAntes = partida.Tabuleiro;
            partida.EditarTabuleiro(new Tabuleiro());
            Tabuleiro tabuleiroDepois = partida.Tabuleiro;

            Assert.AreEqual(tabuleiroAntes.Pecas.Count, 24);
            Assert.AreEqual(tabuleiroDepois.Pecas.Count, 0);
        }

        [TestMethod]
        public void Eliminar_Peca_Preta_E_Terminar_Jogo()
        {
            Peca peca1 = new Peca(new Point(4, 4), Cor.BRANCA);
            Peca peca2 = new Peca(new Point(5, 5), Cor.PRETA);
            Peca peca3 = new Peca(new Point(1, 1), Cor.BRANCA);
            Partida partida = new Partida();
            Jogada jogada1 = new Jogada(new Point(6, 6), new Point(4, 4));

            partida.EditarTabuleiro(new Tabuleiro());
            partida.Tabuleiro.AdicionarPeca(peca1);
            partida.Tabuleiro.AdicionarPeca(peca2);
            partida.Tabuleiro.AdicionarPeca(peca3);

            partida.Tabuleiro.PercorrerTabuleiro(Cor.BRANCA);
            partida.Tabuleiro.AtualizarJogada(jogada1);
            partida.Tabuleiro.AplicarRodadaBonus(jogada1);

            Assert.AreEqual(partida.Tabuleiro.Pecas.Count, 2);
            Assert.AreEqual(partida.Tabuleiro.CorTurnoAtual, Cor.PRETA);
            Assert.IsTrue(partida.ValidarFimJogo(partida.Tabuleiro.CorTurnoAtual));
            Assert.IsTrue(partida.PartidaFinalizada);
        }

        [TestMethod]
        public void Eliminar_Peca_Preta_E_Nao_Terminar_Jogo()
        {
            Peca peca1 = new Peca(new Point(1, 1), Cor.BRANCA);
            Peca peca2 = new Peca(new Point(4, 4), Cor.BRANCA);
            Peca peca3 = new Peca(new Point(5, 5), Cor.PRETA);
            Peca peca4 = new Peca(new Point(8, 8), Cor.PRETA);
            Partida partida = new Partida();
            Jogada jogada1 = new Jogada(new Point(6, 6), new Point(4, 4));

            partida.EditarTabuleiro(new Tabuleiro());
            partida.Tabuleiro.AdicionarPeca(peca1);
            partida.Tabuleiro.AdicionarPeca(peca2);
            partida.Tabuleiro.AdicionarPeca(peca3);
            partida.Tabuleiro.AdicionarPeca(peca4);

            partida.Tabuleiro.PercorrerTabuleiro(Cor.BRANCA);
            partida.Tabuleiro.AtualizarJogada(jogada1);
            partida.Tabuleiro.AplicarRodadaBonus(jogada1);

            Assert.AreEqual(partida.Tabuleiro.Pecas.Count, 3);
            Assert.AreEqual(partida.Tabuleiro.CorTurnoAtual, Cor.PRETA);
            Assert.IsFalse(partida.ValidarFimJogo(partida.Tabuleiro.CorTurnoAtual));
            Assert.IsFalse(partida.PartidaFinalizada);
        }

        [TestMethod]
        public void Inserir_Usuarios_Brancas_Pretas()
        {
            Partida partida = new Partida();
            Usuario jogadorBrancas = new Usuario("loginBrancas", "brancas@email.com", "brancas");
            Usuario jogadorPretas = new Usuario("loginPretas", "pretas@email.com", "pretas");

            partida.InserirUsuario(jogadorBrancas);
            partida.InserirUsuario(jogadorPretas);

            Assert.AreEqual(jogadorBrancas, partida.JogadorBrancas);
            Assert.AreEqual(jogadorPretas, partida.JogadorPretas);
        }

        [TestMethod]
        public void Inserir_Usuarios_Expectadores()
        {
            Partida partida = new Partida();
            Usuario jogadorBrancas = new Usuario("loginBrancas", "brancas@email.com", "brancas");
            Usuario jogadorPretas = new Usuario("loginPretas", "pretas@email.com", "pretas");
            Usuario expectador1 = new Usuario("expectador1", "expectador1@email.com", "expectador1");
            Usuario expectador2 = new Usuario("expectador2", "expectador2@email.com", "expectador2");
            Usuario expectador3 = new Usuario("expectador3", "expectador3@email.com", "expectador3");

            partida.InserirUsuario(jogadorBrancas);
            partida.InserirUsuario(jogadorPretas);
            partida.InserirUsuario(expectador1);
            partida.InserirUsuario(expectador2);
            partida.InserirUsuario(expectador3);

            Assert.AreEqual(jogadorBrancas, partida.JogadorBrancas);
            Assert.AreEqual(jogadorPretas, partida.JogadorPretas);
            Assert.AreEqual(expectador1, partida.Expectadores[0]);
            Assert.AreEqual(expectador2, partida.Expectadores[1]);
            Assert.AreEqual(expectador3, partida.Expectadores[2]);
        }
    }
}
