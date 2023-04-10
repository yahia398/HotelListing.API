using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable


namespace HotelListing.API.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultIdentityRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "51a64d92-69e5-41c2-a772-a8a786314820", null, "Admin", "ADMIN" },
                    { "fb20c2d2-1f59-428b-b3f3-e5b1ffe1fa45", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51a64d92-69e5-41c2-a772-a8a786314820");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fb20c2d2-1f59-428b-b3f3-e5b1ffe1fa45");
        }
    }
}
