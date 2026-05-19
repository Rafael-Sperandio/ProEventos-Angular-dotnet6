using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProEventos.Persistence.Migrations
{
    public partial class AjustaRedeSocialNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RedesSociais_Eventos_EventoId",
                table: "RedesSociais");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "RedesSociais",
                newName: "Nome");

            migrationBuilder.AlterColumn<int>(
                name: "PalestranteId",
                table: "RedesSociais",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_RedesSociais_Eventos_EventoId",
                table: "RedesSociais",
                column: "EventoId",
                principalTable: "Eventos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RedesSociais_Eventos_EventoId",
                table: "RedesSociais");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "RedesSociais",
                newName: "nome");

            migrationBuilder.AlterColumn<int>(
                name: "PalestranteId",
                table: "RedesSociais",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RedesSociais_Eventos_EventoId",
                table: "RedesSociais",
                column: "EventoId",
                principalTable: "Eventos",
                principalColumn: "Id");
        }
    }
}
