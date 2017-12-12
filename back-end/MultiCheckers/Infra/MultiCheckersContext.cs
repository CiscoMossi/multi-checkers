using Dominio;
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
        public MultiCheckersContext() : this("name=MultiCheckersDB") { }

        public MultiCheckersContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UsuarioMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
