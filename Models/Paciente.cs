using backend.Models;

public class Paciente : ModelBase
{
	public int Id { get; set; }
	public string DocumentoIdentificacion { get; set; }

	public string NombreCompleto { get; set; }

	public int Telefono { get; set; }

	public List<string> Beneficiarios { get; set; }

	public int UsuarioId { get; set; }

	public virtual Usuario Usuario { get; set; }
}
