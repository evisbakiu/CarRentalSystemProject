using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalSystem.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    ReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_Reservation_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CarClass",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("34b15eb8-f1bc-4aa6-a59b-957fc0785cb6"), "Luxury" },
                    { new Guid("444fb1a7-a34c-4147-9a9b-804cc5c351c9"), "Van" },
                    { new Guid("616afdfb-ad19-4718-9b14-77358894a23e"), "Compact" },
                    { new Guid("76f104e3-f55f-4abd-930a-920a68362c0d"), "Small" },
                    { new Guid("edf8edeb-5472-436f-9eaa-899df910a390"), "SUV" }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("c5e18e12-be6e-41c6-9fa3-1d2c8efaa47a"), "Standard" },
                    { new Guid("dbc76010-4a1d-4480-aed2-1b4ba185cc76"), "Business" },
                    { new Guid("dc18e284-a3ff-4672-8cf2-eeb874108028"), "Economy" },
                    { new Guid("dee98aaf-d073-400b-ac14-2216c86b5a0e"), "Luxury" },
                    { new Guid("f2e93546-78ce-46bc-b9f7-5fd19c49fe99"), "Family" }
                });

            migrationBuilder.InsertData(
                table: "Car",
                columns: new[] { "Id", "CategoryId", "ClassId", "FuelType", "FuelUsage", "Gearbox", "ImagePath", "IsAvailable", "LicensePlate", "Name", "PricePerDay", "Year" },
                values: new object[,]
                {
                    { new Guid("1e1d9a0f-1d9c-4556-a016-fd01dc77343f"), new Guid("f2e93546-78ce-46bc-b9f7-5fd19c49fe99"), new Guid("444fb1a7-a34c-4147-9a9b-804cc5c351c9"), "Diesel", 7.5m, "Manual", "/images/ford_transit.jpg", true, "JJ-101-KK", "Ford Transit", 38m, 2022 },
                    { new Guid("37e46a14-7cdd-4717-ac86-ada2efaab0ee"), new Guid("f2e93546-78ce-46bc-b9f7-5fd19c49fe99"), new Guid("444fb1a7-a34c-4147-9a9b-804cc5c351c9"), "Diesel", 8m, "Manual", "/images/vw_transporter.jpg", true, "II-999-JJ", "Volkswagen Transporter", 40m, 2021 },
                    { new Guid("384ca78c-fed1-4614-bdfd-83ca602fe72a"), new Guid("dbc76010-4a1d-4480-aed2-1b4ba185cc76"), new Guid("34b15eb8-f1bc-4aa6-a59b-957fc0785cb6"), "Diesel", 8.5m, "Automatic", "/images/bmw_7_series.jpg", true, "HH-888-II", "BMW 7 Series", 80m, 2023 },
                    { new Guid("45e268be-d7f0-4ecd-a219-e6c5187b41ef"), new Guid("f2e93546-78ce-46bc-b9f7-5fd19c49fe99"), new Guid("edf8edeb-5472-436f-9eaa-899df910a390"), "Hybrid", 4.2m, "Automatic", "/images/toyota_rav4.jpg", true, "EE-555-FF", "Toyota RAV4", 25m, 2022 },
                    { new Guid("4bc2c5db-e39a-4658-812e-bec9e00c26f5"), new Guid("f2e93546-78ce-46bc-b9f7-5fd19c49fe99"), new Guid("edf8edeb-5472-436f-9eaa-899df910a390"), "Petrol", 6m, "Automatic", "/images/honda_crv.jpg", true, "FF-666-GG", "Honda CR-V", 27m, 2023 },
                    { new Guid("681bf99b-e157-496a-a43f-52934c7a271d"), new Guid("dee98aaf-d073-400b-ac14-2216c86b5a0e"), new Guid("34b15eb8-f1bc-4aa6-a59b-957fc0785cb6"), "Petrol", 9m, "Automatic", "/images/mercedes_s_class.jpg", true, "GG-777-HH", "Mercedes-Benz S-Class", 85m, 2023 },
                    { new Guid("69e42b9b-7367-4930-8d2e-5ac756eb7aeb"), new Guid("dc18e284-a3ff-4672-8cf2-eeb874108028"), new Guid("76f104e3-f55f-4abd-930a-920a68362c0d"), "Petrol", 4m, "Manual", "/images/fiat_panda.jpg", true, "AA-111-BB", "Fiat Panda", 12m, 2020 },
                    { new Guid("744121d7-43eb-472a-a857-475b14745ed9"), new Guid("c5e18e12-be6e-41c6-9fa3-1d2c8efaa47a"), new Guid("616afdfb-ad19-4718-9b14-77358894a23e"), "Petrol", 4.8m, "Manual", "/images/ford_focus.jpg", true, "DD-444-EE", "Ford Focus", 17m, 2020 },
                    { new Guid("a0bac1b2-a871-4394-801d-406b049a2811"), new Guid("dc18e284-a3ff-4672-8cf2-eeb874108028"), new Guid("76f104e3-f55f-4abd-930a-920a68362c0d"), "Petrol", 3.8m, "Manual", "/images/hyundai_i10.jpg", true, "BB-222-CC", "Hyundai i10", 10m, 2019 },
                    { new Guid("e9ae80b4-aaee-408d-a780-ca365541d291"), new Guid("c5e18e12-be6e-41c6-9fa3-1d2c8efaa47a"), new Guid("616afdfb-ad19-4718-9b14-77358894a23e"), "Diesel", 5.1m, "Automatic", "/images/vw_golf.jpg", true, "CC-333-DD", "Volkswagen Golf", 18m, 2021 }
                });

            migrationBuilder.InsertData(
                table: "CarFeature",
                columns: new[] { "Id", "CarId", "Name" },
                values: new object[,]
                {
                    { new Guid("05dc6d03-caab-4785-a2a8-6dea6fb52834"), new Guid("e9ae80b4-aaee-408d-a780-ca365541d291"), "Airbags" },
                    { new Guid("0b0bb88e-8a8f-4126-ad08-a4e1f225d85c"), new Guid("45e268be-d7f0-4ecd-a219-e6c5187b41ef"), "Airbags" },
                    { new Guid("124e45e7-326e-4d70-846d-e46655e68f63"), new Guid("681bf99b-e157-496a-a43f-52934c7a271d"), "Airbags" },
                    { new Guid("14afbe2d-6c83-4ba2-945e-930706c91d80"), new Guid("45e268be-d7f0-4ecd-a219-e6c5187b41ef"), "Electric Windows" },
                    { new Guid("1781fd1c-ae62-4112-b0bd-50f2fd03a01d"), new Guid("e9ae80b4-aaee-408d-a780-ca365541d291"), "Electric Windows" },
                    { new Guid("1c531aaa-0b65-466e-b9ea-96b9d8f66d16"), new Guid("37e46a14-7cdd-4717-ac86-ada2efaab0ee"), "Central Locking" },
                    { new Guid("1d91de0d-82eb-44fc-ae64-5c6845136533"), new Guid("744121d7-43eb-472a-a857-475b14745ed9"), "Central Locking" },
                    { new Guid("342c9e6f-85b6-4eb8-8e56-5f68a7a7b224"), new Guid("a0bac1b2-a871-4394-801d-406b049a2811"), "Electric Windows" },
                    { new Guid("3a71f5ac-3f2b-4210-aedc-801a08f3a68e"), new Guid("37e46a14-7cdd-4717-ac86-ada2efaab0ee"), "Airbags" },
                    { new Guid("461e195e-2838-4aa9-a9bb-493a5a748da0"), new Guid("681bf99b-e157-496a-a43f-52934c7a271d"), "A/C" },
                    { new Guid("493830a0-16f2-4167-b810-4ce68581d947"), new Guid("69e42b9b-7367-4930-8d2e-5ac756eb7aeb"), "Electric Windows" },
                    { new Guid("49eadbe5-dbd4-42e0-b7b4-c91d856e3717"), new Guid("1e1d9a0f-1d9c-4556-a016-fd01dc77343f"), "A/C" },
                    { new Guid("543745aa-4174-4cd7-b509-16e312cf7493"), new Guid("681bf99b-e157-496a-a43f-52934c7a271d"), "Electric Windows" },
                    { new Guid("5a4f6d03-2d97-492f-84f9-0b57bf6aedca"), new Guid("744121d7-43eb-472a-a857-475b14745ed9"), "A/C" },
                    { new Guid("5f969da3-705e-495a-9092-57cb37d5fa43"), new Guid("4bc2c5db-e39a-4658-812e-bec9e00c26f5"), "A/C" },
                    { new Guid("61c1d49d-6159-4951-8aec-d36821ff1ba1"), new Guid("e9ae80b4-aaee-408d-a780-ca365541d291"), "Central Locking" },
                    { new Guid("675b4b86-79e0-4aa6-bb32-ea5e174816d9"), new Guid("1e1d9a0f-1d9c-4556-a016-fd01dc77343f"), "Central Locking" },
                    { new Guid("6900da3a-3b4e-4557-a8bb-5b28e4feaa25"), new Guid("681bf99b-e157-496a-a43f-52934c7a271d"), "Central Locking" },
                    { new Guid("6a0e1af6-c079-41cd-be0c-e6d4b7a1a424"), new Guid("4bc2c5db-e39a-4658-812e-bec9e00c26f5"), "Central Locking" },
                    { new Guid("71a1bd9b-cdea-47eb-978a-73f7b0125e6d"), new Guid("37e46a14-7cdd-4717-ac86-ada2efaab0ee"), "A/C" },
                    { new Guid("73476ff2-e4aa-4a0f-a7db-620fae0276ff"), new Guid("45e268be-d7f0-4ecd-a219-e6c5187b41ef"), "Central Locking" },
                    { new Guid("7b8c7022-e5bf-41e1-81ad-e2edec2a8060"), new Guid("744121d7-43eb-472a-a857-475b14745ed9"), "Electric Windows" },
                    { new Guid("7d8ceff2-7fbf-4cca-9031-2103910e2ef5"), new Guid("1e1d9a0f-1d9c-4556-a016-fd01dc77343f"), "Electric Windows" },
                    { new Guid("806a3d6f-09ec-451e-bd9e-2e5bdba71a3c"), new Guid("45e268be-d7f0-4ecd-a219-e6c5187b41ef"), "A/C" },
                    { new Guid("811fb172-7293-41fe-9fa7-25523ec9f5c6"), new Guid("37e46a14-7cdd-4717-ac86-ada2efaab0ee"), "Electric Windows" },
                    { new Guid("85d510e5-8f76-4748-b01a-0a429d857b4f"), new Guid("a0bac1b2-a871-4394-801d-406b049a2811"), "Airbags" },
                    { new Guid("a1177890-86c1-429f-8ea4-f85ad3981a37"), new Guid("1e1d9a0f-1d9c-4556-a016-fd01dc77343f"), "Airbags" },
                    { new Guid("a531fdfc-1745-4bf5-b5db-5a16dac0896a"), new Guid("69e42b9b-7367-4930-8d2e-5ac756eb7aeb"), "Airbags" },
                    { new Guid("ab0445e4-4aa5-4231-96b5-bbb80358cc4f"), new Guid("4bc2c5db-e39a-4658-812e-bec9e00c26f5"), "Electric Windows" },
                    { new Guid("ab9a055d-820d-4c60-ae2e-cffa13ef2549"), new Guid("384ca78c-fed1-4614-bdfd-83ca602fe72a"), "A/C" },
                    { new Guid("bef87e3c-4d0e-4a61-9e11-6370ef530115"), new Guid("744121d7-43eb-472a-a857-475b14745ed9"), "Airbags" },
                    { new Guid("ca6b23d8-9c55-4b92-9a8e-0d76490e00cb"), new Guid("a0bac1b2-a871-4394-801d-406b049a2811"), "Central Locking" },
                    { new Guid("d70bd956-95ab-459e-a34b-8a315aab5389"), new Guid("384ca78c-fed1-4614-bdfd-83ca602fe72a"), "Electric Windows" },
                    { new Guid("d88e824d-47ea-4df0-af4d-35f230606a56"), new Guid("a0bac1b2-a871-4394-801d-406b049a2811"), "A/C" },
                    { new Guid("dd905fff-c0e6-4079-9208-4891286404ce"), new Guid("69e42b9b-7367-4930-8d2e-5ac756eb7aeb"), "A/C" },
                    { new Guid("deea5cac-a8f6-4743-b1b8-f7b099a49a06"), new Guid("384ca78c-fed1-4614-bdfd-83ca602fe72a"), "Central Locking" },
                    { new Guid("eb6ab68a-d446-4516-b9e1-af73d2146702"), new Guid("4bc2c5db-e39a-4658-812e-bec9e00c26f5"), "Airbags" },
                    { new Guid("ee440953-d2a7-4ae5-935f-a3d76ddf0e56"), new Guid("69e42b9b-7367-4930-8d2e-5ac756eb7aeb"), "Central Locking" },
                    { new Guid("eeb396d4-4990-41a3-925d-c164ef67266d"), new Guid("e9ae80b4-aaee-408d-a780-ca365541d291"), "A/C" },
                    { new Guid("fd7d79e2-6b42-473d-85af-7736d1f8351b"), new Guid("384ca78c-fed1-4614-bdfd-83ca602fe72a"), "Airbags" }
                });

            migrationBuilder.InsertData(
                table: "PricingTier",
                columns: new[] { "Id", "CarId", "MaxDays", "MinDays", "PricePerDay" },
                values: new object[,]
                {
                    { new Guid("05238982-842d-4145-89a0-6701c1357db2"), new Guid("45e268be-d7f0-4ecd-a219-e6c5187b41ef"), 365, 21, 18.75m },
                    { new Guid("072506b8-a6df-4a81-aa07-c71bee5ae6d9"), new Guid("681bf99b-e157-496a-a43f-52934c7a271d"), 10, 4, 76.5m }
                });

            migrationBuilder.InsertData(
                table: "PricingTier",
                columns: new[] { "Id", "CarId", "MaxDays", "MinDays", "PricePerDay" },
                values: new object[,]
                {
                    { new Guid("096b29c8-93f3-405b-95eb-b6bb2c57a7c8"), new Guid("a0bac1b2-a871-4394-801d-406b049a2811"), 10, 4, 9.0m },
                    { new Guid("0b24eccd-002f-42e1-8f3c-2a2b4d2f75c8"), new Guid("45e268be-d7f0-4ecd-a219-e6c5187b41ef"), 10, 4, 22.5m },
                    { new Guid("0e9938f7-3e9a-4b29-89c7-33fe59993555"), new Guid("37e46a14-7cdd-4717-ac86-ada2efaab0ee"), 15, 11, 34.00m },
                    { new Guid("14424655-60f6-4b24-adbf-72c9d7592853"), new Guid("69e42b9b-7367-4930-8d2e-5ac756eb7aeb"), 20, 16, 9.6m },
                    { new Guid("16c69fdc-fe83-4fe2-8aae-541caf6b87ee"), new Guid("384ca78c-fed1-4614-bdfd-83ca602fe72a"), 365, 21, 60.00m },
                    { new Guid("17953514-48b3-4abe-8a68-9190f049f785"), new Guid("e9ae80b4-aaee-408d-a780-ca365541d291"), 20, 16, 14.4m },
                    { new Guid("19fb0f0c-7095-45fa-8ba2-14341bf20169"), new Guid("37e46a14-7cdd-4717-ac86-ada2efaab0ee"), 20, 16, 32.0m },
                    { new Guid("1cb6afe9-a0e7-4508-9eb3-b3ba6c3ca64d"), new Guid("a0bac1b2-a871-4394-801d-406b049a2811"), 3, 1, 10m },
                    { new Guid("1ccd2ce3-92d5-40dc-8715-094865f0e2ca"), new Guid("744121d7-43eb-472a-a857-475b14745ed9"), 20, 16, 13.6m },
                    { new Guid("1d45cf84-3dff-4dd8-b372-df7cbacc50ed"), new Guid("4bc2c5db-e39a-4658-812e-bec9e00c26f5"), 365, 21, 20.25m },
                    { new Guid("1ff80723-b71c-449e-9ad0-423b005a40ff"), new Guid("45e268be-d7f0-4ecd-a219-e6c5187b41ef"), 3, 1, 25m },
                    { new Guid("281b0f74-107e-4062-8180-1a532e6df78a"), new Guid("384ca78c-fed1-4614-bdfd-83ca602fe72a"), 10, 4, 72.0m },
                    { new Guid("2ae21351-e354-4ed7-941f-fc85f117df5c"), new Guid("1e1d9a0f-1d9c-4556-a016-fd01dc77343f"), 365, 21, 28.50m },
                    { new Guid("310a3d08-e2b5-4212-b09b-1ead111b9c0f"), new Guid("37e46a14-7cdd-4717-ac86-ada2efaab0ee"), 365, 21, 30.00m },
                    { new Guid("31a878c8-a5c9-4e69-84f9-f128a8777566"), new Guid("a0bac1b2-a871-4394-801d-406b049a2811"), 15, 11, 8.50m },
                    { new Guid("349c72ee-cf20-48a2-8f84-4deceed2a35a"), new Guid("e9ae80b4-aaee-408d-a780-ca365541d291"), 3, 1, 18m },
                    { new Guid("34f0f7bf-53f6-4a01-9f8f-325a456357f3"), new Guid("384ca78c-fed1-4614-bdfd-83ca602fe72a"), 3, 1, 80m },
                    { new Guid("429bf382-d0a5-43b1-90d1-0b9b74405a2e"), new Guid("384ca78c-fed1-4614-bdfd-83ca602fe72a"), 20, 16, 64.0m },
                    { new Guid("4b7f4a3b-0796-4e47-91ad-908f9f62a842"), new Guid("1e1d9a0f-1d9c-4556-a016-fd01dc77343f"), 20, 16, 30.4m },
                    { new Guid("55ce5a34-6d66-45ce-9bf2-2687bfe4c1f7"), new Guid("69e42b9b-7367-4930-8d2e-5ac756eb7aeb"), 10, 4, 10.8m },
                    { new Guid("589f355e-64d6-442f-b20c-1c8fbf2f9b9f"), new Guid("744121d7-43eb-472a-a857-475b14745ed9"), 365, 21, 12.75m },
                    { new Guid("594a2286-774e-45ea-9ccc-6c7e420e97d6"), new Guid("4bc2c5db-e39a-4658-812e-bec9e00c26f5"), 15, 11, 22.95m },
                    { new Guid("66c2e2fd-ce46-4f79-ab71-2d9468ae0ecd"), new Guid("45e268be-d7f0-4ecd-a219-e6c5187b41ef"), 20, 16, 20.0m },
                    { new Guid("68dff33c-455c-4a36-90d0-5e00224a5c8c"), new Guid("1e1d9a0f-1d9c-4556-a016-fd01dc77343f"), 15, 11, 32.30m },
                    { new Guid("6bafd4f2-0fca-473c-8056-16a31b1b3b7f"), new Guid("69e42b9b-7367-4930-8d2e-5ac756eb7aeb"), 15, 11, 10.20m },
                    { new Guid("7639b0a5-e721-492f-b408-1164e4252216"), new Guid("e9ae80b4-aaee-408d-a780-ca365541d291"), 365, 21, 13.50m },
                    { new Guid("765551f2-36a5-4674-b2be-de316bb5eb2d"), new Guid("681bf99b-e157-496a-a43f-52934c7a271d"), 3, 1, 85m },
                    { new Guid("7bb0cfb5-83b8-4c22-8b0c-ba79e2c3347f"), new Guid("1e1d9a0f-1d9c-4556-a016-fd01dc77343f"), 10, 4, 34.2m },
                    { new Guid("7f51dbd4-a726-4003-8829-58d740e80f20"), new Guid("744121d7-43eb-472a-a857-475b14745ed9"), 3, 1, 17m },
                    { new Guid("7f7419f7-9590-47d0-8376-021b471ca383"), new Guid("45e268be-d7f0-4ecd-a219-e6c5187b41ef"), 15, 11, 21.25m },
                    { new Guid("8c6c800f-40dc-4fae-bf36-24aee9043ca0"), new Guid("681bf99b-e157-496a-a43f-52934c7a271d"), 365, 21, 63.75m },
                    { new Guid("8cdb860e-6e81-4315-9ec2-8213ef63f714"), new Guid("384ca78c-fed1-4614-bdfd-83ca602fe72a"), 15, 11, 68.00m },
                    { new Guid("939abd05-3ff5-4a7f-bfef-ca038fc83791"), new Guid("681bf99b-e157-496a-a43f-52934c7a271d"), 15, 11, 72.25m },
                    { new Guid("93f5fcc1-d1e0-492c-b1f7-be1143878237"), new Guid("e9ae80b4-aaee-408d-a780-ca365541d291"), 15, 11, 15.30m },
                    { new Guid("a251e046-4151-4741-bb19-811974abe664"), new Guid("69e42b9b-7367-4930-8d2e-5ac756eb7aeb"), 3, 1, 12m },
                    { new Guid("a3fe52d4-0a5d-4c28-9161-cfdaf512549b"), new Guid("e9ae80b4-aaee-408d-a780-ca365541d291"), 10, 4, 16.2m },
                    { new Guid("ae8e3cd4-680e-4fa1-aa20-fc1047a6d68f"), new Guid("a0bac1b2-a871-4394-801d-406b049a2811"), 365, 21, 7.50m },
                    { new Guid("b411413a-a6ec-4632-8e75-351a96227282"), new Guid("4bc2c5db-e39a-4658-812e-bec9e00c26f5"), 3, 1, 27m },
                    { new Guid("b656ef67-6579-4b0a-9cb8-bf4a3b048cf3"), new Guid("4bc2c5db-e39a-4658-812e-bec9e00c26f5"), 10, 4, 24.3m },
                    { new Guid("ca81b5f0-f61f-49a1-80a9-6f8aa2c3fbd0"), new Guid("a0bac1b2-a871-4394-801d-406b049a2811"), 20, 16, 8.0m },
                    { new Guid("d96e3676-6048-4434-95f9-1888907bb113"), new Guid("37e46a14-7cdd-4717-ac86-ada2efaab0ee"), 3, 1, 40m },
                    { new Guid("e50df614-dfec-4163-b39d-8c257d13af4c"), new Guid("744121d7-43eb-472a-a857-475b14745ed9"), 10, 4, 15.3m }
                });

            migrationBuilder.InsertData(
                table: "PricingTier",
                columns: new[] { "Id", "CarId", "MaxDays", "MinDays", "PricePerDay" },
                values: new object[,]
                {
                    { new Guid("e8a1ec8d-6ab9-4eba-9eab-1b60169c910d"), new Guid("4bc2c5db-e39a-4658-812e-bec9e00c26f5"), 20, 16, 21.6m },
                    { new Guid("ebe3286c-7f8e-4206-ba99-0735ac3f9b1d"), new Guid("1e1d9a0f-1d9c-4556-a016-fd01dc77343f"), 3, 1, 38m },
                    { new Guid("f0406c5d-2261-415d-a26d-611f2fb3842a"), new Guid("69e42b9b-7367-4930-8d2e-5ac756eb7aeb"), 365, 21, 9.00m },
                    { new Guid("f29456a5-9212-4925-8236-9482426cbf08"), new Guid("681bf99b-e157-496a-a43f-52934c7a271d"), 20, 16, 68.0m },
                    { new Guid("fbd8c958-bb35-4b8e-a034-f414894113ab"), new Guid("37e46a14-7cdd-4717-ac86-ada2efaab0ee"), 10, 4, 36.0m },
                    { new Guid("fee08ba5-fd41-4768-89fd-cc976a4242a9"), new Guid("744121d7-43eb-472a-a857-475b14745ed9"), 15, 11, 14.45m }
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
                name: "IX_Payment_ReservationId",
                table: "Payment",
                column: "ReservationId");

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
