using Microsoft.EntityFrameworkCore.Migrations;

namespace Project1.DataAccess.Migrations
{
    public partial class StoreAddressID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Store_Address_AddressId",
                table: "Store");

            migrationBuilder.DropIndex(
                name: "IX_Store_AddressId",
                table: "Store");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Store",
                newName: "AddressID");

            migrationBuilder.AlterColumn<int>(
                name: "AddressID",
                table: "Store",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Store_AddressID",
                table: "Store",
                column: "AddressID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Store_Address_AddressID",
                table: "Store",
                column: "AddressID",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Store_Address_AddressID",
                table: "Store");

            migrationBuilder.DropIndex(
                name: "IX_Store_AddressID",
                table: "Store");

            migrationBuilder.RenameColumn(
                name: "AddressID",
                table: "Store",
                newName: "AddressId");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "Store",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Store_AddressId",
                table: "Store",
                column: "AddressId",
                unique: true,
                filter: "[AddressId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Store_Address_AddressId",
                table: "Store",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
