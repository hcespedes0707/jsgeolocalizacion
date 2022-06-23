using Microsoft.EntityFrameworkCore.Migrations;

namespace AgendaWeb.Migrations
{
    public partial class ContactImageNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacto_Imagen_ImagenId",
                table: "Contacto");

            migrationBuilder.AlterColumn<int>(
                name: "ImagenId",
                table: "Contacto",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacto_Imagen_ImagenId",
                table: "Contacto",
                column: "ImagenId",
                principalTable: "Imagen",
                principalColumn: "ImagenId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacto_Imagen_ImagenId",
                table: "Contacto");

            migrationBuilder.AlterColumn<int>(
                name: "ImagenId",
                table: "Contacto",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacto_Imagen_ImagenId",
                table: "Contacto",
                column: "ImagenId",
                principalTable: "Imagen",
                principalColumn: "ImagenId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
