using Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiCheckers.Testes
{
    public static class CleanUp
    {
        public static void LimparTabelas(MultiCheckersContext context)
        {
            context.Usuarios.RemoveRange(context.Usuarios);
            context.Historicos.RemoveRange(context.Historicos);
            context.SaveChanges();
        }
    }
}
