using Dominio;
using Infra.Mapping;
using Infra.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra
{
    public class MultiCheckersContext : DbContext, IMultiCheckersContext
    {
        public MultiCheckersContext() : this("name=MultiCheckers") { }

        public MultiCheckersContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Historico> Historicos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UsuarioMapping());

            modelBuilder.Configurations.Add(new HistoricoMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
