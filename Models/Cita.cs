
public class Cita : ModelBase
{

	public int Id { get; set; }

	public DateTime Fecha { get; set; }

	public int PacienteId { get; set; }

	public int MedicoId { get; set; }

	public Especialidad Especialidad { get; set; }

	// Navegación de propiedades
	public virtual Medico Medico { get; set; }
	public virtual Paciente Paciente { get; set; }
}
