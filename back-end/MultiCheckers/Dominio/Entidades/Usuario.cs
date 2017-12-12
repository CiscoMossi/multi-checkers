using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Dominio.Entidades;
using System.Security.Cryptography;

namespace Dominio
{
    public class Usuario
    {
        private Usuario() { }

        public Usuario(string login, string email, string senha)
        {
            this.Login = login;
            this.Email = email;
            this.Senha = Criptografar(senha);
            this.GerarGravatarHash(email);
        }

        public int Id { get; private set; }

        public string Login { get; private set; }

        public string Email { get; private set; }

        public string Senha { get; private set; }

        public string GravatarHash { get; private set; }

        public string UserHash { get; private set; }

        public string SalaHash { get; private set; }

        public void InserirUserHash(string userHash)
        {
            this.UserHash = userHash;
        }

        public void InserirsalaHash(string salaHash)
        {
            this.SalaHash = salaHash;
        }

        public List<string> Validar()
        {
            List<string> mensagens = new List<string>();

            if (string.IsNullOrEmpty(Login))
                mensagens.Add("Login não pode ser nulo");

            if (Login.Length > 128)
                mensagens.Add("Login deve ter até 128 caracteres");

            if (string.IsNullOrEmpty(Email))
                mensagens.Add("Email não pode ser nulo");

            if (Email.Length > 128)
                mensagens.Add("Email deve ter até 128 caracteres");

            if (!Email.Contains('@'))
                mensagens.Add("Email deve ter formato com @");

            return mensagens;
        }

        public bool ValidarSenha(string senha)
        {
            return Criptografar(this.Email + senha) == Senha;
        }

        private string Criptografar(string texto)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.Default.GetBytes(texto);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
                sb.Append(hash[i].ToString("x2"));

            return sb.ToString();
        }

        private void GerarGravatarHash(string email)
        {
            email.Trim();
            email.ToLower();
            
            this.GravatarHash = this.Criptografar(email);
        }

        public Object GerarUsuarioResposta()
        {
            List<string> papeis = new List<string>() { "Jogador" };

            Object resposta = new
            {
                this.Id,
                this.Login,
                this.Email,
                this.GravatarHash,
                papeis
            };

            return resposta;
        }

    }
}
