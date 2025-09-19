using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketPlace.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CruiseCabinPricingInventories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CruiseInventoryId = table.Column<int>(type: "int", nullable: false),
                    PricingType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommisionRate = table.Column<int>(type: "int", nullable: false),
                    SinglePrice = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    DoublePrice = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    TriplePrice = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Tax = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Grats = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Nccf = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CruiseCabinPricingInventories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CruiseInventories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SailDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GroupId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Package = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Nights = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationId = table.Column<int>(type: "int", nullable: false),
                    DeparturePortId = table.Column<int>(type: "int", nullable: false),
                    CruiseLineId = table.Column<int>(type: "int", nullable: false),
                    CruiseShipId = table.Column<int>(type: "int", nullable: false),
                    ShipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stateroom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CabinOccupancy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnableAdmin = table.Column<bool>(type: "bit", nullable: false),
                    EnableAgent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CruiseInventories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CruiseLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CruiseLines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CruiseShips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CruiseLineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CruiseShips", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeparturePorts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DestinationId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeparturePorts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Destinations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinations", x => x.Id);
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

            migrationBuilder.CreateIndex(
                name: "IX_Promotions_PromotionTypeId",
                table: "Promotions",
                column: "PromotionTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CruiseCabinPricingInventories");

            migrationBuilder.DropTable(
                name: "CruiseInventories");

            migrationBuilder.DropTable(
                name: "CruiseLines");

            migrationBuilder.DropTable(
                name: "CruiseShips");

            migrationBuilder.DropTable(
                name: "DeparturePorts");

            migrationBuilder.DropTable(
                name: "Destinations");

            migrationBuilder.DropTable(
                name: "MarkupDetail");

            migrationBuilder.DropTable(
                name: "Promotions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "PromotionType");
        }
    }
}
