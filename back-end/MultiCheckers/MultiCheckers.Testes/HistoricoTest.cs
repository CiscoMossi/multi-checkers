using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dominio;

namespace MultiCheckers.Testes
{
    [TestClass]
    public class HistoricoTest
    {
        [TestMethod]
        public void Criar_Historico()
        {
            Usuario usuario = new Usuario("login", "email@email", "senha");
            Historico historico = new Historico(usuario, true, 4, 12);

            Assert.AreEqual(historico.Usuario.Login, "login");
            Assert.AreEqual(historico.Usuario.Email, "email@email");
            Assert.IsTrue(historico.Venceu);
            Assert.AreEqual(historico.PecasRestantes, 4);
            Assert.AreEqual(historico.PecasElimandas, 12);
        }

        [TestMethod]
        public void Calcular_Pontos_Se_Ganhou()
        {
            Usuario usuario = new Usuario("login", "email@email", "senha");
            Historico historico = new Historico(usuario, true, 4, 12);

            Assert.AreEqual(historico.Usuario.Login, "login");
            Assert.AreEqual(historico.Usuario.Email, "email@email");
            Assert.IsTrue(historico.Venceu);
            Assert.AreEqual(historico.PecasRestantes, 4);
            Assert.AreEqual(historico.PecasElimandas, 12);
            Assert.AreEqual(historico.Pontos, 32);
        }

        [TestMethod]
        public void Calcular_Pontos_Se_Perdeu()
        {
            Usuario usuario = new Usuario("login", "email@email", "senha");
            Historico historico = new Historico(usuario, false, 0, 3);

            Assert.AreEqual(historico.Usuario.Login, "login");
            Assert.AreEqual(historico.Usuario.Email, "email@email");
            Assert.IsFalse(historico.Venceu);
            Assert.AreEqual(historico.PecasRestantes, 0);
            Assert.AreEqual(historico.PecasElimandas, 3);
            Assert.AreEqual(historico.Pontos, 2);
        }
    }
}
