using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;




public class HospitalContext : DbContext
{
	public DbSet<Usuario> Usuarios { get; set; }
	public DbSet<Paciente> Pacientes { get; set; }

	public DbSet<Administrador> Administradores { get; set; }
	public DbSet<Medico> Medicos { get; set; }
	public DbSet<Cita> Citas { get; set; }
	public DbSet<Pago> Pagos { get; set; }


	public HospitalContext(DbContextOptions<HospitalContext> options) : base(options)
	{
	}


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Usuario>(usuario =>
		{
			usuario.HasKey(p => p.Id);
			usuario.Property(u => u.Email).IsRequired();
			usuario.Property(u => u.Password).IsRequired();
			usuario.Property(u => u.Rol).IsRequired();
		});

		modelBuilder.Entity<Paciente>(paciente =>
		{
			paciente.HasKey(p => p.Id);
			paciente.Property(p => p.Id).ValueGeneratedOnAdd();
			paciente.Property(p => p.DocumentoIdentificacion).IsRequired();
			paciente.Property(p => p.NombreCompleto).IsRequired();
			paciente.Property(p => p.Telefono).IsRequired();
			paciente.Property(p => p.Beneficiarios).HasConversion(new StringListConverter());
			paciente.HasOne(p => p.Usuario).WithOne().HasForeignKey<Paciente>(p => p.UsuarioId);
		});


		modelBuilder.Entity<Medico>(medico =>
		{
			medico.HasKey(m => m.Id);
			medico.Property(m => m.DocumentoIdentificacion).IsRequired();
			medico.Property(m => m.NombreCompleto).IsRequired();
			medico.Property(m => m.Telefono).IsRequired();
			medico.Property(m => m.Especialidad).IsRequired();
			medico.Property(m => m.Disponibilidad).IsRequired().HasConversion(new DateTimeListConverter());
			medico.HasOne(m => m.Usuario).WithOne().HasForeignKey<Medico>(m => m.UsuarioId);


		});

		modelBuilder.Entity<Administrador>(administrador =>
		{
			administrador.HasKey(a => a.Id);
			administrador.Property(a => a.NombreCompleto).IsRequired();
			administrador.Property(a => a.Telefono).IsRequired();
			administrador.Property(a => a.Direccion).IsRequired();
			administrador.HasOne(a => a.Usuario).WithOne().HasForeignKey<Administrador>(a => a.UsuarioId);
		});

		modelBuilder.Entity<Cita>(cita =>
		{
			cita.HasKey(c => c.Id);
			cita.Property(c => c.Id).ValueGeneratedOnAdd();
			cita.Property(c => c.Fecha).IsRequired();
			cita.HasOne(c => c.Paciente).WithMany().IsRequired().HasForeignKey(c => c.PacienteId);
			cita.HasOne(c => c.Medico).WithMany().IsRequired().HasForeignKey(c => c.MedicoId);
			cita.Property(c => c.Especialidad).IsRequired();
		});

		modelBuilder.Entity<Pago>(pago =>
		{
			pago.HasKey(p => p.Id);
			pago.Property(p => p.Id).ValueGeneratedOnAdd();
			pago.Property(p => p.FechaPago).IsRequired();
			pago.Property(p => p.Monto).IsRequired();
			pago.HasOne(p => p.Paciente).WithMany().IsRequired().HasForeignKey(p => p.PacienteId);
			pago.HasOne(p => p.Cita).WithOne().IsRequired().HasForeignKey<Pago>(p => p.CitaId);
		});

	}

	public class DateTimeListConverter : ValueConverter<List<DateTime>, string>
	{
		public DateTimeListConverter(ConverterMappingHints mappingHints = null)
		    : base(
			v => JsonConvert.SerializeObject(v),
			v => JsonConvert.DeserializeObject<List<DateTime>>(v),
			mappingHints)
		{
		}
	}

	public class StringListConverter : ValueConverter<List<string>, string>
	{
		public StringListConverter(ConverterMappingHints mappingHints = null)
		    : base(
			  v => JsonConvert.SerializeObject(v),
			  v => JsonConvert.DeserializeObject<List<string>>(v),
			  mappingHints)
		{
		}
	}
}


