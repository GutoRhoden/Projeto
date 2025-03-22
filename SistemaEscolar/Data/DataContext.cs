using Microsoft.EntityFrameworkCore;
using SistemaEscolar.Models; // Verifique se essa pasta existe

namespace SistemaEscolar.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Aluno> Alunos { get; set; }  // Exemplo de entidade
    }
}
