using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace super_ws.client.Migrations
{
    /// <inheritdoc />
    public partial class AddMinuteQuotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuoteMinutes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Time = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    High = table.Column<decimal>(type: "money", nullable: false),
                    Low = table.Column<decimal>(type: "money", nullable: false),
                    Open = table.Column<decimal>(type: "money", nullable: false),
                    Close = table.Column<decimal>(type: "money", nullable: false),
                    Volume = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuoteMinutes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuoteMinutes");
        }
    }
}
