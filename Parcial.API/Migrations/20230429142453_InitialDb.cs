﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parcial.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "eventControl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaDeUso = table.Column<DateTime>(name: "Fecha_De_Uso", type: "datetime2", nullable: true),
                    FueUsada = table.Column<bool>(name: "Fue_Usada", type: "bit", nullable: false),
                    Porteria = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventControl", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_eventControl_Id",
                table: "eventControl",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "eventControl");
        }
    }
}
