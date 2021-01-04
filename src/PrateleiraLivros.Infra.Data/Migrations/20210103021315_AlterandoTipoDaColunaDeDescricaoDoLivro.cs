using Microsoft.EntityFrameworkCore.Migrations;

namespace PrateleiraLivros.Infra.Data.Migrations
{
    public partial class AlterandoTipoDaColunaDeDescricaoDoLivro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "PL.Livro",
                type: "varchar (MAX)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "PL.Livro",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar (MAX)");
        }
    }
}
