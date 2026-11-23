using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alaska.API.Migrations
{
    /// <inheritdoc />
    public partial class EstruturaCompleta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessoes_Filmes_FilmeId",
                table: "Sessoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessoes_Salas_SalaId",
                table: "Sessoes");

            migrationBuilder.RenameColumn(
                name: "ClassificacaoIndicativa",
                table: "Filmes",
                newName: "PosterUrl");

            migrationBuilder.AddColumn<string>(
                name: "Classificacao",
                table: "Filmes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataLancamento",
                table: "Filmes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Filmes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Vendas",
                columns: table => new
                {
                    IdVenda = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdFilme = table.Column<int>(type: "int", nullable: false),
                    TituloFilme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Horario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoSessao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataCompra = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendas", x => x.IdVenda);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_SessaoId",
                table: "Reservas",
                column: "SessaoId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservaAssentos_AssentoId",
                table: "ReservaAssentos",
                column: "AssentoId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservaAssentos_ReservaId",
                table: "ReservaAssentos",
                column: "ReservaId");

            migrationBuilder.CreateIndex(
                name: "IX_Assentos_SalaId",
                table: "Assentos",
                column: "SalaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assentos_Salas_SalaId",
                table: "Assentos",
                column: "SalaId",
                principalTable: "Salas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservaAssentos_Assentos_AssentoId",
                table: "ReservaAssentos",
                column: "AssentoId",
                principalTable: "Assentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservaAssentos_Reservas_ReservaId",
                table: "ReservaAssentos",
                column: "ReservaId",
                principalTable: "Reservas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Sessoes_SessaoId",
                table: "Reservas",
                column: "SessaoId",
                principalTable: "Sessoes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessoes_Filmes_FilmeId",
                table: "Sessoes",
                column: "FilmeId",
                principalTable: "Filmes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessoes_Salas_SalaId",
                table: "Sessoes",
                column: "SalaId",
                principalTable: "Salas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assentos_Salas_SalaId",
                table: "Assentos");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservaAssentos_Assentos_AssentoId",
                table: "ReservaAssentos");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservaAssentos_Reservas_ReservaId",
                table: "ReservaAssentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Sessoes_SessaoId",
                table: "Reservas");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessoes_Filmes_FilmeId",
                table: "Sessoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessoes_Salas_SalaId",
                table: "Sessoes");

            migrationBuilder.DropTable(
                name: "Vendas");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_SessaoId",
                table: "Reservas");

            migrationBuilder.DropIndex(
                name: "IX_ReservaAssentos_AssentoId",
                table: "ReservaAssentos");

            migrationBuilder.DropIndex(
                name: "IX_ReservaAssentos_ReservaId",
                table: "ReservaAssentos");

            migrationBuilder.DropIndex(
                name: "IX_Assentos_SalaId",
                table: "Assentos");

            migrationBuilder.DropColumn(
                name: "Classificacao",
                table: "Filmes");

            migrationBuilder.DropColumn(
                name: "DataLancamento",
                table: "Filmes");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Filmes");

            migrationBuilder.RenameColumn(
                name: "PosterUrl",
                table: "Filmes",
                newName: "ClassificacaoIndicativa");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessoes_Filmes_FilmeId",
                table: "Sessoes",
                column: "FilmeId",
                principalTable: "Filmes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessoes_Salas_SalaId",
                table: "Sessoes",
                column: "SalaId",
                principalTable: "Salas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
