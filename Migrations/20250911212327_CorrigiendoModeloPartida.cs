using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroJugadores.Migrations
{
    /// <inheritdoc />
    public partial class CorrigiendoModeloPartida : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partidas_Jugadores_JugadoresJugadorId",
                table: "Partidas");

            migrationBuilder.DropForeignKey(
                name: "FK_Partidas_Jugadores_JugadoresJugadorId1",
                table: "Partidas");

            migrationBuilder.DropForeignKey(
                name: "FK_Partidas_Jugadores_JugadoresJugadorId2",
                table: "Partidas");

            migrationBuilder.DropForeignKey(
                name: "FK_Partidas_Jugadores_JugadoresJugadorId3",
                table: "Partidas");

            migrationBuilder.DropIndex(
                name: "IX_Partidas_JugadoresJugadorId",
                table: "Partidas");

            migrationBuilder.DropIndex(
                name: "IX_Partidas_JugadoresJugadorId1",
                table: "Partidas");

            migrationBuilder.DropIndex(
                name: "IX_Partidas_JugadoresJugadorId2",
                table: "Partidas");

            migrationBuilder.DropIndex(
                name: "IX_Partidas_JugadoresJugadorId3",
                table: "Partidas");

            migrationBuilder.DropColumn(
                name: "EstadoPartida",
                table: "Partidas");

            migrationBuilder.DropColumn(
                name: "JugadoresJugadorId",
                table: "Partidas");

            migrationBuilder.DropColumn(
                name: "JugadoresJugadorId1",
                table: "Partidas");

            migrationBuilder.DropColumn(
                name: "JugadoresJugadorId2",
                table: "Partidas");

            migrationBuilder.DropColumn(
                name: "JugadoresJugadorId3",
                table: "Partidas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EstadoPartida",
                table: "Partidas",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "JugadoresJugadorId",
                table: "Partidas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JugadoresJugadorId1",
                table: "Partidas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JugadoresJugadorId2",
                table: "Partidas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JugadoresJugadorId3",
                table: "Partidas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Partidas_JugadoresJugadorId",
                table: "Partidas",
                column: "JugadoresJugadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Partidas_JugadoresJugadorId1",
                table: "Partidas",
                column: "JugadoresJugadorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Partidas_JugadoresJugadorId2",
                table: "Partidas",
                column: "JugadoresJugadorId2");

            migrationBuilder.CreateIndex(
                name: "IX_Partidas_JugadoresJugadorId3",
                table: "Partidas",
                column: "JugadoresJugadorId3");

            migrationBuilder.AddForeignKey(
                name: "FK_Partidas_Jugadores_JugadoresJugadorId",
                table: "Partidas",
                column: "JugadoresJugadorId",
                principalTable: "Jugadores",
                principalColumn: "JugadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Partidas_Jugadores_JugadoresJugadorId1",
                table: "Partidas",
                column: "JugadoresJugadorId1",
                principalTable: "Jugadores",
                principalColumn: "JugadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Partidas_Jugadores_JugadoresJugadorId2",
                table: "Partidas",
                column: "JugadoresJugadorId2",
                principalTable: "Jugadores",
                principalColumn: "JugadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Partidas_Jugadores_JugadoresJugadorId3",
                table: "Partidas",
                column: "JugadoresJugadorId3",
                principalTable: "Jugadores",
                principalColumn: "JugadorId");
        }
    }
}
