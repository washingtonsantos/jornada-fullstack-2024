using Fina.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fina.Api.Data.Mappings;

public class ContaMapping : IEntityTypeConfiguration<Conta>
{
    public void Configure(EntityTypeBuilder<Conta> builder)
    {
        builder.ToTable("conta");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
         .HasColumnName("id");

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasColumnName("nome")
            .HasColumnType("VARCHAR")
            .HasMaxLength(20);

        builder.Property(x => x.TipoConta)
            .IsRequired(true)
            .HasColumnName("tipo_conta")
            .HasColumnType("VARCHAR")
            .HasMaxLength(20);

        builder.Property(x => x.Saldo)
            .IsRequired()
            .HasDefaultValue(0)
            .HasColumnName("valor")
            .HasColumnType("MONEY");

        builder.Property(x => x.Limite)
            .HasColumnName("valor")
            .HasColumnType("MONEY");

        builder.Property(x => x.Ativo)
          .HasColumnName("ativo");

        builder.Property(x => x.CriadoEm)
           .IsRequired()
           .HasColumnName("criado_em");

        builder.Property(x => x.UsuarioId)
            .IsRequired()
            .HasColumnName("usuario_id")
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
    }
}
