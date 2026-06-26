using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pocket_service.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UnitNumber = table.Column<int>(type: "integer", nullable: false),
                    HouseNumber = table.Column<int>(type: "integer", nullable: false),
                    StreetName = table.Column<string>(type: "text", nullable: false),
                    PostCode = table.Column<int>(type: "integer", nullable: false),
                    Suburb = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<string>(type: "text", nullable: false),
                    Latitude = table.Column<int>(type: "integer", nullable: false),
                    Longitude = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    DrivingLicense = table.Column<string>(type: "text", nullable: false),
                    EmailVerified = table.Column<bool>(type: "boolean", nullable: false),
                    EmailTokenUsed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordForgetToken = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordForgetTokenUsed = table.Column<bool>(type: "boolean", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    AddressId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mechanics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MechanicLicense = table.Column<string>(type: "text", nullable: false),
                    MechanicVerified = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mechanics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mechanics_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RevokeAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ReplacedByToken = table.Column<string>(type: "text", nullable: true),
                    RemoteIp = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleType = table.Column<string>(type: "text", nullable: false),
                    Make = table.Column<string>(type: "text", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Vin = table.Column<string>(type: "text", nullable: false),
                    LicensePlate = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceType = table.Column<string>(type: "text", nullable: false),
                    ServiceDescription = table.Column<string>(type: "text", nullable: false),
                    ServiceDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ServiceCost = table.Column<decimal>(type: "numeric", nullable: false),
                    DateOfPayment = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ServiceStatus = table.Column<int>(type: "integer", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uuid", nullable: false),
                    MechanicId = table.Column<Guid>(type: "uuid", nullable: false),
                    AddressId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarServices_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarServices_Mechanics_MechanicId",
                        column: x => x.MechanicId,
                        principalTable: "Mechanics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarServices_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarServices_AddressId",
                table: "CarServices",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_CarServices_MechanicId",
                table: "CarServices",
                column: "MechanicId");

            migrationBuilder.CreateIndex(
                name: "IX_CarServices_VehicleId",
                table: "CarServices",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Mechanics_UserId",
                table: "Mechanics",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AddressId",
                table: "Users",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_UserId",
                table: "Vehicles",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarServices");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Mechanics");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
