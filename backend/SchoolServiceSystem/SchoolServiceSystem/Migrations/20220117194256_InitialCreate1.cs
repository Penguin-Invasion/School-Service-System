using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolServiceSystem.Migrations
{
    public partial class InitialCreate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SurName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecretKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Schools_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plaque = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriverID = table.Column<int>(type: "int", nullable: false),
                    SchoolID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Services_Schools_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "Schools",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Services_Users_DriverID",
                        column: x => x.DriverID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Entries",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceID = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entries", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Entries_Services_ServiceID",
                        column: x => x.ServiceID,
                        principalTable: "Services",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SurName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false),
                    ServiceID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Students_Services_ServiceID",
                        column: x => x.ServiceID,
                        principalTable: "Services",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "Email", "Name", "Password", "Role", "SurName" },
                values: new object[,]
                {
                    { 1, "admin", "Admin", "123", 0, "Test" },
                    { 2, "manager1", "Manager1", "123", 1, "Test" },
                    { 3, "manager2", "Manager2", "123", 1, "Test" },
                    { 4, "driver1", "Driver1", "123", 2, "Test" },
                    { 5, "driver2", "Driver2", "123", 2, "Test" }
                });

            migrationBuilder.InsertData(
                table: "Schools",
                columns: new[] { "ID", "Name", "SecretKey", "UserID" },
                values: new object[] { 1, "Test Okul1", "sadasdasd", 2 });

            migrationBuilder.InsertData(
                table: "Schools",
                columns: new[] { "ID", "Name", "SecretKey", "UserID" },
                values: new object[] { 2, "Test Okul1", "sadasdasd", 3 });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ID", "DriverID", "Name", "Plaque", "SchoolID" },
                values: new object[] { 1, 4, "Test Servis1", "34 A 0001", 1 });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ID", "DriverID", "Name", "Plaque", "SchoolID" },
                values: new object[] { 2, 5, "Test Servis2", "34 A 0002", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Entries_ServiceID",
                table: "Entries",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_UserID",
                table: "Schools",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_DriverID",
                table: "Services",
                column: "DriverID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_SchoolID",
                table: "Services",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ServiceID",
                table: "Students",
                column: "ServiceID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entries");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Schools");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
