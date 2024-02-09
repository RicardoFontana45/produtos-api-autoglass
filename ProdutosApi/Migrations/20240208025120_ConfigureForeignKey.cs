using Microsoft.EntityFrameworkCore.Migrations;

namespace ProdutosApi.Migrations
{
    public partial class ConfigureForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Fornecedores_FornecedorCodigoFornecedor",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_FornecedorCodigoFornecedor",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "FornecedorCodigoFornecedor",
                table: "Produtos");

            migrationBuilder.AddColumn<int>(
                name: "CodigoFornecedor",
                table: "Produtos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CodigoFornecedor",
                table: "Produtos",
                column: "CodigoFornecedor");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Fornecedores_CodigoFornecedor",
                table: "Produtos",
                column: "CodigoFornecedor",
                principalTable: "Fornecedores",
                principalColumn: "CodigoFornecedor",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Fornecedores_CodigoFornecedor",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_CodigoFornecedor",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "CodigoFornecedor",
                table: "Produtos");

            migrationBuilder.AddColumn<int>(
                name: "FornecedorCodigoFornecedor",
                table: "Produtos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_FornecedorCodigoFornecedor",
                table: "Produtos",
                column: "FornecedorCodigoFornecedor");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Fornecedores_FornecedorCodigoFornecedor",
                table: "Produtos",
                column: "FornecedorCodigoFornecedor",
                principalTable: "Fornecedores",
                principalColumn: "CodigoFornecedor",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
