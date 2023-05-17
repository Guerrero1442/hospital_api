public interface IMedicoRepository : IGenericRepository<Medico>
{

}

public class MedicoRepository : GenericRepository<Medico>, IMedicoRepository
{
	private readonly HospitalContext _context;
	public MedicoRepository(HospitalContext context) : base(context)
	{
		_context = context;
	}
}