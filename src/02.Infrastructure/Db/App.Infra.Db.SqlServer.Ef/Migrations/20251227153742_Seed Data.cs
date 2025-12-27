using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace App.Infra.Db.SqlServer.Ef.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
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
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TotalRevenue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admins_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
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
                    UserId = table.Column<int>(type: "int", nullable: false)
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
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
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
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
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
                name: "HomeServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeServices_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    WalletBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Experts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bio = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    ProfilePicture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WalletBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experts_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Experts_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    ExecutionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExecutionTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    HomeServiceId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_HomeServices_HomeServiceId",
                        column: x => x.HomeServiceId,
                        principalTable: "HomeServices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExpertHomeService",
                columns: table => new
                {
                    ExpertsId = table.Column<int>(type: "int", nullable: false),
                    HomeServicesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertHomeService", x => new { x.ExpertsId, x.HomeServicesId });
                    table.ForeignKey(
                        name: "FK_ExpertHomeService_Experts_ExpertsId",
                        column: x => x.ExpertsId,
                        principalTable: "Experts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpertHomeService_HomeServices_HomeServicesId",
                        column: x => x.HomeServicesId,
                        principalTable: "HomeServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderImages_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Proposals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ExpertId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proposals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proposals_Experts_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "Experts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Proposals_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ExpertId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Experts_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "Experts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "78641951-8712-4261-9B5A-431A29D67A41", "Admin", "ADMIN" },
                    { 2, "45127812-1234-5678-9B5A-431A29D67A42", "Customer", "CUSTOMER" },
                    { 3, "12345678-8712-4261-9B5A-431A29D67A43", "Expert", "EXPERT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "081004b3-e25c-4414-9694-44e5c2fd863f", "admin@site.com", true, "Admin", "System", false, null, "ADMIN@SITE.COM", "ADMIN@SITE.COM", "AQAAAAIAAYagAAAAELfo31+h+KKqnm1LH5xAKmHXILVYYm2LFfRa9MBOYhQ7tUa464HktXtipr7xzCC8rQ==", null, false, "B6FE5F0E-18B5-4062-AEF8-11555793E7CB", false, "admin@site.com" },
                    { 2, 0, "df5dbb19-753c-4cc7-a623-13c8508d00f8", "customer@site.com", true, "Ali", "Moshtari", false, null, "CUSTOMER@SITE.COM", "CUSTOMER@SITE.COM", "AQAAAAIAAYagAAAAELfo31+h+KKqnm1LH5xAKmHXILVYYm2LFfRa9MBOYhQ7tUa464HktXtipr7xzCC8rQ==", null, false, "D4D09FBB-ED60-4E17-B03E-B9B4B6C70E5D", false, "customer@site.com" },
                    { 3, 0, "58afcc4e-2a82-4ad6-8e12-665778173973", "expert@site.com", true, "Reza", "Karshenas", false, null, "EXPERT@SITE.COM", "EXPERT@SITE.COM", "AQAAAAIAAYagAAAAELfo31+h+KKqnm1LH5xAKmHXILVYYm2LFfRa9MBOYhQ7tUa464HktXtipr7xzCC8rQ==", null, false, "9489458F-27BA-400D-A45F-AFCE3D9A8D26", false, "expert@site.com" },
                    { 4, 0, "C2-04b3-e25c-4414", "customer2@site.com", true, "Zahra", "Ahmadi", false, null, "CUSTOMER2@SITE.COM", "CUSTOMER2@SITE.COM", "AQAAAAIAAYagAAAAELfo31+h+KKqnm1LH5xAKmHXILVYYm2LFfRa9MBOYhQ7tUa464HktXtipr7xzCC8rQ==", null, false, "S2-68B5-4062-AEF8", false, "customer2@site.com" },
                    { 5, 0, "C3-04b3-e25c-4414", "customer3@site.com", true, "Mohammad", "Hosseini", false, null, "CUSTOMER3@SITE.COM", "CUSTOMER3@SITE.COM", "AQAAAAIAAYagAAAAELfo31+h+KKqnm1LH5xAKmHXILVYYm2LFfRa9MBOYhQ7tUa464HktXtipr7xzCC8rQ==", null, false, "S3-68B5-4062-AEF8", false, "customer3@site.com" },
                    { 6, 0, "C4-04b3-e25c-4414", "customer4@site.com", true, "Maryam", "Moradi", false, null, "CUSTOMER4@SITE.COM", "CUSTOMER4@SITE.COM", "AQAAAAIAAYagAAAAELfo31+h+KKqnm1LH5xAKmHXILVYYm2LFfRa9MBOYhQ7tUa464HktXtipr7xzCC8rQ==", null, false, "S4-68B5-4062-AEF8", false, "customer4@site.com" },
                    { 7, 0, "C5-04b3-e25c-4414", "customer5@site.com", true, "Saeed", "Karimi", false, null, "CUSTOMER5@SITE.COM", "CUSTOMER5@SITE.COM", "AQAAAAIAAYagAAAAELfo31+h+KKqnm1LH5xAKmHXILVYYm2LFfRa9MBOYhQ7tUa464HktXtipr7xzCC8rQ==", null, false, "S5-68B5-4062-AEF8", false, "customer5@site.com" },
                    { 8, 0, "C6-04b3-e25c-4414", "customer6@site.com", true, "Niloufar", "Sadeghi", false, null, "CUSTOMER6@SITE.COM", "CUSTOMER6@SITE.COM", "AQAAAAIAAYagAAAAELfo31+h+KKqnm1LH5xAKmHXILVYYm2LFfRa9MBOYhQ7tUa464HktXtipr7xzCC8rQ==", null, false, "S6-68B5-4062-AEF8", false, "customer6@site.com" },
                    { 9, 0, "CE2-04b3-e25c-4414", "expert2@site.com", true, "Hassan", "Alavi", false, null, "EXPERT2@SITE.COM", "EXPERT2@SITE.COM", "AQAAAAIAAYagAAAAELfo31+h+KKqnm1LH5xAKmHXILVYYm2LFfRa9MBOYhQ7tUa464HktXtipr7xzCC8rQ==", null, false, "E2-68B5-4062-AEF8", false, "expert2@site.com" },
                    { 10, 0, "CE3-04b3-e25c-4414", "expert3@site.com", true, "Sara", "Mousavi", false, null, "EXPERT3@SITE.COM", "EXPERT3@SITE.COM", "AQAAAAIAAYagAAAAELfo31+h+KKqnm1LH5xAKmHXILVYYm2LFfRa9MBOYhQ7tUa464HktXtipr7xzCC8rQ==", null, false, "E3-68B5-4062-AEF8", false, "expert3@site.com" },
                    { 11, 0, "CE4-04b3-e25c-4414", "expert4@site.com", true, "Omid", "Rahmani", false, null, "EXPERT4@SITE.COM", "EXPERT4@SITE.COM", "AQAAAAIAAYagAAAAELfo31+h+KKqnm1LH5xAKmHXILVYYm2LFfRa9MBOYhQ7tUa464HktXtipr7xzCC8rQ==", null, false, "E4-68B5-4062-AEF8", false, "expert4@site.com" },
                    { 12, 0, "CE5-04b3-e25c-4414", "expert5@site.com", true, "Elham", "Jafari", false, null, "EXPERT5@SITE.COM", "EXPERT5@SITE.COM", "AQAAAAIAAYagAAAAELfo31+h+KKqnm1LH5xAKmHXILVYYm2LFfRa9MBOYhQ7tUa464HktXtipr7xzCC8rQ==", null, false, "E5-68B5-4062-AEF8", false, "expert5@site.com" },
                    { 13, 0, "CE6-04b3-e25c-4414", "expert6@site.com", true, "Meysam", "Ghasemi", false, null, "EXPERT6@SITE.COM", "EXPERT6@SITE.COM", "AQAAAAIAAYagAAAAELfo31+h+KKqnm1LH5xAKmHXILVYYm2LFfRa9MBOYhQ7tUa464HktXtipr7xzCC8rQ==", null, false, "E6-68B5-4062-AEF8", false, "expert6@site.com" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "ImagePath", "IsDeleted", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "cleaning-category.jpg", false, "نظافت و پذیرایی" },
                    { 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "repairs-category.jpg", false, "تعمیرات و تأسیسات" },
                    { 3, new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "appliances-category.jpg", false, "تعمیرات لوازم خانگی" },
                    { 4, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "car-services-category.jpg", false, "خدمات خودرو" },
                    { 5, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "beauty-category.jpg", false, "آرایش و زیبایی" },
                    { 6, new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "moving-category.jpg", false, "اسباب‌کشی و حمل و نقل" },
                    { 7, new DateTime(2025, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "gardening-category.jpg", false, "باغبانی و فضای سبز" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "تهران" },
                    { 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "کرج" },
                    { 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "شیراز" },
                    { 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "اصفهان" }
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "AppUserId", "CreatedAt", "IsDeleted", "StaffCode", "TotalRevenue" },
                values: new object[] { 1, 1, new DateTime(2025, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, "ADM-1001", 0m });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "AppUserId", "CityId", "CreatedAt", "IsDeleted", "WalletBalance" },
                values: new object[,]
                {
                    { 1, "تهران، خیابان آزادی، پلاک ۱", 2, 1, new DateTime(2025, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, 100000m },
                    { 2, "تهران، سعادت آباد، خیابان سرو", 4, 1, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 50000m },
                    { 3, "تبریز، ولیعصر، خیابان استانداری", 5, 2, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 250000m },
                    { 4, "مشهد، بلوار سجاد، کوچه بهار", 6, 3, new DateTime(2025, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0m },
                    { 5, "تهران، فلکه دوم صادقیه", 7, 1, new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 120000m },
                    { 6, "شیراز، خیابان عفیف آباد، مجتمع ستاره", 8, 4, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 75000m }
                });

            migrationBuilder.InsertData(
                table: "Experts",
                columns: new[] { "Id", "AppUserId", "Bio", "CityId", "CreatedAt", "IsDeleted", "ProfilePicture", "WalletBalance" },
                values: new object[,]
                {
                    { 1, 3, "متخصص در امور فنی با ۱۰ سال سابقه کار", 1, new DateTime(2025, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, "expert1.jpg", 200000m },
                    { 2, 9, "کارشناس ارشد تاسیسات و سیستم‌های برودتی", 1, new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "expert2.jpg", 500000m },
                    { 3, 10, "متخصص طراحی داخلی و دکوراسیون با مدرک بین‌المللی", 2, new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "expert3.jpg", 1200000m },
                    { 4, 11, "تکنسین برق قدرت و هوشمندسازی منازل", 1, new DateTime(2025, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "expert4.jpg", 0m },
                    { 5, 12, "متخصص باغبانی و فضای سبز", 3, new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "expert5.jpg", 350000m },
                    { 6, 13, "کارشناس تعمیرات لوازم خانگی دیجیتال", 2, new DateTime(2025, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "expert6.jpg", 800000m }
                });

            migrationBuilder.InsertData(
                table: "HomeServices",
                columns: new[] { "Id", "BasePrice", "CategoryId", "CreatedAt", "Description", "ImagePath", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, 500000m, 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "نظافت کامل فضاهای داخلی ساختمان", "cleaning-service.jpg", false, "نظافت منزل" },
                    { 2, 300000m, 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "سرویس دوره‌ای و تعمیر موتور کولر", "cooler-repair.jpg", false, "تعمیر کولر آبی" },
                    { 3, 800000m, 3, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "عیب‌یابی و شارژ گاز انواع یخچال‌های ایرانی و خارجی", "fridge-repair.jpg", false, "تعمیر یخچال و فریزر" },
                    { 4, 250000m, 4, new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "شستشوی کامل بدنه و داخل خودرو با نانو بدون آب", "carwash-service.jpg", false, "کارواش در محل" },
                    { 5, 400000m, 5, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "خدمات آرایشی مردانه و زنانه در منزل شما", "barber-service.jpg", false, "اصلاح سر و صورت" },
                    { 6, 1500000m, 6, new DateTime(2025, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "جمع‌آوری وسایل و حمل اثاثیه با کادر مجرب", "moving-service.jpg", false, "بسته‌بندی و اسباب‌کشی" },
                    { 7, 600000m, 7, new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "رسیدگی به باغچه و طراحی فضای سبز", "gardening-service.jpg", false, "هرس درختان و گل‌کاری" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CityId", "CreatedAt", "CustomerId", "Description", "ExecutionDate", "ExecutionTime", "HomeServiceId", "IsDeleted", "Status" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "نظافت آپارتمان ۸۰ متری، دو خوابه", new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 0, 0, 0), 1, false, 5 },
                    { 2, 1, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "یخچال ساید بای ساید صدای ناهنجار می‌دهد", new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 14, 30, 0, 0), 3, false, 2 },
                    { 3, 2, new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "سرویس کامل کولر آبی برای فصل جدید", new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0), 2, false, 5 },
                    { 4, 3, new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "شستشوی کامل پژو ۲۰۶ در پارکینگ منزل", new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 16, 0, 0, 0), 4, false, 6 },
                    { 5, 1, new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "هرس درختان حیاط و کاشت گل‌های فصلی", new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0), 7, false, 5 },
                    { 6, 4, new DateTime(2025, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "جابجایی اثاثیه به ساختمان مجاور، طبقه سوم با آسانسور", new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 0, 0, 0), 6, false, 4 }
                });

            migrationBuilder.InsertData(
                table: "OrderImages",
                columns: new[] { "Id", "CreatedAt", "ImagePath", "IsDeleted", "OrderId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), "apartment-cleaning-1.jpg", false, 1 },
                    { 2, new DateTime(2025, 1, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), "apartment-cleaning-2.jpg", false, 1 },
                    { 3, new DateTime(2025, 1, 15, 10, 30, 0, 0, DateTimeKind.Unspecified), "fridge-issue-photo.jpg", false, 2 },
                    { 4, new DateTime(2025, 1, 20, 9, 15, 0, 0, DateTimeKind.Unspecified), "cooler-on-roof.jpg", false, 3 },
                    { 5, new DateTime(2025, 1, 21, 16, 0, 0, 0, DateTimeKind.Unspecified), "dirty-car-front.jpg", false, 4 },
                    { 6, new DateTime(2025, 2, 10, 12, 0, 0, 0, DateTimeKind.Unspecified), "garden-yard-view.jpg", false, 5 },
                    { 7, new DateTime(2025, 2, 22, 14, 45, 0, 0, DateTimeKind.Unspecified), "moving-boxes.jpg", false, 6 }
                });

            migrationBuilder.InsertData(
                table: "Proposals",
                columns: new[] { "Id", "CreatedAt", "Description", "ExpertId", "IsDeleted", "OrderId", "Price", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "با تجهیزات کامل نظافتی در زمان تعیین شده حضور خواهم یافت.", 1, false, 1, 550000m, 1 },
                    { 2, new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "تضمین نظافت کامل با قیمت مناسب‌تر.", 4, false, 1, 500000m, 1 },
                    { 3, new DateTime(2025, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "قطعات اصلی و ضمانت ۶ ماهه تعمیرات.", 6, false, 2, 850000m, 2 },
                    { 4, new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "سرویس کامل کولر شامل شستشو و روغن‌کاری.", 2, false, 3, 350000m, 3 },
                    { 5, new DateTime(2025, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "طراحی باغچه و هرس تخصصی درختان میوه.", 5, false, 5, 650000m, 1 },
                    { 6, new DateTime(2025, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "دارای ماشین مخصوص و کارگران ورزیده.", 1, false, 6, 1600000m, 1 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Comment", "CreatedAt", "CustomerId", "ExpertId", "IsDeleted", "OrderId", "Score" },
                values: new object[,]
                {
                    { 1, "سرویس کولر خیلی خوب انجام شد، فقط کمی با تاخیر آمدند.", new DateTime(2025, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2, false, 3, 4 },
                    { 2, "باغبانی عالی و حرفه‌ای! حیاط ما کاملاً متحول شد. ممنونم از خانم جعفری.", new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 5, false, 5, 5 },
                    { 3, "بسیار تمیز و با حوصله کار انجام شد. راضی بودم.", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, false, 1, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_AppUserId",
                table: "Admins",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

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
                name: "IX_Customers_AppUserId",
                table: "Customers",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CityId",
                table: "Customers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertHomeService_HomeServicesId",
                table: "ExpertHomeService",
                column: "HomeServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_Experts_AppUserId",
                table: "Experts",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Experts_CityId",
                table: "Experts",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeServices_CategoryId",
                table: "HomeServices",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderImages_OrderId",
                table: "OrderImages",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CityId",
                table: "Orders",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_HomeServiceId",
                table: "Orders",
                column: "HomeServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_ExpertId",
                table: "Proposals",
                column: "ExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_OrderId",
                table: "Proposals",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CustomerId",
                table: "Reviews",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ExpertId",
                table: "Reviews",
                column: "ExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_OrderId",
                table: "Reviews",
                column: "OrderId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

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
                name: "ExpertHomeService");

            migrationBuilder.DropTable(
                name: "OrderImages");

            migrationBuilder.DropTable(
                name: "Proposals");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Experts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "HomeServices");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
