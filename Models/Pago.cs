namespace backend.Models;

public class Pago : ModelBase
{
	public int Id { get; set; }

	public string MetodoPago { get; set; }

	public decimal Monto { get; set; }

	public DateTime FechaPago { get; set; }

	public int PacienteId { get; set; }

	public int CitaId { get; set; }

	// Navegación de propiedades
	public virtual Paciente Paciente { get; set; }

	public virtual Cita Cita { get; set; }
}