using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalSystem.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarClass",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarClass", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateGenerated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReportDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicensePlate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false),
                    PricePerDay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gearbox = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FuelType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FuelUsage = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Car_CarClass_ClassId",
                        column: x => x.ClassId,
                        principalTable: "CarClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Car_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarFeature",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarFeature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarFeature_Car_CarId",
                        column: x => x.CarId,
                        principalTable: "Car",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PricingTier",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MinDays = table.Column<int>(type: "int", nullable: false),
                    MaxDays = table.Column<int>(type: "int", nullable: false),
                    PricePerDay = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricingTier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PricingTier_Car_CarId",
                        column: x => x.CarId,
                        principalTable: "Car",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservation_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservation_Car_CarId",
                        column: x => x.CarId,
                        principalTable: "Car",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservation_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    ReservationId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_Reservation_ReservationId1",
                        column: x => x.ReservationId1,
                        principalTable: "Reservation",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "CarClass",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("44174ecc-ba3f-4a7f-80e2-0d4b195d6e2f"), "SUV" },
                    { new Guid("7abc5447-4fed-41ba-8c3a-5422da1ea921"), "Van" },
                    { new Guid("86a055a5-19c2-48e5-9e0f-cf9c2d065a82"), "Small" },
                    { new Guid("8c740178-11a2-4201-a8fb-bd35fbb7e604"), "Compact" },
                    { new Guid("9eb7fe2f-67f9-44c0-917f-077a5994a7c2"), "Luxury" }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("006c7f03-2944-472c-9d78-9bb42c841c9c"), "Luxury" },
                    { new Guid("44a0794d-5360-42ba-a276-a1b4cac7ff6e"), "Standard" },
                    { new Guid("5325d50a-c054-42c2-a3f2-7dde39ae8721"), "Economy" },
                    { new Guid("5f6b83b4-a93e-4a9d-8e46-46a4977c69a9"), "Business" },
                    { new Guid("f1e8e72b-e7eb-45b6-b288-9559047444db"), "Family" }
                });

            migrationBuilder.InsertData(
                table: "Car",
                columns: new[] { "Id", "CategoryId", "ClassId", "FuelType", "FuelUsage", "Gearbox", "ImagePath", "IsAvailable", "LicensePlate", "Name", "PricePerDay", "Year" },
                values: new object[,]
                {
                    { new Guid("348897f5-935a-4523-b69b-f13e9df249ab"), new Guid("5325d50a-c054-42c2-a3f2-7dde39ae8721"), new Guid("86a055a5-19c2-48e5-9e0f-cf9c2d065a82"), "Petrol", 3.8m, "Manual", "/images/hyundai_i10.jpg", true, "BB-222-CC", "Hyundai i10", 10m, 2019 },
                    { new Guid("44043075-a6cb-46f2-aadf-f3e1d610b8b0"), new Guid("f1e8e72b-e7eb-45b6-b288-9559047444db"), new Guid("7abc5447-4fed-41ba-8c3a-5422da1ea921"), "Diesel", 7.5m, "Manual", "/images/ford_transit.jpg", true, "JJ-101-KK", "Ford Transit", 38m, 2022 },
                    { new Guid("4d8c916f-a893-4b71-a472-9dd2a47f0a50"), new Guid("006c7f03-2944-472c-9d78-9bb42c841c9c"), new Guid("9eb7fe2f-67f9-44c0-917f-077a5994a7c2"), "Petrol", 9m, "Automatic", "/images/mercedes_s_class.jpg", true, "GG-777-HH", "Mercedes-Benz S-Class", 85m, 2023 },
                    { new Guid("8039d225-76e1-4477-8236-e5c354153f91"), new Guid("5325d50a-c054-42c2-a3f2-7dde39ae8721"), new Guid("86a055a5-19c2-48e5-9e0f-cf9c2d065a82"), "Petrol", 4m, "Manual", "/images/fiat_panda.jpg", true, "AA-111-BB", "Fiat Panda", 12m, 2020 },
                    { new Guid("9587fc17-a4c6-487c-be94-b7b6fc9e3678"), new Guid("f1e8e72b-e7eb-45b6-b288-9559047444db"), new Guid("44174ecc-ba3f-4a7f-80e2-0d4b195d6e2f"), "Hybrid", 4.2m, "Automatic", "/images/toyota_rav4.jpg", true, "EE-555-FF", "Toyota RAV4", 25m, 2022 },
                    { new Guid("9a79c6e7-38ca-4c94-81c9-9d359fa692f6"), new Guid("5f6b83b4-a93e-4a9d-8e46-46a4977c69a9"), new Guid("9eb7fe2f-67f9-44c0-917f-077a5994a7c2"), "Diesel", 8.5m, "Automatic", "/images/bmw_7_series.jpg", true, "HH-888-II", "BMW 7 Series", 80m, 2023 },
                    { new Guid("a185a4d2-5fc3-4c53-89d8-45c83e1a815c"), new Guid("44a0794d-5360-42ba-a276-a1b4cac7ff6e"), new Guid("8c740178-11a2-4201-a8fb-bd35fbb7e604"), "Petrol", 4.8m, "Manual", "/images/ford_focus.jpg", true, "DD-444-EE", "Ford Focus", 17m, 2020 },
                    { new Guid("a62d2f76-6fe0-4e21-935b-6b0e2d933f01"), new Guid("f1e8e72b-e7eb-45b6-b288-9559047444db"), new Guid("44174ecc-ba3f-4a7f-80e2-0d4b195d6e2f"), "Petrol", 6m, "Automatic", "/images/honda_crv.jpg", true, "FF-666-GG", "Honda CR-V", 27m, 2023 },
                    { new Guid("bcba206b-c474-48d8-85b1-5bf3a09891a1"), new Guid("f1e8e72b-e7eb-45b6-b288-9559047444db"), new Guid("7abc5447-4fed-41ba-8c3a-5422da1ea921"), "Diesel", 8m, "Manual", "/images/vw_transporter.jpg", true, "II-999-JJ", "Volkswagen Transporter", 40m, 2021 },
                    { new Guid("e2a5c157-5f55-4543-af53-b549a0a9e75b"), new Guid("44a0794d-5360-42ba-a276-a1b4cac7ff6e"), new Guid("8c740178-11a2-4201-a8fb-bd35fbb7e604"), "Diesel", 5.1m, "Automatic", "/images/vw_golf.jpg", true, "CC-333-DD", "Volkswagen Golf", 18m, 2021 }
                });

            migrationBuilder.InsertData(
                table: "CarFeature",
                columns: new[] { "Id", "CarId", "Name" },
                values: new object[,]
                {
                    { new Guid("169f5a2c-fdae-4510-aee2-cf4bc61bc184"), new Guid("a185a4d2-5fc3-4c53-89d8-45c83e1a815c"), "Central Locking" },
                    { new Guid("1e94c9a8-6a54-4703-98c2-a393a2208ce0"), new Guid("348897f5-935a-4523-b69b-f13e9df249ab"), "A/C" },
                    { new Guid("3157782e-e936-437f-bfaf-52ff7271c15e"), new Guid("348897f5-935a-4523-b69b-f13e9df249ab"), "Central Locking" },
                    { new Guid("31cf6dff-b3c0-4ad7-ae2f-aedea3622eae"), new Guid("a62d2f76-6fe0-4e21-935b-6b0e2d933f01"), "Central Locking" },
                    { new Guid("34a8c490-0f35-4a18-92f5-6d2f6be82a5f"), new Guid("44043075-a6cb-46f2-aadf-f3e1d610b8b0"), "Electric Windows" },
                    { new Guid("3908abaf-9ee6-48a3-814c-6d919e8c3713"), new Guid("44043075-a6cb-46f2-aadf-f3e1d610b8b0"), "Airbags" },
                    { new Guid("444e8b11-75e7-4964-b1ce-42883b9185f2"), new Guid("bcba206b-c474-48d8-85b1-5bf3a09891a1"), "A/C" },
                    { new Guid("48b0b616-ab16-4da8-89e9-bb893f25c4a7"), new Guid("4d8c916f-a893-4b71-a472-9dd2a47f0a50"), "A/C" },
                    { new Guid("4d96f1a2-3b7b-4744-88f6-1ba13f1a973a"), new Guid("9587fc17-a4c6-487c-be94-b7b6fc9e3678"), "A/C" },
                    { new Guid("50481f00-b38c-4ed6-b8a4-f5bb057b2fa3"), new Guid("348897f5-935a-4523-b69b-f13e9df249ab"), "Airbags" },
                    { new Guid("56a0a808-8e86-4470-97a4-9f4060228df0"), new Guid("a62d2f76-6fe0-4e21-935b-6b0e2d933f01"), "Electric Windows" },
                    { new Guid("5d61f2c2-7826-43f0-92df-fe8c2c7c8085"), new Guid("8039d225-76e1-4477-8236-e5c354153f91"), "Central Locking" },
                    { new Guid("682baafe-8403-4c09-9d6f-eeea43c933cb"), new Guid("4d8c916f-a893-4b71-a472-9dd2a47f0a50"), "Electric Windows" },
                    { new Guid("6eb27f45-7283-46d2-bae6-f4ef62b2c964"), new Guid("bcba206b-c474-48d8-85b1-5bf3a09891a1"), "Central Locking" },
                    { new Guid("6f76dde6-3e63-47c7-ba64-4f7fd651d5a4"), new Guid("a185a4d2-5fc3-4c53-89d8-45c83e1a815c"), "Airbags" },
                    { new Guid("72eac944-4910-4054-837e-e356f155f65c"), new Guid("348897f5-935a-4523-b69b-f13e9df249ab"), "Electric Windows" },
                    { new Guid("77d22e70-8233-47c9-a89e-5fa949b950d4"), new Guid("9a79c6e7-38ca-4c94-81c9-9d359fa692f6"), "A/C" },
                    { new Guid("77d363e0-6968-4258-aea6-a468ab7090d4"), new Guid("4d8c916f-a893-4b71-a472-9dd2a47f0a50"), "Central Locking" },
                    { new Guid("78e95788-f6c7-4446-b499-070e3b98b1a4"), new Guid("9a79c6e7-38ca-4c94-81c9-9d359fa692f6"), "Central Locking" },
                    { new Guid("83ea80ba-fce6-479d-8ffd-5dc080f4f2e8"), new Guid("9587fc17-a4c6-487c-be94-b7b6fc9e3678"), "Airbags" },
                    { new Guid("92746b75-1b14-4a2f-b0ab-d9c1c80061cb"), new Guid("bcba206b-c474-48d8-85b1-5bf3a09891a1"), "Airbags" },
                    { new Guid("93fcdc19-375c-4675-8977-f51d8e7ceec9"), new Guid("e2a5c157-5f55-4543-af53-b549a0a9e75b"), "Electric Windows" },
                    { new Guid("a3961e2a-fe54-434b-aded-99485bd2d90d"), new Guid("9a79c6e7-38ca-4c94-81c9-9d359fa692f6"), "Electric Windows" },
                    { new Guid("ad38ae4d-731b-4bbe-bbe1-f94fd8d6e336"), new Guid("44043075-a6cb-46f2-aadf-f3e1d610b8b0"), "A/C" },
                    { new Guid("b3745b9f-327f-4e9b-9cb3-f8cc0e3eb919"), new Guid("e2a5c157-5f55-4543-af53-b549a0a9e75b"), "A/C" },
                    { new Guid("b3a09dd1-8bef-44db-94d6-92ebb9d0c4e7"), new Guid("8039d225-76e1-4477-8236-e5c354153f91"), "Electric Windows" },
                    { new Guid("b55d852e-d4dc-4ad7-927c-bfd2bb15f6cd"), new Guid("e2a5c157-5f55-4543-af53-b549a0a9e75b"), "Airbags" },
                    { new Guid("b5ae7c6d-0546-47ed-894a-263db255e2cb"), new Guid("e2a5c157-5f55-4543-af53-b549a0a9e75b"), "Central Locking" },
                    { new Guid("bf167d75-a746-4e02-9714-6b7dd00babff"), new Guid("a62d2f76-6fe0-4e21-935b-6b0e2d933f01"), "A/C" },
                    { new Guid("c17a1a7e-59c1-4c3f-9ab3-552c14d762b2"), new Guid("9a79c6e7-38ca-4c94-81c9-9d359fa692f6"), "Airbags" },
                    { new Guid("c6b33e51-3901-44b1-8d09-8bb84db6db88"), new Guid("9587fc17-a4c6-487c-be94-b7b6fc9e3678"), "Central Locking" },
                    { new Guid("c76af3ae-5175-456a-924d-b95d4d48c683"), new Guid("a185a4d2-5fc3-4c53-89d8-45c83e1a815c"), "Electric Windows" },
                    { new Guid("cb906912-480e-4180-a0dd-7a0bbdcc4604"), new Guid("bcba206b-c474-48d8-85b1-5bf3a09891a1"), "Electric Windows" },
                    { new Guid("d4ad237d-7a40-4a5b-baa3-9418a6c93da4"), new Guid("8039d225-76e1-4477-8236-e5c354153f91"), "Airbags" },
                    { new Guid("d7bd88e9-b896-49c0-9347-ff8dae64fede"), new Guid("4d8c916f-a893-4b71-a472-9dd2a47f0a50"), "Airbags" },
                    { new Guid("d98a0eeb-ae20-42c7-ad4f-82fd7b206499"), new Guid("44043075-a6cb-46f2-aadf-f3e1d610b8b0"), "Central Locking" },
                    { new Guid("e17ea914-5082-453d-938c-ff4456aaf0f7"), new Guid("a62d2f76-6fe0-4e21-935b-6b0e2d933f01"), "Airbags" },
                    { new Guid("ea9b71fd-7ea7-44ce-b327-9d410bad05f6"), new Guid("9587fc17-a4c6-487c-be94-b7b6fc9e3678"), "Electric Windows" },
                    { new Guid("fa943172-b74b-49f3-aec4-d11637e2bc98"), new Guid("8039d225-76e1-4477-8236-e5c354153f91"), "A/C" },
                    { new Guid("fc6565dd-df9c-41a9-a1ea-0dc2f06ef5d3"), new Guid("a185a4d2-5fc3-4c53-89d8-45c83e1a815c"), "A/C" }
                });

            migrationBuilder.InsertData(
                table: "PricingTier",
                columns: new[] { "Id", "CarId", "MaxDays", "MinDays", "PricePerDay" },
                values: new object[,]
                {
                    { new Guid("0d5bbe3b-f54c-4694-808f-c0d89af75a61"), new Guid("a185a4d2-5fc3-4c53-89d8-45c83e1a815c"), 365, 21, 12.75m },
                    { new Guid("1dea457f-92c3-4a95-9e47-00218af2092d"), new Guid("e2a5c157-5f55-4543-af53-b549a0a9e75b"), 20, 16, 14.4m }
                });

            migrationBuilder.InsertData(
                table: "PricingTier",
                columns: new[] { "Id", "CarId", "MaxDays", "MinDays", "PricePerDay" },
                values: new object[,]
                {
                    { new Guid("2271d4c7-ae33-4bfa-b81a-dae1f06b79b9"), new Guid("e2a5c157-5f55-4543-af53-b549a0a9e75b"), 10, 4, 16.2m },
                    { new Guid("2dc92b7b-1831-434d-bac6-983b4506a075"), new Guid("44043075-a6cb-46f2-aadf-f3e1d610b8b0"), 3, 1, 38m },
                    { new Guid("2ee69fb1-dce1-43c1-b3e6-3f5889acc6a0"), new Guid("a62d2f76-6fe0-4e21-935b-6b0e2d933f01"), 365, 21, 20.25m },
                    { new Guid("3156b2d5-4318-4da6-a610-d49667e12272"), new Guid("a62d2f76-6fe0-4e21-935b-6b0e2d933f01"), 20, 16, 21.6m },
                    { new Guid("34395a06-535c-4c82-a5d4-5113b2f987eb"), new Guid("a185a4d2-5fc3-4c53-89d8-45c83e1a815c"), 3, 1, 17m },
                    { new Guid("3567fd71-7065-4b00-850f-ff5f9d12b5d6"), new Guid("a62d2f76-6fe0-4e21-935b-6b0e2d933f01"), 3, 1, 27m },
                    { new Guid("3b2b29cb-c31f-4525-a092-ab7c77bc91d4"), new Guid("44043075-a6cb-46f2-aadf-f3e1d610b8b0"), 20, 16, 30.4m },
                    { new Guid("3c5c902e-bede-4033-a520-13547a108954"), new Guid("44043075-a6cb-46f2-aadf-f3e1d610b8b0"), 365, 21, 28.50m },
                    { new Guid("53de1a13-41eb-48a9-b340-8181bb9f7adb"), new Guid("9587fc17-a4c6-487c-be94-b7b6fc9e3678"), 15, 11, 21.25m },
                    { new Guid("5b228642-a53f-43b2-82b7-6570199b9faf"), new Guid("348897f5-935a-4523-b69b-f13e9df249ab"), 20, 16, 8.0m },
                    { new Guid("5d751021-c45f-48b5-a04d-6bf4019202bd"), new Guid("4d8c916f-a893-4b71-a472-9dd2a47f0a50"), 365, 21, 63.75m },
                    { new Guid("5ec8349c-4c83-4180-a784-cf6b38464043"), new Guid("8039d225-76e1-4477-8236-e5c354153f91"), 15, 11, 10.20m },
                    { new Guid("60fe7e9c-c455-466a-9d76-c11e87b4e3bd"), new Guid("bcba206b-c474-48d8-85b1-5bf3a09891a1"), 3, 1, 40m },
                    { new Guid("659efff6-8f28-475a-a8de-dbff76851a2f"), new Guid("bcba206b-c474-48d8-85b1-5bf3a09891a1"), 15, 11, 34.00m },
                    { new Guid("65af53d4-aee8-4b5a-b6e1-31a9d39cfc19"), new Guid("a185a4d2-5fc3-4c53-89d8-45c83e1a815c"), 15, 11, 14.45m },
                    { new Guid("6d88bdd4-9ccc-422c-a20d-e71ee8cfca25"), new Guid("a185a4d2-5fc3-4c53-89d8-45c83e1a815c"), 20, 16, 13.6m },
                    { new Guid("6dff2f51-bcd9-4a59-bead-01c9bca9e024"), new Guid("9a79c6e7-38ca-4c94-81c9-9d359fa692f6"), 10, 4, 72.0m },
                    { new Guid("7b64b5da-59ba-4358-9b18-fc8216ee537f"), new Guid("44043075-a6cb-46f2-aadf-f3e1d610b8b0"), 10, 4, 34.2m },
                    { new Guid("86b3ba12-8236-485c-a7c0-9e8970d35779"), new Guid("e2a5c157-5f55-4543-af53-b549a0a9e75b"), 365, 21, 13.50m },
                    { new Guid("8a09bffe-9bdf-4153-800a-01416887f970"), new Guid("8039d225-76e1-4477-8236-e5c354153f91"), 365, 21, 9.00m },
                    { new Guid("8d7a200c-c904-4e2c-8baa-4fff7432764b"), new Guid("9587fc17-a4c6-487c-be94-b7b6fc9e3678"), 365, 21, 18.75m },
                    { new Guid("8ec44ac9-a8ef-483f-b997-8e6a171a2cce"), new Guid("9a79c6e7-38ca-4c94-81c9-9d359fa692f6"), 15, 11, 68.00m },
                    { new Guid("9c7cfe52-6293-4217-961f-e30ec670ca38"), new Guid("4d8c916f-a893-4b71-a472-9dd2a47f0a50"), 3, 1, 85m },
                    { new Guid("9fdd37b5-011f-423f-95bc-b3ca64ea697f"), new Guid("9a79c6e7-38ca-4c94-81c9-9d359fa692f6"), 20, 16, 64.0m },
                    { new Guid("a1e16edc-6c38-4f68-be66-95eba8ffdc72"), new Guid("e2a5c157-5f55-4543-af53-b549a0a9e75b"), 3, 1, 18m },
                    { new Guid("a2e0a90c-d7db-4c27-8b55-d0a0e69708f1"), new Guid("4d8c916f-a893-4b71-a472-9dd2a47f0a50"), 15, 11, 72.25m },
                    { new Guid("a3332580-33a6-45da-856c-6fd284553b89"), new Guid("9a79c6e7-38ca-4c94-81c9-9d359fa692f6"), 365, 21, 60.00m },
                    { new Guid("a4cbcff0-3510-4fa6-bd58-ff20b3e63cb6"), new Guid("4d8c916f-a893-4b71-a472-9dd2a47f0a50"), 20, 16, 68.0m },
                    { new Guid("a715d5db-6b5e-4db6-b263-1e4d8470836d"), new Guid("8039d225-76e1-4477-8236-e5c354153f91"), 20, 16, 9.6m },
                    { new Guid("b16529cc-e108-41ae-809f-e7e622587447"), new Guid("348897f5-935a-4523-b69b-f13e9df249ab"), 365, 21, 7.50m },
                    { new Guid("b7463c76-6b5c-4ede-98c6-495c6545121b"), new Guid("bcba206b-c474-48d8-85b1-5bf3a09891a1"), 20, 16, 32.0m },
                    { new Guid("c23adba4-f770-4e03-9aac-0fa3a81c2fc2"), new Guid("e2a5c157-5f55-4543-af53-b549a0a9e75b"), 15, 11, 15.30m },
                    { new Guid("c72c9545-4f57-4ea6-a792-e0bc8b5818c9"), new Guid("bcba206b-c474-48d8-85b1-5bf3a09891a1"), 10, 4, 36.0m },
                    { new Guid("c8585406-13fa-4c6c-b19e-62e28d1efdc1"), new Guid("bcba206b-c474-48d8-85b1-5bf3a09891a1"), 365, 21, 30.00m },
                    { new Guid("c8887c64-87df-44ec-a5a9-5311ba94abd9"), new Guid("348897f5-935a-4523-b69b-f13e9df249ab"), 15, 11, 8.50m },
                    { new Guid("cc298124-08df-48ad-b4a7-190d1a7b5ac2"), new Guid("9a79c6e7-38ca-4c94-81c9-9d359fa692f6"), 3, 1, 80m },
                    { new Guid("cf834d92-f7c7-4760-bb3a-ac00b45be4d8"), new Guid("9587fc17-a4c6-487c-be94-b7b6fc9e3678"), 3, 1, 25m },
                    { new Guid("d1a85e79-d7e3-44fc-80da-b6653d350648"), new Guid("a185a4d2-5fc3-4c53-89d8-45c83e1a815c"), 10, 4, 15.3m },
                    { new Guid("da21779d-f893-4aff-8e1d-43100d2a25d7"), new Guid("a62d2f76-6fe0-4e21-935b-6b0e2d933f01"), 15, 11, 22.95m },
                    { new Guid("df2f1940-ac65-40b3-9c87-9573b4dc35e7"), new Guid("8039d225-76e1-4477-8236-e5c354153f91"), 10, 4, 10.8m },
                    { new Guid("e03c2ac6-ddf9-4474-870e-4c5abde1fb10"), new Guid("8039d225-76e1-4477-8236-e5c354153f91"), 3, 1, 12m },
                    { new Guid("e3e233b3-1999-48c3-922a-f3cb120c3f99"), new Guid("44043075-a6cb-46f2-aadf-f3e1d610b8b0"), 15, 11, 32.30m }
                });

            migrationBuilder.InsertData(
                table: "PricingTier",
                columns: new[] { "Id", "CarId", "MaxDays", "MinDays", "PricePerDay" },
                values: new object[,]
                {
                    { new Guid("ea4b7f50-4ce6-4806-8383-85787fea5c93"), new Guid("348897f5-935a-4523-b69b-f13e9df249ab"), 3, 1, 10m },
                    { new Guid("ee514a9d-7f0a-4c22-a1c3-76af8caaedd2"), new Guid("9587fc17-a4c6-487c-be94-b7b6fc9e3678"), 20, 16, 20.0m },
                    { new Guid("eeee6195-f8ae-4566-9957-183239b2cce5"), new Guid("348897f5-935a-4523-b69b-f13e9df249ab"), 10, 4, 9.0m },
                    { new Guid("f0ee34ca-4bc8-4f71-9a1e-ee53cc83ba9f"), new Guid("4d8c916f-a893-4b71-a472-9dd2a47f0a50"), 10, 4, 76.5m },
                    { new Guid("f28a1667-2a7d-405e-8ece-2080f7dd641a"), new Guid("a62d2f76-6fe0-4e21-935b-6b0e2d933f01"), 10, 4, 24.3m },
                    { new Guid("fb5d0f43-1227-41de-9303-1531c3d16df5"), new Guid("9587fc17-a4c6-487c-be94-b7b6fc9e3678"), 10, 4, 22.5m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_UserId",
                table: "AspNetRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Car_CategoryId",
                table: "Car",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Car_ClassId",
                table: "Car",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_CarFeature_CarId",
                table: "CarFeature",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_ReservationId1",
                table: "Payment",
                column: "ReservationId1");

            migrationBuilder.CreateIndex(
                name: "IX_PricingTier_CarId",
                table: "PricingTier",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_UserId1",
                table: "Report",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_CarId",
                table: "Reservation",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_StatusId",
                table: "Reservation",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_UserId",
                table: "Reservation",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CarFeature");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "PricingTier");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Car");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "CarClass");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
