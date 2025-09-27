using Microsoft.EntityFrameworkCore;
using MES.Rastreabilidade.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Registra os serviços dos Controllers
builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// O Swagger/OpenAPI precisa saber sobre os controllers
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi(); // Substituído pelo UseSwaggerUI para uma melhor experiência
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 2. Mapeia as rotas definidas nos Controllers
app.MapControllers();

app.Run();