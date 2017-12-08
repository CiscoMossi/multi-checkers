using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dominio;
using System.Drawing;
using Dominio.Entidades;

namespace MultiCheckers.Testes
{
    [TestClass]
    public class PecaTest
    {
        [TestMethod]
        public void Criar_Peca()
        {
            Peca peca = new Peca(new Posicao(1,1), Cor.BRANCA);

            Assert.AreEqual(peca.PosicaoAtual, new Posicao(1,1));
            Assert.AreEqual(peca.Cor, Cor.BRANCA);
            Assert.AreEqual(peca.PosicoesPossiveis.Count, 0);
        }

        [TestMethod]
        public void Adicionar_Posicoes()
        {
            Peca peca = new Peca(new Posicao(1, 1), Cor.BRANCA);

            peca.AdicionarPosicao(new Posicao(2, 2));
            peca.AdicionarPosicao(new Posicao(3, 3));

            Assert.AreEqual(peca.PosicoesPossiveis[0], new Posicao(2, 2));
            Assert.AreEqual(peca.PosicoesPossiveis[1], new Posicao(3, 3));
        }
    }
}
