using Fina.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fina.Api.Data.Mappings;

public class TransacaoMapping : IEntityTypeConfiguration<Transacao>
{
    public void Configure(EntityTypeBuilder<Transacao> builder)
    {
        builder.ToTable("transacao");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
          .HasColumnName("id");

        builder.Property(x => x.Titulo)
            .IsRequired()
            .HasColumnName("titulo")
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);

        builder.Property(x => x.Descricao)
            .IsRequired()
            .HasColumnName("descricao")
            .HasColumnType("VARCHAR");

        builder.Property(x => x.TipoTransacao)
            .IsRequired()
            .HasColumnName("tipo_transacao")
            .HasColumnType("VARCHAR");

        builder.Property(x => x.Valor)
            .IsRequired()
            .HasColumnName("valor")
            .HasColumnType("MONEY");

        builder.Property(x => x.FormaPagamentoRecebimento)
           .IsRequired(false)
           .HasColumnName("forma_pagamento_recebimento")
           .HasColumnType("VARCHAR");

        builder.Property(x => x.ContaId)
           .IsRequired(false)
           .HasColumnName("origem_pagamento_recebimento")
           .HasColumnType("VARCHAR");

        builder.Property(x => x.PagoRecebidoEm)
            .IsRequired(false)
            .HasColumnName("pago_recebido_em");

        builder.Property(x => x.UsuarioId)
            .IsRequired()
            .HasColumnName("usuario_id")
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);

        builder.Property(x => x.StatusTransacao)
           .IsRequired()
           .HasColumnName("status_transacao")
           .HasColumnType("VARCHAR");

        builder.Property(x => x.CategoriaId)
            .HasColumnName("categoria_id");

        builder.Property(x => x.SubCategoriaId)
            .IsRequired(false)
            .HasColumnName("subcategoria_id");

        builder.Property(x => x.CriadoEm)
           .IsRequired()
           .HasColumnName("criado_em");
    }
}
