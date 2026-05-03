using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineProdavniceEF.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Oprema" },
                    { 2, "Laptopovi" },
                    { 3, "Telefoni" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FullName", "Email", "Password", "IsAdmin" },
                values: new object[] { 1, "Administrator", "admin@prodavnica.com", "admin123", true });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price", "Quantity", "CategoryId", "Description", "ImageUrl" },
                values: new object[,]
                {
                    { 1, "Gaming miš", 2490m, 15, 1, "Osnovni gaming miš.", "/images/product-placeholder.png" },
                    { 2, "Laptop Start", 59990m, 5, 2, "Laptop za školu i posao.", "/images/product-placeholder.png" },
                    { 3, "Telefon Mini", 29990m, 8, 3, "Praktičan telefon.", "/images/product-placeholder.png" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Products");
            migrationBuilder.DropTable(name: "Users");
            migrationBuilder.DropTable(name: "Categories");
        }
    }
}
