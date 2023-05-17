namespace backend.Models;

public class Cita : ModelBase
{

	public int Id { get; set; }
	public DateTime Fecha { get; set; }

	public Paciente Paciente { get; set; }

	public Medico Medico { get; set; }

	public Especialidad Especialidad { get; set; }

}