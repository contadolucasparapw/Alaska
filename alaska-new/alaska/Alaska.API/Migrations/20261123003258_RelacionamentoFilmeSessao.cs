using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alaska.API.Migrations;

/// <inheritdoc />
public partial class _20261123003258_RelacionamentoFilmeSessao : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateIndex(
            name: "IX_Sessoes_FilmeId",
            table: "Sessoes",
            column: "FilmeId");

        migrationBuilder.AddForeignKey(
            name: "FK_Sessoes_Filmes_FilmeId",
            table: "Sessoes",
            column: "FilmeId",
            principalTable: "Filmes",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Sessoes_Filmes_FilmeId",
            table: "Sessoes");

        migrationBuilder.DropIndex(
            name: "IX_Sessoes_FilmeId",
            table: "Sessoes");
    }
}
