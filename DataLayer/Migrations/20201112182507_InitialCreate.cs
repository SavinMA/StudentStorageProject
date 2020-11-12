using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Name = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UniqueIds",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    UID = table.Column<string>(maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniqueIds", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Gender = table.Column<string>(nullable: false),
                    Surname = table.Column<string>(maxLength: 40, nullable: false),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    Patronomic = table.Column<string>(maxLength: 60, nullable: true),
                    UniqueIDID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Students_UniqueIds_UniqueIDID",
                        column: x => x.UniqueIDID,
                        principalTable: "UniqueIds",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentGroup",
                columns: table => new
                {
                    StudentGuid = table.Column<Guid>(nullable: false),
                    GroupGuid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentGroup", x => new { x.GroupGuid, x.StudentGuid });
                    table.ForeignKey(
                        name: "FK_StudentGroup_Groups_GroupGuid",
                        column: x => x.GroupGuid,
                        principalTable: "Groups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentGroup_Students_StudentGuid",
                        column: x => x.StudentGuid,
                        principalTable: "Students",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroup_StudentGuid",
                table: "StudentGroup",
                column: "StudentGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UniqueIDID",
                table: "Students",
                column: "UniqueIDID");

            migrationBuilder.CreateIndex(
                name: "IX_UniqueIds_UID",
                table: "UniqueIds",
                column: "UID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentGroup");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "UniqueIds");
        }
    }
}
