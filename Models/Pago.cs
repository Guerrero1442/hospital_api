namespace backend.Models;

public class Pago
{
	public int Id { get; set; }

	public string MetodoPago { get; set; }

	public decimal Monto { get; set; }

	public DateTime FechaPago { get; set; }

	public Paciente Paciente { get; set; }

	public Cita Cita { get; set; }
}