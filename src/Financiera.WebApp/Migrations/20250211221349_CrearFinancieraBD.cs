using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Financiera.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class CrearFinancieraBD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CLIENTES",
                columns: table => new
                {
                    ID_CLIENTE = table.Column<int>(type: "int", nullable: false, comment: "Identificador unico del cliente")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NOM_CLIENTE = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, comment: "Nombre del cliente")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENTES", x => x.ID_CLIENTE);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TIPOS_MOVIMIENTO",
                columns: table => new
                {
                    ID_TIPO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DES_TIPO = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIPOS_MOVIMIENTO", x => x.ID_TIPO);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CUENTAS_AHORRO",
                columns: table => new
                {
                    ID_CUENTA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NUM_CUENTA = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ID_CLIENTE = table.Column<int>(type: "int", nullable: false),
                    Tasa = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Saldo = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    FechaApertura = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Estado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUENTAS_AHORRO", x => x.ID_CUENTA);
                    table.ForeignKey(
                        name: "FK_CUENTAS_AHORRO_CLIENTES_ID_CLIENTE",
                        column: x => x.ID_CLIENTE,
                        principalTable: "CLIENTES",
                        principalColumn: "ID_CLIENTE",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MOVIMIENTOS_CUENTA",
                columns: table => new
                {
                    NUM_MOVIMIENTO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_CUENTA = table.Column<int>(type: "int", nullable: false),
                    ID_TIPO = table.Column<int>(type: "int", nullable: false),
                    FechaMovimiento = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MON_MOVIMIENTO = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Estado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CuentaAhorroIdCuenta = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MOVIMIENTOS_CUENTA", x => x.NUM_MOVIMIENTO);
                    table.ForeignKey(
                        name: "FK_MOVIMIENTOS_CUENTA_CUENTAS_AHORRO_CuentaAhorroIdCuenta",
                        column: x => x.CuentaAhorroIdCuenta,
                        principalTable: "CUENTAS_AHORRO",
                        principalColumn: "ID_CUENTA");
                    table.ForeignKey(
                        name: "FK_MOVIMIENTOS_CUENTA_CUENTAS_AHORRO_ID_CUENTA",
                        column: x => x.ID_CUENTA,
                        principalTable: "CUENTAS_AHORRO",
                        principalColumn: "ID_CUENTA",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MOVIMIENTOS_CUENTA_TIPOS_MOVIMIENTO_ID_TIPO",
                        column: x => x.ID_TIPO,
                        principalTable: "TIPOS_MOVIMIENTO",
                        principalColumn: "ID_TIPO",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CUENTAS_AHORRO_ID_CLIENTE",
                table: "CUENTAS_AHORRO",
                column: "ID_CLIENTE");

            migrationBuilder.CreateIndex(
                name: "IX_MOVIMIENTOS_CUENTA_CuentaAhorroIdCuenta",
                table: "MOVIMIENTOS_CUENTA",
                column: "CuentaAhorroIdCuenta");

            migrationBuilder.CreateIndex(
                name: "IX_MOVIMIENTOS_CUENTA_ID_CUENTA",
                table: "MOVIMIENTOS_CUENTA",
                column: "ID_CUENTA");

            migrationBuilder.CreateIndex(
                name: "IX_MOVIMIENTOS_CUENTA_ID_TIPO",
                table: "MOVIMIENTOS_CUENTA",
                column: "ID_TIPO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MOVIMIENTOS_CUENTA");

            migrationBuilder.DropTable(
                name: "CUENTAS_AHORRO");

            migrationBuilder.DropTable(
                name: "TIPOS_MOVIMIENTO");

            migrationBuilder.DropTable(
                name: "CLIENTES");
        }
    }
}
