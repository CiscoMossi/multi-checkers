using Dominio;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Migrations
{
    class UsuarioMapping : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMapping()
        {
            ToTable("Usuario", "mc");

            HasKey(x => x.Id);
            

            Property(x => x.Login).HasMaxLength(128).IsRequired();

            Property(x => x.Email).HasMaxLength(128).IsRequired();

            Property(x => x.Senha).HasMaxLength(256).IsRequired();

            Property(x => x.GravatarHash).HasMaxLength(256).IsRequired();

            //HasMany<decimal>(x => x.Pontos).AsList(x => x.WithColumn("ListPosition"));

        }
    }
}
