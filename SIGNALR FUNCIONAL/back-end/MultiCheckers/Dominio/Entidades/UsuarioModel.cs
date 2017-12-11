using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class UsuarioModel
    {
        public UsuarioModel(string login)
        {
            this.Login = login;
        }

        public string Login { get; private set; }

        public string GravatarHash { get; private set; }
    }
}
