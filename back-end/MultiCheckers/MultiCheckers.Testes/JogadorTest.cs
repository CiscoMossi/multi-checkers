using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dominio;
using Dominio.Entidades;
using System.Drawing;

namespace MultiCheckers.Testes
{
    [TestClass]
    public class JogadaTest
    {
        [TestMethod]
        public void Criar_Jogada()
        {
            Point posicaoEscolhida = new Point(4,4); ;
            Point posicaoAntiga = new Point(3,3);
            Jogada jogada = new Jogada(posicaoEscolhida, posicaoAntiga); 

            Assert.AreEqual(jogada.PosicaoEscolhida, new Point(4,4));
            Assert.AreEqual(jogada.PosicaoAntiga, new Point(3,3));
        }
    }
}
