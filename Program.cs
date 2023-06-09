using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Conection data base
builder.Services.AddDbContext<HospitalContext>(DbContextOptions => DbContextOptions.UseMySql(builder.Configuration.GetConnectionString("cnHospital"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("cnHospital"))));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IMedicoRepository, MedicoRepository>();
builder.Services.AddScoped<ICitaRepository, CitaRepository>();
// builder.Services.AddScoped<IAdministradorRepository, AdministradorRepository>();
builder.Services.AddControllers().AddJsonOptions(opts =>
{
	opts.JsonSerializerOptions.Converters.Add(new RolJsonConverter());
	opts.JsonSerializerOptions.Converters.Add(new EspecialidadJsonConverter());
});

builder.Services.AddAutoMapper(config =>
{
	config.CreateMap<CrearCitaDTO, Cita>();
}
    , typeof(Program));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:Key").Value)),
			ValidateIssuer = false,
			ValidateAudience = false
		};
	}
);

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll", builder =>
	{
		builder.AllowAnyOrigin()
			   .AllowAnyMethod()
			   .AllowAnyHeader();
	});
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.	
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
