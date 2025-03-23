using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Server=localhost;Database=Banco Alunos;Trusted_Connection=True;"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/api/alunos", async ([FromBody] Aluno aluno, AppDbContext db) =>
{
    if (string.IsNullOrWhiteSpace(aluno.Nome) || string.IsNullOrWhiteSpace(aluno.CPF))
        return Results.BadRequest("Nome e CPF são obrigatórios.");

    db.Alunos.Add(aluno);
    await db.SaveChangesAsync();

    // Alterar a forma de retorno para garantir que a mensagem apareça
    var response = new { message = "Aluno cadastrado com sucesso!" };
    return Results.Ok(response);
});

app.Run();

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Aluno> Alunos { get; set; }
}

public class Aluno
{
    public int Id { get; set; }
    [Required]
    public string Nome { get; set; }
    [Required]
    public string DataNascimento { get; set; }
    [Required]
    public string CPF { get; set; }
}
