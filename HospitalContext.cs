using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
	public class HospitalContext : DbContext
	{
		public HospitalContext(DbContextOptions<HospitalContext> options) : base(options)
		{
		}

		public DbSet<Usuario> Usuarios { get; set; }
		public DbSet<Paciente> Pacientes { get; set; }
		public DbSet<Medico> Medicos { get; set; }
		public DbSet<Cita> Citas { get; set; }
		public DbSet<Pago> Pagos { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Usuario>(usuario =>
			{
				usuario.HasKey(u => u.Id);
				usuario.Property(u => u.Id).ValueGeneratedOnAdd();
				usuario.Property(u => u.NombreUsuario).IsRequired();
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
				paciente.Property(p => p.Beneficiarios).IsRequired();
			});


			modelBuilder.Entity<Medico>(medico =>
			{
				medico.HasKey(m => m.Id);
				medico.Property(m => m.Id).ValueGeneratedOnAdd();
				medico.Property(m => m.DocumentoIdentificacion).IsRequired();
				medico.Property(m => m.NombreCompleto).IsRequired();
				medico.Property(m => m.Telefono).IsRequired();
				medico.Property(m => m.Especialidad).IsRequired();
				medico.Property(m => m.Disponibilidad).IsRequired();
			});

			modelBuilder.Entity<Cita>(cita =>
			{
				cita.HasKey(c => c.Id);
				cita.Property(c => c.Id).ValueGeneratedOnAdd();
				cita.Property(c => c.Fecha).IsRequired();
				cita.Property(c => c.Paciente).IsRequired();
				cita.Property(c => c.Medico).IsRequired();
				cita.Property(c => c.Especialidad).IsRequired();
			});

			modelBuilder.Entity<Pago>(pago =>
			{
				pago.HasKey(p => p.Id);
				pago.Property(p => p.Id).ValueGeneratedOnAdd();
				pago.Property(p => p.FechaPago).IsRequired();
				pago.Property(p => p.Monto).IsRequired();
				pago.Property(p => p.Paciente).IsRequired();
				pago.Property(p => p.Cita).IsRequired();
			});

		}
	}
}