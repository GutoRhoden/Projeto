using Microsoft.EntityFrameworkCore;
using SistemaEscolar.Data;  // Importa o namespace onde o DataContext está

var builder = WebApplication.CreateBuilder(args);

// Configura o DbContext com a string de conexão
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
