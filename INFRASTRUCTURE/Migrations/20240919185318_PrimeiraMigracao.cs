using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INFRASTRUCTURE.Migrations
{
    /// <inheritdoc />
    public partial class PrimeiraMigracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contato",
                columns: table => new
                {
                    Id = table.Column<int>(type: "Int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "VarChar(200)", nullable: false),
                    Telefone = table.Column<string>(type: "VarChar(11)", nullable: false),
                    Email = table.Column<string>(type: "VarChar(200)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "DateTime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contato", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regiao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "Int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ddd = table.Column<string>(type: "VarChar(2)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "DateTime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regiao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContatoRegiao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "Int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContatoId = table.Column<int>(type: "Int", nullable: false),
                    RegiaoId = table.Column<int>(type: "Int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "DateTime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContatoRegiao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContatoRegiao_Contato_ContatoId",
                        column: x => x.ContatoId,
                        principalTable: "Contato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContatoRegiao_Regiao_RegiaoId",
                        column: x => x.RegiaoId,
                        principalTable: "Regiao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contato_Email",
                table: "Contato",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contato_Telefone",
                table: "Contato",
                column: "Telefone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContatoRegiao_ContatoId",
                table: "ContatoRegiao",
                column: "ContatoId");

            migrationBuilder.CreateIndex(
                name: "IX_ContatoRegiao_RegiaoId",
                table: "ContatoRegiao",
                column: "RegiaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Regiao_Ddd",
                table: "Regiao",
                column: "Ddd",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContatoRegiao");

            migrationBuilder.DropTable(
                name: "Contato");

            migrationBuilder.DropTable(
                name: "Regiao");
        }
    }
}
