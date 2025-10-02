using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SevSU.HabitsTracker.Identity.Api.Migrations
{
    /// <inheritdoc />
    public partial class User_index : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_users_email_username",
                table: "users");

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_username",
                table: "users",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_users_email",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_users_username",
                table: "users");

            migrationBuilder.CreateIndex(
                name: "ix_users_email_username",
                table: "users",
                columns: new[] { "email", "username" },
                unique: true);
        }
    }
}
