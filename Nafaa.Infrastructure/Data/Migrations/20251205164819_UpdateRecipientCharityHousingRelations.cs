using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nafaa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRecipientCharityHousingRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Housings_Recipients_RecipientId",
                table: "Housings");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipients_Charities_CharityId",
                table: "Recipients");

            migrationBuilder.DropIndex(
                name: "IX_Recipients_CharityId",
                table: "Recipients");

            migrationBuilder.DropIndex(
                name: "IX_Housings_RecipientId",
                table: "Housings");

            migrationBuilder.DropColumn(
                name: "CharityId",
                table: "Recipients");

            migrationBuilder.CreateTable(
                name: "CharityRecipient",
                columns: table => new
                {
                    CharityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharityRecipient", x => new { x.CharityId, x.RecipientId });
                    table.ForeignKey(
                        name: "FK_CharityRecipient_Charities_CharityId",
                        column: x => x.CharityId,
                        principalTable: "Charities",
                        principalColumn: "CharityId");
                    table.ForeignKey(
                        name: "FK_CharityRecipient_Recipients_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Recipients",
                        principalColumn: "RecipientId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Housings_RecipientId",
                table: "Housings",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_CharityRecipient_RecipientId",
                table: "CharityRecipient",
                column: "RecipientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Housings_Recipients_RecipientId",
                table: "Housings",
                column: "RecipientId",
                principalTable: "Recipients",
                principalColumn: "RecipientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Housings_Recipients_RecipientId",
                table: "Housings");

            migrationBuilder.DropTable(
                name: "CharityRecipient");

            migrationBuilder.DropIndex(
                name: "IX_Housings_RecipientId",
                table: "Housings");

            migrationBuilder.AddColumn<Guid>(
                name: "CharityId",
                table: "Recipients",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_CharityId",
                table: "Recipients",
                column: "CharityId");

            migrationBuilder.CreateIndex(
                name: "IX_Housings_RecipientId",
                table: "Housings",
                column: "RecipientId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Housings_Recipients_RecipientId",
                table: "Housings",
                column: "RecipientId",
                principalTable: "Recipients",
                principalColumn: "RecipientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipients_Charities_CharityId",
                table: "Recipients",
                column: "CharityId",
                principalTable: "Charities",
                principalColumn: "CharityId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
