using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class DataInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "CategoriaId", "Descripcion", "Nombre", "Peso" },
                values: new object[,]
                {
                    { new Guid("6f49b05b-696b-4c8d-b227-aa679901b932"), null, "Actividades Pendientes", 20 },
                    { new Guid("e1c3e934-fd3c-4602-bb04-99ab5dabfe53"), null, "Actividades Personales", 50 }
                });

            migrationBuilder.InsertData(
                table: "Tarea",
                columns: new[] { "TareaId", "CategoriaId", "Descripcion", "FechaCreacion", "PriodidadTarea", "Titulo" },
                values: new object[,]
                {
                    { new Guid("9460c722-8fb4-4373-bc81-26baba96a843"), new Guid("e1c3e934-fd3c-4602-bb04-99ab5dabfe53"), null, new DateTime(2023, 1, 27, 18, 14, 24, 178, DateTimeKind.Local).AddTicks(100), 0, "Terminar de ver peliculas" },
                    { new Guid("a8cd181f-64ef-420a-be02-b8e28a79b2dc"), new Guid("6f49b05b-696b-4c8d-b227-aa679901b932"), null, new DateTime(2023, 1, 27, 18, 14, 24, 178, DateTimeKind.Local).AddTicks(84), 1, "Pago Servicios Publicos" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tarea",
                keyColumn: "TareaId",
                keyValue: new Guid("9460c722-8fb4-4373-bc81-26baba96a843"));

            migrationBuilder.DeleteData(
                table: "Tarea",
                keyColumn: "TareaId",
                keyValue: new Guid("a8cd181f-64ef-420a-be02-b8e28a79b2dc"));

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaId",
                keyValue: new Guid("6f49b05b-696b-4c8d-b227-aa679901b932"));

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaId",
                keyValue: new Guid("e1c3e934-fd3c-4602-bb04-99ab5dabfe53"));
        }
    }
}
