using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilySchedule.Migrations
{
    /// <inheritdoc />
    public partial class creacionDeModelosGF : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Creador",
                table: "Eventos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "GFamiliares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CorreoAdmin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GFamiliares", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioGFModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrupoId = table.Column<int>(type: "int", nullable: false),
                    grupoFamiliarId = table.Column<int>(type: "int", nullable: false),
                    usuarioID = table.Column<int>(type: "int", nullable: false),
                    FechaUnion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioGFModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioGFModel_GFamiliares_grupoFamiliarId",
                        column: x => x.grupoFamiliarId,
                        principalTable: "GFamiliares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioGFModel_Usuarios_usuarioID",
                        column: x => x.usuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioGFModel_grupoFamiliarId",
                table: "UsuarioGFModel",
                column: "grupoFamiliarId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioGFModel_usuarioID",
                table: "UsuarioGFModel",
                column: "usuarioID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioGFModel");

            migrationBuilder.DropTable(
                name: "GFamiliares");

            migrationBuilder.AlterColumn<string>(
                name: "Creador",
                table: "Eventos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
