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
        public Usuario(string login, string email, string senha)
        {
            this.Login = login;
            this.Email = email;
            this.Senha = senha;
            this.GerarGravatarHash(email);
            this.Pontos = new List<decimal>();
        }

        public int Id { get; private set; }

        public string Login { get; private set; }

        public string Email { get; private set; }

        public string Senha { get; private set; }

        public string GravatarHash { get; private set; }

        public string UserHash { get; private set; }

        public List<decimal> Pontos { get; private set; }

        public void InserirUserHash(string userHash)
        {
            this.UserHash = userHash;
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

        private string CriptografarSenha(string senha)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.Default.GetBytes(Email + senha);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
                sb.Append(hash[i].ToString("x2"));

            return sb.ToString();
        }

        // https://gist.github.com/danesparza/973923
        private void GerarGravatarHash(string email)
        {
            email.Trim();
            email.ToLower();
            // Create a new instance of the MD5CryptoServiceProvider object.  
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.  
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(email));

            // Create a new Stringbuilder to collect the bytes  
            // and create a string.  
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string.  
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // Return the hexadecimal string.
            GravatarHash = sBuilder.ToString();
        }

    }
}
