using Microsoft.EntityFrameworkCore.Migrations;

namespace business_manager_api.Migrations
{
    public partial class business_manager_mvc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IdentificationData",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    TVA = table.Column<string>(nullable: true),
                    EmailPro = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Logo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentificationData", x => x.Id);
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
                    UrlLinkedIn = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessInfo_IdentificationData_AddressId",
                        column: x => x.AddressId,
                        principalTable: "IdentificationData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BusinessData",
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
                    table.PrimaryKey("PK_BusinessData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessData_BusinessInfo_BusinessInfoId",
                        column: x => x.BusinessInfoId,
                        principalTable: "BusinessInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BusinessData_IdentificationData_IdentificationDataId",
                        column: x => x.IdentificationDataId,
                        principalTable: "IdentificationData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BusinessImage",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessId = table.Column<long>(nullable: false),
                    ImageData = table.Column<string>(nullable: true),
                    BusinessDataModelId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessImage_BusinessData_BusinessDataModelId",
                        column: x => x.BusinessDataModelId,
                        principalTable: "BusinessData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessData_BusinessInfoId",
                table: "BusinessData",
                column: "BusinessInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessData_IdentificationDataId",
                table: "BusinessData",
                column: "IdentificationDataId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImage_BusinessDataModelId",
                table: "BusinessImage",
                column: "BusinessDataModelId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessInfo_AddressId",
                table: "BusinessInfo",
                column: "AddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessImage");

            migrationBuilder.DropTable(
                name: "BusinessData");

            migrationBuilder.DropTable(
                name: "BusinessInfo");

            migrationBuilder.DropTable(
                name: "IdentificationData");
        }
    }
}
