public interface IAdministradorRepository : IGenericRepository<Medico>
{

}

public class AdministradorRepository : GenericRepository<Medico>, IAdministradorRepository
{
	private readonly HospitalContext _context;
	public AdministradorRepository(HospitalContext context) : base(context)
	{
		_context = context;
	}
}