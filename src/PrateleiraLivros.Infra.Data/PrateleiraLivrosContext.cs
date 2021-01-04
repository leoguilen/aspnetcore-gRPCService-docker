using Microsoft.EntityFrameworkCore;
using PrateleiraLivros.Dominio.Entidades;
using System.Reflection;

namespace PrateleiraLivros.Data
{
    public class PrateleiraLivrosContext : DbContext
    {
        public PrateleiraLivrosContext() { }
        public PrateleiraLivrosContext(DbContextOptions<PrateleiraLivrosContext> options) : base(options) { }

        public DbSet<Livro> Livros { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Editora> Editoras { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=PrateleiraLivros;Trusted_Connection=yes;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
