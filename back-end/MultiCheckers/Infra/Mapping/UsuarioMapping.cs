using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Mapping
{
    class UsuarioMapping : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMapping()
        {
            ToTable("Usuario", "mc");

            HasKey(x => x.Id);
            
            Property(x => x.Login).HasMaxLength(128).HasColumnAnnotation(
                                                        IndexAnnotation.AnnotationName,
                                                        new IndexAnnotation(
                                                        new IndexAttribute("IX_Login") { IsUnique = true })).IsRequired();

            Property(x => x.Email).HasMaxLength(128).HasColumnAnnotation(
                                                        IndexAnnotation.AnnotationName,
                                                        new IndexAnnotation(
                                                        new IndexAttribute("IX_Email") { IsUnique = true })).IsRequired();

            Property(x => x.Senha).HasMaxLength(256).IsRequired();

            Property(x => x.GravatarHash).HasMaxLength(256).IsRequired();

            Ignore(x => x.UserHash);

        }
    }
}
