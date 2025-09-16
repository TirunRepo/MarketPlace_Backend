using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketPlace.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CruiseLines",
                columns: table => new
                {
                    CruiseLineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CruiseLineName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CruiseLineCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CruiseLines", x => x.CruiseLineId);
                });

            migrationBuilder.CreateTable(
                name: "Destinations",
                columns: table => new
                {
                    DestinationCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DestinationName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinations", x => x.DestinationCode);
                });

            migrationBuilder.CreateTable(
                name: "MarkupDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinMarkup = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaxMarkup = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MinBaseFare = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaxBaseFare = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MarkupPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SupplierId = table.Column<int>(type: "int", nullable: true),
                    SailingId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarkupDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PromotionType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CruiseShips",
                columns: table => new
                {
                    CruiseShipId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShipName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ShipCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CruiseLineId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CruiseShips", x => x.CruiseShipId);
                    table.ForeignKey(
                        name: "FK_CruiseShips_CruiseLines_CruiseLineId",
                        column: x => x.CruiseLineId,
                        principalTable: "CruiseLines",
                        principalColumn: "CruiseLineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeparturePorts",
                columns: table => new
                {
                    DeparturePortId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeparturePortCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DeparturePortName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DestinationCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeparturePorts", x => x.DeparturePortId);
                    table.ForeignKey(
                        name: "FK_DeparturePorts_Destinations_DestinationCode",
                        column: x => x.DestinationCode,
                        principalTable: "Destinations",
                        principalColumn: "DestinationCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Promotions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromotionTypeId = table.Column<int>(type: "int", nullable: false),
                    PromotionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PromotionDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountPer = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    PromoCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoyaltyLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFirstTimeCustomer = table.Column<bool>(type: "bit", nullable: true),
                    MinNoOfAdultRequired = table.Column<int>(type: "int", nullable: true),
                    MinNoOfChildRequired = table.Column<int>(type: "int", nullable: true),
                    IsAdultTicketDiscount = table.Column<bool>(type: "bit", nullable: true),
                    IsChildTicketDiscount = table.Column<bool>(type: "bit", nullable: true),
                    MinPassengerAge = table.Column<int>(type: "int", nullable: true),
                    MaxPassengerAge = table.Column<int>(type: "int", nullable: true),
                    PassengerType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CabinCountRequired = table.Column<int>(type: "int", nullable: true),
                    SailingId = table.Column<int>(type: "int", nullable: true),
                    SupplierId = table.Column<int>(type: "int", nullable: true),
                    AffiliateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncludesAirfare = table.Column<bool>(type: "bit", nullable: true),
                    IncludesHotel = table.Column<bool>(type: "bit", nullable: true),
                    IncludesWiFi = table.Column<bool>(type: "bit", nullable: true),
                    IncludesShoreExcursion = table.Column<bool>(type: "bit", nullable: true),
                    OnboardCreditAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    FreeNthPassenger = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsStackable = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyBy = table.Column<int>(type: "int", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Promotions_PromotionType_PromotionTypeId",
                        column: x => x.PromotionTypeId,
                        principalTable: "PromotionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SailDates",
                columns: table => new
                {
                    SaleDateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CruiseShipId = table.Column<int>(type: "int", nullable: false),
                    SailDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SailDates", x => x.SaleDateId);
                    table.ForeignKey(
                        name: "FK_SailDates_CruiseShips_CruiseShipId",
                        column: x => x.CruiseShipId,
                        principalTable: "CruiseShips",
                        principalColumn: "CruiseShipId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                 name: "CruiseInventories",
                 columns: table => new
                 {
                     CruiseInventoryId = table.Column<int>(type: "int", nullable: false)
                         .Annotation("SqlServer:Identity", "1, 1"),
                     SailDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                     GroupId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                     PackageDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                     Nights = table.Column<int>(type: "int", nullable: false),
                     AgencyID = table.Column<int>(type: "int", nullable: false),
                     CruiseShipId = table.Column<int>(type: "int", nullable: false),
                     DestinationCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                     DeparturePortId = table.Column<int>(type: "int", nullable: false),
                     CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                     CreatedBy = table.Column<int>(type: "int", nullable: false),
                     ModifiedBy = table.Column<int>(type: "int", nullable: true),
                     LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                 },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_CruiseInventories", x => x.CruiseInventoryId);
                     table.ForeignKey(
                         name: "FK_CruiseInventories_CruiseShips_CruiseShipId",
                         column: x => x.CruiseShipId,
                         principalTable: "CruiseShips",
                         principalColumn: "CruiseShipId");
                     table.ForeignKey(
                         name: "FK_CruiseInventories_DeparturePorts_DeparturePortId",
                         column: x => x.DeparturePortId,
                         principalTable: "DeparturePorts",
                         principalColumn: "DeparturePortId");
                     table.ForeignKey(
                         name: "FK_CruiseInventories_Destinations_DestinationCode",
                         column: x => x.DestinationCode,
                         principalTable: "Destinations",
                         principalColumn: "DestinationCode");
                 });

            migrationBuilder.CreateTable(
                name: "CruisePricingInventories",
                columns: table => new
                {
                    CruisePricingInventoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CruiseInventoryId = table.Column<int>(type: "int", nullable: false),
                    PriceValidFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PriceValidTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SinglePrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DoublePrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ThreeFourthPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Tax = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Grats = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    NCCF = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CabinCategory = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false),
                    CabinOccupancy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CruisePricingInventories", x => x.CruisePricingInventoryId);
                    table.ForeignKey(
                        name: "FK_CruisePricingInventories_CruiseInventories_CruiseInventoryId",
                        column: x => x.CruiseInventoryId,
                        principalTable: "CruiseInventories",
                        principalColumn: "CruiseInventoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CruisePricingCabin",
                columns: table => new
                {
                    CruisePricingCabinId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CruisePricingInventoryId = table.Column<int>(type: "int", nullable: false),
                    CabinNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CruisePricingCabin", x => x.CruisePricingCabinId);
                    table.ForeignKey(
                        name: "FK_CruisePricingCabin_CruisePricingInventories_CruisePricingInventoryId",
                        column: x => x.CruisePricingInventoryId,
                        principalTable: "CruisePricingInventories",
                        principalColumn: "CruisePricingInventoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CruiseInventories_CruiseShipId",
                table: "CruiseInventories",
                column: "CruiseShipId");

            migrationBuilder.CreateIndex(
                name: "IX_CruiseInventories_DeparturePortId",
                table: "CruiseInventories",
                column: "DeparturePortId");

            migrationBuilder.CreateIndex(
                name: "IX_CruiseInventories_DestinationCode",
                table: "CruiseInventories",
                column: "DestinationCode");

            migrationBuilder.CreateIndex(
                name: "IX_CruisePricingCabin_CruisePricingInventoryId",
                table: "CruisePricingCabin",
                column: "CruisePricingInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CruisePricingInventories_CruiseInventoryId",
                table: "CruisePricingInventories",
                column: "CruiseInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CruiseShips_CruiseLineId",
                table: "CruiseShips",
                column: "CruiseLineId");

            migrationBuilder.CreateIndex(
                name: "IX_DeparturePorts_DestinationCode",
                table: "DeparturePorts",
                column: "DestinationCode");

            migrationBuilder.CreateIndex(
                name: "IX_Promotions_PromotionTypeId",
                table: "Promotions",
                column: "PromotionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SailDates_CruiseShipId",
                table: "SailDates",
                column: "CruiseShipId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CruisePricingCabin");

            migrationBuilder.DropTable(
                name: "MarkupDetail");

            migrationBuilder.DropTable(
                name: "Promotions");

            migrationBuilder.DropTable(
                name: "SailDates");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "CruisePricingInventories");

            migrationBuilder.DropTable(
                name: "PromotionType");

            migrationBuilder.DropTable(
                name: "CruiseInventories");

            migrationBuilder.DropTable(
                name: "CruiseShips");

            migrationBuilder.DropTable(
                name: "DeparturePorts");

            migrationBuilder.DropTable(
                name: "CruiseLines");

            migrationBuilder.DropTable(
                name: "Destinations");
        }
    }
}
