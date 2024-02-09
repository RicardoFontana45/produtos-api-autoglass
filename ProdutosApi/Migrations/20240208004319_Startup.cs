using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProdutosApi.Migrations
{
    public partial class Startup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fornecedores",
                columns: table => new
                {
                    CodigoFornecedor = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DescricaoFornecedor = table.Column<string>(type: "TEXT", nullable: true),
                    CNPJ = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedores", x => x.CodigoFornecedor);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    CodigoProduto = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DescricaoProduto = table.Column<string>(type: "TEXT", nullable: true),
                    SituacaoProduto = table.Column<bool>(type: "INTEGER", nullable: false),
                    DataFabricacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataValidade = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FornecedorCodigoFornecedor = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.CodigoProduto);
                    table.ForeignKey(
                        name: "FK_Produtos_Fornecedores_FornecedorCodigoFornecedor",
                        column: x => x.FornecedorCodigoFornecedor,
                        principalTable: "Fornecedores",
                        principalColumn: "CodigoFornecedor",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_FornecedorCodigoFornecedor",
                table: "Produtos",
                column: "FornecedorCodigoFornecedor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Fornecedores");
        }
    }
}
