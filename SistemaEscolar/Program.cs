using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SistemaEscolar.Data; // Certifique-se de que o namespace do seu DataContext está correto!

var builder = WebApplication.CreateBuilder(args);

// Carregar configurações do appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Obter a string de conexão
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Adicionar o DbContext ao contêiner de serviços
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connectionString));

// Adicionar o Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

// Habilitar o Swagger para visualizar a documentação da API
app.UseSwagger();
app.UseSwaggerUI();

// Use os Controllers
app.MapControllers();

app.MapGet("/", () => "Sistema Escolar rodando!");


app.Run();
