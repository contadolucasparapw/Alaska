using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alaska.API.Migrations;

/// <inheritdoc />
public partial class _20261122231954_InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Assentos",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                SalaId = table.Column<int>(type: "int", nullable: false),
                Fileira = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Numero = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Assentos", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Filmes",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Duracao = table.Column<int>(type: "int", nullable: false),
                ClassificacaoIndicativa = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Filmes", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Pessoas",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                SenhaHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Nivel = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Pessoas", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "ReservaAssentos",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ReservaId = table.Column<int>(type: "int", nullable: false),
                AssentoId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ReservaAssentos", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Reservas",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                SessaoId = table.Column<int>(type: "int", nullable: false),
                DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Reservas", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Salas",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Capacidade = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Salas", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Sessoes",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                FilmeId = table.Column<int>(type: "int", nullable: false),
                SalaId = table.Column<int>(type: "int", nullable: false),
                DataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                QuantidadeMinima = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Sessoes", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Assentos");

        migrationBuilder.DropTable(
            name: "Filmes");

        migrationBuilder.DropTable(
            name: "Pessoas");

        migrationBuilder.DropTable(
            name: "ReservaAssentos");

        migrationBuilder.DropTable(
            name: "Reservas");

        migrationBuilder.DropTable(
            name: "Salas");

        migrationBuilder.DropTable(
            name: "Sessoes");
    }
}
