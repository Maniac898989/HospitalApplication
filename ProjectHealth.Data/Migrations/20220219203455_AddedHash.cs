using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectHealth.Data.Migrations
{
    public partial class AddedHash : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "AccessTable");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "AccessTable",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "Passwordhash",
                table: "AccessTable",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "AccessTable");

            migrationBuilder.DropColumn(
                name: "Passwordhash",
                table: "AccessTable");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AccessTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
