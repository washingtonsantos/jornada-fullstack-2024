using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fina.Api.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "banco",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "VARCHAR", maxLength: 20, nullable: false),
                    descricao = table.Column<string>(type: "VARCHAR", maxLength: 100, nullable: false),
                    usuario_id = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_banco", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "categoria",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    descricao = table.Column<string>(type: "VARCHAR", maxLength: 255, nullable: true),
                    usuario_id = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false),
                    icon = table.Column<string>(type: "VARCHAR", maxLength: 50, nullable: true),
                    color = table.Column<string>(type: "VARCHAR", maxLength: 10, nullable: true),
                    is_default = table.Column<bool>(type: "boolean", nullable: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    criada_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    tipo_categoria = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categoria", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sub-categoria",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    descricao = table.Column<string>(type: "VARCHAR", maxLength: 255, nullable: true),
                    categoria_id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario_id = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sub_categoria", x => x.id);
                    table.ForeignKey(
                        name: "fk_subcategorias_categorias_categoria_id",
                        column: x => x.categoria_id,
                        principalTable: "categoria",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "transacao",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    titulo = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    descricao = table.Column<string>(type: "VARCHAR", nullable: false),
                    pago_recebido_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tipo_transacao = table.Column<string>(type: "VARCHAR", nullable: false),
                    valor = table.Column<decimal>(type: "MONEY", nullable: false),
                    status_transacao = table.Column<string>(type: "VARCHAR", nullable: false),
                    categoria_id = table.Column<Guid>(type: "uuid", nullable: false),
                    subcategoria_id = table.Column<Guid>(type: "uuid", nullable: true),
                    forma_pagamento_recebimento = table.Column<string>(type: "VARCHAR", nullable: true),
                    origem_pagamento_recebimento = table.Column<string>(type: "VARCHAR", nullable: true),
                    usuario_id = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transacao", x => x.id);
                    table.ForeignKey(
                        name: "fk_transacao_categoria_categoria_id",
                        column: x => x.categoria_id,
                        principalTable: "categoria",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_transacao_sub_categoria_sub_categoria_id",
                        column: x => x.subcategoria_id,
                        principalTable: "sub-categoria",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "categoria",
                columns: new[] { "id", "ativo", "color", "criada_em", "descricao", "icon", "is_default", "nome", "tipo_categoria", "usuario_id" },
                values: new object[,]
                {
                    { new Guid("d2afc928-38e8-4d8b-8c40-000000000001"), true, "#4CAF50", new DateTime(2025, 1, 1, 3, 0, 0, 0, DateTimeKind.Utc), "Recebimento de salário", "wallet", true, "Salário", 1, "" },
                    { new Guid("d2afc928-38e8-4d8b-8c40-000000000002"), true, "#2E7D32", new DateTime(2025, 1, 1, 3, 0, 0, 0, DateTimeKind.Utc), "Trabalhos extras", "briefcase", true, "Freelance", 1, "" },
                    { new Guid("d2afc928-38e8-4d8b-8c40-000000000003"), true, "#388E3C", new DateTime(2025, 1, 1, 3, 0, 0, 0, DateTimeKind.Utc), "Rendimentos de investimentos", "trending_up", true, "Investimentos", 1, "" },
                    { new Guid("d2afc928-38e8-4d8b-8c40-000000000101"), true, "#F44336", new DateTime(2025, 1, 1, 3, 0, 0, 0, DateTimeKind.Utc), "Aluguel, condomínio, IPTU", "home", true, "Moradia", 2, "" },
                    { new Guid("d2afc928-38e8-4d8b-8c40-000000000102"), true, "#FF9800", new DateTime(2025, 1, 1, 3, 0, 0, 0, DateTimeKind.Utc), "Mercado e restaurantes", "restaurant", true, "Alimentação", 2, "" },
                    { new Guid("d2afc928-38e8-4d8b-8c40-000000000103"), true, "#2196F3", new DateTime(2025, 1, 1, 3, 0, 0, 0, DateTimeKind.Utc), "Combustível, ônibus, Uber", "directions_car", true, "Transporte", 2, "" },
                    { new Guid("d2afc928-38e8-4d8b-8c40-000000000104"), true, "#E91E63", new DateTime(2025, 1, 1, 3, 0, 0, 0, DateTimeKind.Utc), "Plano de saúde, consultas e medicamentos", "favorite", true, "Saúde", 2, "" },
                    { new Guid("d2afc928-38e8-4d8b-8c40-000000000105"), true, "#3F51B5", new DateTime(2025, 1, 1, 3, 0, 0, 0, DateTimeKind.Utc), "Cursos e mensalidades", "school", true, "Educação", 2, "" },
                    { new Guid("d2afc928-38e8-4d8b-8c40-000000000106"), true, "#9C27B0", new DateTime(2025, 1, 1, 3, 0, 0, 0, DateTimeKind.Utc), "Cinema, viagens e entretenimento", "sports_esports", true, "Lazer", 2, "" },
                    { new Guid("d2afc928-38e8-4d8b-8c40-000000000107"), true, "#795548", new DateTime(2025, 1, 1, 3, 0, 0, 0, DateTimeKind.Utc), "Roupas e compras em geral", "shopping_cart", true, "Compras", 2, "" },
                    { new Guid("d2afc928-38e8-4d8b-8c40-000000000108"), true, "#607D8B", new DateTime(2025, 1, 1, 3, 0, 0, 0, DateTimeKind.Utc), "Netflix, Spotify e outros serviços", "subscriptions", true, "Assinaturas", 2, "" },
                    { new Guid("d2afc928-38e8-4d8b-8c40-000000000109"), true, "#B71C1C", new DateTime(2025, 1, 1, 3, 0, 0, 0, DateTimeKind.Utc), "Tributos e taxas", "receipt_long", true, "Impostos", 2, "" },
                    { new Guid("d2afc928-38e8-4d8b-8c40-000000000110"), true, "#757575", new DateTime(2025, 1, 1, 3, 0, 0, 0, DateTimeKind.Utc), "Outras despesas", "Categoria", true, "Outros", 2, "" },
                    { new Guid("d2afc928-38e8-4d8b-8c40-000000000111"), true, "#757575", new DateTime(2025, 1, 1, 3, 0, 0, 0, DateTimeKind.Utc), "Despesas com Pet", "Categoria", true, "Pet", 2, "" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_sub_categoria_categoria_id",
                table: "sub-categoria",
                column: "categoria_id");

            migrationBuilder.CreateIndex(
                name: "ix_transacao_categoria_id",
                table: "transacao",
                column: "categoria_id");

            migrationBuilder.CreateIndex(
                name: "ix_transacao_sub_categoria_id",
                table: "transacao",
                column: "subcategoria_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "banco");

            migrationBuilder.DropTable(
                name: "transacao");

            migrationBuilder.DropTable(
                name: "sub-categoria");

            migrationBuilder.DropTable(
                name: "categoria");
        }
    }
}
