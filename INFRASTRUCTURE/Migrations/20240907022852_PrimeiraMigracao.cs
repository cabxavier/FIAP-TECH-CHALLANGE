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
                    IdContato = table.Column<int>(type: "Int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "VarChar(100)", nullable: false),
                    Telefone = table.Column<string>(type: "VarChar(11)", nullable: false),
                    Email = table.Column<string>(type: "VarChar(200)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "DateTime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contato", x => x.IdContato);
                });

            migrationBuilder.CreateTable(
                name: "Regiao",
                columns: table => new
                {
                    IdRegiao = table.Column<int>(type: "Int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ddd = table.Column<int>(type: "Int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "DateTime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regiao", x => x.IdRegiao);
                });

            migrationBuilder.CreateTable(
                name: "ContatoRegiao",
                columns: table => new
                {
                    IdContatoRegiao = table.Column<int>(type: "Int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdContato = table.Column<int>(type: "Int", nullable: false),
                    IdRegiao = table.Column<int>(type: "Int", nullable: false),
                    ContatoIdContato = table.Column<int>(type: "Int", nullable: false),
                    RegiaoIdRegiao = table.Column<int>(type: "Int", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "DateTime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContatoRegiao", x => x.IdContatoRegiao);
                    table.ForeignKey(
                        name: "FK_ContatoRegiao_Contato_ContatoIdContato",
                        column: x => x.ContatoIdContato,
                        principalTable: "Contato",
                        principalColumn: "IdContato",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContatoRegiao_Regiao_RegiaoIdRegiao",
                        column: x => x.RegiaoIdRegiao,
                        principalTable: "Regiao",
                        principalColumn: "IdRegiao");
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
                name: "IX_ContatoRegiao_ContatoIdContato",
                table: "ContatoRegiao",
                column: "ContatoIdContato");

            migrationBuilder.CreateIndex(
                name: "IX_ContatoRegiao_IdContato_IdRegiao",
                table: "ContatoRegiao",
                columns: new[] { "IdContato", "IdRegiao" });

            migrationBuilder.CreateIndex(
                name: "IX_ContatoRegiao_RegiaoIdRegiao",
                table: "ContatoRegiao",
                column: "RegiaoIdRegiao");

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
