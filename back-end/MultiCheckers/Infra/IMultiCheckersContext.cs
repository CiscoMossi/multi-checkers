using Dominio;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Migrations
{
    public interface IMultiCheckersContext
    {
        int SaveChanges();

        DbSet<Usuario> Usuarios { get; set; }

        DbSet<Historico> Historicos { get; set; }
    }
}
