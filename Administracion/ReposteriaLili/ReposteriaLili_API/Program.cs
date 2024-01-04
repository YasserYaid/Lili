using MediatR;
using Microsoft.EntityFrameworkCore;
using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios;
using ReposteriaLili_API.Core.Application.Mapping;
using ReposteriaLili_API.Infraestructure.Internal.Persistence;
using ReposteriaLili_API.Infraestructure.Internal.Repositorio;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

////////////////////////////////////////////////////////BLOQUE DE CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    );

});
///////////////////////////////////////////////////////

builder.Services.AddMediatR(typeof(Program).Assembly);

builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddDbContext<ReposteriaDBContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnectionString"));
});

builder.Services.AddScoped<IUnidadTrabajo, UnidadTrabajo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("CorsPolicy");//////////////////////////////////////////////////////CORS

app.MapControllers();

app.Run();
