using Fina.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fina.Api.Data.Mappings;

public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("categoria");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
           .HasColumnName("id");

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasColumnName("nome")
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);

        builder.Property(x => x.Descricao)
            .IsRequired(false)
            .HasColumnName("descricao")
            .HasColumnType("VARCHAR")
            .HasMaxLength(255);

        builder.Property(x => x.UsuarioId)
            .IsRequired()
            .HasColumnName("usuario_id")
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);

        builder.Property(x => x.Descricao)
           .IsRequired(false)
           .HasColumnName("descricao")
           .HasColumnType("VARCHAR")
           .HasMaxLength(255);

        builder.Property(x => x.Icon)
           .IsRequired(false)
           .HasColumnName("icon")
           .HasColumnType("VARCHAR")
           .HasMaxLength(50);

        builder.Property(x => x.Color)
           .IsRequired(false)
           .HasColumnName("color")
           .HasColumnType("VARCHAR")
           .HasMaxLength(10);

        builder.Property(x => x.IsDefault)
           .HasColumnName("is_default");

        builder.Property(x => x.Ativo)
           .HasColumnName("ativo");

        builder.Property(x => x.CriadaEm)
           .IsRequired()
           .HasColumnName("criada_em");
    }
}
