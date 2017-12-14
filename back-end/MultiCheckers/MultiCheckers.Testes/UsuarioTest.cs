using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dominio;

namespace MultiCheckers.Testes
{
    [TestClass]
    public class UsuarioTest
    {
        [TestMethod]
        public void Criar_Usuario_E_Inserir_Hash()
        {
            Usuario usuario = new Usuario("login", "email@email.com", "senha");
            usuario.InserirUserHash("hash");

            Assert.AreEqual("login", usuario.Login);
            Assert.AreEqual("email@email.com", usuario.Email);
            Assert.AreEqual("a9c071cb8d49c3505a9a0f4eea814e1e", usuario.Senha);
            Assert.AreEqual("hash", usuario.UserHash);
        }

        [TestMethod]
        public void Gerar_Hash_Do_Gravatar()
        {
            Usuario usuario = new Usuario("login", "email@email.com", "senha");

            Assert.AreEqual("login", usuario.Login);
            Assert.AreEqual("email@email.com", usuario.Email);
            Assert.AreEqual("a9c071cb8d49c3505a9a0f4eea814e1e", usuario.Senha);
            Assert.AreEqual("4f64c9f81bb0d4ee969aaf7b4a5a6f40", usuario.GravatarHash);
        }
    }
}
