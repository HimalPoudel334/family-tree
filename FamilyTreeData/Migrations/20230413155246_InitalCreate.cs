using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyTreeData.Migrations
{
    /// <inheritdoc />
    public partial class InitalCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", maxLength: 20, nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    first_name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    wife_name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    father_id = table.Column<int>(type: "INTEGER", maxLength: 20, nullable: false),
                    mother_name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Image = table.Column<string>(type: "TEXT", nullable: false),
                    WifeImage = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_persons_persons_father_id",
                        column: x => x.father_id,
                        principalTable: "persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_persons_father_id",
                table: "persons",
                column: "father_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "persons");
        }
    }
}
