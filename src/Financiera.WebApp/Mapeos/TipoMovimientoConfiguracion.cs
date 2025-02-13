using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Financiera.WebApp.Modelos;
namespace Financiera.WebApp.Mapeos;
public class TipoMovimientoConfiguracion : IEntityTypeConfiguration<TipoMovimiento>
{
    public void Configure(EntityTypeBuilder<TipoMovimiento> builder)
    {
        builder.ToTable("TIPOS_MOVIMIENTO");
        builder.HasKey(c => c.IdTipo);
        builder.Property(c => c.IdTipo).HasColumnName("ID_TIPO").UseMySqlIdentityColumn();
        builder.Property(c => c.DescripcionTipo).HasColumnName("DES_TIPO").HasMaxLength(100).IsRequired();
    }
}