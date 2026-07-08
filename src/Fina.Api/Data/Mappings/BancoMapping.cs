using Fina.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fina.Api.Data.Mappings;

public class BancoMapping : IEntityTypeConfiguration<Banco>
{
    public void Configure(EntityTypeBuilder<Banco> builder)
    {
        builder.ToTable("banco");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
         .HasColumnName("id");

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasColumnName("nome")
            .HasColumnType("VARCHAR")
            .HasMaxLength(20);

        builder.Property(x => x.Descricao)
            .IsRequired()
            .HasColumnName("descricao")
            .HasColumnType("VARCHAR")
            .HasMaxLength(100);

        builder.Property(x => x.UsuarioId)
            .IsRequired()
            .HasColumnName("usuario_id")
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
    }
}
