
public interface ICitaRepository : IGenericRepository<Cita>
{

}

public class CitaRepository : GenericRepository<Cita>, ICitaRepository
{
	private readonly HospitalContext _context;
	public CitaRepository(HospitalContext context) : base(context)
	{
		_context = context;
	}
}