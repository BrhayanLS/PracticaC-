using GestionReservas.CasosDeUso;
using GestionReservas.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(routing=>routing.LowercaseUrls = true);

builder.Services.AddDbContext<HabitacionRepository>(mysqlBuilder =>
{
    mysqlBuilder.UseMySQL(builder.Configuration.GetConnectionString("Connection1"));
});

builder.Services.AddDbContext<ClienteRepository>(mysqlBuilder =>
{
    mysqlBuilder.UseMySQL(builder.Configuration.GetConnectionString("Connection1"));
});

builder.Services.AddDbContext<ReservaRepository>(mysqlBuilder =>
{
    mysqlBuilder.UseMySQL(builder.Configuration.GetConnectionString("Connection1"));
});

builder.Services.AddScoped<IUpdateClienteUseCase, UpdateClienteUseCase>();
builder.Services.AddScoped<IUpdateReservaUseCase, UpdateReservaUseCase>();
builder.Services.AddScoped<IUpdateHabitacionUseCase, UpdateHabitacionUseCase>();

var app = builder.Build();

app.UseCors(builder => builder
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
