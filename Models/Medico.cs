
using System.ComponentModel;
using backend.Models;

public class Medico : ModelBase
{
	public int Id { get; set; }
	public string DocumentoIdentificacion { get; set; }

	public string NombreCompleto { get; set; }

	public int Telefono { get; set; }

	public Especialidad Especialidad { get; set; }

	public List<DateTime> Disponibilidad { get; set; }

	public int UsuarioId { get; set; }

	public virtual Usuario Usuario { get; set; }
}

public enum Especialidad
{
	[Description("Cardiologia")]
	Cardiologia,
	[Description("Dermatologia")]
	Dermatologia,
	[Description("Gastroenterologia")]
	Gastroenterologia,
	[Description("Ginecologia")]
	Ginecologia,
	[Description("Neurologia")]
	Neurologia,
	[Description("Oftalmologia")]
	Oftalmologia,
	[Description("Oncologia")]
	Oncologia,
	[Description("Pediatria")]
	Pediatria,
	[Description("Psicologia")]
	Psicologia,
	[Description("Reumatologia")]
	Reumatologia,
	[Description("Urologia")]
	Urologia
}