using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Server=SEU_SERVIDOR;Database=SistemaEscolar;Trusted_Connection=True;"));
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
    return Results.Ok(new { message = "Aluno cadastrado com sucesso!" });
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
