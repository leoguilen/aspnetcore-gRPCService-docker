using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrateleiraLivros.Infra.Data.Migrations
{
    public partial class Initial_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PL.Autor",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(maxLength: 30, nullable: false),
                    Sobrenome = table.Column<string>(maxLength: 30, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Bio = table.Column<string>(type: "TEXT", nullable: true),
                    Avatar = table.Column<string>(maxLength: 100, nullable: true),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    DataAtualizacao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PL.Autor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PL.Editora",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    SiteURL = table.Column<string>(maxLength: 150, nullable: true),
                    Endereco = table.Column<string>(maxLength: 255, nullable: false),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    DataAtualizacao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PL.Editora", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PL.Livro",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Titulo = table.Column<string>(maxLength: 200, nullable: false),
                    Descricao = table.Column<string>(maxLength: 255, nullable: false),
                    Valor = table.Column<decimal>(type: "decimal (5,2)", nullable: false),
                    ISBN_10 = table.Column<string>(nullable: false),
                    Edicao = table.Column<int>(nullable: false, defaultValue: 1),
                    DataPublicacao = table.Column<DateTime>(nullable: false),
                    Idioma = table.Column<string>(maxLength: 50, nullable: true, defaultValue: "Português"),
                    NumeroPaginas = table.Column<int>(nullable: false),
                    EditoraId = table.Column<Guid>(nullable: false),
                    AutorId = table.Column<Guid>(nullable: false),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    DataAtualizacao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PL.Livro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PL.Livro_PL.Autor_AutorId",
                        column: x => x.AutorId,
                        principalTable: "PL.Autor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PL.Livro_PL.Editora_EditoraId",
                        column: x => x.EditoraId,
                        principalTable: "PL.Editora",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PL.Livro_AutorId",
                table: "PL.Livro",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_PL.Livro_EditoraId",
                table: "PL.Livro",
                column: "EditoraId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PL.Livro");

            migrationBuilder.DropTable(
                name: "PL.Autor");

            migrationBuilder.DropTable(
                name: "PL.Editora");
        }
    }
}
