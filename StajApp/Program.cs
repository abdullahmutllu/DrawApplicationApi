using Microsoft.EntityFrameworkCore;
using StajApp.AppDbContext;
using StajApp.Models;
using StajApp.Repository;
using StajApp.Repository.CoordinateRepository;
using StajApp.Services;
using StajApp.Services.Map;
using StajApp.Services.Postgres;
using StajApp.UnitOfWorks;
using System.Net.Sockets;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));
builder.Services.AddDbContext<ApplicationDbContext>(options=>options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
//builder.Services.AddSingleton<ICoordinateService, CoordinateService>();
builder.Services.AddScoped<IPostgresServiceRawQuery, PostgresServiceRawQuery>();
builder.Services.AddScoped<IUnitOfWorks, UnitOfWorks>();
builder.Services.AddScoped<ICoordinateRepository, CoordinateRepository>();
builder.Services.AddScoped<IMapControlService, MapControlService>();
builder.Services.AddScoped<IGenericRepository<Coordinate>,GenericRepository<Coordinate>>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGet("/update", (string work) =>
{
    Results.Ok(work);

});
app.Run();
