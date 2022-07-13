using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Deliverix.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddDeliveryDateTimeToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDateTime",
                table: "Order",
                type: "datetime",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryDateTime",
                table: "Order");
        }
    }
}
