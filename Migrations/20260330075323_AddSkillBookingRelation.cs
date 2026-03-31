using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillSwapBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddSkillBookingRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SkillId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_SkillId",
                table: "Bookings",
                column: "SkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Skills_SkillId",
                table: "Bookings",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Skills_SkillId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_SkillId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "SkillId",
                table: "Bookings");
        }
    }
}
