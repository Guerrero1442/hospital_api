using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class RelacionesModelos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cita",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "Paciente",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "Medico",
                table: "Citas");

            migrationBuilder.DropColumn(
                name: "Paciente",
                table: "Citas");

            migrationBuilder.AddColumn<int>(
                name: "CitaId",
                table: "Pagos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PacienteId",
                table: "Pagos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MedicoId",
                table: "Citas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PacienteId",
                table: "Citas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_CitaId",
                table: "Pagos",
                column: "CitaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_PacienteId",
                table: "Pagos",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_MedicoId",
                table: "Citas",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_PacienteId",
                table: "Citas",
                column: "PacienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Medicos_MedicoId",
                table: "Citas",
                column: "MedicoId",
                principalTable: "Medicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Pacientes_PacienteId",
                table: "Citas",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_Citas_CitaId",
                table: "Pagos",
                column: "CitaId",
                principalTable: "Citas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_Pacientes_PacienteId",
                table: "Pagos",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Medicos_MedicoId",
                table: "Citas");

            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Pacientes_PacienteId",
                table: "Citas");

            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_Citas_CitaId",
                table: "Pagos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_Pacientes_PacienteId",
                table: "Pagos");

            migrationBuilder.DropIndex(
                name: "IX_Pagos_CitaId",
                table: "Pagos");

            migrationBuilder.DropIndex(
                name: "IX_Pagos_PacienteId",
                table: "Pagos");

            migrationBuilder.DropIndex(
                name: "IX_Citas_MedicoId",
                table: "Citas");

            migrationBuilder.DropIndex(
                name: "IX_Citas_PacienteId",
                table: "Citas");

            migrationBuilder.DropColumn(
                name: "CitaId",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "PacienteId",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "MedicoId",
                table: "Citas");

            migrationBuilder.DropColumn(
                name: "PacienteId",
                table: "Citas");

            migrationBuilder.AddColumn<string>(
                name: "Cita",
                table: "Pagos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Paciente",
                table: "Pagos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Medico",
                table: "Citas",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Paciente",
                table: "Citas",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
