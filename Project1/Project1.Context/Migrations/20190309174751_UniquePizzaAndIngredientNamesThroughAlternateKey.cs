using Microsoft.EntityFrameworkCore.Migrations;

namespace Project1.DataAccess.Migrations
{
    public partial class UniquePizzaAndIngredientNamesThroughAlternateKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Pizza",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Ingredient",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Pizza_Name",
                table: "Pizza",
                column: "Name");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Ingredient_Name",
                table: "Ingredient",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Pizza_Name",
                table: "Pizza");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Ingredient_Name",
                table: "Ingredient");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Pizza",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Ingredient",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
