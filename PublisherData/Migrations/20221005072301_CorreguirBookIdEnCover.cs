using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublisherData.Migrations
{
    public partial class CorreguirBookIdEnCover : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Covers",
                keyColumn: "CoverId",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "Covers",
                columns: new[] { "CoverId", "BookId", "DesignIdeas", "DigitalOnly" },
                values: new object[] { 1, 3, "How about a left hand in the dark?", false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Covers",
                keyColumn: "CoverId",
                keyValue: 1);
        }
    }
}
