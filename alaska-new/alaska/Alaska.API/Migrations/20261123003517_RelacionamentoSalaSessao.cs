using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alaska.API.Migrations;

/// <inheritdoc />
public partial class _20261123003517_RelacionamentoSalaSessao : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateIndex(
            name: "IX_Sessoes_SalaId",
            table: "Sessoes",
            column: "SalaId");

        migrationBuilder.AddForeignKey(
            name: "FK_Sessoes_Salas_SalaId",
            table: "Sessoes",
            column: "SalaId",
            principalTable: "Salas",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Sessoes_Salas_SalaId",
            table: "Sessoes");

        migrationBuilder.DropIndex(
            name: "IX_Sessoes_SalaId",
            table: "Sessoes");
    }
}
