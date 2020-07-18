using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace business_manager_api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostalCode = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    BoxNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessImage",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessId = table.Column<long>(nullable: false),
                    ImageData = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessImage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Identification",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    TVA = table.Column<string>(nullable: true),
                    EmailPro = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    LogoPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityId = table.Column<long>(nullable: false),
                    ImageData = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAccount",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    Profession = table.Column<bool>(nullable: false),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessInfo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressId = table.Column<long>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    EmailBusiness = table.Column<string>(nullable: true),
                    UrlSite = table.Column<string>(nullable: true),
                    UrlInstagram = table.Column<string>(nullable: true),
                    UrlFaceBook = table.Column<string>(nullable: true),
                    UrlLinkedIn = table.Column<string>(nullable: true),
                    PhotoPath1 = table.Column<string>(nullable: true),
                    PhotoPath2 = table.Column<string>(nullable: true),
                    PhotoPath3 = table.Column<string>(nullable: true),
                    PhotoPath4 = table.Column<string>(nullable: true),
                    PhotoPath5 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessInfo_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Business",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentificationDataId = table.Column<long>(nullable: true),
                    BusinessInfoId = table.Column<long>(nullable: true),
                    WorkHours = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Business", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Business_BusinessInfo_BusinessInfoId",
                        column: x => x.BusinessInfoId,
                        principalTable: "BusinessInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Business_Identification_IdentificationDataId",
                        column: x => x.IdentificationDataId,
                        principalTable: "Identification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Business_BusinessInfoId",
                table: "Business",
                column: "BusinessInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Business_IdentificationDataId",
                table: "Business",
                column: "IdentificationDataId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessInfo_AddressId",
                table: "BusinessInfo",
                column: "AddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Business");

            migrationBuilder.DropTable(
                name: "BusinessImage");

            migrationBuilder.DropTable(
                name: "Logo");

            migrationBuilder.DropTable(
                name: "UserAccount");

            migrationBuilder.DropTable(
                name: "BusinessInfo");

            migrationBuilder.DropTable(
                name: "Identification");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
