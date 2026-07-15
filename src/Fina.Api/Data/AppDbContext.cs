using Fina.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Fina.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Categoria> Categorias { get; set; } = null!;
    public DbSet<SubCategoria> SubCategorias { get; set; } = null!;
    public DbSet<Transacao> Transacoes { get; set; } = null!;
    public DbSet<Conta> Contas { get; set; } = null!;

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
    }
}
