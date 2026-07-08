using Fina.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fina.Api.Data.Mappings;

public class SubCategoriaMapping : IEntityTypeConfiguration<SubCategoria>
{
    public void Configure(EntityTypeBuilder<SubCategoria> builder)
    {
        builder.ToTable("sub-categoria");

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

        builder.Property(x => x.CategoriaId)
           .HasColumnName("categoria_id");
    }
}
