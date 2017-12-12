using Dominio;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Mapping
{
    class HistoricoMapping : EntityTypeConfiguration<Historico>
    {
        public HistoricoMapping()
        {
            ToTable("Historico", "mc");

            HasKey(x => x.Id);

            Property(x => x.Venceu).IsRequired();

            Property(x => x.PecasRestantes).IsRequired();

            Property(x => x.PecasElimandas).IsRequired();

            Property(x => x.Pontos).IsRequired();

            HasRequired(x => x.Usuario).WithMany().Map(x => x.MapKey("UsuarioId")).WillCascadeOnDelete(false);

        }
    }
}