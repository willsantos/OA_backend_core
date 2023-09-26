using Microsoft.EntityFrameworkCore;
using OA_Core.Api.Filters;
using OA_Core.Domain.Config;
using OA_Core.Domain.Interfaces.Notifications;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Domain.Interfaces.Service;
using OA_Core.Domain.Notifications;
using OA_Core.Repository.Context;
using OA_Core.Repository.Repositories;
using OA_Core.Service;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();


#region AutoMapper

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

#endregion

#region appConfig

var appConfig = builder.Configuration.GetSection(nameof(AppConfig)).Get<AppConfig>();
builder.Services.AddSingleton(appConfig);

#endregion

#region DbContext

builder.Services.AddDbContext<CoreDbContext>(options =>
{
    options.UseMySql(appConfig.ConnectionString, ServerVersion.AutoDetect(appConfig.ConnectionString));
});

#endregion

#region Filtros

builder.Services.AddMvc(options =>
{
	options.Filters.Add<NotificatonFilter>();
	options.Filters.Add<ExceptionFilter>();
});

#endregion

#region Injecao de dependencias

builder.Services.AddScoped<INotificador, Notificador>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>();
builder.Services.AddScoped<IProfessorService, ProfessorService>();
builder.Services.AddScoped<ICursoRepository, CursoRepository>();
builder.Services.AddScoped<ICursoService, CursoService>();
builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
builder.Services.AddScoped<IAulaRepository, AulaRepository>();
builder.Services.AddScoped<IAulaService, AulaService>();
builder.Services.AddScoped<IImagemService, ImagemService>();
builder.Services.AddScoped<ICursoProfessorService, CursoProfessorService>();
builder.Services.AddScoped<ICursoProfessorRepository, CursoProfessorRepository>();

#endregion

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
