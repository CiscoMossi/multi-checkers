using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dominio;

namespace MultiCheckers.Testes
{
    [TestClass]
    public class UsuarioTest
    {
        [TestMethod]
        public void Criar_Usuario_E_Inserir_UserHash()
        {
            Usuario usuario = new Usuario("login", "email@email.com", "senha");
            usuario.InserirUserHash("hash");

            Assert.AreEqual("login", usuario.Login);
            Assert.AreEqual("email@email.com", usuario.Email);
            Assert.AreEqual("a9c071cb8d49c3505a9a0f4eea814e1e", usuario.Senha);
            Assert.AreEqual("hash", usuario.UserHash);
        }

        [TestMethod]
        public void Criar_Usuario_E_Inserir_SalaHash()
        {
            Usuario usuario = new Usuario("login", "email@email.com", "senha");
            usuario.InserirsalaHash("hash");

            Assert.AreEqual("login", usuario.Login);
            Assert.AreEqual("email@email.com", usuario.Email);
            Assert.AreEqual("a9c071cb8d49c3505a9a0f4eea814e1e", usuario.Senha);
            Assert.AreEqual("hash", usuario.SalaHash);
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

        [TestMethod]
        public void Validar_Usuario_Com_Login_E_Email_Vazios()
        {
            Usuario usuario = new Usuario("", "", "senha");

            var listaErros = usuario.Validar();

            Assert.AreEqual(3, listaErros.Count);
            Assert.AreEqual("Login não pode ser nulo", listaErros[0]);
            Assert.AreEqual("Email não pode ser nulo", listaErros[1]);
            Assert.AreEqual("Email deve ter formato com @", listaErros[2]);
        }

        [TestMethod]
        public void Validar_Usuario_Com_Login_E_Email_Muito_Grandes()
        {
            Usuario usuario = new Usuario("usuario usuario usuario usuario usuario usuario usuario usuario usuario usuario usuario usuario usuario usuario usuario usuario " +
            "usuario usuario usuario usuario ", "usuario usuario usuario usuario usuario usuario usuario usuario usuario usuario usuario usuario usuario usuario usuario " +
            "usuario usuario usuario usuario usuario usuario usuario usuario usuario usuario usuario usuario usuario usuario usuario usuario usuario ", "senha");

            var listaErros = usuario.Validar();

            Assert.AreEqual(3, listaErros.Count);
            Assert.AreEqual("Login deve ter até 128 caracteres", listaErros[0]);
            Assert.AreEqual("Email deve ter até 128 caracteres", listaErros[1]);
            Assert.AreEqual("Email deve ter formato com @", listaErros[2]);
        }

        [TestMethod]
        public void Validar_Senha()
        {
            Usuario usuario = new Usuario("login", "email@email.com", "senha");

            usuario.ValidarSenha("senha");

            Assert.AreEqual("login", usuario.Login);
            Assert.AreEqual("email@email.com", usuario.Email);
            Assert.AreEqual("a9c071cb8d49c3505a9a0f4eea814e1e", usuario.Senha);
        }
    }
}
