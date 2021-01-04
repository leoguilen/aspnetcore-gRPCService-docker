using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrateleiraLivros.Dominio.Entidades;

namespace PrateleiraLivros.Data.Config
{
    public class EditoraConfiguration : IEntityTypeConfiguration<Editora>
    {
        public void Configure(EntityTypeBuilder<Editora> builder)
        {
            builder.ToTable("PL.Editora");

            builder.HasKey(editora => editora.Id);

            builder.HasMany(b => b.Livros)
                .WithOne(e => e.Editora)
                .HasForeignKey(t => t.EditoraId);

            builder.Property(editora => editora.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(editora => editora.SiteURL)
                .IsRequired(false)
                .HasMaxLength(150);

            builder.Property(editora => editora.Endereco)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
