using backend.Models;

public class Administrador : ModelBase
{


	public string NombreCompleto { get; set; }
	public string Direccion { get; set; }

	public string Telefono { get; set; }

	public int UsuarioId { get; set; }

	public virtual Usuario Usuario { get; set; }
	public int Id { get; set; }
}