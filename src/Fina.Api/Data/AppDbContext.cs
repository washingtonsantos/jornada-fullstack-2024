using Fina.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Fina.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Categoria> Categorias { get; set; } = null!;
    public DbSet<SubCategoria> SubCategorias { get; set; } = null!;
    public DbSet<Transacao> Transacoes { get; set; } = null!;
    public DbSet<Banco> Bancos { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<SubCategoria>()
            .HasOne(x => x.Categoria)
            .WithMany(x => x.SubCategorias)
            .HasForeignKey(x => x.CategoriaId)
            .HasConstraintName("fk_subcategorias_categorias_categoria_id")
            .OnDelete(DeleteBehavior.Cascade);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var dateTimeProperties = entityType.GetProperties()
           .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?));

            foreach (var property in dateTimeProperties)
            {
                property.SetValueConverter(new Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<DateTime, DateTime>(
                    v => v.Kind == DateTimeKind.Utc ? v : v.ToUniversalTime(),
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc)));
            }
        }

        modelBuilder.Entity<Categoria>().HasData(
            // RECEITAS
            new Categoria
{
    Id = Guid.Parse("D2AFC928-38E8-4D8B-8C40-000000000001"),
    Nome = "Salário",
    Descricao = "Recebimento de salário",
    TipoCategoria = Core.Enums.TipoTransacao.Receita,
    Icon = "wallet",
    Color = "#4CAF50",
    IsDefault = true,
    Ativo = true,
    CriadaEm = new DateTime(2025, 1, 1)
},
            new Categoria
{
    Id = Guid.Parse("D2AFC928-38E8-4D8B-8C40-000000000002"),
    Nome = "Freelance",
    Descricao = "Trabalhos extras",
    TipoCategoria = Core.Enums.TipoTransacao.Receita,
    Icon = "briefcase",
    Color = "#2E7D32",
    IsDefault = true,
    Ativo = true,
    CriadaEm = new DateTime(2025, 1, 1)
},
            new Categoria
{
    Id = Guid.Parse("D2AFC928-38E8-4D8B-8C40-000000000003"),
    Nome = "Investimentos",
    Descricao = "Rendimentos de investimentos",
    TipoCategoria = Core.Enums.TipoTransacao.Receita,
    Icon = "trending_up",
    Color = "#388E3C",
    IsDefault = true,
    Ativo = true,
    CriadaEm = new DateTime(2025, 1, 1)
},

            // DESPESAS
            new Categoria
{
    Id = Guid.Parse("D2AFC928-38E8-4D8B-8C40-000000000101"),
    Nome = "Moradia",
    Descricao = "Aluguel, condomínio, IPTU",
    TipoCategoria = Core.Enums.TipoTransacao.Despesa,
    Icon = "home",
    Color = "#F44336",
    IsDefault = true,
    Ativo = true,
    CriadaEm = new DateTime(2025, 1, 1)
},
            new Categoria
{
    Id = Guid.Parse("D2AFC928-38E8-4D8B-8C40-000000000102"),
    Nome = "Alimentação",
    Descricao = "Mercado e restaurantes",
    TipoCategoria = Core.Enums.TipoTransacao.Despesa,
    Icon = "restaurant",
    Color = "#FF9800",
    IsDefault = true,
    Ativo = true,
    CriadaEm = new DateTime(2025, 1, 1)
},
            new Categoria
{
    Id = Guid.Parse("D2AFC928-38E8-4D8B-8C40-000000000103"),
    Nome = "Transporte",
    Descricao = "Combustível, ônibus, Uber",
    TipoCategoria = Core.Enums.TipoTransacao.Despesa,
    Icon = "directions_car",
    Color = "#2196F3",
    IsDefault = true,
    Ativo = true,
    CriadaEm = new DateTime(2025, 1, 1)
},
            new Categoria
{
    Id = Guid.Parse("D2AFC928-38E8-4D8B-8C40-000000000104"),
    Nome = "Saúde",
    Descricao = "Plano de saúde, consultas e medicamentos",
    TipoCategoria = Core.Enums.TipoTransacao.Despesa,
    Icon = "favorite",
    Color = "#E91E63",
    IsDefault = true,
    Ativo = true,
    CriadaEm = new DateTime(2025, 1, 1)
},
            new Categoria
{
    Id = Guid.Parse("D2AFC928-38E8-4D8B-8C40-000000000105"),
    Nome = "Educação",
    Descricao = "Cursos e mensalidades",
    TipoCategoria = Core.Enums.TipoTransacao.Despesa,
    Icon = "school",
    Color = "#3F51B5",
    IsDefault = true,
    Ativo = true,
    CriadaEm = new DateTime(2025, 1, 1)
},
            new Categoria
{
    Id = Guid.Parse("D2AFC928-38E8-4D8B-8C40-000000000106"),
    Nome = "Lazer",
    Descricao = "Cinema, viagens e entretenimento",
    TipoCategoria = Core.Enums.TipoTransacao.Despesa,
    Icon = "sports_esports",
    Color = "#9C27B0",
    IsDefault = true,
    Ativo = true,
    CriadaEm = new DateTime(2025, 1, 1)
},
            new Categoria
{
    Id = Guid.Parse("D2AFC928-38E8-4D8B-8C40-000000000107"),
    Nome = "Compras",
    Descricao = "Roupas e compras em geral",
    TipoCategoria = Core.Enums.TipoTransacao.Despesa,
    Icon = "shopping_cart",
    Color = "#795548",
    IsDefault = true,
    Ativo = true,
    CriadaEm = new DateTime(2025, 1, 1)
},
            new Categoria
{
    Id = Guid.Parse("D2AFC928-38E8-4D8B-8C40-000000000108"),
    Nome = "Assinaturas",
    Descricao = "Netflix, Spotify e outros serviços",
    TipoCategoria = Core.Enums.TipoTransacao.Despesa,
    Icon = "subscriptions",
    Color = "#607D8B",
    IsDefault = true,
    Ativo = true,
    CriadaEm = new DateTime(2025, 1, 1)
},
            new Categoria
{
    Id = Guid.Parse("D2AFC928-38E8-4D8B-8C40-000000000109"),
    Nome = "Impostos",
    Descricao = "Tributos e taxas",
    TipoCategoria = Core.Enums.TipoTransacao.Despesa,
    Icon = "receipt_long",
    Color = "#B71C1C",
    IsDefault = true,
    Ativo = true,
    CriadaEm = new DateTime(2025, 1, 1)
},
            new Categoria
{
    Id = Guid.Parse("D2AFC928-38E8-4D8B-8C40-000000000110"),
    Nome = "Outros",
    Descricao = "Outras despesas",
    TipoCategoria = Core.Enums.TipoTransacao.Despesa,
    Icon = "Categoria",
    Color = "#757575",
    IsDefault = true,
    Ativo = true,
    CriadaEm = new DateTime(2025, 1, 1)
},
            new Categoria
            {
                Id = Guid.Parse("D2AFC928-38E8-4D8B-8C40-000000000111"),
                Nome = "Pet",
                Descricao = "Despesas com Pet",
                TipoCategoria = Core.Enums.TipoTransacao.Despesa,
                Icon = "Categoria",
                Color = "#757575",
                IsDefault = true,
                Ativo = true,
                CriadaEm = new DateTime(2025, 1, 1)
            }
            );
    }
}
